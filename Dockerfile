FROM mcr.microsoft.com/dotnet/sdk:3.1.414

RUN apt-get update && apt-get install -y

ADD ./ /app
WORKDIR /app

RUN dotnet restore

CMD sleep infinity
