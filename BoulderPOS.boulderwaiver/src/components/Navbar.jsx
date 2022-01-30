import React from "react";
import LanguageSwitcher from "./ui/LanguageSwitcher";
import HideOnScroll from './ui/HideOnScoll';
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