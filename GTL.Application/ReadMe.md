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

Det kan maksimere performance, scalability og security men f�rst og fremmest �ger det simpliciten, fordi vi kan
enkapsulere enten vores read eller write. 

Det er let at tilf�je nye features - bare tilf�j en querry eller en command

# MediatR

Message bibliotek der passer rigtig godt sammen med CQRS

Queries og commands defineres som requests, s� vores application layer bliver en r�kke af request / response objecter

Det giver os mulighed for at tilf�je yderligere behaviour f�r/efter hver request - det kan fx v�re logging, validation, caching, authorisation osv.