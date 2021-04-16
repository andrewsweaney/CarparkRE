# CarparkRE
Repo for a carpark rate engine coding example

There are 4 projects in this Solution.
1. CarparkRE_Lib: This a re-usable library with the models and the core calculation functionality
2. CarparkRE_Lib.Tests: contain the unit tests used to validate the library functionality
3. CarparkRE: Web API which serves as an interface for the Carpark rate calculation engine
4. CarparkRE.Tests: Contains the Unit Tests to validate the operation of the Web API

Other files of note:
* CarParkRates.json : Holds the data for the Rates that will be used for the demonstration code
* CarparkRateEngine.postman : A postman collection for running against the Web API

This demo app was written to serve as a solution to a code challenge to develop an API that performs rate calculations for carpark customers

# The brief is as outlined below
The problem: We need an API that does a rate calculation engine for a carpark 
 The inputs for this engine are:    
1. Car Entry Date and Time 
2. Car Exit Date and Time 
 
Based on these 2 inputs the engine program should calculate the correct rate for the customer and display the name of the rate along with the total price to the customer using the following rates:
* Name of the Rate	:	Early Bird
* Type				:	Flat Rate
* Total Price		:	$13.00
* Entry Condition	:	Enter between 6:00 AM to 9:00 AM
* Exit Condition	:	Exit between 3:30 PM to 11:30 PM
-----------------------------------------------------
* Name of the Rate	:	Night Rate
* Type				:	Flat Rate
* Total Price		:	$6.50
* Entry Condition	:	Enter between 6:00 PM to midnight (weekdays)
* Exit Condition	:	Exit before 8 AM
------------------------------------------------------
* Name of the Rate	:	Weekend Rate
* Type				:	Flat Rate
* Total Price		:	$10.00
* Entry Condition	:	Enter anytime past midnight on Friday 
* Exit Condition	:	Exit any time before midnight of Sunday
------------------------------------------------------

Note: If a customer enters the carpark before midnight on Friday and if they qualify for Night rate on a Saturday morning, then the program should charge the night rate instead of weekend rate.  
For any other entry and exit times the program should refer the following table for calculating the total price. 

* Name of the Rate	:	Standard Rate
* Type				:	Hourly Rate
*		0-1 Hours	:	$5.00
*		1-2 Hours	:	$10.00
*		2-3 Hours	:	$15.00
*		3+ Hours	:	$20.00 flat rate for each calendar day of parking

Note: The customer should get the cheapest deal based on the rules which apply to the time period.

This test gives you the opportunity to demonstrate your flair for technology and your programming style.  The result does not have to be complete but should show your ability to structure and test production quality code.    

