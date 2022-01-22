import React, { useState, useEffect } from 'react';
import { Button, FormControl, InputLabel, MenuItem, Select, TextField } from '@material-ui/core';
import {
    useHistory,
    useParams
} from 'react-router-dom';
import "./ProductForm.css";
import { useSnackbar } from 'notistack';
import { Product, ProductCategory } from '../../../data';
import { CategoryService, ProductService } from '../../../services/api';
import { CurrencyInput } from '../../customUi';

export interface UpdateProductProps {
    onUpdate? : () => void;
}

interface RouteParams {
    productId : string
}

const UpdateProduct = (props : UpdateProductProps) => {
    const history = useHistory();
    const { enqueueSnackbar } = useSnackbar();
    const [product, setProduct] = useState<Product | null>(null);
    const { productId } = useParams<RouteParams>();
    const [categories, setCategories] = useState<ProductCategory[]>([]);

    const handleSubmit = (event : React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        if (product === null) {
            enqueueSnackbar('An error occured, product is null.', { variant : 'error'})
            return;
        }
        const promise = ProductService.updateProduct(product);
        promise.then( (response) => {
            enqueueSnackbar('Product updated.', { variant : 'success'});
            if (props.onUpdate !== undefined) { props.onUpdate(); }
            history.goBack();
        }).catch( (error) => {
            // Send error to api
            enqueueSnackbar('An error occured during the update.', { variant : 'error'})
            console.error(error);
        })
    }

    useEffect(() => {
        const promise = CategoryService.getCategories();
        const productPromise = ProductService.getProduct(productId);
        promise.then((response) => setCategories(response.data))
            .catch((error) => {
                console.error(error);
                enqueueSnackbar('An error occured while fetching categories', {variant:'error'})
            });
        productPromise.then((response) => setProduct(response.data))
            .catch((error) => {
                console.error(error);
                enqueueSnackbar('An error occured while fetching the product', {variant:'error'})
            });
    }, [])

    return <form className='productForm' onSubmit={(event) => handleSubmit(event)}>
            <h1> Modifier le produit </h1>
            { product !== null && 
            <>
                <TextField className='categoryField' 
                    label='Nom du produit' 
                    type='text'
                    value={product.name} 
                    onChange={(event: any) => 
                        setProduct({
                            ...product,
                            name : event.target.value
                        })}/>
                <CurrencyInput
                    label="Prix du produit"
                    value={product.price.toString()}
                    onChange={(event) => 
                        setProduct({
                            ...product,
                            price : parseFloat(event.target.value)
                        })}/>
                { (product.category?.isSubscription || product.category?.isEntries) && 
                <TextField className='quantityField' 
                    label={product.category?.isSubscription ? "Durée en mois" : 'Quantité'} 
                    type='number' 
                    value={product.quantity}
                    onChange={(event : any) => setProduct({
                        ...product,
                        quantity : parseInt(event.target.value)
                    })}/>
                }
                <FormControl>
                    <InputLabel id="category-select-label"> Catégorie </InputLabel>
                    <Select labelId="category-select-label" 
                            id="category-select"
                            value={product.categoryId}
                            onChange={(event: any) => setProduct( {
                                ...product,
                                categoryId: event.target.value as number
                            })}>
                            {categories.map(category => <MenuItem key={category.id} value={category.id}>
                                {category.name}
                            </MenuItem>)}
                    </Select>
                </FormControl> 
            </>}
            <div className='productsButton'>
                <Button variant='contained' color='secondary' onClick={() => history.goBack()}>
                    Retour arrière
                </Button>
                <Button type="submit" variant='contained' color='primary'>
                    Modifier
                </Button>
            </div>
        </form>
}

export { UpdateProduct }