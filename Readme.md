# Project BoulderPOS

## Running the application with Docker

### Configuration
The project uses Linux containers if you are on windows make sure to install WSL (Windows Subsystem for Linux) and to have Docker configured with WSL.

### Run the project
At the root folder, run `docker-compose up`.
This command will start the postgres database, the api, the reverse proxy and the frontend applications.

### Build and start the project
`docker-compose up --build`

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
