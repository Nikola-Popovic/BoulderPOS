import React from 'react';
import { useHistory } from "react-router-dom";
import { makeStyles } from '@material-ui/core/styles';
import { Button, ButtonProps } from "@material-ui/core";


interface INavigationButtonProps {
    text : string,
    route : string,
}

interface INavigationButtonState {
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