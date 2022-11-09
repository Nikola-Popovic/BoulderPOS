# Project BoulderPOS (no longer supported)

## Running the application with Docker

### Configuration
The project uses Linux containers if you are on windows make sure to install WSL (Windows Subsystem for Linux) and to have Docker configured with WSL.

### Run the project
At the root folder, run `docker-compose up`.
This command will start the postgres database, the api, the reverse proxy and the frontend applications.

### Build and start the project
`docker-compose up --build`

#### Build services will composed
`docker-compose build --no-cache boulder-waiver boulder-pos`

### Debug a process
To debug an application inside a Docker container, Attach a debugger to the process.

Inside visual studio, `CTRL+ALT+P` will open the **Attach to Process** dialog box.

Select Docker (Linux Container) and find the target,


## Running without Docker

### Configuration 

+ Install Postgres 12
+ Configure the database and modify the Postgres ConnectionString in BoulderPOS.API's `appsettings.json` according to your configuration.
+ Inside **BoulderPOS.API** run the EntityFramework Core migrations. (look at Readme).
+ Start the API with IIS
+ Read the frontend application's readme for setup and installation.

## About 
This proposes a solution for the Point of Sale systems in climbing gyms.
One client application (boulderwaiver) allows the inscription of users along a series of security rules to follow. 
  This application uses webpack to compile static content (including a markdown file for Terms and Conditions (that would be later on easily updateable))
The other react application is used to manage the point of sale systems (products and categories), checkin users (when they come to the gym) and access the shop portion of the POS (to sell products, entries and subscriptions).
A rest api with integrations testing on every endpoint is present to handle the transactions to the postgresql database.
The whole project is bootstrapped by a docker orchestration file and nginx is used as a reverse proxy in different to allow the communication between the different docker images.

Even though this project had a lot of architectural challenges completed. This project is no longer maintained. A lot of the dependencies are outdated. 
If I were to start it again, I would remove webpack from the waiver app (as the interactions between Docker + webpack can be tidious to maintain) and use react's styled.components. Also a series of components should be developped to remove the outdated dependancies (such as react-phone-input).

## The good
+ The dockerisation and orchestration of every component
+ The integration tests for the API
+ A lot of key features already implemented (such as adding products and categories with icons, internationnalisation)
## The bad
+ Lot of room for improving the fold structures and code cleaness
+ Linting
## The next steps
+ Adding CI/CD pipelines with a script for the infrastructure
+ Adding camera support and photos for customers
+ Separating admin function in another applications
+ Adding Oath security to the application

