import React, { useState, useEffect } from 'react';
import { LinearProgress } from '@material-ui/core';
import { Customer } from '../../data';
import { CustomerService } from '../../services/api'
import { debounce } from 'lodash';
import "./checkin.css";
import {
    useRouteMatch,
    Route,
    Switch,
    useHistory
} from "react-router-dom";
import { useSnackbar } from 'notistack';
import ClientPreview from './clientPage/clientPreview';

export interface CheckInProps {

}

const CheckIn : React.FunctionComponent<CheckInProps> = (props) => {
    let match = useRouteMatch();
    const history = useHistory();
    const { enqueueSnackbar } = useSnackbar();
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
            enqueueSnackbar('An error occured while fetching clients');
            console.error(e);
        }
    }

    const displayCustomers = () => {
        return <tbody>
            {customers.map((value : Customer) =>  
                <tr className="clientRow" onClick={() => handleRowClick(value.id)}>
                        <td>{value.id}</td>
                        <td>{value.firstName}</td>
                        <td>{value.lastName}</td>
                        <td>{value.phoneNumber}</td>
                </tr>
            )}
        </tbody>
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

                <div className="clientScroll">
                    <table className="clientTable">
                        <thead>
                            <tr>
                                <th>Id</th>
                                <th>Prénom</th>
                                <th>Nom</th>
                                <th>No téléphone</th>
                            </tr>
                        </thead>
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

export { CheckIn };