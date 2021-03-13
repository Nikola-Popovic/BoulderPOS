# Getting Started with Create React App

## Run with docker
In the terminal run :
```
docker-compose up --build
```
Once started and compiled, the client will run on 
`http://localhost:3000/`

If you makes edits, webpack will compile the changes and they will be available upon reloading.

## To run locally

Make sure `webpack-cli` is installed.

```
npm install webpack-cli -g
```
Then you can run the followin script : 
```npm start``

### Notes for a production build

Neither the docker nor the reverse proxy are really ready for a production build. Those would have to be changed 
with environnment variables and other considerations.




