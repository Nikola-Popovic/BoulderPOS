import React from 'react'
import history from "../../assets/history1.png"
import './navbar.css';

const NavBar = () => {
    return (
        <>
            <div className={true ? "Nav" : "NavBlack"}>
                <a href="/">
                    <span className="logo" >Logo</span>
                </a>

                <div className="EndSection">
                    <span className="NavLink" style={{cursor:"pointer"}}>
                        <img 
                        src={history}
                        alt="Undo" 
                        width="40" 
                        height="40"
                        style={{filter: "invert(100%)"}}/>                    
                    </span>
                </div>
            </div>
        </>
    )
}

export default NavBar
