# Application Layer

Det yderste lag af vores Core.

Det er her alt vores business logic ligger

Vores Application lager indeholder

Interfaces

Models

Logic

Commands / Queries

Validators

Exceptions


# CQRS

Adskiller reads (queries) fra writest (commands)

Det kan maksimere performance, scalability og security men først og fremmest øger det simpliciten, fordi vi kan
enkapsulere enten vores read eller write. 

Det er let at tilføje nye features - bare tilføj en querry eller en command

# MediatR

Message bibliotek der passer rigtig godt sammen med CQRS

Queries og commands defineres som requests, så vores application layer bliver en række af request / response objecter

Det giver os mulighed for at tilføje yderligere behaviour før/efter hver request - det kan fx være logging, validation, caching, authorisation osv.