import React, { Component } from 'react'

export class CreateProductForm extends Component {

    getDataList() {
return (    
    <select name="prodcat" id="prodcat">
        <option value="beverage">Breuvage</option>
        <option value="food">Nourriture</option>
        <option value="entry">Entrée</option>
        <option value="promotion">Promotion</option>
    </select>
    );
}

getForm() {
   return (
    <form>
    <label htmlFor="prodname">Product name:</label><br/>
    <input type="text" id="prodname" /><br/>
    <label htmlFor="prodprice">Product price:</label><br/>
    <input type="number" id="prodprice" min="0" step="0.01"/><br/>
    <label htmlFor="prodcat">Product category:</label><br/>
    {this.getDataList()} <br/>
    <input type="reset"/>
    <input type="submit"/>
    
</form>
   ) 
}

    render() {
        return (
            <div style={{display:"flex", justifyContent:"center"}}>
                <h1>Add product</h1>
                {this.getForm()}
            </div>
        )
    }
}
