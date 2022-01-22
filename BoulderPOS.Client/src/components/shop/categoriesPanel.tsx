import React, { useState, useEffect } from 'react';
import { ProductCategory } from '../../data';
import './categoriesPanel.css';

interface CategoriesPanelProps { 
    categories: ProductCategory[],
    onCategoryClick: (categoryId:number) => void
}

const CategoriesPanel = (props : CategoriesPanelProps) => {
    
    const [selectedCategory, setSelected] = useState<number>(1);

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
                <button  key={category.id} className={selectedCategory === category.id ? "shopCategory selectedCategory" : "shopCategory"} 
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