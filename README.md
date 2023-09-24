# Notes by Odysseus #

Initial decisions:
- in order to "view employees and dependents" a UI was my prefered route, spun up a new React app to render data from the API
- the requirements did not specify the ability to modify/add/remove employees/dependents, I decided to not implement said features as it was not explicitly asked for
- some business rules are implemented in the API (only 1 spoue/dometic partner) while other are in the UI (paycheck calculation and tests for said calculation)
- some of the decisions are based on the idea that each customer/user-base for the system may have their own settings/configurations
- the tests provided with the API fail to run out of the box, an error that points to either a firewall issue or a setting issue kept coming up, instead of putting all my time into attempting to reolve said issue I focused on the intent of the test code
- the initial API code provided returned all dependent(s) with the employee(s), this lead to no actual need for the "DependentsController", as the data it would provided was already exposed via the EmployeeController;  I refactored both DependentsController and EmployeesController to separate the data and allow each controller to be responsible for their own "domain"
- I added additional properties to the DTO's: a profile url for an image for the employee (mainly for the UI), and the EmployeeId that a dependent was associated with, otherwise there was no connection between the two
- I moved all "data fetching" out of the controller(s) and into Providers which are setup in the builder via dependancy injection

Additional notes:
- The provided API application required the install of an older version of the SDK
- ~~I focused on presneting the data being exposed by the provided endpoints, and small alterations to what the controllers were setup to expose; work could have been done to flesh out missing REST calls (put/post/delete), I did not focus on this, as it was not part of the requirements~~
- Update on put/post/delete calls: decided to implement these, as it seemed heavily implied, although not explicit in the requirements.

# Prerequisites
1. install npm (https://nodejs.org/en/download)
1. install .net core SDK v6 (latest is v7, the API seed project requires v6)

# How to run the app #
1. From the base directory (./PaylocityHomework) you can Run `.NET Core Launch (Web)` to start the API (https://localhost:7124/swagger)
2. From the base UI directory (./PaylocityBenefitsUi/paylocity-benefits-ui/) you can run `npm run start` to launch the UI (http://localhost:3000/)
3. Open the UI in your browser (http://localhost:3000/)

To run PaymentCalc tests: From the base UI directory (./PaylocityBenefitsUi/paylocity-benefits-ui/) you can run `npm run test cost-calcs`
Cost Calculation tests can be found in `./PaylocityBenefitsUi/paylocity-benefits-ui/src/Utils/cost-calcs.test.ts`

---

# ORIGINAL README BELOW #

# What is this?

A project seed for a C# dotnet API ("PaylocityBenefitsCalculator").  It is meant to get you started on the Paylocity BackEnd Coding Challenge by taking some initial setup decisions away.

The goal is to respect your time, avoid live coding, and get a sense for how you work.

# Coding Challenge

**Show us how you work.**

Each of our Paylocity product teams operates like a small startup, empowered to deliver business value in
whatever way they see fit. Because our teams are close knit and fast moving it is imperative that you are able
to work collaboratively with your fellow developers. 

This coding challenge is designed to allow you to demonstrate your abilities and discuss your approach to
design and implementation with your potential colleagues. You are free to use whatever technologies you
prefer but please be prepared to discuss the choices you’ve made. We encourage you to focus on creating a
logical and functional solution rather than one that is completely polished and ready for production.

The challenge can be used as a canvas to capture your strengths in addition to reflecting your overall coding
standards and approach. There’s no right or wrong answer.  It’s more about how you think through the
problem. We’re looking to see your skills in all three tiers so the solution can be used as a conversation piece
to show our teams your abilities across the board.

Requirements will be given separately.