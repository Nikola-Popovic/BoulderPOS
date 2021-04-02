import React, { useState, useEffect } from 'react';
import { LinearProgress } from '@material-ui/core';
import { Customer } from '../data';
import { CustomerService } from '../services/api'
import { debounce } from 'lodash';
import "./checkin.css";
import {
    useRouteMatch,
    Route,
    Switch,
    useHistory
} from "react-router-dom";
import ClientPreview from '../components/clientPreview/clientPreview';

export interface CheckInProps {

}

const CheckIn : React.FunctionComponent<CheckInProps> = (props) => {
    let match = useRouteMatch();
    const history = useHistory();
    const [customers, setCustomers] =  useState<Customer[]>([]);
    const [isLoading, setIsLoading] = useState<boolean>(false);
    const [query, setQuery] = useState<string>('');

    const search = () => {
        let promise = CustomerService.getCustomers(query);

        try {
            promise.then((response) => {
                setIsLoading(false);
                setCustomers(response.data);
            }) 
        } catch (e) {
            // todo : send error to an api for server side logging
            console.error(e);
        }
    }

    const displayCustomers = () => {
        return <>
            {customers.map((value : Customer) =>  
                <tr className="clientRow" onClick={() => handleRowClick(value.id)}>
                        <th>{value.id}</th>
                        <th>{value.firstName}</th>
                        <th>{value.lastName}</th>
                        <th>{value.phoneNumber}</th>
                </tr>
            )}
        </>
    }

    const handleRowClick = (clientId : number) => {
        history.push(`${match.url}/${clientId}`);
    }

    useEffect(() => {
        setIsLoading(true);
        const debouncedSearch = debounce(() => search(), 500);
        debouncedSearch();
    }, [query])

    const displayCustomerTable = () => {
        return <div className="parent">
            <div className="checkin">
                <input className="searchBar"
                    type="text"
                    id="searchBar"
                    placeholder="Rechercher par # de telephone..."
                    onChange={(e) => setQuery(e.target.value)} />

                {isLoading && <LinearProgress color='primary' />}

                <div className="scrollable">
                    <table className="clientTable">
                        <tr className="header">
                            <th>Id</th>
                            <th>Prénom</th>
                            <th>Nom</th>
                            <th>No téléphone</th>
                        </tr>
                        {displayCustomers()}
                    </table>
                </div>
            </div>
        </div>
    }

    return <> 
        <Switch>
            <Route path={`${match.path}/:clientId`}>
                <ClientPreview />
            </Route>
            <Route path={match.path}>
                { displayCustomerTable() }
            </Route>
        </Switch>
        
    </>
}

export default CheckIn;