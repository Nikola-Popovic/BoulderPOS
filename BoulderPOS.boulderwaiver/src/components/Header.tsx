import React from 'react';
import { useTranslation } from 'react-i18next';
import './Header.css';
import i18n from '../i18n'

const Header = () => {
    const { t } = useTranslation('translation', { i18n });
    return <a href="/">
    <h1 className="header">
        {t("waiver")}
    </h1>
    </a>
}

export default Header;