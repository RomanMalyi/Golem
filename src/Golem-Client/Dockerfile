
FROM node:13.3.0 AS compile-image

RUN npm install -g @angular/cli

WORKDIR /opt/ng
COPY Golem-Client/package.json ./
COPY Golem-Client/package-lock.json ./
RUN npm install

ENV PATH="./node_modules/.bin:$PATH" 

COPY . ./
WORKDIR "/opt/ng/Golem-Client/"
RUN ng build --prod

FROM nginx
COPY --from=compile-image /opt/ng/Golem-Client/dist/Golem-Client /usr/share/nginx/html