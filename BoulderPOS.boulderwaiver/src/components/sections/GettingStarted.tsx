import React from 'react';
import './GettingStarted.css';
import { useTranslation } from 'react-i18next';
import i18n from '../../i18next';
import { IWizardSectionProps } from '../wizard/wizard-component';
import { Button } from '@mui/material';
import { WizardActions } from '../wizard';


const GettingStarted = (props : IWizardSectionProps) => {
    const { t } = useTranslation('translation', { i18n });
   
    const _handleClick = () => {
        props.dispatch({type: WizardActions.NEXT});
    }

    return <div className='get-started'>
        <Button onClick={_handleClick} variant='contained'>
            {t("getStarted")}
        </Button>
    </div>
}

export default GettingStarted;