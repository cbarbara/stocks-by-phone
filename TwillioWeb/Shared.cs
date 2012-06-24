using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TwillioWeb
{
	public static class Shared
	{
		public static string XigniteAuthenticationToken
		{
			get
			{
				return System.Configuration.ConfigurationManager.AppSettings[ "xigniteToken" ];
			}
		}
	}
}