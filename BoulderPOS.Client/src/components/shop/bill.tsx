import React, {  useEffect, useState } from 'react'
import { PaymentMethod } from '../../data/PaymentMethod';
import { Customer, ProductInCart, toCurrency} from '../../data';
import './Bill.css';

const TPS = 0.05;
const TVQ = 0.09975 ;

export interface BillProps {
    items: ProductInCart[], 
    client: Customer | undefined,
    onPaymentConfirm: (method: PaymentMethod) => void,
    setBillRefresh : React.Dispatch<React.SetStateAction<boolean>>,
    billRefresh : boolean
}

const Bill = (props : BillProps) => {
    const [subTotal, setSubtotal] = useState<number>(0);

    useEffect(() => {
        setSubtotal(evaluateSubtotal());
        props.setBillRefresh(false);
    }, [props.items, props.billRefresh]);

    const evaluateSubtotal = () =>{
        const priceList = props.items.map(a => a.price * a.quantity)
        return priceList.reduce((a, b) => a + b, 0)
    }

    const makeBillTable = () => {
        return props.items.map((item) => <tr>
            <td>{item.name}</td>
            <td>{item.quantity}</td>
            <td>{toCurrency(item.price * item.quantity)}</td>
        </tr>)
    }

    return <div className="billContext">
                <h1>Facture Courante</h1>
                <div className='clientFrame'>
                {
                    props.client === undefined ? 
                    <><button className="pictureFrame"> Add client </button>
                       <div className="clientName"> 
                            <h2> No client associated </h2>  
                       </div>
                    </> 
                    : <> 
                        <div className="pictureFrame"> Img </div>
                        <div className="clientName"> 
                            <h2> {props.client.firstName} {props.client.lastName} </h2>  
                        </div>
                        <div className="informationPanel">
                            <div>Email : {props.client.email} </div>
                            <div>Numéro de tél. : {props.client.phoneNumber} </div>
                        </div>
                      </>
                }
                </div>
                
                <div className="scrollable">
                    <table className='billTable'>
                        <thead>
                            <tr>
                                <th>Nom</th>
                                <th>Quantité</th>
                                <th>Prix</th>
                            </tr>
                        </thead>
                        <tbody>
                            {makeBillTable()}
                        </tbody> 
                    </table>
                </div>
                <div className="bottomSection">
                    <div className="bottomSectionChild">
                    <span>Sous-total</span>
                    <span>TPS</span>
                    <span>TVQ</span>
                    <span className="billTotalSpan">TOTAL</span>

                    </div>
                    <div className="bottomSectionChild">
                    <span >{toCurrency(subTotal)} $</span>
                    <span>{toCurrency(TPS * subTotal)} $</span>
                    <span>{toCurrency(TVQ * subTotal)} $</span>
                    <span className="billTotalSpan">{toCurrency(subTotal + TVQ * subTotal+ TPS * subTotal)} $</span>
                    <button className="billPaymentButton" onClick={() => props.onPaymentConfirm(PaymentMethod.Cash)}>Payer</button>

                    </div>
                </div>
            </div>
            
    }
    

export { Bill }
           