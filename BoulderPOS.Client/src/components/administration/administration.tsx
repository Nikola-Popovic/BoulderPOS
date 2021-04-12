import React from 'react'
import { 
    useRouteMatch,
    Switch,
    Route,
    useHistory
} from 'react-router-dom'
import { NavigationButton } from '../customUi';
import "./administration.css";
import { 
    Button,
    TextField
} from '@material-ui/core';
import {
    CategoriesPage,
    ProductsPage
} from './';

const Administration = () => {
    let match = useRouteMatch();
    let history = useHistory();

    
    const displayLoginPanel = () => {
        return <form onSubmit={(event) => handleLogin(event)} className='loginPanel'>
            <TextField id='userName' label='Identifiant' type='text' />
            <TextField id='password' label='Mot de passe' type='password' />
            <div className='loginButtons'>
                <NavigationButton text='Cancel' route='/' variant='contained' color='secondary'/>
                <Button type="submit" variant='contained' color='primary'>
                    Login
                </Button>
            </div>
        </form>
    }

    const diplayAdminActions = () => {
        return <div className='actionButtons'>
            <NavigationButton text='Produits' route={`${match.url}/products`} style={{backgroundColor:'mediumseagreen'}}/>
            <NavigationButton text='Catégories' route={`${match.url}/categories`} style={{backgroundColor:'orange'}}/>
        </div>
    }

    const handleLogin = (event : React.FormEvent<HTMLFormElement>) => {
        // Call Administration Authentification
        // Add logout
        // Use context api to track admin state
        history.push(`${match.url}/actions`);
    }

    return (
        <div className='adminPanel'>
            <Switch>
                <Route path={`${match.path}/actions`}>
                    { diplayAdminActions() }
                </Route>
                <Route path={`${match.path}/categories`}>
                    <CategoriesPage />
                </Route>
                <Route path={`${match.path}/products`}>
                    <ProductsPage />
                </Route>
                <Route path={`${match.path}`}>
                    { displayLoginPanel() }
                </Route>
            </Switch>
        </div>
    )
}

export { Administration };
