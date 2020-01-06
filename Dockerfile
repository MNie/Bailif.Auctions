FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim

ENV CHROME_DRIVER_VERSION 79.0.3945.36

RUN wget -q -O - https://dl-ssl.google.com/linux/linux_signing_key.pub | apt-key add - \
      && sh -c 'echo "deb [arch=amd64] http://dl.google.com/linux/chrome/deb/ stable main" >> /etc/apt/sources.list.d/google.list' \
      && apt-get update \
      && apt-get install xvfb unzip google-chrome-stable -y \
      && wget https://chromedriver.storage.googleapis.com/$CHROME_DRIVER_VERSION/chromedriver_linux64.zip \
      && unzip -d /usr/local/bin chromedriver_linux64.zip

COPY /deploy /
WORKDIR /Server
EXPOSE 8085
ENTRYPOINT [ "dotnet", "Server.dll" ]
