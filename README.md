# WaterShortage
REST API to know if there is water shortage in french company departement
First api used is [sirene API](https://api.gouv.fr/les-api/sirene_v3) to get company's department  
Second is [VigiEau](https://api.vigieau.beta.gouv.fr/swagger) to know if ther is wather shortage in the department

## Endpoints
/api/siren?siren={siren}&api-version={version}

#### intputs :
| param | description | sample |
| ----- | ----------- | ------ |
| siren | entity siren | 972105410 |
| version | api version | 1.0 |

#### outputs :
| param | description | sample |
| ----- | ----------- | ------ |
| department | department fullname | Gironde |
| isShortage | boolean to see if ther is a shortage | true |
| gravity | shortage gravity | alerte_renforcee |
