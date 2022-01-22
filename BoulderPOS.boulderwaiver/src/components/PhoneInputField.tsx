import React from 'react';
import { useFormikContext } from 'formik';
import PhoneInput from 'react-phone-input-2'
import 'react-phone-input-2/lib/material.css'

export type PhoneInputFieldProps = {
    name : string
}

export function PhoneInputField( props : PhoneInputFieldProps ) {
    const formik = useFormikContext();
    const field = formik.getFieldProps(props.name);

    return (
        <PhoneInput
            value={field.value}
            country={'ca'}
            autoFormat={true}
            onChange={value => formik.setFieldValue(props.name, value)}
        />
    );
}