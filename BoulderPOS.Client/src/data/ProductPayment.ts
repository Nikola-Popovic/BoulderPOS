import { Product } from ".";

export interface ProductQuantity {
    quantity : number
}

export type ProductInCart = Product & ProductQuantity;

export interface ProductPayment {
    customerId? : number,
    isRefunded? : boolean,
    sellinPrice : number,
    productId : number,
    quantity : number
}