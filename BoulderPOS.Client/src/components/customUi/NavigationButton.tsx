import React from 'react';
import { useHistory } from "react-router-dom";
import { makeStyles } from '@material-ui/core/styles';
import { Button, ButtonProps } from "@material-ui/core";

const useStyles = makeStyles((theme) => ({
    buttonContainer: {
      marginTop: theme.spacing(1)
    }
  }));


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
    
    const classes = useStyles();

    return (
        <Button onClick={_handleClick} className={classes.buttonContainer} size="large" variant='outlined' {...props}> 
                {props.text}
            </Button>
    );
}
export { NavigationButton };