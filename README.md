# GenesysServices
Les Data Actions permettent de personnaliser les parcours clients de vos flux en récupérant des données éxterieur à l'organization Genesys Cloud.  
Ce projet est une vitrine de possiblités technique pour récupérer des informations de différentes manière.  
Features:  
* [Water shortage](#watershortage)
* [ISBN checker](#isbn-checker)
* [Database](#db)
# WaterShortage
Cette feature vise à montrer la possibilité d'utiliser des des services externes REST et à y ajouter les logiques métiers nécessaire pour renvoyer des donnees ciblées et pertinentes au flux.
## Cas d'utilisation
Au sein d'un flux, vous pouvez appeler ce service pour récupérer via le SIRET de l'interlocauteur le niveau de sécheresse de la région de l'établissement.
## Flux de données
[Sirene API](https://api.gouv.fr/les-api/sirene_v3) est appelée pour récuperer la région ou est enregistré l'etablissement.   
[VigiEau](https://api.vigieau.beta.gouv.fr/swagger) est appelée pour récuperer les secheresses signalées dans l'hexagone.
## Endpoints
/api/v1/watershortage?siret={siret}
#### intputs :
| param | description | sample |
| ----- | ----------- | ------ |
| siret | entity siret | 972105410 |
#### outputs :
| param | description | sample |
| ----- | ----------- | ------ |
| department | department fullname | Gironde |
| isShortage | boolean to see if ther is a shortage | true |
| gravity | shortage gravity | alerte_renforcee |
# ISBN Checker
Cette feature vise à montrer la possibilité d'utiliser des des services externes SOAP et à y ajouter les logiques métiers nécessaire pour renvoyer des donnees ciblées et pertinentes au flux.
## Cas d'utilisation
Avant d'utiliser un code ISBN au sein d'un flux, vous voulez verifier la validité du code.
## Flux de données
[ISBN Numbers](http://webservices.daehosting.com/services/ISBNService.wso) est appelée pour valider le code.
## Endpoints
/api/v1/isbn?isbn={isbn}
#### intputs :
| param | description | sample |
| ----- | ----------- | ------ |
| isbn | isbn number (10 or 13 numbers) | 9798507236343 |
#### outputs :
| param | description | sample |
| ----- | ----------- | ------ |
| isISBN | boolean response | true |
# DB
Cette feature vise à montrer la possibilité d'utiliser directement une base de données (MongoDB ici).
## Cas d'utilisation
Vous voulez voir si le numéro qui vous appel est un numéro d'un client présent dans votre base de donnée client.  
Si oui vous souhaité récupérer son id client.
## Flux de données
Aucun service externe n'est utilisé.  
Cependant une base de données doit être accessible localement.
## Endpoints
/api/v1/client?phoneNumber={phoneNumber}
#### intputs :
| param | description | sample |
| ----- | ----------- | ------ |
| phoneNumber | customer phone number | 0600000000 |
#### outputs :
| param | description | sample |
| ----- | ----------- | ------ |
| id | customer unique id, empty if customer not found | "1984523" |
