import React, {useState, useEffect} from 'react';
import "./CategoryForm.css"
import { 
    useParams, 
    useHistory
 } from "react-router-dom";
import { ProductCategory } from "../../../data";
import { useSnackbar } from 'notistack';
import { CategoryService } from '../../../services/api';
import { Button, TextField } from '@material-ui/core';
import { IconPicker } from '../../customUi/icons/IconPicker';

interface RouteParams {
    categoryId : string
}

interface UpdateCategoryProps {
    onUpdate? : () => void;
}

const UpdateCategory : React.FunctionComponent<UpdateCategoryProps> = (props : UpdateCategoryProps) => {
    const [category, setCategory] = useState<ProductCategory | null>(null);
    const history = useHistory();
    const { enqueueSnackbar } = useSnackbar();
    let { categoryId } = useParams<RouteParams>();

    useEffect(() => {
        let promise = CategoryService.getCategory(categoryId);
        promise.then((response) => {
            setCategory(response.data);
        }).catch((error) => {
            enqueueSnackbar('An error occured while fetching category', {variant : 'error'});
            console.error(error);
        })
    }, []);

    const handleSubmit = (event : React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        if (category === null) {
            enqueueSnackbar('An error occured, category is null', {variant : 'error'});
            return;
        }
        let promise = CategoryService.updateCategory(category);
        promise.then( (response) => {
            enqueueSnackbar('Category updated.', { variant : 'success'});
            if (props.onUpdate !== undefined) { props.onUpdate(); }
        }).catch( (error) => {
            // Send error to api
            enqueueSnackbar('An error occured during the creationn.', { variant : 'error'})
            console.error(error);
        })
    }
    
    return <form className='categoryForm' onSubmit={(event) => handleSubmit(event)}>
        <h1> Modifier la catégorie</h1>
        {
            category !== null && <>
                <TextField className='categoryField' 
                    label='Nom de la catégorie' 
                    type='text' 
                    value={category.name}
                    onChange={(event) => setCategory({
                        ...category,
                        name: `${event.target.value}`,
                    })}/>
                <IconPicker 
                    value={category.iconName} 
                    onChange={(value) => setCategory({
                        ...category,
                        iconName : value
                    })} 
                    cName='categoryPicker'/>
            </>
        }
        <div className='categoryButtons'>
            <Button variant='contained' color='secondary' onClick={() => history.goBack()}>
                Retour arrière
            </Button>
            <Button type="submit" variant='contained' color='primary'>
                Modifier
            </Button>
        </div>
    </form>
}

export { UpdateCategory };