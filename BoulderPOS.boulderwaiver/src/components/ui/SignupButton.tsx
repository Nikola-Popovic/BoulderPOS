import React from 'react';
import { useHistory } from "react-router-dom";
import { Button } from '@mui/material';
import { useTranslation } from 'react-i18next';


// Replaced by more general Navigation Button
function SignupButton() {
    const history = useHistory();
    const { t } = useTranslation();
    const handleStart = () => {
        history.push("/signup")
    }

    return <Button onClick={handleStart} size="large" variant="outlined" color="primary"> 
            {t('signup')}
        </Button>
}

export default SignupButton;