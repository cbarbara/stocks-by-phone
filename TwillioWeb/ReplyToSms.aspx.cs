using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

namespace TwillioWeb
{
	public partial class ReplyToSms : System.Web.UI.Page
	{
		protected void Page_Load( object sender, EventArgs e )
		{
			string from = Request[ "From" ];
			string to = Request[ "To" ];
			string body = Request[ "Body" ];
			string[] symbols = null;

			if( string.IsNullOrEmpty( body ) == false )
			{
				body = body.Trim();
				if( string.IsNullOrEmpty( body ) == false )
				{
					symbols = body.Split( new string[] { ",", ";" }, StringSplitOptions.RemoveEmptyEntries );
				}
			}

			Twilio.TwiML.TwilioResponse twml = new Twilio.TwiML.TwilioResponse();
			if( symbols == null || symbols.Length == 0 )
			{
				twml.Sms( "No Symbols Found" );
			}
			else
			{
				RemoteBatsLastSale.XigniteBATSLastSale bats = new RemoteBatsLastSale.XigniteBATSLastSale();
				bats.HeaderValue = new RemoteBatsLastSale.Header();
				bats.HeaderValue.Username = Shared.XigniteAuthenticationToken;

				string s = StringJoiner<string>.Join( symbols, "," );
				try
				{
					StringBuilder sb = new StringBuilder();
					RemoteBatsLastSale.LastSaleQuote[] quotes = bats.GetLastSales( s.ToUpper() );
					for( int i = 0; i < quotes.Length; i++ )
					{
						if( quotes[ i ].Outcome == RemoteBatsLastSale.OutcomeTypes.Success )
						{
							sb.AppendFormat(
								"{0}: {1} @ {2}\r\n",
								quotes[ i ].Symbol,
								quotes[ i ].Last,
								quotes[ i ].Time
							);
						}
					}
					twml.Sms( sb.ToString() );
				}
				catch( Exception )
				{
					twml.Sms( "Sorry, couldn't fetch any quotes." );
				}
			}

			var doc = twml.ToXDocument();
			Response.ContentType = "application/xml";
			doc.Save( Response.Output );
		}
	}
}