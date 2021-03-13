import { ItemCategory } from "./ItemCategory"
import { ShopItem } from "./ShopItem"


export class DBEmul  {
    private categories: ItemCategory[] = [];
    private products: ShopItem[] = [];

    public getCategories() {
        return this.categories;
    }

    public addCategory(category: ItemCategory) {
        this.categories.push(category);
    }

    public removeCategory(id: number) {
        let categoryToRemove = this.categories.find( a => a.id == id)
        if(categoryToRemove != undefined){
            let indexToRemove = this.categories.indexOf(categoryToRemove)
            this.categories.splice(indexToRemove, 1);
        }
    }

    public getProducts() {
        return this.products;
    }

    public addProduct(product: ShopItem) {
        this.products.push(product);
    }

    public removeProduct(id: number) {
        let productToRemove = this.products.find( a => a.id == id)
        if(productToRemove != undefined) {
            let indexToRemove = this.products.indexOf(productToRemove)
            this.products.splice(indexToRemove, 1);
        }
    }

}

export default DBEmul
