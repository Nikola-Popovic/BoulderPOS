import React, { useEffect, useState } from 'react'
import { useParams, useHistory } from 'react-router';
import { ProductsPanel, CategoriesPanel, Bill } from './';
import { PaymentMethod } from '../../data/PaymentMethod';
import { useSnackbar } from 'notistack';
import { Customer, Product, ProductCategory, ProductInCart } from '../../data';
import { CategoryService, CustomerService } from '../../services/api';
import "./Shop.css";

interface RouteParams {
    clientId : string
}

const Shop = () => {
    const [categories, setCategories] = useState<ProductCategory[]>([]);
    const history = useHistory();
    const [products, setProducts] = useState<Product[]>([]);
    const [selectedCategory, setSelectedCategory] = useState<number>();
    const [ cart, setCart ] = useState<ProductInCart[]>([]);
    const [ billRefresh, setBillRefresh] = useState(false);
    const { clientId } = useParams<RouteParams>();
    const [client, setClient] = useState<Customer | undefined>();
    const { enqueueSnackbar } = useSnackbar();

    useEffect(() => {
        // Categories
        const promise = CategoryService.getCategories();
        promise.then((response) => { 
            setCategories(response.data);
            setSelectedCategory(response.data[0]?.id);
        }).catch((error) => {
            enqueueSnackbar('Could not fetch categories', {variant:'error'})
            console.error(error);
        });
    }, []);

    useEffect(() => {
        if (clientId === undefined) return;
        const clientPromise = CustomerService.getCustomer(clientId);
        clientPromise.then((reponse) => setClient(reponse.data))
        .catch((error) => {
            console.error(error);
        });
    }, [clientId])

    useEffect(() => {
        if (selectedCategory !== undefined) {
            const promise = CategoryService.getProductsByCategory(selectedCategory);
            promise.then((response) => setProducts(response.data))
            .catch((error) => {
                enqueueSnackbar('Could not fetch products', {variant:'error'})
                console.error(error);
            })
        }
    }, [selectedCategory]);
    
    const handleShopProductClick = (product : Product) => {
        if(cart.find(a => a.id == product.id) === undefined) {
            cart.push(
                {
                    id : product.id,
                    price: product.price,
                    name: product.name,
                    categoryId: product.categoryId,
                    category: product.category,
                    quantity: 1
                }
            );
        } else {
            const i = cart.findIndex(a => a.id === product.id);
            cart[i].quantity += 1;
        }
        setBillRefresh(true);
    }

    const onPaymentConfirm = (method: PaymentMethod) => { 
        if (window.confirm('Has the payment been completed?')) {
            // Save transaction in database
            // Remove # of items from inventory
            // Add money amount to day total for stats
            console.log('Transaction was saved in the database.');
            setCart([]);
            history.push('/');
          } 
    }
    
    return <div className="shopPage">
        <Bill items={cart} client={client} setBillRefresh={setBillRefresh} billRefresh={billRefresh}
            onPaymentConfirm={(method: PaymentMethod) => onPaymentConfirm(method)}/>
        <div className="shopContext">
            <CategoriesPanel categories={categories} onCategoryClick={(categoryId) => setSelectedCategory(categoryId)}/>
            <ProductsPanel products={products} onItemClick={(product) => handleShopProductClick(product)}/>
        </div>
    </div>

    
}

export { Shop };
