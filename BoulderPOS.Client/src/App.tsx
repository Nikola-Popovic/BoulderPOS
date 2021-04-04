import React from 'react';
import './App.css';
import NavBar from './components/navbar/navbar';
import { BrowserRouter as Router, Route, Switch } from 'react-router-dom';
import { SnackbarProvider } from 'notistack';
import Home from './pages/home';
import Shop from './pages/shop';
import CheckIn from './pages/checkin';
import Administration from './pages/administration';

function App() {
  return (
    <Router>
      <SnackbarProvider maxSnack={2}
        anchorOrigin={{
          vertical: 'bottom',
          horizontal: 'center'
        }}>
        <NavBar/>
        <Switch>
          <Route path='/' exact component={Home}/>
          <Route path='/shop' component={Shop}/>
          <Route path='/checkin' component={CheckIn}/>
          <Route path='/administration' component={Administration}/>
        </Switch>
      </SnackbarProvider>
    </Router>
  );
}

export default App;
