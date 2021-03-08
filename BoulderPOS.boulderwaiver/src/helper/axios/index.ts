import axios from 'axios';

/*
 You can use declared environment variable here with port as an example
 by modifying he .env file at the root of the project.
*/
// const port = process.env['API_PORT'] ? process.env['API_PORT'] : '8080';
const port = '81';

const toString =  (url : ReturnType<typeof baseUrl>) => {
    return url.protocol + '//' + url.hostname + ':' + url.port + '/api';
}

var baseUrl = (port : String) => ({
    protocol : window.location.protocol,
    hostname : 'localhost',
    port : port
})

const apiClient = axios.create({
    baseURL : toString(baseUrl(port)),
    timeout : 5000
});

const { get, post } = apiClient;

export { get, post }