import React from 'react';
import { useFormikContext } from 'formik';
import { useTranslation } from 'react-i18next';
import i18n from '../../i18next';
import { TextField } from '@mui/material';

export type PhoneInputFieldProps = {
    name : string
}

export function PhoneInputField( props : PhoneInputFieldProps ) {
    const formik = useFormikContext();
    const field = formik.getFieldProps(props.name);
    const { t } = useTranslation(undefined, { i18n })
    return (
        <TextField
            value={field.value}
            label={t('phone')}
            onChange={value => formik.setFieldValue(props.name, value)}
        />
    );
}