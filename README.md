## Pre-requisites:

***Visual Studio 2019***

***.net core 3.1***

***.net Framework 4.7.2***


## Objective:
To create a simple WPF application (using dotnet core) to connect with Informix and to perform some tasks (like user account portal).
User/Admin account portal, where Admin and Users can view, update, delete their information.
            Two User System -**Admin** and **User**.

## Application Functionality:
***1) Public User Registration Page (Registration Page)***- by default user will be "Normal User" (and from public registration page one cannot register as "Admin")

**Login Page (MainWindow Page)** - Once registered one can login into the system (only valid users can).

***a. If (Admin)***

***AllUsersDetails Page will open*** - which will have all the user's details. From this page Admin can perform:

***- Add new user*** - AddNewUserFromAdmin page. (this is different from public registration page)- from here a admin user can add another admin user.

***- Delete*** any existing user.

***- Update details of any existing user.*** (Once click on Refresh button, information will get updated in UI as well).

***b. If (Normal User)***
***UserProfile Page will open*** - From here a user can edit his own details (limited). User won't be able to see other user's details.

***2) Logout*** : Once click on the logout button user will be redirected to the login page.

