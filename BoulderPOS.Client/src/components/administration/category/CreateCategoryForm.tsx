import { Button, TextField } from '@material-ui/core';
import React, { useState } from 'react';
import { IconPicker } from '../../icons/IconPicker';
import {
    useHistory
} from 'react-router-dom';
import "./CategoryForm.css";
import { useSnackbar } from 'notistack';
import { CategoryService } from '../../../services/api';

export interface CreateCategoryFormProps {
}

export const CreateCategoryForm = (props : CreateCategoryFormProps) => {
    const history = useHistory();
    const [categoryName, setCategory] = useState<string>('');
    const [iconName, setCategoryIcon] = useState<string>('');
    const { enqueueSnackbar } = useSnackbar();

    const handleSubmit = (event : React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        let promise = CategoryService.postCategory({name : categoryName, iconName : iconName});
        promise.then( (response) => {
            enqueueSnackbar('Category created.', { variant : 'success'});
            history.goBack();
        }).catch( (error) => {
            // Send error to api
            enqueueSnackbar('An error occured during the creationn.', { variant : 'error'})
            console.error(error);
        })
    }
    return (
        <form className='categoryForm' onSubmit={(event) => handleSubmit(event)}>
            <h1> Nouvelle catégorie </h1>
            <TextField className='categoryField' label='Nom de la catégorie' type='text' onChange={(event) => setCategory(event.target.value)}/>
            <IconPicker value={iconName} onChange={(value) => setCategoryIcon(value)} cName='categoryPicker'/>
            <div className='categoryButtons'>
                <Button variant='contained' color='secondary' onClick={() => history.goBack()}>
                    Annuler
                </Button>
                <Button type="submit" variant='contained' color='primary'>
                    Créer
                </Button>
            </div>
        </form>
    )
}
