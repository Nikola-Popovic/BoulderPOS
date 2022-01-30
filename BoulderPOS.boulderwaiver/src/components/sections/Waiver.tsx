import './Waiver.css';
import React from 'react';
import  NavigationButton from "../ui/NavigationButton";
import content from '../../markdown/Terms-And-Conditions.md';
import { useTranslation } from 'react-i18next';
import i18n from '../../i18next';
import { IWizardSectionProps } from '../wizard/wizard-component';
import { WizardActions } from '../wizard';
import { Button } from '@mui/material';

export default function Waiver(props : IWizardSectionProps) {
    const { t } = useTranslation(undefined, { i18n });
    const rawMarkup = () => ({
        __html: content
    });
    return (
        <div className="waiver-box">
            <h2>{t('t&c')}</h2>
            <>
                <div className="waiver-content" dangerouslySetInnerHTML={rawMarkup()} /> 
                <div className='waiver-bottom'>
                    <NavigationButton text={t('decline')} route="/" color='secondary' variant='outlined'/>
                    <Button onClick={() => props.dispatch({type: WizardActions.NEXT})} color='primary' variant='outlined'>
                        {t('accept')}
                    </Button>
                </div>
            </>
        </div>
    );
}