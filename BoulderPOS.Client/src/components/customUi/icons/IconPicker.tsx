import * as React from 'react';
import { icons } from './Icons';
import { Select, MenuItem } from "@mui/material";

interface IconPickerProps { 
    value: string,
    cName: string,
    onChange: (iconName: string) => void
}

class IconPicker extends React.Component<IconPickerProps>{
    render() {
        return <Select
            id="icon-picker-select"
            className={`${this.props.cName}`}
            onChange={(event) => this.props.onChange(event.target.value as string)}>
                {icons.map((icon, index) => 
                    <MenuItem value={icon} key={`${index}`}>
                        <i className={`${icon} fa-2x`} />
                    </MenuItem>
                )}
            </Select>

    }
}


export { IconPicker };
