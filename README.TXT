========================================Amazon Like Authentication ==========================================================
L'Architecture:
---------------
Modèle d'architecture type MVC appliqué aux REST API.

Le controller  	RestApiAuthenticateController.cs ( expose les appels REST)
Le modèle  RepositoryAuthenticateData (positionné derrière une interface IRepositoryAuthenticateData) pilote l'authentification ( login/password and AmazonLikeAuthentication)
Les données (login/password, SecretKey, AccessKey ) sont stockées dans un objet "mocké" (MyMockedDatabase.cs) derrière une interface IDatabaseAccess
Le module d'authentification "AmazonLike" est intégré dans un objet SINGLETON (SecurityManagementProvider.cs) défini dans la couche de service.

=====================================================================================================================
Le controller RepositoryAuthenticateData gère Les appels REST API suivants:

Appel avec simple authentification (Login/Password) passé en parametre
----------------------------------------------------------------------

http://localhost:62931/api/authenticate/guest/mySuperPassword23$

Appel authentifié avec signature passée dans la clé authorisation de l'entete HTTP 
----------------------------------------------------------------------------------

http://localhost:62931/api/confidentials/david.dupont@gmail.com
ajouter la clé Authorisation : AWS AKIAIOSFODNN7EXAMPLE:OB1j8iM3JMfNSEODyLQ+agJtYeE=77 dans l'entete HTTP
(Utiliser un outil type Postman pour tester cette fonctionnalitée)


Appel authentifié avec signature passé en parametre
---------------------------------------------------
http://localhost:62931/api/confidentials/david.dupont@gmail.com/QQBXAFMAIABBAEsASQBBAEkATwBTAEYATwBEAE4ATgA3AEUAWABBAE0AUABMAEUAOgBPAEIAMQBqADgAaQBNADMASgBNAGYATgBTAEUATwBEAHkATABRACsAYQBnAEoAdABZAGUARQA9AA2


Appel permettant de signer une adresse email
--------------------------------------------

http://localhost:62931/api/signature/david.dupont@gmail.com

Donne une signature sans encodage HTTP: AWS AKIAIOSFODNN7EXAMPLE:OB1j8iM3JMfNSEODyLQ+agJtYeE= 
Donne une signature sans encodage HTTP: QQBXAFMAIABBAEsASQBBAEkATwBTAEYATwBEAE4ATgA3AEUAWABBAE0AUABMAEUAOgBPAEIAMQBqADgAaQBNADMASgBNAGYATgBTAEUATwBEAHkATABRACsAYQBnAEoAdABZAGUARQA9AA2
=====================================================================================================================

Idées d'évolutions:
-------------------

- Afin de découpler encore un peu plus ce modèle MVC, il est possible d'ajouter une couche d'injection de dépendance entre le Controller et le Repository (Modèle) et entre le modèle et l'accés aux données.
- Possibilité de publier ce webService dans un container Docker (via la couche Mono) afin de déployer le service dans le Cloud et le load-balancer pour piloter au mieux la montée en charge.
- Aller un peu plus loin dans le "split" de la solution (Controller, Model, Module d'authentification) dans le but d'en faire une architecture de microservices interconnectés.





