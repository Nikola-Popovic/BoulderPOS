import { Product } from "./Product";

export interface CategoryToCreate {
    name : string,
    iconName : string
}

export interface ProductCategory {
    id : number,
    name : string,
    iconName : string,
    products : Product[] | null
}