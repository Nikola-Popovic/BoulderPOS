FROM node:current-buster-slim as build

# set working directory
WORKDIR /app

# add `/app/node_modules/.bin` to $PATH
ENV PATH /app/node_modules/.bin:$PATH
EXPOSE 3001

COPY package*.json ./
COPY webpack* ./
COPY tsconfig.json ./
COPY babel.config.js ./

# install app dependencies
RUN npm install -g webpack webpack-cli --silent
RUN npm install react-scripts@3.4.1 -g --silent
RUN npm install --silent

COPY src /app/src
COPY public /app/public
RUN npm run build

# Nginx relevante setup
FROM nginx:stable-alpine as production
COPY --from=build /app/dist /usr/share/nginx/html 

# Serve the application
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]