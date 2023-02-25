# Backend for Task Manager app

This is a simple Web Api which is a backend for Task Manager app.

This Web Api allows users to perform basic CRUD operations on their tasks.

Only users with valid access token are allowed to perform this operations. To create an access token user needs to provide his credentials. If user does not have an account, he needs to register. Created token is valid for 15 minutes, after that user needs to login again or he can create a new access token by providing the refresh token assigned to his account.

In case user wants to receive specific task, update it or delete, Api verifies if he is an owner/creator of this task. If not, Api returns a 403 Forbidden status code.

# Technologies

Project is build with usage of .Net 6.0

I am using Entity Framework Core to access the data. 
