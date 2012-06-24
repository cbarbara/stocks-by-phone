using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

namespace TwillioWeb
{
	public partial class ReplyToPhone : System.Web.UI.Page
	{
		protected void Page_Load( object sender, EventArgs e )
		{
			Twilio.TwiML.TwilioResponse twml = new Twilio.TwiML.TwilioResponse();
			string digits = Request[ "Digits" ];
			if( string.IsNullOrEmpty( digits ) )
			{
				twml.Say( "Sorry, we did not recieve the numbers you entered. " );
			}
			else
			{
				string[] split = digits.Trim().Split( '*' );
				foreach( var digitGroup in split )
				{
					List<string> symbols = SymbolsToKeyPadNumbers.Map( digitGroup );
					if( symbols == null || symbols.Count == 0 )
					{
						twml.Say( "Sorry, no symbols found using the digits " + digitGroup + ". " );
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
										"The latest price for {0} as of {2} is {1}. ",
										quotes[ i ].CompanyName,
										quotes[ i ].Last,
										quotes[ i ].Time
									);
								}
							}
							twml.Say( sb.ToString(), new { voice = "woman" } );
						}
						catch( Exception ex )
						{
							twml.Say( "Sorry, couldn't fetch any quotes for " + digitGroup + ". " );
						}
					}
				}
			}

			twml.Redirect( "PhoneCallMenu.xml", "GET" );

			var doc = twml.ToXDocument();
			Response.ContentType = "application/xml";
			doc.Save( Response.Output );
		}
	}
}