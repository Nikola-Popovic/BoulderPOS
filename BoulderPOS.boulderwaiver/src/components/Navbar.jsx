import React from "react";
import LanguageSwitcher from "./LanguageSwitcher";
import AppBar from '@mui/material/AppBar';
import Toolbar from '@mui/material/Toolbar';
import Typography from '@mui/material/Typography';
import HideOnScroll from './HideOnScoll';

export default function Navbar(props) {
  return (
    <div>
      <HideOnScroll {...props}>
        <AppBar>
          <Toolbar>
            <Typography variant="h6" component="div">
              <Typography variant="h6" component="div" sx={{ flexGrow: 1 }} />
              <LanguageSwitcher />
            </Typography>
          </Toolbar>
        </AppBar>
      </HideOnScroll>
      <Toolbar />
    </div>
  );
}