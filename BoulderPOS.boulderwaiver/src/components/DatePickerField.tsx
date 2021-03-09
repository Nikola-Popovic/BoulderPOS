import React from 'react';
import { useFormikContext } from 'formik';
import { DatePicker, MuiPickersUtilsProvider } from '@material-ui/pickers'
import DateFnsUtils from '@date-io/date-fns'

export type DatePickerProps = {
    name : string
}

export function DatePickerField( props : DatePickerProps ) {
    const formik = useFormikContext();
    const field = formik.getFieldProps(props.name);

    return (
        <MuiPickersUtilsProvider utils={DateFnsUtils}>
            <DatePicker
                value={field.value}
                onChange={value => formik.setFieldValue(props.name, value)}
            />
        </MuiPickersUtilsProvider>
    );
}