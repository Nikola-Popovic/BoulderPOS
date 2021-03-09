import React from 'react'
import { NavLink } from 'react-router-dom'
import './navbar.css';

const NavBar = () => {
    return (
        <>
            <div className="Nav">
                <span className="NavLink" >Logo</span>

                <div className="EndSection">
                    <span className="NavLink" style={{cursor:"pointer"}}>
                        <img 
                        src="https://cdn1.iconfinder.com/data/icons/user-interface-2-glyph/32/ui_history_schedule_time-512.png" 
                        alt="Undo" 
                        width="40" 
                        height="40"
                        style={{filter: "invert(100%)"}}/>                    
                    </span>
                    <NavLink className="NavLink" style={{cursor:"pointer"}} to="/">
                        <img 
                        src="https://cdn4.iconfinder.com/data/icons/pictype-free-vector-icons/16/home-512.png" 
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
