FROM microsoft/dotnet
MAINTAINER gamelinfabien@gmail.com
ENV ASPNETCORE_URLS="0.0.0.0:5000"
WORKDIR /dotnetapp
COPY . .
EXPOSE 5000
RUN dotnet restore
ENTRYPOINT ["dotnet", "run", "--server.urls", "http://*:5000"]




