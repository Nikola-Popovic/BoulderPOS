FROM node:14.14.0-buster-slim

# set working directory
WORKDIR /app

# add `/app/node_modules/.bin` to $PATH
ENV PATH /app/node_modules/.bin:$PATH
EXPOSE 3000

COPY package*.json ./
COPY webpack* ./

# install app dependencies
RUN npm install --silent
RUN npm install -g webpack webpack-cli --silent
RUN npm install react-scripts@3.4.1 -g --silent

COPY tsconfig.json ./
COPY src /app/src

CMD ["npm", "run", "docker"]