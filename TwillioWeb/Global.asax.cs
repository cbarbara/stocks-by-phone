using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace TwillioWeb
{
	public class Global : System.Web.HttpApplication
	{

		protected void Application_Start( object sender, EventArgs e )
		{
			RemoteBatsLastSale.XigniteBATSLastSale initializer = new RemoteBatsLastSale.XigniteBATSLastSale();
			initializer.HeaderValue = new RemoteBatsLastSale.Header();
			initializer.HeaderValue.Username = Shared.XigniteAuthenticationToken;

			SymbolsToKeyPadNumbers.Initialize( initializer.ListTradedSymbols( 4, "", "" ) );
		}

		protected void Session_Start( object sender, EventArgs e )
		{

		}

		protected void Application_BeginRequest( object sender, EventArgs e )
		{

		}

		protected void Application_AuthenticateRequest( object sender, EventArgs e )
		{

		}

		protected void Application_Error( object sender, EventArgs e )
		{

		}

		protected void Session_End( object sender, EventArgs e )
		{

		}

		protected void Application_End( object sender, EventArgs e )
		{

		}
	}
}