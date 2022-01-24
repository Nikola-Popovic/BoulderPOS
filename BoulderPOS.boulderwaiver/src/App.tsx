import React from 'react';
import CssBaseline from '@mui/material/CssBaseline';
import { SnackbarProvider } from 'notistack';
import { createTheme, ThemeProvider, Theme, StyledEngineProvider, adaptV4Theme } from '@mui/material/styles';
import { BrowserRouter as Router, Route, Switch} from 'react-router-dom';
import GettingStarted from './components/GettingStarted';
import Waiver from './components/Waiver';
import Signup from './components/Signup';
import './App.css';
import Header from './components/Header';
import Navbar from './components/Navbar'
import LocalizationProvider from '@mui/lab/LocalizationProvider';
import AdapterDateFns from '@mui/lab/AdapterDateFns';

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
              <Router>
                <Switch>
                  <Route path="/waiver" component={Waiver} />
                  <Route path="/signup" component={Signup} />
                  <Route path="/" component={GettingStarted} />
                </Switch>
              </Router>
              </div>
            </SnackbarProvider>
          </div>
        </ThemeProvider>
      </StyledEngineProvider>
    </LocalizationProvider>
  );
}

export default App;
