Readme for Problem D
====================

Creating a full MVC application for this seemed to be an effective way to handle this problem. In order to run it, you will need to run the query from the ProblemC.sql file in the Problem C folder.  The appsettings.json file will also need to be modified to match your MySQL setup.  In the Connection string, please adjust the userid, password, and port to your local system settings.  From there, I believe the app should run by simply running the 'dotnet run' command from the terminal then navigating to localhost:5000. 

Accessing the main functions of the app will require registration, all validations should create error messages that describe what is needed, but to save some time, the password is required to be 8 characters, containing at least 1 letter, 1 number, and a special character($@$!%*#?&).

The web app does allow the database to create the policy number since that logic is currently built-in to the database.  To change from database-generated to controller-generated would require pulling the fresh PetOwner out of the database(as well as the country in order to get the country code) and using the Id it was assigned to generate the PolicyNumber in a similar manner.  I did not program this, but this would not be difficult to do if this would be preferred.

The logic of having an Active status rather than deleting when canceling a policy was due to record keeping, as I'm sure records need to be kept for a period of time even after a policy has been canceled.

The act of transferring a pet from one PetOwner to another does de-activate the pet's policy.  The logic of this was that the new owner should actually approve the new addition.  Additionally, deactivating your own account deactivates every pet associated with the account, in case of a query to find all active pets, these ones do not appear since they are no longer part of an active plan. This would require the owner that re-activates their plan to re-activate each pet as well.