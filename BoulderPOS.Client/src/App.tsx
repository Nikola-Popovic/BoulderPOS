import React from 'react';
import './App.css';
import NavBar from './components/navbar/navbar';
import { BrowserRouter as Router, Route, Switch } from 'react-router-dom';
import Home from './pages/home';
import Shop from './pages/shop';
import CheckIn from './pages/checkin';
import Administration from './pages/administration';
import CreateProductForm from './components/administration/newProduct/CreateProductForm';
import CreateCategoryForm from './components/administration/newCategory/CreateCategoryForm';

function App() {
  return (
    <Router>
      <NavBar/>
      <Switch>
        <Route path='/' exact component={Home}/>
        <Route path='/shop' component={Shop}/>
        <Route path='/checkin' component={CheckIn}/>
        <Route path='/administration' component={Administration}/>
        <Route path='/addproduct' component={CreateProductForm}/>
        <Route path='/addproductcategory' component={CreateCategoryForm}/>
      </Switch>
    </Router>
  );
}

export default App;
