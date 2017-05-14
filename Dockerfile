FROM microsoft/dotnet
MAINTAINER gamelinfabien@gmail.com
WORKDIR /app
COPY . .
RUN ls
RUN dotnet restore
EXPOSE 5000
RUN dotnet run
