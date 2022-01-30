import React from 'react';
import { useTranslation } from 'react-i18next';

export const Footer = () => {
    const {t} = useTranslation();
    return <div>
       <span>{t('forMoreInfo')}: <a href="https://github.com/Nikola-Popovic"></a></span> 
        {t('propulsépar')}: Nikola Popovic ©  | 2021-2022
    </div>
}