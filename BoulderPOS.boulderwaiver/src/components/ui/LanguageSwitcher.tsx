import { FormControl, MenuItem, Select } from "@mui/material";
import React from "react";
import { useTranslation } from "react-i18next";
import './LanguageSwitcher.css';

function LanguageSwitcher() {
  const { i18n } = useTranslation();
  return (
    <FormControl className="lang-select">
      <Select
        value={i18n.language}
        id="demo-simple-select"
        onChange={(e) =>
          i18n.changeLanguage(e.target.value)
        }
      >
        <MenuItem value="fr">Français</MenuItem>
        <MenuItem value="en">English</MenuItem>
      </Select>
    </FormControl>
  );
}
export default LanguageSwitcher;