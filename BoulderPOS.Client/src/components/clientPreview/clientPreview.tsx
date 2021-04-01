import React, { useEffect, useState } from 'react'
import { Customer } from '../../data';
import { useParams } from "react-router-dom";
import { CustomerService } from '../../services/api';
import { Alert, AlertTitle } from '@material-ui/lab';

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

    return (
        <div>
            {error && <Alert severity="error"> 
                <AlertTitle> Error </AlertTitle>
                An error occured while <strong> fetching the client </strong>
            </Alert> }
            {!error && 
                <h2> {client?.firstName} {client?.lastName} </h2> 
            }
        </div>
    )
}

export default ClientPreview;