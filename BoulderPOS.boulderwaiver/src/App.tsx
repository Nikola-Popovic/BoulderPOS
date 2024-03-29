import React from 'react';
import CssBaseline from '@mui/material/CssBaseline';
import { SnackbarProvider } from 'notistack';
import { createTheme, ThemeProvider, Theme, StyledEngineProvider, adaptV4Theme } from '@mui/material/styles';
import './App.css';
import Header from './components/Header';
import Navbar from './components/Navbar'
import LocalizationProvider from '@mui/lab/LocalizationProvider';
import AdapterDateFns from '@mui/lab/AdapterDateFns';
import { WizardComponent } from './components/wizard/wizard-component';

declare module '@mui/styles/defaultTheme' {
  // eslint-disable-next-line @typescript-eslint/no-empty-interface
  interface DefaultTheme extends Theme {}
}


function App() {
  const theme = createTheme(adaptV4Theme({
    palette: {
      primary: {
        main : '#4791db'
      }
    }
  }));

  return (
    <LocalizationProvider dateAdapter={AdapterDateFns}>
      <StyledEngineProvider injectFirst>
        <ThemeProvider theme={theme}>
          <div className="App">
            <CssBaseline />
            <SnackbarProvider maxSnack={1}>
              <Header />
              <Navbar />
              <div className="box-center">
                <WizardComponent></WizardComponent>
              </div>
              
            </SnackbarProvider>
          </div>
        </ThemeProvider>
      </StyledEngineProvider>
    </LocalizationProvider>
  );
}

export default App;
