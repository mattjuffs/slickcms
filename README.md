# SlickCMS

A Blog/CMS inspired by WordPress, built using ASP.NET MVC and SQL Server

## Releases

v1.0 Coming soon!

## Requirements

IIS with support for .NET Core 3.0 required. SQL Server or Azure SQL required for the database.

## Installation

1. Checkout the repo
2. [Setup the database using the files provided](/Database)
3. Run `SlickCMS.Web`
4. Update the connection string created (`~ConnectionStrings\slickcms.txt`) to point to your database
5. Re-run `SlickCMS.Web`
6. You should get the homepage as per the [Demo](http://slickcms.azurewebsites.net/)
7. You are now ready to begin!

## [Milestones](https://github.com/mattjuffs/slickcms/milestones)

1. [Beta](https://github.com/mattjuffs/slickcms/milestone/1) - First release of SlickCMS with the core features required to run a Blog.
2. [Integrations](https://github.com/mattjuffs/slickcms/milestone/2) - Integrating with third parties to further enhance SlickCMS.
3. [New Features](https://github.com/mattjuffs/slickcms/milestone/3)

## Links

* [Demo](http://slickcms.azurewebsites.net/) - _hosted on Azure_
* [Branching](/BRANCHING.md)
* [Code of Conduct](/CODE_OF_CONDUCT.md)
* [Contributing](/CONTRIBUTING.md)
* [License](/LICENSE) - _slickcms is licensed under the MIT License_
