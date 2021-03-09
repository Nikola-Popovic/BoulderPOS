import React, { Component } from 'react'
import { PaymentMethod } from '../../../data/PaymentMethod';
import { BillItem, toCurrency } from '../../../data/ShopItem';
import './bill.css';

const TPS = 0.05;
const TVQ = 0.09975 ;

class Bill extends Component<{items: BillItem[], onPaymentConfirm: (method: PaymentMethod) => void},{}> {

    constructor(props : {items: BillItem[], onPaymentConfirm: (method: PaymentMethod) => void}) {
        super(props);
    }

    evaluateSubtotal() {
        const priceList = this.props.items.map(a => a.price * a.quantity)
        return priceList.reduce((a, b) => a + b, 0)
    }

    makeBillTable() {
        const table = [];
        for (let item of this.props.items) {
            table.push(
            <tr>
                <td>{item.name}</td>
                <td>{item.quantity}</td>
                <td>{toCurrency(item.price * item.quantity)}</td>
            </tr>
            )
        }

        return table
    }

    render() {
    return (
            <div className="billContext">
                <h1 style={{display:"flex",justifyContent:"center"}}>Facture Courante</h1>
                <p style={{display:"flex",justifyContent:"center"}}>PLACEHOLDER CLIENTPREVIEW</p>
                <div className="scrollable">
                    <table style={{width: "90%", marginLeft: "auto", marginRight: "auto", marginTop: "50px"}}>
                        <thead>
                            <tr>
                                <th>Nom</th>
                                <th>Quantité</th>
                                <th>Prix</th>
                            </tr>
                        </thead>
                        <tbody>
                            {this.makeBillTable()}
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
                    <span >{toCurrency(this.evaluateSubtotal())} $</span>
                    <span>{toCurrency(TPS * this.evaluateSubtotal())} $</span>
                    <span>{toCurrency(TVQ * this.evaluateSubtotal())} $</span>
                    <span className="billTotalSpan">{toCurrency(this.evaluateSubtotal() + TVQ * this.evaluateSubtotal()+ TPS * this.evaluateSubtotal())} $</span>
                    <button className="billPaymentButton" onClick={() => this.props.onPaymentConfirm(PaymentMethod.Cash)}>Payer</button>

                    </div>
                </div>
            </div>)
            }
        }


export default Bill
           