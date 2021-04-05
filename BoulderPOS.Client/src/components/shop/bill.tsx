import React, { useEffect } from 'react'
import { PaymentMethod } from '../../data/PaymentMethod';
import { ProductInCart, toCurrency} from '../../data';
import './bill.css';

const TPS = 0.05;
const TVQ = 0.09975 ;

export interface BillProps {
    items: ProductInCart[], 
    onPaymentConfirm: (method: PaymentMethod) => void
}

const Bill = (props : BillProps) => {

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

    useEffect(() => {}, [props.items]);

    return <div className="billContext">
                <h1 style={{display:"flex",justifyContent:"center"}}>Facture Courante</h1>
                <p style={{display:"flex",justifyContent:"center"}}>PLACEHOLDER CLIENTPREVIEW</p>
                <div className="scrollable">
                    <table style={{width:"90%",marginLeft: "auto", marginRight: "auto", marginTop: "50px"}}>
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
                    <span >{toCurrency(evaluateSubtotal())} $</span>
                    <span>{toCurrency(TPS * evaluateSubtotal())} $</span>
                    <span>{toCurrency(TVQ * evaluateSubtotal())} $</span>
                    <span className="billTotalSpan">{toCurrency(evaluateSubtotal() + TVQ * evaluateSubtotal()+ TPS * evaluateSubtotal())} $</span>
                    <button className="billPaymentButton" onClick={() => props.onPaymentConfirm(PaymentMethod.Cash)}>Payer</button>

                    </div>
                </div>
            </div>
            
    }
    

export { Bill }
           