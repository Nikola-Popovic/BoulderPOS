import React, { useEffect, useState } from 'react';
import { useHistory, Switch, Route, useRouteMatch } from 'react-router-dom';
import { ProductCategory } from '../../data';
import { CategoryService } from '../../services/api';
import { useSnackbar } from 'notistack';
import "./categories.css";
import { Button, LinearProgress } from '@material-ui/core';
import { NavigationButton, DeleteDialog } from '../customUi';
import { UpdateCategory, CreateCategoryForm } from "./category";

const CategoriesPage = () => {
    const [categories, setCategories] = useState<ProductCategory[]>([]);
    const [isLoading, setLoading] = useState<boolean>(false);
    const history = useHistory();
    const { enqueueSnackbar } = useSnackbar();
    const [selectedCategory, setSelected] = useState<number>(-1);
    const [openDelete, setOpenDelete] = useState(false);
    const [shouldRefresh, setShouldRefresh] = useState(false);
    const match = useRouteMatch();

    useEffect(() => {
        setLoading(true);
        setShouldRefresh(false);
        let promise = CategoryService.getCategories();
        promise.then((response) => {
            setLoading(false);
            setCategories(response.data);
        }).catch(error => {
            enqueueSnackbar('An error occured while fetching categories', {variant:'error'});
            console.error(error);
        })
    }, [shouldRefresh])

    const displayCategories = () => {
        return  <tbody> {categories.map((category) => 
                <tr className="category">
                    <td>{category.name}</td>
                    <td><i className={`${category.iconName}`}/> </td>
                    <td style={{textAlign:'end'}}><Button variant='outlined' color='secondary' onClick={() => openDeleteCategory(category.id)}> Delete </Button></td>
                    <td><Button variant='outlined' color='primary' onClick={() => editCategory(category.id)}> Edit </Button></td>
                </tr>
            )}</tbody>
    }

    const openDeleteCategory = (id : number) => {
        setSelected(id);
        setOpenDelete(true);
    }

    const deleteCategory = () => {
        let promise = CategoryService.deleteCategory(selectedCategory);
        promise.then(() => {
            enqueueSnackbar('Category deleted successfully', {variant:'success'});
            setShouldRefresh(true);
            setOpenDelete(false);
        }).catch((error) => {
            console.error(error);
            enqueueSnackbar('An error occured while deleting category', {variant:'error'});
        });
    }

    const editCategory = (id : number) => {
        history.push(`${match.url}/${id}`);
    }

    const displayCategoriesPage = () => {
        return <>
            <h1> Catégories </h1>
            <div className="scrollable">
                <table className="categoriesTable">
                    <thead>
                        <tr>
                            <th>Nom</th>
                            <th>Icône</th>
                            <th colSpan={2}>Actions</th>
                        </tr>
                    </thead>
                    {displayCategories()}
                </table>
            </div>
            <DeleteDialog open={openDelete} 
                            handleClose={() => setOpenDelete(false)} 
                            handleConfirm={()=> deleteCategory()}
                            title='Êtes vous sûr de vouloir supprimer la catégorie?'/>
            {isLoading && <LinearProgress color='primary' />}
            <div className="buttonPanel">
                <Button variant='contained' color='secondary' onClick={() => history.goBack()}>
                    Retour arrière
                </Button>
                <NavigationButton text='Ajouter une catégorie' route={`${match.url}/addproductcategory`} variant='contained' style={{backgroundColor:'#003598'}}/>
            </div>
        </>
    } 

    return <div className="categoriesPage">
        <Switch>
            <Route exact path={`${match.path}/addproductcategory`}>
                <CreateCategoryForm onCreate={() => setShouldRefresh(true)}/>
            </Route>
            <Route path={`${match.path}/:categoryId`}>
                <UpdateCategory onUpdate={() => setShouldRefresh(true)}/>
            </Route>
            <Route path={match.path}>
                { displayCategoriesPage() }
            </Route>
        </Switch>
    </div>
}

export { CategoriesPage };