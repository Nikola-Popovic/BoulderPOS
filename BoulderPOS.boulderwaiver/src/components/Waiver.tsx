import './Waiver.css';
import React from 'react';
import  NavigationButton from "./NavigationButton";
import content from '../markdown/Terms-And-Conditions.md';

export default function Waiver() {
    const rawMarkup = () => ({
        __html: content
    });
    return (
        <div className="waiver-box">
            <h2> Terms and conditions </h2>
            <>
                <div className="waiver-content" dangerouslySetInnerHTML={rawMarkup()} /> 
                <div className='waiver-bottom'>
                    <NavigationButton text="Decline" route="/" color='secondary' variant='outlined'/>
                    <NavigationButton text="Accept" route="/signup" color='primary' variant='outlined'/>
                </div>
            </>
        </div>
    );
}