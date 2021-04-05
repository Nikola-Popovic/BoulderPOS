import React from 'react';
import { ProductCategory } from '../../data';
import './categoriesPanel.css';

interface CategoriesPanelProps { 
    categories: ProductCategory[],
    onCategoryClick: (categoryId:number) => void
}

const CategoriesPanel = (props : CategoriesPanelProps) => {
    
    let selectedCategories : number;

    const handleClick = (categoryId : number) => {
        props.onCategoryClick(categoryId);
        selectedCategories = categoryId;
    }

    const listCategories = () => {
        return <>
            { props.categories.map((category) => 
                <button className={selectedCategories === category.id ? "selectedCategory" : "shopCategory"} 
                onClick={() => handleClick(category.id)}>
                    <i className={`${category.iconName} fa-2x`} />
                </button>
            )}
        </>
    }

    return <div className="shopCategoriesSection">
            {listCategories()}
    </div>
}

export { CategoriesPanel };