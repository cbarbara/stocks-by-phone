using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace TwillioWeb
{
	public class SymbolsToKeyPadNumbers
	{
		static Dictionary<string, List<string>> _mapping = new Dictionary<string, List<string>>();

		public static List<string> Map( string digits )
		{
			List<string> ret = null;
			_mapping.TryGetValue( digits, out ret );
			return ret;
		}

		internal static void Initialize( RemoteBatsLastSale.SymbolList symbolList )
		{
			if( symbolList.Outcome != RemoteBatsLastSale.OutcomeTypes.Success )
			{
				return;
			}

			foreach( var sym in symbolList.Symbols )
			{
				string digits = ConvertToDigits( sym.Symbol );
				if( _mapping.ContainsKey( digits ) == false )
				{
					_mapping.Add( digits, new List<string>() );
				}
				_mapping[ digits ].Add( sym.Symbol );
			}
		}

		private static string ConvertToDigits( string p )
		{
			StringBuilder sb = new StringBuilder();
			char[] chars = p.ToCharArray();
			foreach( var c in chars )
			{
				switch( c )
				{
					case 'A':
					case 'B':
					case 'C':
						sb.Append( "2" );
						break;

					case 'D':
					case 'E':
					case 'F':
						sb.Append( "3" );
						break;

					case 'G':
					case 'H':
					case 'I':
						sb.Append( "4" );
						break;

					case 'J':
					case 'K':
					case 'L':
						sb.Append( "5" );
						break;

					case 'M':
					case 'N':
					case 'O':
						sb.Append( "6" );
						break;

					case 'P':
					case 'Q':
					case 'R':
					case 'S':
						sb.Append( "7" );
						break;

					case 'T':
					case 'U':
					case 'V':
						sb.Append( "8" );
						break;

					case 'W':
					case 'X':
					case 'Y':
					case 'Z':
						sb.Append( "9" );
						break;
				}
			}
			return sb.ToString();
		}
	}
}