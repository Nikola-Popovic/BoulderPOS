import React from 'react';
import { useHistory } from "react-router-dom";
import makeStyles from '@mui/styles/makeStyles';
import { Button, ButtonProps } from "@mui/material";

const useStyles = makeStyles((theme) => ({
    buttonContainer: {
      marginTop: theme.spacing(1)
    }
  }));


interface INavigationButtonProps {
    text : string,
    route : string,
}

function NavigationButton( props : INavigationButtonProps & ButtonProps) {
    const history = useHistory();

    const _handleClick = () => {
        history.push(props.route);
    }
    
    const classes = useStyles();

    return (
        <Button onClick={_handleClick} className={classes.buttonContainer} size="large" color='primary' variant='outlined' {...props}> 
                {props.text}
            </Button>
    );
}
export default NavigationButton;