#Teatastic
Teatastic is a web application for buying and managing tea.

##Features
- Browse and purchase tea by brand, function, and type
- Add items to a cart and place an order
- Create an account and login to track orders and manage account details
- View order history and details
- Manage brands, functions, and teas through admin features

##Technologies
- ASP.NET Core 6.0
- Entity Framework Core
- Identity Framework
- Bootstrap 4
- Localization and Internationalization
- MailKit

##How to use
Clone the repository
Open the project in Visual Studio
Update the ConnectionString in the appsettings.json file to match your local database
Run migrations to create the database:
Copy code
dotnet ef database update
Run the application

##Note
Make sure you have the latest version of .NET Core SDK and Visual Studio installed
The app uses the MailKit library to send email, so you need to configure your own email server
Translate-Resx tool is not included in the solution, but it's a tool that you can use to generate the resource file for localization and internationalization.
You can find the tool here: https://github.com/tomakita/Translate-Resx

##Support
If you have any issues or questions, please feel free to reach out to me through the contact information provided.