import React, { useState, useEffect } from 'react';
import { LinearProgress } from '@material-ui/core';
import { Customer } from '../data';
import { CustomerService } from '../services/api'
import { debounce } from 'lodash';
import "./checkin.css";
import { setFlagsFromString } from 'v8';
import ActionSearch from 'material-ui/svg-icons/action/search';

export interface CheckInProps {

}

const CheckIn : React.FunctionComponent<CheckInProps> = (props) => {

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
            {customers.map((value : Customer) =>  <tr className="clientRow">
                    <th>{value.id}</th>
                    <th>{value.firstName}</th>
                    <th>{value.lastName}</th>
                    <th>{value.phoneNumber}</th>
                </tr>
            )}
        </>
    }

    useEffect(() => {
        setIsLoading(true);
        const debouncedSearch = debounce(() => search(), 500);
        debouncedSearch();
    }, [query])

    return <> 
        <div className="parent">
            <div className="checkin">
            <input className="searchBar" 
                   type="text" 
                   id="searchBar" 
                   placeholder="Rechercher par # de telephone..."
                   onChange={(e) => setQuery(e.target.value)}/>

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
    </>
}

export default CheckIn;