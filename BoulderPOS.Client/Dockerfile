FROM node:current-buster-slim as build

# set working directory
WORKDIR /app

# add `/app/node_modules/.bin` to $PATH
ENV PATH /app/node_modules/.bin:$PATH
EXPOSE 3000

COPY package*.json ./

# install app dependencies
RUN npm install --silent
RUN npm install react-scripts@3.4.1 -g --silent

COPY tsconfig.json ./
COPY src /app/src
COPY public /app/public

RUN npm run build

# Nginx relevante setup
FROM nginx:stable-alpine as production
COPY --from=build /app/build /usr/share/nginx/html 
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
