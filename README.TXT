
------------------------------------------------------------------------------------------------------------------------------------------------------
L'Architecture:

Le controller  	RestApiAuthenticateController.cs ( expose les appels REST)
Le modèle  RepositoryAuthenticateData (positionné dernier une interface IRepositoryAuthenticateData) pilote l'authentification ( login/password and AmazonLikeAuthentication)
Les données (login/password, SecretKey, AccessKey ) sont stockées dans un objet "mocké" (MyMockedDatabase.cs) derrière une interface IDatabaseAccess
Le module d'authentification "AmazonLike" est intégrer dans un objet SINGLETON (SecurityManagementProvider.cs) défini dans la couche de service.

--------------------------------------------------------------------------------------------------------------------------------------------------
Le controller RepositoryAuthenticateData gère Les appels REST API suivant:

Appel avec simple authentification (Login/Password) passé en parametre
http://localhost:62931/api/authenticate/guest/mySuperPassword23$

Appel authentifié avec signature passée dans la clé authorisation de l'entete HTTP 
http://localhost:62931/api/confidentials/david.dupont@gmail.com
ajouter la clé Authorisation : AWS AKIAIOSFODNN7EXAMPLE:OB1j8iM3JMfNSEODyLQ+agJtYeE=77 dans l'entete HTTP


Appel authentifié avec signature passé en parametre
http://localhost:62931/api/confidentials/david.dupont@gmail.com/QQBXAFMAIABBAEsASQBBAEkATwBTAEYATwBEAE4ATgA3AEUAWABBAE0AUABMAEUAOgBPAEIAMQBqADgAaQBNADMASgBNAGYATgBTAEUATwBEAHkATABRACsAYQBnAEoAdABZAGUARQA9AA2


Appel permettant de signer une adresse email
http://localhost:62931/api/signature/david.dupont@gmail.com

Donne une signature sans encodage HTTP: AWS AKIAIOSFODNN7EXAMPLE:OB1j8iM3JMfNSEODyLQ+agJtYeE= 
Donne une signature sans encodage HTTP: QQBXAFMAIABBAEsASQBBAEkATwBTAEYATwBEAE4ATgA3AEUAWABBAE0AUABMAEUAOgBPAEIAMQBqADgAaQBNADMASgBNAGYATgBTAEUATwBEAHkATABRACsAYQBnAEoAdABZAGUARQA9AA2
------------------------------------------------------------------------------------------------------------------------------------------------------------

