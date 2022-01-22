import * as React from 'react';
import { icons } from './Icons';
import PropTypes from 'prop-types';
import FontIconPicker from '@fonticonpicker/react-fonticonpicker';
import '@fonticonpicker/react-fonticonpicker/dist/fonticonpicker.base-theme.react.css';
import '@fonticonpicker/react-fonticonpicker/dist/fonticonpicker.material-theme.react.css';

IconPicker.propTypes = {
    value: PropTypes.string,
    cName: PropTypes.string,
    onChange: PropTypes.func
}

class IconPicker extends React.Component{
    render() {
        return <FontIconPicker 
            icons={icons}
            theme="indigo"
            className={`${this.props.cName}`}
            isMulti={false}
            {...this.props}
        />
    }
}


export { IconPicker };
