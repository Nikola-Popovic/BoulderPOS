import './ConsigneSecurite.css';
import React from "react";
import { useTranslation } from "react-i18next";
import { IWizardSectionProps, WizardActions } from "../wizard";
import frContent from '../../markdown/fr/Consignes.md';
import { Button } from '@mui/material';

const ConsigneIntro = (props : IWizardSectionProps) => {
    const { t } = useTranslation();
    const rawMarkup = () => ({
        __html: frContent
    });
    return <div className='consigne'>
        <h3>{t('securityIntro1')}</h3>
        <h2>{t('securityIntro2')}</h2>
        <div className="securite-content" dangerouslySetInnerHTML={rawMarkup()} /> 
        <Button onClick={() => props.dispatch({type: WizardActions.NEXT})} color='primary' variant='outlined'>
                        {t('iUnderstand')}
        </Button>
    </div>
}

export default ConsigneIntro;