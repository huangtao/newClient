﻿//***************************************************************************************
// Autor:Tecky
// Date:2008-06-03
// Desc:xxtea 算法的C#加密实现
// e-mail:likui318@163.com
//
//
//***************************************************************************************
using System;

namespace MyXXTEA
{
	public class XXTEA
	{
		public static Byte[] Encrypt (Byte[] Data, Byte[] Key)
		{
			try
			{
				if (Data.Length == 0)
				{
					return Data;
				}
				return ToByteArray (Encrypt (ToUInt32Array (Data, true), ToUInt32Array (Key, false)), false);
			} catch (Exception e)
			{
				UnityEngine.Debug.LogError ("XXTEA.Encrypt :" + e.Message);
				return null;
			}
		}

		public static Byte[] Decrypt (Byte[] Data, Byte[] Key)
		{
			try
			{
				if (Data.Length == 0)
				{
					return Data;
				}
				return ToByteArray (Decrypt (ToUInt32Array (Data, false), ToUInt32Array (Key, false)), false);
			} catch (Exception e)
			{
				UnityEngine.Debug.LogError ("XXTEA.Decrypt :" + e.Message);
				return null;
			}
		}

		static UInt32[] Encrypt (UInt32[] v, UInt32[] k)
		{
			try
			{
				Int32 n = v.Length - 1;
				if (n < 1)
				{
					return v;
				}
				if (k.Length < 4)
				{
					UInt32[] Key = new UInt32[4];
					k.CopyTo (Key, 0);
					k = Key;
				}
				UInt32 z = v [n], y = v [0], delta = 0x9E3779B9, sum = 0, e;
				Int32 p, q = 6 + 52 / (n + 1);
				while (q-- > 0)
				{
					sum = unchecked(sum + delta);
					e = sum >> 2 & 3;
					for (p = 0; p < n; p++)
					{
						y = v [p + 1];
						z = unchecked(v [p] += (z >> 5 ^ y << 2) + (y >> 3 ^ z << 4) ^ (sum ^ y) + (k [p & 3 ^ e] ^ z));
					}
					y = v [0];
					z = unchecked(v [n] += (z >> 5 ^ y << 2) + (y >> 3 ^ z << 4) ^ (sum ^ y) + (k [p & 3 ^ e] ^ z));
				}
				return v;
			} catch (Exception e)
			{
				//WeChat.WeChatLogError(e.Message);
				return null;
			}
		}

		static UInt32[] Decrypt (UInt32[] v, UInt32[] k)
		{
			try
			{
				Int32 n = v.Length - 1;
				if (n < 1)
				{
					return v;
				}
				if (k.Length < 4)
				{
					UInt32[] Key = new UInt32[4];
					k.CopyTo (Key, 0);
					k = Key;
				}
				UInt32 z = v [n], y = v [0], delta = 0x9E3779B9, sum, e;
				Int32 p, q = 6 + 52 / (n + 1);
				sum = unchecked((UInt32)(q * delta));
				while (sum != 0)
				{
					e = sum >> 2 & 3;
					for (p = n; p > 0; p--)
					{
						z = v [p - 1];
						y = unchecked(v [p] -= (z >> 5 ^ y << 2) + (y >> 3 ^ z << 4) ^ (sum ^ y) + (k [p & 3 ^ e] ^ z));
					}
					z = v [n];
					y = unchecked(v [0] -= (z >> 5 ^ y << 2) + (y >> 3 ^ z << 4) ^ (sum ^ y) + (k [p & 3 ^ e] ^ z));
					sum = unchecked(sum - delta);
				}
				return v;
			} catch (Exception e)
			{
				//WeChat.WeChatLogError(e.Message);
				return null;
			}
		}

		private static UInt32[] ToUInt32Array (Byte[] Data, Boolean IncludeLength)
		{
			try
			{
				Int32 n = (((Data.Length & 3) == 0) ? (Data.Length >> 2) : ((Data.Length >> 2) + 1));
				UInt32[] Result;
				if (IncludeLength)
				{
					Result = new UInt32[n + 1];
					Result [n] = (UInt32)Data.Length;
				}
				else
				{
					Result = new UInt32[n];
				}
				n = Data.Length;
				for (Int32 i = 0; i < n; i++)
				{
					Result [i >> 2] |= (UInt32)Data [i] << ((i & 3) << 3);
				}
				return Result;
			} catch (Exception e)
			{
				//WeChat.WeChatLogError(e.Message);
				return null;
			}
		}

		private static Byte[] ToByteArray (UInt32[] Data, Boolean IncludeLength)
		{
			try
			{
				Int32 n = Data.Length << 2;
				if (IncludeLength)
				{
					Int32 m = (Int32)Data [Data.Length - 1];
					if (m > n)
					{
						//WeChat.DebugLog("xxtea wrong!");
					}
					else
					{
						n = m;
					}
				}
				else
				{
					n = (Int32)Data.Length << 2;
				}
				//WeChat.DebugLog("data.length:" + n);
				Byte[] Result = new Byte[n];
				for (Int32 i = 0; i < n; i++)
				{
					Result [i] = (Byte)(Data [i >> 2] >> ((i & 3) << 3));
				}
				return Result;
			} catch (Exception e)
			{
				//WeChat.WeChatLogError(e.Message);
				return null;
			}
		}

	}
}