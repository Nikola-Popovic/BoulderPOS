import React from 'react';
import * as content from '../../../markdown/Terms-And-Conditions.md';
import { useTranslation } from 'react-i18next';
import i18n from '../../../i18next';
import { IWizardSectionProps } from '../wizard-component';

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
            </>
        </div>
    );
}