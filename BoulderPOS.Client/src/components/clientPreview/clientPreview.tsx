import React, { useEffect, useState } from 'react'
import { Customer } from '../../data';
import { 
    useParams, 
    useHistory
 } from "react-router-dom";
import { CustomerService } from '../../services/api';
import { Button } from '@material-ui/core';
import { useSnackbar } from 'notistack';
import './clientPreview.css';

export interface ClientPreviewProps {
}

interface RouteParams {
    clientId : string
}

const ClientPreview : React.FunctionComponent<ClientPreviewProps> = (props) => {
    const [client, setClient] = useState<Customer | null>(null);
    const history = useHistory();
    const { enqueueSnackbar } = useSnackbar();
    let { clientId } = useParams<RouteParams>();

    useEffect( () => {
        CustomerService.getCustomer(clientId).then(
            // todo : fetch client image
            (customer) => setClient(customer.data)
        ).catch( e => {
            // todo : Send to Error API
            enqueueSnackbar('An error occured while fetching the client.', {
                variant: 'error',
            });
            console.error(e);
        });
    }, [])

    const handleCheckin = () => {
        let promise = CustomerService.checkinCustomer(clientId);
        promise.then((response) => {
            if (response.data === false) {
                enqueueSnackbar('Insufficient entries for checkin. Go to shop.', {
                    variant: 'error',
                });
            }
            else {
                enqueueSnackbar('Successfully checked in !', {
                    variant: 'success',
                });
            }
        }).catch((error) => {
            // todo : Send error to API
            enqueueSnackbar('An error occured during checkin.', {
                variant: 'error',
            });
            console.error(error);
        })
    }

    const handleShop = () => {
        history.push(`/shop/${clientId}`);
    }

    return (
        <div className="parent">
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
                    <div><strong>Subscription: </strong></div>
                    {client?.subscription === null? <div> No subscription </div>
                    : <div>
                        <div>Start date: {client?.subscription.startDate}</div>
                        <div>End date: {client?.subscription.endDate}</div>
                    </div>
                    }
                </div>
                <div className="entriesPanel"> 
                    <div><strong>Entries: </strong></div>
                    {client?.entries === null? <div> No entries </div>
                    : <div>
                        <div>Quantity: {client?.entries.quantity}</div>
                        <div>Unlimited: {client?.entries.unlimitedEntries? "true" : "false"}</div>
                    </div>
                    }
                </div>
                <div className="buttonPanel">
                    <Button onClick={() => handleShop()} color='primary' variant='outlined'> Shop </Button>
                    <Button onClick={() => handleCheckin()} color='primary' variant='contained'> Check-In </Button> 
                </div>
            </div>
        </div>
    )
}

export default ClientPreview;