import { Component } from 'react'
import AvailableProducts from '../components/shop/availableProducts/availableProducts';
import Bill from '../components/shop/bill/bill';
import { ItemCategory } from '../data/ItemCategory';
import { PaymentMethod } from '../data/PaymentMethod';
import { BillItem, ShopItem } from '../data/ShopItem';

class Shop extends Component<{},{cart: BillItem[], selectedCategory: ItemCategory}> {
    constructor(props:any) {
        super(props);
        this.state = {
            cart: [],
            selectedCategory: CATEGORIES[0],
        }
        console.log("Initialized Shop!")
    }

    onShopItemClick(item: ShopItem) {
        let tempCart = this.state.cart;
        if(tempCart.find(a => a.id === item.id ) === undefined) {
            tempCart.push(
                {
                    id: item.id,
                    price: item.price,
                    name: item.name,
                    categoryId: item.categoryId,
                    quantity: 1
                });
        }
        else {
            let i = tempCart.findIndex(a => a.id === item.id)
            tempCart[i].quantity++;
        }
        if(tempCart !== undefined){
            this.setState({
                cart: tempCart
            })
        }
        return item;
    }

    onCategoryClick(category: ItemCategory) {
        console.log("Clicked category " + category.name + "!");
        console.log("current category: "+this.state.selectedCategory)
        this.setState({
            selectedCategory: category
        })
        return category
    }

    onPaymentConfirm(method: PaymentMethod) {
        if (window.confirm('Has the payment been completed?')) {
            // Save transaction in database
            // Remove # of items from inventory
            // Add money amount to day total for stats
            console.log('Transaction was saved in the database.');
            this.setState({
                cart: []
            });
          } else {
            console.log('Transaction was not saved in the database.');
          }
    }

    render() {
        return (
            <div style={{height:"100%"}}>
                <Bill items={this.state.cart} onPaymentConfirm={(method: PaymentMethod) => this.onPaymentConfirm(method)}/>
                <AvailableProducts categories={CATEGORIES} items={ITEMS} onItemClick={(item: ShopItem) => this.onShopItemClick(item)} onCategoryClick={(category: ItemCategory) => this.onCategoryClick(category)} selectedCategory={this.state.selectedCategory}/>
            </div>
        )
    }
}

const CATEGORIES = [
    {
        id: 1,
        name:"Breuvage"
    },
    {
        id: 2,
        name:"Nourriture"
    },
    {
        id: 3,
        name:"Merch"
    },
    {
        id: 4,
        name:"Entrée"
    },
    {
        id: 5,
        name:"Abonnement"
    },
    {
        id: 6,
        name:"Équipement"
    },
    {
        id: 7,
        name:"Promotions"
    },

]

const ITEMS = [
    {
        id: 1,
        name:"Patate frite",
        price: 2.99,
        categoryId: 2,
    },
    {
        id: 1123,
        name:"ITEM AVEC UN TRES TRES TRES TRES TRES TRES TRES TRES TRES TRES TRES TRES TRES TRES TRES TRES TRES TRES TRES TRES TRES TRES TRES TRES TRES TRES TRES TRES TRES TRES TRES TRES TRES TRES TRES TRES TRES TRES TRES TRES TRES TRES TRES TRES TRES TRES TRES TRES TRES TRES TRES TRES TRES TRES TRES TRES TRES TRES TRES TRES TRES TRES TRES TRES TRES TRES TRES TRES TRES TRES TRES TRES TRES TRES TRES TRES TRES TRES TRES TRES TRES TRES TRES TRES TRES TRES TRES TRES TRES TRES TRES TRES TRES TRES TRES TRES TRES TRES LONG NOM",
        price: 1.00,
        categoryId: 2,
    },
    {
        id: 2,
        name:"Grilled Cheese",
        price: 3.99,
        categoryId: 2,
    },
    {
        id: 3,
        name:"Ramen",
        price: 6.99,
        categoryId: 2,
    },
    {
        id: 4,
        name:"Crudités",
        price: 2.99,
        categoryId: 2,
    },
    {
        id: 5,
        name:"Sandwich",
        price: 4.99,
        categoryId: 2,
    },
    {
        id: 6,
        name:"Salade de fruits",
        price: 2.99,
        categoryId: 2,
    },
    {
        id: 7,
        name:"Chips",
        price: 1.99,
        categoryId: 2,
    },
    {
        id: 8,
        name:"Fleurs Comestibles",
        price: 2.99,
        categoryId: 2,
    },
    {
        id: 9,
        name:"Bouteille d'eau",
        price: 0.99,
        categoryId: 1,
    },
    {
        id: 10,
        name:"Gatorade",
        price: 2.99,
        categoryId: 1,
    },
    {
        id: 11,
        name:"Casquette BB",
        price: 19.99,
        categoryId: 3,
    },
    {
        id: 12,
        name:"Hoodie BB",
        price: 49.99,
        categoryId: 3,
    },
    {
        id: 13,
        name:"Entrée journalière",
        price: 5.66,
        categoryId: 4,
    },
    {
        id: 14,
        name:"Abonnement annuel",
        price: 300.00,
        categoryId: 5,
    },
    {
        id: 15,
        name:"Chaussons Sportiva ",
        price: 219.95,
        categoryId: 6,
    },
    {
        id: 16,
        name:"2 pour 1 fête des mères ",
        price: 2.83,
        categoryId: 7,
    },
    {
        id: 17,
        name:"rabais paques 25%",
        price: 4.25,
        categoryId: 7,
    },
    {
        id: 18,
        name:"forfait fete enfants",
        price: 75,
        categoryId: 7,
    },
    {
        id: 19,
        name:"carte cadeau 50$",
        price: 50,
        categoryId: 7,
    },
    {
        id: 20,
        name:"sirop gratis fete des patriotes",
        price: 0,
        categoryId: 7,
    },
]


export default Shop
