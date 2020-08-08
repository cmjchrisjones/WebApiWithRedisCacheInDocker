A quick solution and example of a Web API which calls a 3rd party service (in this case [ISBNDB](https://isbndb.com/apidocs/v2)) which will read in a CSV file of ISBN numbers, and get further information about the book, either as a JSON output or as another CSV file.

This project also has Redis to cache results, so on successful query, the result is stored so when searched for again it will check Redis to save flooding requests into the ISBNDB API, and Redis Commander which gives a nice little GUI over the Redis DB so you can see it!

## Pre-requisites

Docker

## Instructions

- First, open and edit the docker-compose file, and edit line 30

    ```yaml
   - ApiKey= #update this EG - ApiKey=mySecretPassword
     ```

     so that you provide the API key for the ISBNDB service

- Next, from a command line, run `docker-compose up --build` or if you'd prefer it to run silently in the background (EG no logs) pass in the `-d` for detached mode, like so `docker-compose up --build -d`

- Navigate to the API's Swagger interface over at [http://localhost:8086](http://localhost:8086)

- If you want to check out the Redis Cache, head on over to Redis Commander at [http://localhost:8085](http://localhost:8085)

Enjoy!


## Breakdown of the projects and what they do!

### API

This is just a relatively simple web api written in ASP.NET Core 3.1. 

It has a simple controller named `UploadBookController` which exposes 2 endpoints called `uploadAndReturnJSON` and `uploadAndReturnCSVFile`.

The virtual paths for the API routes are defined by using the Route decorator attibute, eg

```csharp
[HttpPost]
[Route("/uploadAndReturnJSON")]
public async Task<IActionResult> Post(IFormFile file)
{
    // Code removed for brevity
}
```

The only other thing I added was Swagger/Swashbuckle to give a nice i linterface to interact/test with - the configuration can be found in [Startup.cs](API/Startup.cs)

### Console

When I first started prototyping - I had a hardcoded list of strings as the ISBN's for us to test/loop through.

This was initally where all of the logic lived. After I was able to successfully make API calls to the 3rd party endpoint and deserialize and manipulate the data into how it was needed, I moved the core logic out from here into its own seperate class library (BookProcessor) so it could be shared between the console app and the Web API.

### Book Processor

This is where the crux of the functionality lives and I'll break this down file by file

- [ProcessBooks.cs](BookProcessor/ProcessBooks.cs):
  This is where we start. In both WebApi and Console apps, we can call into this using `await new BookProcessor.ProcessBooks().ByISBNsAsync(isbns, Environment.GetEnvironmentVariable("ApiKey"));`

  Inside this `ByISBNsAsync` method, we take in 2 parameters, the list of ISBN's we want to search for, and the API key for the 3rd party service.

  This method returns a Tuple, of 2 lists, those of the books we've found and got the data we wanted, and a list of string where we can add the ISBNs that were not found.

  We then loop over them using a foreach loop, and for every ISBN, we'll remove any hyphens, then we'll call a localised method called `TryGetFromCache`.

  If we get a result from our local Redis cache, we'll return that and serialize that into our own custom class of `OutputDetails` and Format it accordingly.

  If the call to `TryGetFromCache` returns a null result, then we'll go and call the API, and if we get a result back, We'll add it to our cache by calling `AddToCache`. If we don't get a result from the API, we'll add the ISBN into the list of `NotFounds`

  - TryGetFromCache - This is where we connect up to our local redis database, and see if we already have a key with the ISBN in there, if we do, we'll just return the value from there.

- [IsbnDb.cs](BookProcessor/IsbnDb.cs)
  This contains the logic for calling the 3rd Party API. First it'll check if an ISBN has been supplied, then also checks if an ApiKey has been supplied. If both of these conditions are met, we're OK to go ahead and invoke a call to the API, and if the response returns a 2xx status code, we'll return it as the JSON string to the caller.

- [Redis](BookProcessor/Redis.cs)
  This handle the query/updating of Redis - We'll connect to it, see if a given key (aka ISBN) is present, if it is, we'll return the value, otherwise we'll return an empty string

- [SerializeBook](BookProcessor/SerializeBook.cs)
  This is where we serialize the JSON text that we received from Redis/API to .NET POCO's (Plain old C#/CLR Object).

- [FormatForOutput](BookProcessor/FormatForOutput.cs)
  First we have a safety check just to make sure the book details are not null, if they are, we exit!

  Because the requirement was to output results as CSV, I came across a bit of a consistency problem with the results that were being returned for books that had multiple authors. For example, the authors property returns an array, but I had 3 examples like so:
    - `authors: [ "James", "Patterson", "Maxine", "Peacock"]
    - `authors: [ "James Patterson"]
    - `authors: [ "Dan Brown" ]
  Now as you can probably guess, those commas in the author arrays kept breaking the formatting, so I decided the easiest/quickest thing to do was just to loop over the authors and concatenate the strings and removing all the commas!

  Now that the authors were tidied, I could add these results into our very own custom POCO of `OutputDetails`

  Hopefully there is some decent explanations in here! :)