import React, { Component } from 'react'
import { ItemCategory } from '../../../data/ItemCategory';
import { ShopItem, toCurrency } from '../../../data/ShopItem';
import './availableProducts.css';

class AvailableProducts extends Component<{categories: ItemCategory[], items: ShopItem[], onItemClick: (item:ShopItem) => ShopItem, onCategoryClick: (item:ItemCategory) => ItemCategory, selectedCategory: ItemCategory},{}> {

    constructor(props : {categories: ItemCategory[], items: ShopItem[], onItemClick: (item:ShopItem) => ShopItem, onCategoryClick: (item:ItemCategory) => ItemCategory,  selectedCategory: ItemCategory}) {
        super(props);

        this.state = {
            categories: this.props.categories,
            items: this.props.items
        };
    }

    

    listCategories() {
        const itemCategories = [];
        for(let category of this.props.categories) {
            if(category.id === this.props.selectedCategory.id) {
                itemCategories.push(<button className="selectedCategory">{category.name}</button>)
            } else {
                itemCategories.push(<button className="shopCategory" onClick={() => this.props.onCategoryClick(category)} >{category.name}</button>)
            }
        }


        return itemCategories;
    }
    
    listItems() {
        const shopItems = [];
        for(let item of this.props.items) {
            if(item.categoryId === this.props.selectedCategory.id)
                shopItems.push(<button className="shopItem" onClick={() => this.props.onItemClick(item)}>{item.name}<br/><br/>{toCurrency(item.price)}</button>)
        }


        return shopItems;
    }

    // props liste de shopItems disponibles

    render() {
    return (
        <div className="shopContext">                    
            <div className="shopCategoriesSection">
                {this.listCategories()}
            </div>
            <div className="shopItemsSection">
                {this.listItems()}
            </div>
        </div>)
            }
        }


export default AvailableProducts
           