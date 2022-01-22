import React, { useState, useEffect } from 'react';
import { Button, FormControl, InputLabel, MenuItem, Select, TextField } from '@mui/material';
import {
    useHistory
} from 'react-router-dom';
import "./ProductForm.css";
import { useSnackbar } from 'notistack';
import { ProductCategory } from '../../../data';
import { CategoryService, ProductService } from '../../../services/api';
import { CurrencyInput } from '../../customUi';

export interface CreateProductFormProps {
    onCreate? : () => void;
}

const CreateProductForm = (props : CreateProductFormProps) => {
    const history = useHistory();
    const { enqueueSnackbar } = useSnackbar();
    const [productName, setProductName] = useState<string>('');
    const [productPrice, setProductPrice] = useState<string>('');
    const [productQty, setProductQty] = useState<number>(1);
    const [category, setCategory] = useState<ProductCategory>();
    const [categories, setCategories] = useState<ProductCategory[]>([]);

    const handleSubmit = (event : React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        if (category === undefined) {
            enqueueSnackbar('Category must be defined.', { variant : 'warning'});
            return;
        }
        const promise = ProductService.postProduct({   
            name : productName, 
            price : parseFloat(productPrice),
            categoryId : category.id
        });
        promise.then( (response) => {
            enqueueSnackbar('Product created.', { variant : 'success'});
            if (props.onCreate !== undefined) { props.onCreate(); }
            history.goBack();
        }).catch( (error) => {
            // Send error to api
            enqueueSnackbar('An error occured during the creation.', { variant : 'error'})
            console.error(error);
        })
    }

    useEffect(() => {
        const promise = CategoryService.getCategories();
        promise.then((response) => setCategories(response.data))
            .catch((error) => {
                console.error(error);
                enqueueSnackbar('An error occured while fetching categories', {variant:'error'})
            });
    }, [])

    const getCategory = (categoryId : number) =>
        categories.find(c => c.id == categoryId);
    

    const getCategoresSelect = () => {
        return <FormControl>
                <InputLabel id="category-select-label"> Catégorie </InputLabel>
                <Select labelId="category-select-label" 
                        id="category-select"
                        onChange={(event : any) => setCategory(getCategory(event.target.value as number))}>
                        {categories.map(category => <MenuItem key={category.id} value={category.id}>
                            {category.name}
                        </MenuItem>)}
                </Select>
            </FormControl>
    }

    return <form className='productForm' onSubmit={(event) => handleSubmit(event)}>
            <h1> Nouveau produit </h1>
            <TextField className='categoryField' 
                label='Nom du produit' 
                type='text' 
                onChange={(event : any) => setProductName(event.target.value)}/>
            <CurrencyInput
                label="Prix du produit"
                onChange={(event) => setProductPrice(event.target.value)}
            />
            {
                (category?.isSubscription || category?.isEntries) && 
                <TextField className='quantityField' 
                    label={category?.isSubscription ? "Durée en mois" : 'Quantité'} 
                    type='number' 
                    value={productQty}
                    onChange={(event : any) => setProductQty(parseInt(event.target.value))}/>
            }
            {getCategoresSelect()}
            <div className='productsButton'>
                <Button variant='contained' color='secondary' onClick={() => history.goBack()}>
                    Annuler
                </Button>
                <Button type="submit" variant='contained' color='primary'>
                    Créer
                </Button>
            </div>
        </form>
}

export { CreateProductForm }