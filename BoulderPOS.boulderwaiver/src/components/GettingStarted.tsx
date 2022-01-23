import React from 'react';
import NavigationButton from './NavigationButton';
import './GettingStarted.css';
import { useTranslation } from 'react-i18next';
import i18n from '../i18n';

const GettingStarted = () => {
    const { t } = useTranslation('translation', { i18n });
    return <div className='get-started'>
        <NavigationButton text={t("getStarted")} route="/waiver" variant='contained'/>
    </div>
}

export default GettingStarted;