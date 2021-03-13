import { ItemCategory } from "./ItemCategory";

export interface ShopItem {
    id: number,
    price: number;
    name: string;
    categoryId: number;
}

export interface BillItem extends ShopItem {
    quantity: number;
}

export function toCurrency(num: number) {
    return (Math.round(num*Math.pow(10,2))/Math.pow(10,2)).toFixed(2)
}