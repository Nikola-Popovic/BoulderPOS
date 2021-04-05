import React, { useEffect, useState } from 'react'
import { useParams } from 'react-router';
import { ProductsPanel, CategoriesPanel, Bill } from '../components/shop';
import { PaymentMethod } from '../data/PaymentMethod';
import { useSnackbar } from 'notistack';
import { Product, ProductCategory, ProductInCart } from '../data';
import { CategoryService } from '../services/api';
import "./shop.css";

interface RouteParams {
    clientId : string
}

interface ShopProps {

}

const Shop = (props : ShopProps) => {
    const [categories, setCategories] = useState<ProductCategory[]>([]);
    const [products, setProducts] = useState<Product[]>([]);
    const [selectedCategory, setSelectedCategory] = useState<number>();
    let { clientId } = useParams<RouteParams>();
    const [ cart, setCart ] = useState<ProductInCart[]>([]);
    const { enqueueSnackbar } = useSnackbar();

    useEffect(() => {
        let promise = CategoryService.getCategories();
        promise.then((response) => { 
            setCategories(response.data);
            setSelectedCategory(response.data[0]?.id);
        }).catch((error) => {
            enqueueSnackbar('Could not fetch categories', {variant:'error'})
            console.error(error);
        })
    }, []);

    useEffect(() => {
        if (selectedCategory !== undefined) {
            let promise = CategoryService.getProductsByCategory(selectedCategory);
            promise.then((response) => setProducts(response.data))
            .catch((error) => {
                enqueueSnackbar('Could not fetch products', {variant:'error'})
                console.error(error);
            })
        }
    }, [selectedCategory]);

    /**
     * 
    useEffect(() => {
    }, [cart])
    
    */
    const handleShopProductClick = (product : Product) => {
        let tempCart = cart;
        if(tempCart.find(a => a.id == product.id) === undefined) {
            tempCart.push(
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
            let i = tempCart.findIndex(a => a.id === product.id);
            tempCart[i].quantity += 1;
        }
        setCart(tempCart);
    }

    const onPaymentConfirm = (method: PaymentMethod) => { 
        if (window.confirm('Has the payment been completed?')) {
            // Save transaction in database
            // Remove # of items from inventory
            // Add money amount to day total for stats
            console.log('Transaction was saved in the database.');
            setCart([]);
          } else {
          }
    }
    
    return <div className="shopPage">
        <Bill items={cart} 
            onPaymentConfirm={(method: PaymentMethod) => onPaymentConfirm(method)}/>
        <div className="shopContext">
            <CategoriesPanel categories={categories} onCategoryClick={(categoryId) => setSelectedCategory(categoryId)}/>
            <ProductsPanel products={products} onItemClick={(product) => handleShopProductClick(product)}/>
        </div>
    </div>

    
}

export default Shop;
