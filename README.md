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
