import React from 'react';
import './Header.css';

interface IHeaderProps {
    title? : String
}

interface IHeaderState {
}

export default class Header extends React.Component<IHeaderProps, IHeaderState>  {
    render()
    {
        const {title} = this.props;
        return (
            <a href="/">
                <h1 className="header">
                    {title ? title : "Boulder Waiver"}
                </h1>
            </a>
        );
    }
}