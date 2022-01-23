import React from 'react';
import { useHistory } from "react-router-dom";
import makeStyles from '@mui/styles/makeStyles';
import { Button } from '@mui/material';
import { useTranslation } from 'react-i18next';

const useStyles = makeStyles((theme) => ({
    buttonContainer: {
      marginTop: theme.spacing(1)
    }
  }));

// Replaced by more general Navigation Button
function SignupButton() {
    const history = useHistory();
    const { t } = useTranslation();
    const handleStart = () => {
        history.push("/signup")
    }

    const classes = useStyles();

    return <Button onClick={handleStart} className={classes.buttonContainer} size="large" variant="outlined" color="primary"> 
            {t('signup')}
        </Button>
}

export default SignupButton;