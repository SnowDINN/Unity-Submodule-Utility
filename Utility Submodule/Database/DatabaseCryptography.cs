using System;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

namespace Redbean
{
	public class DatabaseCryptography
	{
		private const int byteIndex = 16;
		private readonly Aes aes;

		public DatabaseCryptography()
		{
			aes = Aes.Create();
			aes.Mode = CipherMode.CBC;
			aes.Padding = PaddingMode.PKCS7;
			aes.KeySize = 128;
			aes.BlockSize = 128;
		}

		private string md5Key
		{
			get
			{
				const string key = "Cryptography Key";

				if (!PlayerPrefs.HasKey(key))
					PlayerPrefs.SetString(key, CodeCreate(8));
				
				return PlayerPrefs.GetString(key);
			}
		}

		private string key
		{
			get
			{
				var md5 = MD5.Create();
				var result = md5.ComputeHash(Encoding.UTF8.GetBytes(md5Key));

				return Encoding.UTF8.GetString(result);
			}
		}

		public string Encryption(string text)
		{
			var sourceArray = Encoding.UTF8.GetBytes(key);
			var keyBytes = new byte[byteIndex];
			
			var count = sourceArray.Length;
			if (count > keyBytes.Length) 
				count = keyBytes.Length;
			Array.Copy(sourceArray, keyBytes, count);
			
			aes.Key = keyBytes;
			aes.IV = keyBytes;
			
			var encryptor = aes.CreateEncryptor();
			var buffer = Encoding.UTF8.GetBytes(text);

			return Convert.ToBase64String(encryptor.TransformFinalBlock(buffer, 0, buffer.Length));
		}

		public string Decryption(string text)
		{
			var fromBase64String = Convert.FromBase64String(text);
			var sourceArray = Encoding.UTF8.GetBytes(key);
			var keyBytes = new byte[byteIndex];
			
			var count = sourceArray.Length;
			if (count > keyBytes.Length) 
				count = keyBytes.Length;
			Array.Copy(sourceArray, keyBytes, count);
			
			aes.Key = keyBytes;
			aes.IV = keyBytes;
			
			var decryptedString = aes.CreateDecryptor().TransformFinalBlock(fromBase64String, 0, fromBase64String.Length);
			return Encoding.UTF8.GetString(decryptedString);
		}

		private string CodeCreate(int length)
		{
			const string combineCode = "ABCDEFGHIJKLMNPQRSTUVWXYZ123456789";
			
			var max = Math.Max(1, Math.Min(length, 128));
			var floor = Mathf.FloorToInt(255.0f / combineCode.Length) * combineCode.Length;
			if (floor <= 0)
				return null;

			var stringBuilder = new StringBuilder();
			using (var provider = new RNGCryptoServiceProvider()) 
			{
				while (stringBuilder.Length != max) 
				{
					var bytes = new byte[8];
					provider.GetBytes(bytes);
					
					foreach (var b in bytes) 
					{
						if (b >= floor || stringBuilder.Length == max) 
							continue;
						
						var character = combineCode[b % combineCode.Length];
						stringBuilder.Append(character);
					}
				}
			}
			
			return stringBuilder.ToString();
		}
	}
}