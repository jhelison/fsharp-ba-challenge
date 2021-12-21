FROM mcr.microsoft.com/dotnet/sdk:6.0.100

RUN apt-get update && apt-get install -y

ADD ./ /app
WORKDIR /app

RUN dotnet restore

CMD sleep infinity
