import React, { Component } from 'react'
import { Product, toCurrency } from '../../data';
import './ProductsPanel.css';

interface ProductsPanelProps { 
    products: Product[], 
    onItemClick: (item:Product) => void
}

const ProductsPanel = (props: ProductsPanelProps ) => {

    const listProducts = () => {
        return  props.products.map((product) => 
            <button className="shopItem" onClick={() => props.onItemClick(product)}>
                {product.name} <br/> <br/>{toCurrency(product.price)}
            </button>
        )
    }

    return <div className="shopItemsSection">
        {listProducts()}
    </div>
}

export { ProductsPanel }