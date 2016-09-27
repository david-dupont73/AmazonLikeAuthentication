using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuthenticationRestApi.Data
{
	public class MyMockedDatabase : IDatabaseAccess
	{
		public MyMockedDatabase() { }

		private const string SecretAccessKeyID = "wJalrXUtnFEMI/K7MDENG/bPxRfiCYEXAMPLEKEY";
		private const string AccessKeyId = "AKIAIOSFODNN7EXAMPLE";
		private const string Login = "guest";
		private const string Password = "mySuperPassword23$";

		public string GetAccessKeyId()
		{
			return 	 AccessKeyId;
		}
		public string GetSecretKey( string accessKeyId )
		{
			if( accessKeyId == AccessKeyId )
			{
				return SecretAccessKeyID;
			}
			else
			{
				throw new Exception( string.Format( "AccessKey {0} doesn't exist", accessKeyId ) );
			}
		}

		public string GetUserPassword( string login )
		{
			if( login == Login )
			{
				return Password;
			}
			else
			{
				throw new Exception( string.Format( "login {0} doesn't exist", login ) );
			}
		}
	}
}