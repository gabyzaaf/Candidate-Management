#lancer la bdd dans un container : sudo docker run --name mysql  -v /storage/docker/mysql-datadir:/var/lib/mysql/CandidateManagement \
#                -e MYSQL_ROOT_PASSWORD=fabien \
#                -e MYSQL_DB=CandidateManagement -d -p 6604:3306 mysql


#Création de l'image du server applicatif
FROM microsoft/dotnet
MAINTAINER gamelinfabien@gmail.com
WORKDIR /app
COPY . .
EXPOSE 5000
ENTRYPOINT ["dotnet", "training.dll"]


#en ligne de cmd : docker build -t candidatemanagement
#                  docker run --name candidatemanagement --link mysql -p 5000:80 -d candidatemanagement
#pb :
#le container de s'arrête tout seul (pour l'app)
#manager la bdd



