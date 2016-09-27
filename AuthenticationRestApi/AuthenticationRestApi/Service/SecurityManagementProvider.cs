using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using AuthenticationRestApi.Data;

namespace AuthenticationRestApi.Service
{
	public class SecurityManagementProvider
	{
		private static SecurityManagementProvider instance;

		private SecurityManagementProvider() { }

		public static SecurityManagementProvider Instance
		{
			get
			{
				if( instance == null )
				{
					instance = new SecurityManagementProvider();
				}
				return instance;
			}
		}

		// StringToVerify = email
		public string ValidateAuthentication( string stringToEncode, string accessKeyId)
		{
			string userSecretKey = new MyMockedDatabase().GetSecretKey( accessKeyId );

			HMACSHA1 hmac = new HMACSHA1( Utilities.GetBytes(userSecretKey) );
			byte[] stringBytes = System.Text.Encoding.UTF8.GetBytes( stringToEncode );
			byte[] hashedValue = hmac.ComputeHash( stringBytes );
			return Convert.ToBase64String( hashedValue );


		}
	}
}