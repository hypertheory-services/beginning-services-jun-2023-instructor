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