import React from 'react'
import { NavLink } from 'react-router-dom'
import { Button } from '@material-ui/core';


const Home = () => {
    return (
        <div style={{
            display: 'flex', 
            flexDirection: 'column',
            justifyContent: 'center', 
            alignItems: 'center', 
            height: '90vh'
        }}
        >
            <Button variant="outlined" style={{margin:'2vh'}}>
                    <NavLink style={{textDecoration: "none", color:"black", fontWeight:"bold"}} to="/checkin">Check In</NavLink>
            </Button>
            <Button variant="outlined" style={{margin:'2vh'}}>
                    <NavLink style={{textDecoration: "none", color:"black", fontWeight:"bold"}} to="/shop">Magasin</NavLink>
            </Button>
            <Button variant="outlined" style={{margin:'2vh'}}>
                    <NavLink style={{textDecoration: "none", color:"black", fontWeight:"bold"}} to="/administration">Administration</NavLink>
            </Button>
        </div>
    )
}

export default Home
