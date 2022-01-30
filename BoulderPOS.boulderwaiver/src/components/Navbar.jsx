import React from "react";
import LanguageSwitcher from "./LanguageSwitcher";
import HideOnScroll from './HideOnScoll';
import './Navbar.css';

export default function Navbar(props) {
  return (
    <HideOnScroll {...props}>
      <div className="Nav">
        <div className="EndSection">
            <LanguageSwitcher />
        </div>
      </div>
    </HideOnScroll>
  );
}