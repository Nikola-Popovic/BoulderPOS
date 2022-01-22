import React from 'react';
import { useField, useFormikContext } from 'formik';
import DateTimePicker from '@mui/lab/DateTimePicker';
import TextField from '@mui/material/TextField';

export type DatePickerProps = {
    name : string
}

export function DatePickerField( props : DatePickerProps ) {
    const { setFieldValue } = useFormikContext();
    const [ field ] = useField(props);

    return (
        <DateTimePicker
            {...props}
            value={field.value}
            onChange={value => setFieldValue(field.name, value)}
            renderInput={(params) => <TextField {...params} />}
        />
    );
}