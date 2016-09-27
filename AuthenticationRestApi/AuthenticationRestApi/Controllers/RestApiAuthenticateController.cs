using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using AuthenticationRestApi.Data;
using AuthenticationRestApi.Models;
using AuthenticationRestApi.Service;

namespace AuthenticationRestApi.Controllers
{
	//[Route( "api" )]
	public class RestApiAuthenticateController : ApiController
    {

		[Route( "api/index" )]
		public void Get()
		{
			string str = "Hello";			
		}

		[Route( "api/authenticate/{login}/{password}" )]
		[HttpGet]
		public IHttpActionResult Authenticate( string login, string password )
		{
			IRepositoryAuthenticationData authenticationData = new RepositoryAuthenticateData();

			return  Json(authenticationData.Authenticate( login, password ));
		}


		[Route( "api/confidentials/{email}" )]
		[HttpGet]
		public IHttpActionResult Confidentials( string email )
		{
			//Authorization = "AWS" + " " + AWSAccessKeyId + ":" + Signature;
			//Signature = Base64( HMAC-SHA1( MySecretAccessKeyID, UTF-8-Encoding-Of( email ) ) );

			IRepositoryAuthenticationData authenticationData = new RepositoryAuthenticateData();

			try
			{
				string authorisationKey =  Request.Headers.GetValues("Authorization").FirstOrDefault<string>();
				return Json( authenticationData.AuthenticateWithAccessKey( email, authorisationKey ));
			}
			catch( Exception)
			{
				throw new Exception("Bad Authorisation Exception");
			}


		}

		[Route( "api/confidentials/{email}/{authorisationKey}" )]		
		[HttpGet]
		public IHttpActionResult Confidentials( string email, string authorisationKey )
		{
			string decodedauthorisationKey = Utilities.GetString(HttpServerUtility.UrlTokenDecode(authorisationKey));

			IRepositoryAuthenticationData authenticationData = new RepositoryAuthenticateData();
			try
			{
				return Json(authenticationData.AuthenticateWithAccessKey( email, decodedauthorisationKey ));
			}
			catch( Exception ex )
			{
				throw new Exception( "Bad Authorisation Exception" );
			}
		}


		[Route( "api/signature/{stringToEncode}" )]
		[HttpGet]
		public string GetSignature(string stringToEncode )
		{
			IDatabaseAccess databaseAccess = new MyMockedDatabase();

			string signature = SecurityManagementProvider.Instance.ValidateAuthentication( stringToEncode, databaseAccess.GetAccessKeyId() );

			string signatureWithouEncoding = String.Format( "{0} {1}:{2}", "AWS", databaseAccess.GetAccessKeyId(), signature );
			string signatureWithHTTPEncoding = HttpServerUtility.UrlTokenEncode( Utilities.GetBytes( signatureWithouEncoding ) );
			
			return String.Format("Without HTTP Encoding :{0} \n\r With HTTP Encoding: {1}", signatureWithouEncoding, signatureWithHTTPEncoding );
		}

	}
}
