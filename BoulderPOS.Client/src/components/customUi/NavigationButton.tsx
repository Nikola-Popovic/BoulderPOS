import React from 'react';
import { useHistory } from "react-router-dom";
import { Button, ButtonProps } from "@material-ui/core";


interface INavigationButtonProps {
    text : string,
    route : string,
}

const NavigationButton = ( props : INavigationButtonProps & ButtonProps) => {
    const history = useHistory();

    const _handleClick = () => {
        history.push(props.route);
    }

    return (
        <Button onClick={_handleClick} size="large" variant='outlined' {...props}> 
                {props.text}
            </Button>
    );
}
export { NavigationButton };