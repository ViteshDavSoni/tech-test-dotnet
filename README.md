### Test Description
In the 'PaymentService.cs' file you will find a method for making a payment. At a high level the steps for making a payment are:

 - Lookup the account the payment is being made from
 - Check the account is in a valid state to make the payment
 - Deduct the payment amount from the account's balance and update the account in the database
 
What we’d like you to do is refactor the code with the following things in mind:  
 - Adherence to SOLID principals
 - Testability  
 - Readability 

We’d also like you to add some unit tests to the ClearBank.DeveloperTest.Tests project to show how you would test the code that you’ve produced. The only specific ‘rules’ are:  

 - The solution should build.
 - The tests should all pass.
 - You should not change the method signature of the MakePayment method.

You are free to use any frameworks/NuGet packages that you see fit.  
 
You should plan to spend around 1 to 3 hours to complete the exercise.

### Improvements I made

I began by reorganising the project folder structure to follow the Clean Architecture approach, creating separate projects for Application, Domain, and Infrastructure. I applied the same structure to the test projects.

Next, I renamed the data access classes to have a 'Repository' suffix and introduced interfaces, which allowed me to abstract the data access layer and simplify testing when referencing these repositories.

Once the project structure was in place, I wrote tests for the repositories using Test Driven Development (TDD). I wrote failing tests first and then iteratively updating the code to make them pass.

After some minor refactoring, I focused on the PaymentService, which contained most of the business logic. I again started with failing tests and refactored the code to make them pass. This included removing the ConfigurationManager logic and instead injecting the account repository via the constructor. This reduced coupling and made the service easier to test by allowing dependencies to be mocked. I then expanded the test coverage of the PaymentService.

Next, I refactored the PaymentService further by applying the Strategy and Factory patterns to replace the original switch case logic in the MakePayment method. I created separate rules for Bacs, FasterPayments, and Chaps. I then noticed that much of the logic was repeated between the rules, so I introduced a base class containing the common functionality, leaving the individual rules to handle only their specific differences. This approach made the code more maintainable, extensible, and easier to understand.

If I had more time, I would have improved test coverage for the Domain logic, including the rules and the PaymentRuleFactory. Additionally, if this were an API, I would implement dependency injection in the API layer.



