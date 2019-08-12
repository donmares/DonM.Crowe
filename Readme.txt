This solution was developed using Visual Studio Enterprise 2019 Version 16.2.0
Microsoft.NET Framework Version 4.7.03062
.NET Core 2.2

The application works with a simple file for the repo. This could be easily replaced with a database using the same interface. Running the console application with no parameters will execute an API-Get operation and list all greetings. Running the console application with any number of parameters will post those to the file that will be returned on the next get operation. Similarly, you could run postman or any other client to access the API and do get or post operations. Finally there are integration tests for the get and post successful operations. 

The application project consists of a service that contains all of the code to get and save greetings. The Infrastructure project contains the model and mechanism for reading and writing to the file. 