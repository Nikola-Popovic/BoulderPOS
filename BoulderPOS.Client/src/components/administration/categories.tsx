import React, { useEffect, useState } from 'react';
import { useHistory, Switch, Route, useRouteMatch } from 'react-router-dom';
import { ProductCategory } from '../../data';
import { CategoryService } from '../../services/api';
import { useSnackbar } from 'notistack';
import "./categories.css";
import { Button, LinearProgress } from '@material-ui/core';
import { NavigationButton, DeleteDialog } from '../customUi';
import { UpdateCategory, CreateCategoryForm } from "./category";
import { DragDropContext, Droppable, Draggable, DropResult} from 'react-beautiful-dnd';

const getRowStyle = (isDragging : boolean, draggableStyle : any) => ({
    // some basic styles to make the items look a bit nicer
    userSelect: "none",
    display: "table-row",
    // styles we need to apply on draggables
    ...draggableStyle
  });

const CategoriesPage = () => {
    const [categories, setCategories] = useState<ProductCategory[]>([]);
    const [isLoading, setLoading] = useState<boolean>(false);
    const [isDirty, setDirty] = useState<boolean>(false);
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

    const handleDragEnd = (result : DropResult) => {
        if (!result.destination) {
            return;
        }

        const items = reorder(
            categories, 
            result.source.index,
            result.destination.index
        )
        setDirty(true);
        setCategories(items);
    }

    const reorder = (list : ProductCategory[], startIndex : number, endIndex :number) => {
        const result = Array.from(list);
        const [removed] = result.splice(startIndex, 1);
        result.splice(endIndex, 0, removed);
      
        return result;
    };

    const displayCategories = () => {
        return  <Droppable droppableId="droppable"> 
                    {(provided, snapshot) => (
                    <tbody ref={provided.innerRef} {...provided.droppableProps}>
                        {categories.map((category, index) => (
                            <Draggable key={category.id} draggableId={`cat${category.id}`} index={index}>
                            {(provided, snapshot) => (
                                <tr
                                    className="category"
                                    ref={provided.innerRef}
                                    {...provided.draggableProps}
                                    {...provided.dragHandleProps}
                                    style={getRowStyle(
                                        snapshot.isDragging,
                                        provided.draggableProps.style
                                    )}
                                >
                                    <td>{category.name}</td>
                                    <td><i className={`${category.iconName}`}/> </td>
                                    <td style={{textAlign:'end'}}>
                                        <Button variant='outlined' 
                                        color='secondary' 
                                        onClick={() => openDeleteCategory(category.id)}
                                        disabled={category.isSubscription || category.isEntries}> Delete </Button>
                                    </td>
                                    <td>
                                        <Button variant='outlined' color='primary' onClick={() => editCategory(category.id)}> Edit </Button>
                                    </td>
                                </tr>
                            )}
                            </Draggable>
                        ))}
                    </tbody>
                    )}
            </Droppable> 
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

    const handleSaveChanges = () => {
        const categoryReordered = categories.map((category, index) => (
            {
                ...category, 
                order: index
        }));
        let promise = CategoryService.updateCategories(categoryReordered);
        setLoading(true);
        promise.then(() => {
            enqueueSnackbar('Categories updateed successfully', {variant:'success'});
            setLoading(false);
            setDirty(false);
        }).catch((error) => {
            console.error(error);
            enqueueSnackbar('An error occured while updating categories', {variant:'error'});
        });
    }

    const editCategory = (id : number) => {
        history.push(`${match.url}/${id}`);
    }

    const displayCategoriesPage = () => {
        return <>
            <h1> Catégories </h1>
            <div>
                <table className="categoriesTable">
                    <thead>
                        <tr>
                            <th>Nom</th>
                            <th>Icône</th>
                            <th colSpan={2}>Actions</th>
                        </tr>
                    </thead>
                    <DragDropContext
                        onDragEnd={(result) => handleDragEnd(result)}
                    > 
                        {displayCategories()}
                    </DragDropContext>
                </table>
            </div>
            <DeleteDialog open={openDelete} 
                            handleClose={() => setOpenDelete(false)} 
                            handleConfirm={()=> deleteCategory()}
                            title='Êtes vous sûr de vouloir supprimer la catégorie?'/>
            {isLoading && <LinearProgress color='primary' style={{margin:"5px"}} />}
            <NavigationButton
                style={{margin: "5px", color: "#ffffff", backgroundColor: "#0E489D"}}
                text='Ajouter une catégorie' 
                route={`${match.url}/addproductcategory`} 
                variant='contained'/>
            <div className="categoriesButtonPanel">
                <Button variant='contained' color='secondary' onClick={() => history.goBack()}>
                    Retour arrière
                </Button>
                <Button variant='contained' onClick={() => handleSaveChanges()} disabled={!isDirty} style={{backgroundColor:"#007961"}}>
                    Sauvegarder les changements
                </Button>
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