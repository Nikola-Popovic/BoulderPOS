import React from 'react'
import { 
    useRouteMatch,
    Switch,
    Route,
    useHistory
} from 'react-router-dom'
import NavigationButton from '../components/customUi/NavigationButton';
import "./administration.css";
import { 
    Button,
    TextField
} from '@material-ui/core';
import { 
    CreateCategoryForm,
    CreateProductForm
} from '../components/administration';

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
            <NavigationButton text='Add a new product' route={`${match.url}/addproduct`} style={{backgroundColor:'mediumseagreen'}}/>
            <NavigationButton text='Add a new category' route={`${match.url}/addproductcategory`} style={{backgroundColor:'orange'}}/>
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
                <Route exact path={`${match.path}/actions`}>
                    { diplayAdminActions() }
                </Route>
                <Route exact path={`${match.path}/addproduct`}>
                    <CreateProductForm />
                </Route>
                <Route exact path={`${match.path}/addproductcategory`}>
                    <CreateCategoryForm />
                </Route>
                <Route path={`${match.path}`}>
                    { displayLoginPanel() }
                </Route>
            </Switch>
        </div>
    )
}

export default Administration
