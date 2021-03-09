import React from 'react';
import CssBaseline from '@material-ui/core/CssBaseline';
import { SnackbarProvider } from 'notistack';
import { createMuiTheme, ThemeProvider } from '@material-ui/core/styles';
import { BrowserRouter as Router, Route, Switch} from 'react-router-dom';
import GettingStarted from './components/GettingStarted';
import Waiver from './components/Waiver';
import Signup from './components/Signup';

import './App.css';
import Header from './components/Header';

function App() {
  const theme = createMuiTheme({
    palette: {
      primary: {
        main : '#4791db'
      }
    }
  });

  return (
    <ThemeProvider theme={theme}>
        <div className="App">
          <CssBaseline />
          <SnackbarProvider maxSnack={1}>
            <Header />
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
  );
}

export default App;
