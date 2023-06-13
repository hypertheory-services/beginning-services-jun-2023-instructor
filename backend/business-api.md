# Business API

- Introduce APIs
    - In Particular, "Minimal APIs"
- Developer Testing
    - Testing from the "Outside In"
- This will be updated later in some discussion about CI/CD


## /clock

GET /clock

```json
{
    "open": true,
    "nextOpenTime": null
}

```

```json
{
    "open": false,
    "nextOpenTime": "6-13-2023 9:00 AM"
}

```

Open 9-5, Monday-Friday.


If we are open, return true with no timenext open

If we are closed, return false with the opening time of the next business day.


## integration tests
The most important. Test it from the POV of the consumer of your API. Makes HTTP Requests. Fakes attached resources.

## Unit Tests

