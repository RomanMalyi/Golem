
FROM node:13.3.0 AS compile-image

RUN npm install -g @angular/cli

WORKDIR /opt/ng
COPY Golem-Client/package.json ./
COPY Golem-Client/package-lock.json ./
COPY Golem-Client/nginx-custom.conf ./
RUN npm install

ENV PATH="./node_modules/.bin:$PATH" 

COPY . ./
WORKDIR "/opt/ng/Golem-Client/"
RUN ng build

FROM nginx
COPY --from=compile-image /opt/ng/Golem-Client/dist/Golem-Client /usr/share/nginx/html/
COPY --from=compile-image /opt/ng/nginx-custom.conf /etc/nginx/conf.d/default.conf