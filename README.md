# HealthCatalyst Assessment

The code within this repository represents a technical assessment for Health Catalyst

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes. 

### Prerequisites

- Internet access (for downloading the code and the dependencies)
- Git
- Visual Studio (preferably v2017 or higher)
- .Net Framework 4.7.1 
- Powershell v2+


### Installing

1. Using git, clone the code locally.
2. If the machine does not have the pre-requisites installed, install them at this time.
3. Start Visual Studio and open the solution by browsing to the root code folder and selecting the .SLN file. 
4. Run the application in Visual Studio.  The application will automatically start and will seed the empty database.
5. Starting the application will open the browser to the API Help documentation.  Seeing the Help page indicates that the application is working successfully.
6. For an additional test, open up a browser window and navigate to the following link: http://localhost:50647/team/5.  A JSON response representing a person should return to the browser.


## Running the tests

- In Visual Studio, at the menu, select TEST -> RUN -> All Tests

The unit test explorer window should display running all 22 tests and all 22 tests should pass.  If for any reason the Unit Tests do not work, enable the VS settings as displayed with the instructions at https://github.com/fixie/fixie/issues/193 to allow Visual Studio to find the Test adapters.


## Built With

- [ASP.Net Web API] - The web framework used
- [Nuget] - Dependency Management
- [Microsoft Unity] - A Dependency Injection library
- [AutoMapper] - A library for mapping between domain objects and DTO
- [MS Test] - A library for automated unit testing
- [Moq] - A library for creating mock dependencies
- [Log4net] - A logging framework
- [LocalDB] - A lightweight database
- [Entity Framework 6] - A code-first ORM framework 

## Target Objectives

Summarizing the assessment targets as well as providing a status.

### Business Requirements

- Provide a REST endpoint that takes a search input then returns a list of people whose first or last name matches what the input (including at least name, address, age, and interests).  
  - In a follow up with Justin, the understanding was that a string value would be provided and that string value could be found in the first and/or last name.  As such, the single value constraint made a GET request more plausible and RESTful.  
  - As well, there are two types of searches.  The default is to allow search by partial strings.  For example, if a person's name is "Ted Mosby", this name would be found when entering a search with the letters "sby". 
    - If a name must be matched in whole, set the **searchStrictMatching** config setting to true. 
- Seed the application with some users.
  - This solution comes with an empty LocalDB instance located in the APP_DATA folder.  It is recommended that you view and confirm the db is empty before running this application.  When the application starts, the Roster database will be created and seeded accordingly.
- Provide a REST endpoint that allows addition of new users.
  - This was completed.  In principle, a Level 2 RESTful definition was provided (see https://developers.redhat.com/blog/2017/09/13/know-how-restful-your-api-is-an-overview-of-the-richardson-maturity-model/ for the REST Maturity model definition).  
  - Also included, two additional endpoints to aid the assessment evaluation.  Specifically, an endpoint to get an individual by ID (http://localhost:50647/team/{id}), and an endpoint to retrieve the entire database (http://localhost:50647/team).  These URLs are GET requests, and therefore can be typed directly into the browser.
- Provide documentation to interact with the REST endpoints.
  - The REST API specification will display automatically when the application starts.  This page lists all of the endpoints.  Clicking on the endpoints will drill down further with the request and response specification and examples for how to make requests and what type of response is received.  Further drilldown at this layer will provide a specific description of each property for each object.
 
### Technical Requirements

- Use Entity Framework Code First to talk to the database.
  - Entity Framework Code First was leveraged accordingly.
- Add automated testing for appropriate parts of the application.
  - There are currently 20+ unit tests with the code that can be found by expanding the TEST folder under the solution.  Moq was leveraged to mock dependencies and validate calls.  
 
### Extra Credit Ideas

- Write PowerShell scripts to add or search for particular users.
  - The Powershell script can be downloaded from the REST API specification (help) page.  
    - This script is runnable as is.  It will automatically add a new person AND it will prompt to perform a search on a person.  
    - The script will display output to the console to provide clear feedback on the results.  
    - Exception handling has also been included. 
- Simulate latency in the API call.
  - Latency simulation has also been included in the solution.  
  - Latency is disabled by default.  
    - To enable latency, set the **latencyMS** config setting.  
	- Review the config details in the settings section below.

## Project Notes

### Configuration Settings

The web.config file is where default settings are stored. These settings can be changed to effect a behavioral change in the application. There are two specific settings of note that may be necessary for testing.

1. latencyMS
   - The value is measured in millisecond units. 
   - The default value is set to zero. A value of 0 or less disables the latency feature.
   - A value of 1ms or greater enables latency
     - Latency will vary randomly within the range of 0 to the latencyMS value.
	 
2. searchStrictMatching
   - The default value is set to **False**.
   - Setting this setting to **False** enables a loose search on first or last name.  Specifically, any character match will return that matching result. 
   - To switch to strict matching criterion - where the search has to match an exact name - set this setting to **True** 

### Architecture

The application architecture implements a Hexagonal architecture approach, which is designed to achieve low technical debt and easy to maintain. 
In short, the logical organization and separation of code enabled best practices to be employed, specifically Inversion of Control, Dependency Injection, SOLID principles, and Testability.  
In turn, changes to the code can be made quickly with a minimized risk footprint. As well, the employment of interfaces at the boundaries enables components to be switched out with dependency injection.  
The requirement did not necessarily require this amount of thought, but the requirement to craft a representation of my work motivated such consideration.

### Action Filters

Action Filters are applied for cross-cutting concerns at the request/response boundary context.  This solution employees several Action Filters that may not be apparent, but necessary for code review.
Below is a list of the major Action Filters that were created for this API.

1. ActionExceptionFilter - A centralized exception handler to handle and return properly formed exceptions.
2. ApiNotFoundFilter - When a request is made for data and that data is not found, a Level 2 REST API should return a **404 NOT FOUND** Http response.  This filter reviews the response object and, if the object is NULL, it returns the 404 Http message.
3. DBTransactionFilter - This filter enables holistic database transaction management.  This is particularly important when multiple WRITE database events are occurring to allow for a full/complete rollback in the event of an error.  This particular exercise is light and did not require transaction management, but its a good practice to implement anyway.
4. ModelValidatorFilter - Enforces that any data received is immediately validated according to the data rules annotated on the body's class.
5. SimulatedLatency - Simulates latency at the request/response boundary context.

### Security

Security was initially included in this design.  It was disabled after testing the deployment due to concerns that this was the most complicated area of configuration and could lead to an unpredictable experience.  
Rather, the assumption that is being made with this implementation is that Integrated Security (Windows security) would be used and enforced at the App Pool layer in a typical implementation.

## Author

- Luis Reyes 




