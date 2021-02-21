# EIHDemo

# EIHTestPortal
EIHTestportal is a web application developed in .net MVC. It is thin UI. 

Authentication is a simple process of authenticating user and password stored in database. Passwords are stored as hash+salt to provide better security aspect. 

Controller are handling request and response made by UI.
Views are different screens presenting Add,Edit and Delete contact funcationlity

Helpers contains various classes to provide functionality such as App Database communicaiton, Logging, data parser and salt generation.
Portal calls api exposed by EIHTest to perform above operations

Portal uses AppLog and User collections to store logs and user information


 
# EIHTest
EIHTest is a web api developed on .net core platform. Since .net core platform provides inbuilt funcationlity of DI, it makes developer's life easy.
Web api uses IUserService, IContactService and ILog interfaces to build logic. 

IUserService provides authorization implementation for API. Client first authenticates itself. On success receives token which it has to pass to all api call as bearer token. Authentication is simple based on userid and password. However at production level it should be handled by api gateway. 

# Database
Mongodb


Enviornment
Deployed API is private and can't be called from outside.
Solutions are developed in Microsoft visual studio community 2019 version. 
Solution is deployed at gcp and same build is also uploded github 

http://34.68.111.107/


Mongo 3.2 server is used

