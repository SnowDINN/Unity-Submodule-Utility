namespace Redbean
{
	public static class DatabaseExtension
	{
		/// <summary>
		/// 로컬 데이터 저장
		/// </summary>
		public static float SetPlayerPrefs(this float value, string key) =>
			Database.Save(key, value);
		
		/// <summary>
		/// 로컬 데이터 저장
		/// </summary>
		public static int SetPlayerPrefs(this int value, string key) =>
			Database.Save(key, value);
		
		/// <summary>
		/// 로컬 데이터 저장
		/// </summary>
		public static string SetPlayerPrefs(this string value, string key) =>
			Database.Save(key, value);

		/// <summary>
		/// 로컬 데이터 저장
		/// </summary>
		public static T SetPlayerPrefs<T>(this T value, string key) where T : class => 
			Database.Save(key, value);
		
		/// <summary>
		/// 로컬 데이터 호출
		/// </summary>
		public static T GetPlayerPrefs<T>(this T value, string key) where T : class =>
			Database.Load<T>(key);
	}
	
	public static class CryptographyExtension
	{
		private static readonly DatabaseCryptography cryptography = new();
		public static string Encryption(this string value) => cryptography.Encryption(value);
		public static string Decryption(this string value) => cryptography.Decryption(value);
	}
}