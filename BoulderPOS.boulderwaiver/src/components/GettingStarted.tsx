import React from 'react';
import NavigationButton from './NavigationButton';
import './Header.css';

function GettingStarted() {
    return <div className='get-started'>
        <NavigationButton text="Get Started" route="/waiver" variant='contained'/>
    </div>
}

export default GettingStarted;