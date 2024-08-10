<h2>Centerfield Coffee API</h2>

To execute the application run the following command in a terminal window. If you don't have NetCore runtime installed you can download it here: https://dotnet.microsoft.com/en-us/download
It must be run inside the CenterfieldCoffee folder inside the root folder.

```
dotnet run
```

## The application uses .NetCore controllers for the api endpoints and code first EntityFramework classes backed by a sqllite database. The first time the application runs the database is seeded using the following function:

```
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
      var now = DateTime.Today;
      modelBuilder.Entity<Store>().HasData(
          new Store
          {
              Name = "Store 1",
              opening_hour = 6,
              opening_minute = 30,
              closing_hour = 18,
              closing_minute = 0,
          },
          new Store
          {
              Name = "Store 2",
              opening_hour = 6,
              opening_minute = 30,
              closing_hour = 21,
              closing_minute = 0,
          },
          new Store
          {
              Name = "Store 4",
              opening_hour = 16,
              opening_minute = 30,
              closing_hour = 20,
              closing_minute = 0,
          }
      );
  }
```

## To retrieve a list coffee shops call the following api endpoint: (Note you made need to adjust the port based on what dotnet runs on your local machine)

http://localhost:5068/api/stores

The endpoint supports an optional currtime querystring parameter. If it's not passed the endpoint will return the is_open field based on the current time, if it is passed then the field is populated based on the passed in time parameter in utc format. The following sample link sets the current time to 2am so all of the sample coffee shops should be closed:

http://localhost:5068/api/stores?currtime=2024-08-10T02:38:35.417Z

You can retrieve ISO 8601 dates for sample code tests here: https://www.timestamp-converter.com/

## To retrieve a single store you can pass the id of the store to the following api endpoint, for example:

https://localhost:7125/api/stores/b064ca2b-d10c-4a4d-891e-01f3895d9f35

The id is in the form of a guid. The same optional currtime can be passed as a query string parameter similar to the default get api.

## To create a new coffee shop store entity, you can do a post using a store entity as a parameter. The store hours and minutes are represented as separate fields for easier parsing and processing of store times. The folowing store opens at 6:30am and closes at 9pm:

```
 {
     Name = "New Store 1",
     opening_hour = 6,
     opening_minute = 30,
     closing_hour = 21,
     closing_minute = 0,
 },
```

## Notes: Steps to make production worthy
* Do a full security threat level scan based on where the api will be hosted including locking down access using authorization tokens, protecting against SQL injection attacks etc.
* Optimize the database fields to reduce the storage types along with query optimizations based on the complexity of a fully fleshed out data model
* Add location data fields including latitude and longitude for location based searches
* Add fully fleshed out testing across multiple timezones
* Conduct stress tests for the api endpoints based on expected usage patterns.
* Fully document API endpoints by enhancing the automatic swagger generated documentation
* Normally I separate my models from my controllers in separate projects for standard separation of concerns and to facilitate easier testing.

## Notes: Personal Experience level

I've been working with .Net and C# for 18+ years including creating hundreds of endpoints for a variety of industries including finance, retail, food services and the insurance industry.
