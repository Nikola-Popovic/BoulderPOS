import React from 'react';
import { useHistory } from "react-router-dom";
import { makeStyles } from '@material-ui/core/styles';
import { Button } from '@material-ui/core';

const useStyles = makeStyles((theme) => ({
    buttonContainer: {
      marginTop: theme.spacing(1)
    }
  }));

// Replaced by more general Navigation Button
function SignupButton() {
    const history = useHistory();

    const handleStart = () => {
        history.push("/signup")
    }

    const classes = useStyles();

    return <Button onClick={handleStart} className={classes.buttonContainer} size="large" variant="outlined" color="primary"> 
            Sign up
        </Button>
}

export default SignupButton;