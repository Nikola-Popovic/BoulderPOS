import { Button } from '@material-ui/core'
import React from 'react'
import { NavLink } from 'react-router-dom'

const Administration = () => {
    return (
        <div style={{
            display: 'flex', 
            flexDirection: 'column',
            justifyContent: 'center', 
            alignItems: 'center', 
            height: '95vh'
        }}
        >
            <Button variant="outlined" style={{margin:'2vh'}}>
                    <NavLink style={{textDecoration: "none", color:"black", fontWeight:"bold"}} to="/addproduct">Add a new product</NavLink>
            </Button>
            <Button variant="outlined" style={{margin:'2vh'}}>
                    <NavLink style={{textDecoration: "none", color:"black", fontWeight:"bold"}} to="/addproductcategory">Add a product category</NavLink>
            </Button>
        </div>
    )
}

export default Administration
