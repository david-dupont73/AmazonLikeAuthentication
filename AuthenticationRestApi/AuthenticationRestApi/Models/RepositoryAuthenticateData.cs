using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AuthenticationRestApi.Data;
using AuthenticationRestApi.Service;

namespace AuthenticationRestApi.Models
{
	public class RepositoryAuthenticateData:IRepositoryAuthenticationData
	{

		private IDatabaseAccess dataBaseAccess;
		public RepositoryAuthenticateData()
		{
			dataBaseAccess = new MyMockedDatabase();
		}	
		bool IRepositoryAuthenticationData.Authenticate( string login, string password )
		{
			string userPassword = dataBaseAccess.GetUserPassword( login );
			if( password == userPassword )
			{
				return true;
			}
			else
			{
				return false;
			}
		}
	

		 bool IRepositoryAuthenticationData.AuthenticateWithAccessKey( string email, string authorisationKey )
		{
			string[] splitKey = authorisationKey.Split( new Char[] { ' ', ':' }, StringSplitOptions.RemoveEmptyEntries );

			if( ( splitKey.Length == 3 )&&( splitKey[0] == "AWS" ))
			{
				string accessKey = splitKey[1];
				string signature = splitKey[2];
				string encodedEmail = SecurityManagementProvider.Instance.ValidateAuthentication( email, accessKey );

				if( encodedEmail == signature )
				{
					return true;
				}
				else
				{
					return false;
				}

			}
			else
			{
				throw new Exception( "Bad Authorisation Key" );
			}

		}
	}
			
				
}