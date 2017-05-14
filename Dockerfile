FROM microsoft/dotnet
MAINTAINER gamelinfabien@gmail.com
WORKDIR /app
COPY . .
EXPOSE 5000
ENTRYPOINT ["dotnet", "training.dll"]

#en ligne de cmd : docker build -t candidatemanagement
#                  docker run --name candidatemanagement -p 5000:80 -d candidatemanagement
