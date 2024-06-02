# SirenApi
REST API to get company informations thanks to siren number
It use an other REST API to get outputs informations ([sirene API](https://api.gouv.fr/les-api/sirene_v3))

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
| siren | entity siren | 972105410 |
| version | api version | 1.0 |
