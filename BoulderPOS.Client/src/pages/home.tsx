import React from 'react'
import { NavLink } from 'react-router-dom'
import { Button } from '@material-ui/core';
import "./home.css"

const Home = () => {
    return (
        <div className='home'>
            <Button variant="outlined" style={{backgroundColor:'snow'}}>
                    <NavLink to="/checkin">Check In</NavLink>
            </Button>
            <Button variant="outlined" style={{backgroundColor:'gainsboro'}}>
                    <NavLink to="/shop">Magasin</NavLink>
            </Button>
            <Button variant="outlined" style={{backgroundColor:'silver'}}>
                    <NavLink to="/administration">Administration</NavLink>
            </Button>
        </div>
    )
}

export default Home
