import * as React from 'react';
import { icons } from './Icons';
//@ts-ignore
import FontIconPicker from '@fonticonpicker/react-fonticonpicker';
import '@fonticonpicker/react-fonticonpicker/dist/fonticonpicker.base-theme.react.css';
import '@fonticonpicker/react-fonticonpicker/dist/fonticonpicker.material-theme.react.css';

interface IconPickerProps {
    value: string;
    onChange: (value: string) => void;
    cName?: string;
}

const IconPicker = (props : IconPickerProps) => {
    return <FontIconPicker 
        icons={icons}
        theme="indigo"
        className={`${props.cName}`}
        isMulti={false}
        {...props}
    />
}

export { IconPicker };
