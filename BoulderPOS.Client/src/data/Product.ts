import { ProductCategory } from "./ProductCategory";

export interface Product {
    id : number,
    name : string,
    price : number,
    category : ProductCategory,
    quantity : number,
    durationInMonth? : number,
    categoryId : number
}

export interface ProductToCreate {
    name : string,
    price : number,
    categoryId : number
}

export function toCurrency(num: number) {
    return (Math.round(num*Math.pow(10,2))/Math.pow(10,2)).toFixed(2)
}