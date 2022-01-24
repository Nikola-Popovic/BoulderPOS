import './Waiver.css';
import React from 'react';
import  NavigationButton from "./NavigationButton";
import content from '../markdown/Terms-And-Conditions.md';
import { useTranslation } from 'react-i18next';
import i18n from '../i18next';

export default function Waiver() {
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
                    <NavigationButton text={t('accept')} route="/signup" color='primary' variant='outlined'/>
                </div>
            </>
        </div>
    );
}