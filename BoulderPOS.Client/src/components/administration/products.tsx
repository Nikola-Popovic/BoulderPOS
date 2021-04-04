import React, { useEffect, useState } from 'react';
import { useHistory, Switch, Route, useRouteMatch } from 'react-router-dom';
import { Product } from '../../data';
import { useSnackbar } from 'notistack';
import "./products.css";
import { Button, LinearProgress } from '@material-ui/core';
import { NavigationButton, DeleteDialog } from '../customUi';
import { ProductService } from '../../services/api/Product';
import { CreateProductForm, UpdateProduct } from './product';

const ProductsPage = () => {
    const [products, setProducts] = useState<Product[]>([]);
    const [isLoading, setLoading] = useState<boolean>(false);
    const history = useHistory();
    const { enqueueSnackbar } = useSnackbar();
    const [selectedProduct, setSelected] = useState<number>(-1);
    const [openDelete, setOpenDelete] = useState(false);
    const [shouldRefresh, setShouldRefresh] = useState(false);
    const match = useRouteMatch();

    useEffect(() => {
        setLoading(true);
        setShouldRefresh(false);
        let promise = ProductService.getProducts();
        promise.then((response) => {
            setLoading(false);
            setProducts(response.data);
        }).catch(error => {
            enqueueSnackbar('An error occured while fetching products', {variant:'error'});
            console.error(error);
        })
    }, [shouldRefresh])

    const displayProducts = () => {
        return  <tbody> {products.map((product) => 
                <tr>
                    <td>{product.name}</td>
                    <td>{`${product.price}`}</td>
                    <td>{`${product.category?.name}`}</td>
                    <td style={{textAlign:'end'}}><Button variant='outlined' color='secondary' onClick={() => openDeleteProduct(product.id)}> Delete </Button></td>
                    <td><Button variant='outlined' color='primary' onClick={() => editProduct(product.id)}> Edit </Button></td>
                </tr>
            )}</tbody>
    }

    const openDeleteProduct = (id : number) => {
        setSelected(id);
        setOpenDelete(true);
    }

    const deleteCategory = () => {
        let promise = ProductService.deleteProduct(selectedProduct);
        promise.then(() => {
            enqueueSnackbar('Product deleted successfully', {variant:'success'});
            setShouldRefresh(true);
            setOpenDelete(false);
        }).catch((error) => {
            console.error(error);
            enqueueSnackbar('An error occured while deleting product', {variant:'error'});
        });
    }

    const editProduct = (id : number) => {
        history.push(`${match.url}/${id}`);
    }

    const displayProductsPage = () => {
        return <>
            <h1> Produits </h1>
            <div className="scrollable">
                <table className="productsTable">
                    <thead>
                        <tr>
                            <th>Nom</th>
                            <th>Prix</th>
                            <th>Catégorie</th>
                            <th colSpan={2}>Actions</th>
                        </tr>
                    </thead>
                    {displayProducts()}
                </table>
            </div>
            <DeleteDialog 
                open={openDelete} 
                handleClose={() => setOpenDelete(false)} 
                handleConfirm={()=> deleteCategory()}
                title='Êtes vous sûr de vouloir supprimer le produit'
            />
            {isLoading && <LinearProgress color='primary' />}
            <div className="buttonPanel">
                <Button variant='contained' color='secondary' onClick={() => history.goBack()}>
                    Retour arrière
                </Button>
                <NavigationButton text='Ajouter un produit' route={`${match.url}/addproduct`} variant='contained' style={{backgroundColor:'#003598'}}/>
            </div>
        </>
    } 

    return <div className="productsPage">
        <Switch>
            <Route exact path={`${match.path}/addproduct`}>
                <CreateProductForm onCreate={() => setShouldRefresh(true)}/>
            </Route>
            <Route path={`${match.path}/:productId`}>
                <UpdateProduct onUpdate={() => setShouldRefresh(true)}/>
            </Route>
            <Route path={match.path}>
                { displayProductsPage() }
            </Route>
        </Switch>
    </div>
}

export { ProductsPage };