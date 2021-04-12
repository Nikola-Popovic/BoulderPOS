import React, { useState, useEffect } from 'react';
import { ProductCategory } from '../../data';
import './CategoriesPanel.css';

interface CategoriesPanelProps { 
    categories: ProductCategory[],
    onCategoryClick: (categoryId:number) => void
}

const CategoriesPanel = (props : CategoriesPanelProps) => {
    
    const [selectedCategory, setSelected] = useState<Number>(1);

    const handleClick = (categoryId : number) => {
        setSelected(categoryId);
        props.onCategoryClick(categoryId);
    }

    useEffect(() => {
        if (props.categories[0] !== undefined) {
            setSelected(props.categories[0].id);
        }
    }, [props.categories])

    const listCategories = () => {
        return <>
            { props.categories.map((category) => 
                <button className={selectedCategory === category.id ? "shopCategory selectedCategory" : "shopCategory"} 
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