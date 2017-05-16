#lancer la bdd dans un container : sudo docker run --name mysql  -v /storage/docker/mysql-datadir:/var/lib/mysql/CandidateManagement \
#                -e MYSQL_ROOT_PASSWORD=fabien \
#                -e MYSQL_DB=CandidateManagement -d -p 6604:3306 mysql


#Création de l'image du server applicatif
FROM microsoft/dotnet
MAINTAINER gamelinfabien@gmail.com
ENV ASPNETCORE_URLS="0.0.0.0:5000"
WORKDIR /dotnetapp
COPY . .
EXPOSE 5000
RUN dotnet restore
ENTRYPOINT ["dotnet", "run", "--server.urls", "http://*:5000"]


#en ligne de cmd : docker build -t candidatemanagement
#                 docker run -it -d --name plugincm -p 5000:5000 plugincm

#pb :
#le container de s'arrête tout seul (pour l'app)
#manager la bdd



