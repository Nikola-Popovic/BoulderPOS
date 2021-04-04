import React from 'react'
import { NavLink } from 'react-router-dom'
import { Button } from '@material-ui/core';
import { NavigationButton } from '../components/customUi';
import "./home.css"

const Home = () => {
    return (
        <div className='home'>
            <NavigationButton text='Check In' route='/checkin' style={{backgroundColor:'snow'}} />
            <NavigationButton text='Magasin' route='/shop' style={{backgroundColor:'gainsboro'}} />
            <NavigationButton text='Administration' route='/administration' style={{backgroundColor:'silver'}} />
        </div>
    )
}

export default Home
