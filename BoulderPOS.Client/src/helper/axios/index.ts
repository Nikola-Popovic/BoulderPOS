import axios from 'axios';

/*
 You can use declared environment variable here with port as an example
 by modifying he .env file at the root of the project.
*/
// const port = process.env['API_PORT'] ? process.env['API_PORT'] : '8080';

// Ideally this would be an environment variable
const API_URL = 'localhost/api'

const toString =  (url : ReturnType<typeof baseUrl>) => {
    return url.protocol + '//' + API_URL;
}

const baseUrl = (url : string) => ({
    protocol : window.location.protocol,
    url : url
})

const apiClient = axios.create({
    baseURL : toString(baseUrl(API_URL)),
    timeout : 5000
});

const { get, post, put, delete : destroy } = apiClient;

export { get, post, put, destroy }