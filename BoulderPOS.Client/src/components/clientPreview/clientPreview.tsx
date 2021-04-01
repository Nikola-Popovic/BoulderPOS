import React, { useEffect, useState } from 'react'
import { Customer } from '../../data';
import { useParams } from "react-router-dom";
import { CustomerService } from '../../services/api';
import { Button } from '@material-ui/core';
import { Alert, AlertTitle } from '@material-ui/lab';
import './clientPreview.css';

export interface ClientPreviewProps {
}

interface RouteParams {
    clientId : string
}

const ClientPreview : React.FunctionComponent<ClientPreviewProps> = (props) => {
    const [client, setClient] = useState<Customer | null>(null);
    const [error, setError] = useState<boolean>(false);
    let { clientId } = useParams<RouteParams>();

    useEffect( () => {
        CustomerService.getCustomer(clientId).then(
            (customer) => setClient(customer.data)
        ).catch( e => {
            // An error occured. Show notif. Send to Error API
            setError(true);
            console.error(e);
        });
    }, [])

    const handleCheckin = () => {

    }

    return (
        <div className="parent">
            {error && <Alert severity="error"> 
                <AlertTitle> Error </AlertTitle>
                An error occured while <strong> fetching the client </strong>
            </Alert> }
            {!error && 
            
                <div className="clientPanel" >
                    <div className="pictureFrame"> Img </div>
                    <div className="clientName"> 
                        <h2> {client?.firstName} {client?.lastName} </h2>  
                    </div>
                    <div className="informationPanel">
                        <div><strong>Email</strong>: {client?.email} </div>
                        <div><strong>Birthdate</strong>: {client?.birthDate} </div>
                    </div>
                    <div className="informationPanel">
                        <div><strong>Phone#</strong>: {client?.phoneNumber} </div>
                    </div>
                    <div className="subscriptionPanel"> 
                        <div> Subscription: </div>
                        {client?.subscription === null? <div> No subscription </div>
                        : <div>
                            <div>Start date: {client?.subscription.startDate}</div>
                            <div>End date: {client?.subscription.endDate}</div>
                        </div>
                        }
                    </div>
                    <div className="entriesPanel"> 
                        <div> Entries: </div>
                        {client?.entries === null? <div> No entries </div>
                        : <div>
                            <div>Quantity: {client?.entries.quantity}</div>
                            <div>Unlimited: {client?.entries.unlimitedEntries? "true" : "false"}</div>
                        </div>
                        }
                    </div>
                    <div className="buttonPanel">
                        <Button onClick={() => handleCheckin()} color='primary' variant='contained'> Check-In </Button> 
                    </div>
                </div>
            }
        </div>
    )
}

export default ClientPreview;