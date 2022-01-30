import { Button } from '@mui/material';
import React from 'react';
import { useTranslation } from "react-i18next";
import i18n from '../../../i18next';
import { IWizardSectionProps } from '../wizard-component';
import { WizardActions } from '../wizard-reducer';


const GettingStarted = (props : IWizardSectionProps) => {
    const { t } = useTranslation('translation', { i18n });

    const _handleClick = () => {
        props.dispatch({type: WizardActions.NEXT});
    }
    return <div className='getting-started'>
        <Button onClick={_handleClick} color='primary' variant='outlined'>{t("getStarted")}</Button>
    </div>
}

export default GettingStarted;