import React from 'react'
import { NavLink } from 'react-router-dom'
import './navbar.css';
import history from "../../assets/history1.png"
import home from "../../assets/home.png"

const NavBar = () => {
    return (
        <>
            <div className="Nav">
                <span className="NavLink" >Logo</span>

                <div className="EndSection">
                    <span className="NavLink" style={{cursor:"pointer"}}>
                        <img 
                        src={history}
                        alt="Undo" 
                        width="40" 
                        height="40"
                        style={{filter: "invert(100%)"}}/>                    
                    </span>
                    <NavLink className="NavLink" style={{cursor:"pointer"}} to="/">
                        <img 
                        src={home} 
                        alt="Home" 
                        width="35" 
                        height="35"
                        style={{filter: "invert(100%)"}}/>                    
                    </NavLink>
                </div>
            </div>
        </>
    )
}

export default NavBar
