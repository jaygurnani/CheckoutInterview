# Checkout Interview - Jay Gurnani

## How to run

Navigate to the `CheckoutInterview` folder and run the following in a command line

```
dotnet run
```

Navigate to: `https://localhost:5001/swagger/`
And you should be able to make a request via the Swagger UI
Alternatively you import the Postman file `Checkout Interview.postman_collection.json` to test the requests

To run the unit test run in a command line
```
dotnet test
```

## Assumptions Made
- Interaction with the API is done by `merchantId`. With this flow it means the merchant always get a static URL to use.
- A cursor is returned so that the merchant can reconcillate the existing data. `Null` is returned for the cursor if we are on the last item.
- The Merchant does not need the full credit card number on the response and so we mask it and assume all credit cards have 12+ digits
- Only 4 currencies can currently be used right now.
- Unit test only written for the Service layer

## Going beyond
- Application intially seeds the database and so we can instantly test the code
- Swagger UI is hooked up for testing
- Credit card validation is done using a 3rd party library. Invalid credit cards numbers will be rejected.
- Cursor is returned so the merchant can see what records are next in the database. 

## Sample Response
### Get Request
```
{
    "paymentRecord": {
        "paymentRecordId": 30,
        "merchantId": 1,
        "success": true,
        "payment": {
            "creditCardNumber": "****-****-****-798",
            "expiryMonth": "10",
            "expiryYear": "21",
            "amount": 336.0,
            "currency": "GBP",
            "cvv": "760"
        }
    },
    "nextItem": 35
}
```

### Post Response
```
{
    "paymentRecordId": 51
}
```
- Returns the Id of the newly created payment record

## Areas for improvement
- MockDb currently write the whole list to the JSON file every time a save is made. We should instead append to the JSON file.
- No concurrency testing since we don't use a `ConcurrentDictionary`. 
- Make the application run using Docker
- Add additional unit tests to test edge cases
- Seed data currently only seeds from 3 credit cards. We should change this so it seeds from multiple credit card types.

## Cloud Technologies to be used
- Once Dockerized could be use EC2 and Fargate
- Service code can be split into seperate seperate micro-services
- Database can be written to RDS in AWS
- Logger is currently unused, but can be wired up to CloudWatch logs.
- No heartbeat page so auto-scaling won't be enabled 