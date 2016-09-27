namespace AuthenticationRestApi.Data
{
	public interface IDatabaseAccess
	{
		string GetSecretKey( string accessKeyId );
		string GetUserPassword( string login );

		string GetAccessKeyId();
	}
}