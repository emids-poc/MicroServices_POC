# ETCMeetup11_MicroServices
ETC Meetup micro-services with Kafta 

Configuration Steps :
================================================================
1. Put the kafka_2.12-2.0.0 folder in proper location.
2. Open the "startkafka.bat" and "startzookeper.bat" file in notepad/notepad++
3. The same location we need to give in the "startkafka.bat" and "startzookeper.bat" file just after the cd command.
4. After that Save and close "startkafka.bat" and "startzookeper.bat" file.
5. Run "startkafka.bat" and "startzookeper.bat" file by double click on the same.
6. Create two separate Database in sql. 
7. Provide one datebase details in the web.config file of "DemoMicroservices". This project will be first entry point. 
   By this project user will insert the data to the database.
8. Please provide the second database details in the web.config file of "UserProjectConsumer". This project will be responsible for taking the 
   record from kafka and insert the same into the second database.
    
   note :
         1.no need to create entities into the database. Because in this POC we used Entity Framework Database first approach. 
           we can use separate ORM as well
         2. In this POC we used kafka_2. We can also use RabitMQ or other massaging queue services.	