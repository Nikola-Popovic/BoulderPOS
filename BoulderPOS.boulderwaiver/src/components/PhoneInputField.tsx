import React from 'react';
import { useFormikContext } from 'formik';
import PhoneInput from 'react-phone-input-2'
import 'react-phone-input-2/lib/material.css'
import { useTranslation } from 'react-i18next';
import i18n from '../i18n';

export type PhoneInputFieldProps = {
    name : string
}

export function PhoneInputField( props : PhoneInputFieldProps ) {
    const formik = useFormikContext();
    const field = formik.getFieldProps(props.name);
    const { t } = useTranslation(undefined, { i18n })
    return (
        <PhoneInput
            value={field.value}
            specialLabel={t('phone')}
            country={'ca'}
            autoFormat={true}
            onChange={value => formik.setFieldValue(props.name, value)}
        />
    );
}