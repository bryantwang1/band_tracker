# _Band Tracker_

#### _A project for demonstrating knowledge of SQL Database management with C# for Epicodus that can hold data for bands and the venues they're playing at, 12/16/2016_

#### By _**Bryant Wang**_

## Setup/Installation Requirements

_To run this web app you need the Nancy framework for C# as well as a local SQL server._

1. Clone this repository
2. Open Windows PowerShell
3. Change your directory in PowerShell to the cloned project folder
4. Type `dnu restore` into Powershell and hit enter
5. Create the databases with the appropriate schema.
  * If you have Microsoft SQL Server Management Studio, select _File_ > _Open_ > _File_ and select `band_tracker.sql`, then execute it by clicking the `!Execute` button.
    * Do the same with `band_tracker_test.sql`
  * If you don't have MS SQL Server Management Studio, do this in SQLCMD:

          > CREATE DATABASE band_tracker;
          > GO
          > USE band_tracker;
          > GO
          > CREATE TABLE bands (id INT IDENTITY(1,1), name VARCHAR(255), description VARCHAR(255));
          > CREATE TABLE venues (id INT IDENTITY(1,1), name VARCHAR(255), location VARCHAR(255));
          > CREATE TABLE bands_venues (id INT IDENTITY(1,1), band_id INT, venue_id INT);
          > GO

  * Do the above for a database called band_tracker_test or just delete the test files if you don't want to bother;
6. Check the Startup.cs (also the Test cs files if you want to bother testing) file to make sure that the DBConfiguration ConnectionString's data source has the correct path for your local SQL server.
7. Type `dnx kestrel` into PowerShell and hit enter, the local server should now be running.
  * Type `dnx test` instead if you want to run the automated tests.
8. Open your preferred web browser and navigate to localhost:5004, the main page should appear
9. Go wild

## Technologies Used

_C#, Nancy, Razor, SQL by way of MS SQL Server Express and MS SQL Server Management Studio, edited in Atom_

### License

*GPL*

Copyright (c) 2016 **_Bryant Wang_**
