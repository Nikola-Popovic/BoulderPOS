import React from "react";
import NumberFormat from "react-number-format";
import {TextField, makeStyles} from "@material-ui/core";


interface CustomNumberFormatProps {
    inputRef: (instance: NumberFormat | null) => void;
    onChange: (event: { target: { value: string } }) => void;
}

const CustomNumberFormat = (props: CustomNumberFormatProps) => {
    const { inputRef, onChange, ...other } = props;
    return (
      <NumberFormat
        {...other}
        getInputRef={props.inputRef}
        onValueChange={values => {
          props.onChange({
            target: {
              value: values.value
            }
          });
        }}
        isNumericString
      />
    );
}

const useStyles = makeStyles((theme) => ({
    inputContainer: {
      border : 0,
      fontFamily : 'sansSerif'
    }
  }));



interface CurrenctyInputProps {
    value? : string,
    label : string,
    onChange : (event : React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => void
}

const CurrencyInput = (props : CurrenctyInputProps) => {
    const classes = useStyles();

    return <TextField
          label={props.label}
          value={props.value}
          onChange={props.onChange}
          id="formatted-currency-input"
          InputProps={{
            inputComponent: CustomNumberFormat as any
          }}
          type="number"
        />
}

export { CurrencyInput }