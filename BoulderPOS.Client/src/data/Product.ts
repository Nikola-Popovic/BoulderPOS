import { ProductCategory } from "./ProductCategory";

export interface Product {
    id : number,
    name : string,
    price : number,
    category : ProductCategory,
    categoryId : number
}

export interface ProductToCreate {
    name : string,
    price : number,
    categoryId : string
}