import React, { Component } from 'react'
import { Client } from '../data/Client';
import "./checkin.css";

class CheckIn extends Component {

    filterRows() {

    }

    render() {
        return (
        <div className="parent">
            <div className="checkin">
            <input className="searchBar" type="text" id="searchBar" placeholder="Rechercher..."/>
            <div className="scrollable">
            <table className="clientTable">
            <tr className="header">
                <th>ID</th>
                <th>Prénom</th>
                <th>Nom</th>
                <th>No téléphone</th>
            </tr>
            {this.createTableRowsWithFilter()}
            </table>
            </div>
            </div>
        </div>
    )}

    createTableRowsWithFilter() {
        //get client database
        let clients = TEST_CLIENTS as Client[];
        let displayClients = [];

        for(let client of clients) {
            // if row matches current filter
            displayClients.push(
            <tr className="clientRow">
                <th>{client.id}</th>
                <th>{client.firstName}</th>
                <th>{client.lastName}</th>
                <th>{client.phoneNumber}</th>
            </tr>
            )
        }

        return displayClients;
    }
}

export default CheckIn



const TEST_CLIENTS = [
    {
        id: 1,
        firstName: "Jean",
        lastName: "Doe",
        phoneNumber: "555-523-4321",
        birthDate: "01-04-1980",
        newletterSubscription: false,
    },
    {
        id: 2,
        firstName: "Jeanne",
        lastName: "Eod",
        phoneNumber: "525-523-0632",
        birthDate: "07-04-1982",
        newletterSubscription: false,
    },
    {
        id: 3,
        firstName: "Jacques",
        lastName: "Tremblay",
        phoneNumber: "555-523-4321",
        birthDate: "11-04-1999",
        newletterSubscription: true,
    },
    {
        id: 4,
        firstName: "Mimi",
        lastName: "Myrtille",
        phoneNumber: "555-523-4321",
        birthDate: "01-04-1980",
        newletterSubscription: false,
    },
    {
        id: 5,
        firstName: "Ano",
        lastName: "Nymat",
        phoneNumber: "111-111-1111",
        birthDate: "01-01-2000",
        newletterSubscription: false,
    },
    {
        id: 6,
        firstName: "Pourquoi",
        lastName: "Pas",
        phoneNumber: "418-444-4419",
        birthDate: "01-04-1980",
        newletterSubscription: false,
    },
    {
        id: 1,
        firstName: "Jean",
        lastName: "Doe",
        phoneNumber: "555-523-4321",
        birthDate: "01-04-1980",
        newletterSubscription: false,
    },
    {
        id: 2,
        firstName: "Jeanne",
        lastName: "Eod",
        phoneNumber: "525-523-0632",
        birthDate: "07-04-1982",
        newletterSubscription: false,
    },
    {
        id: 3,
        firstName: "Jacques",
        lastName: "Tremblay",
        phoneNumber: "555-523-4321",
        birthDate: "11-04-1999",
        newletterSubscription: true,
    },
    {
        id: 4,
        firstName: "Mimi",
        lastName: "Myrtille",
        phoneNumber: "555-523-4321",
        birthDate: "01-04-1980",
        newletterSubscription: false,
    },
    {
        id: 5,
        firstName: "Ano",
        lastName: "Nymat",
        phoneNumber: "111-111-1111",
        birthDate: "01-01-2000",
        newletterSubscription: false,
    },
    {
        id: 6,
        firstName: "Pourquoi",
        lastName: "Pas",
        phoneNumber: "418-444-4419",
        birthDate: "01-04-1980",
        newletterSubscription: false,
    },
    {
        id: 1,
        firstName: "Jean",
        lastName: "Doe",
        phoneNumber: "555-523-4321",
        birthDate: "01-04-1980",
        newletterSubscription: false,
    },
    {
        id: 2,
        firstName: "Jeanne",
        lastName: "Eod",
        phoneNumber: "525-523-0632",
        birthDate: "07-04-1982",
        newletterSubscription: false,
    },
    {
        id: 3,
        firstName: "Jacques",
        lastName: "Tremblay",
        phoneNumber: "555-523-4321",
        birthDate: "11-04-1999",
        newletterSubscription: true,
    },
    {
        id: 4,
        firstName: "Mimi",
        lastName: "Myrtille",
        phoneNumber: "555-523-4321",
        birthDate: "01-04-1980",
        newletterSubscription: false,
    },
    {
        id: 5,
        firstName: "Ano",
        lastName: "Nymat",
        phoneNumber: "111-111-1111",
        birthDate: "01-01-2000",
        newletterSubscription: false,
    },
    {
        id: 6,
        firstName: "Pourquoi",
        lastName: "Pas",
        phoneNumber: "418-444-4419",
        birthDate: "01-04-1980",
        newletterSubscription: false,
    },

]