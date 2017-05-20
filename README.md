# Candidate-Management

<h1>ANDROID FOR CANDIDATE MANAGEMENT</h1></br></br>

Development of this android application (same as the the IOS application), in order to help humand ressource to manage their candidates.
I use Android studio.</br></br>


<h1>ENVIRONMENT SPECIFICATIONS</h1></br></br>
Here we'll implement the human ressource application in a specific environnement : Docker.
We'll introduce Docker and the different steps in order ton run correctly this application
with the database and server running efficiency.</br></br>

1- Why Docker ?</br></br>
2- All about Candidate-Management</br></br>
3- Prerequisites</br></br>
4- Dockerfile & Run application</br></br>

    <h3>Run and create database in a container</h3></br></br>
First of all, we have to create a database (here a mysql database) and run the database server in a container.</br>
We'll use the official mysql image from docker hub, so it's very siple to create a mysql container with just a docker run cmd :</br>
docker run --name=mysql-CM -e "MYSQL_ROOT_PASSWORD=pwd" -v /storage/docker/volume/mysqlCM:/var/lib/mysql mysql </br>
--name : I name my container mysql-CM</br>
-e     : I set up the mysql environmental variable password by 'pwd' </br>
-v     : I creat a volume, wich it's means that I mount volumes between my host machine and my container, so in case my container stops or crashes I will not lose my datas (very important in case of a database, only inconvenient : we lost portability) </br></br>
Now check if the container is alive : </br>
///// insert screenshot </br>
Now we have to create our database inside the container :</br></br>
docker exec -it mysql-CM mysql -u root -p</br>
You can creat it the way you want with mysql basics commands (I used a script.sql) </br>
Your database server is ready to use, now we have to run the api server. </br></br>
    <h3>Run API Server in an another container and access database<h3></br></br>
First,you have to publish your application before building its image</br>
Since it's done, you can create your image and run it in a container by building the dockerfile :</br>
//////////insert dockerfile image</br>
/////explanation of the dockerfiles lines </br>
command line in the right location : docker build -t plugincm .</br>
here I named my image plugincm, don't forget the dot at the end.</br>
check if the image is well created : docker images</br>
////insert screenshot </br>
And run the image in order to get the server running in a container :</br>
docker run -it -d --name plugincm -p 5000:5000 --link mysql-CM plugincm</br>
Here I named the container the same that my image (plugincm)</br>
-d running it on daemon mode</br>
-p 5000:5000 : Expose the port 5000 from my container to port 5000 of my host machine (to access it)</br>
--link : allow my container to access at my database container</br>
Now the API server is running in docker ! </br></br>



6- Conclusion</br></br>
