# GenesysServices
Les Data Actions permettent de personnaliser les parcours clients de vos flux en récupérant des données éxterieur à l'organization Genesys Cloud.  
Ce projet est une vitrine de possiblités technique pour récupérer des informations de différentes manière.

# WaterShortage
Cette feature vise à montrer la possibilité de faire d'autres appels REST à des services externes et à y ajouter les logiques métiers nécessaire pour renvoyer des donnees ciblées et pertinentes au flux.
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
