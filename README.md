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

you have to publish your application before put it in a container</br>
Since it's done, you can create your image and run it in a container by building the dockerfile :</br>
command line in the right location : docker build -t nameofyourimage </br>
Now the API server is running in docker ! </br></br>

Next step is to run MySQL Server in an other container : </br>

sudo docker run -it -v /data/:/var/lib/mysql --name mysql-server -p 3306:3306 -e MYSQL_ROOT_PASSWORD=fabien -e MYSqL_DATABASE=CandidateManagement centurylink/mysql</br></br>

How to communicate between containers ?</br>

6- Conclusion</br></br>
