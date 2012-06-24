using System;
using System.Collections.Generic;
using System.Text;

namespace TwillioWeb
{
	public static class StringJoiner<T>
	{		
		public static List<string> JoinBatches(ICollection<T> StringCollection, char Delimiter)
		{
			if( StringCollection == null )
			{
				return null;
			}

			List<string> lstString = new List<string>();
			StringBuilder sb = new StringBuilder();

			IEnumerator<T> enumerator = StringCollection.GetEnumerator();
			if (enumerator.MoveNext())
			{
				sb.Append(enumerator.Current);
				// starts counter at 1 since it forces append of the current node
				int iCounter = 1;
				while (enumerator.MoveNext())
				{
					if (sb == null)
					{
						sb = new StringBuilder();
					}
					iCounter++;

					sb.Append(Delimiter);
					sb.Append(enumerator.Current);

					// batches of 5000
					if (iCounter == 5000 && iCounter < StringCollection.Count)
					{
						lstString.Add(sb.ToString());
						sb = null;
					}
				}
			}

			// if not null, it has not closed out
			// add it to the batches of strings to be returned to the caller
			if (sb != null)
			{
				lstString.Add(sb.ToString());
			}

			return lstString;
		}

		/// <summary>
		/// Given a collection of string, make a delimited string.
		/// </summary>
		/// <param name="StringCollection">A collection of string to be delimited.</param>
		/// <param name="Delimiter">The delimiter to use to delimit each string.</param>
		/// <returns>The delimited string.</returns>
		public static string Join(ICollection<T> StringCollection, char Delimiter)
		{
			return Join(StringCollection, Delimiter.ToString());
		}

		/// <summary>
		/// Given a collection of string, make a delimited string.
		/// </summary>
		/// <param name="StringCollection">A collection of string to be delimited.</param>
		/// <param name="Delimiter">The delimiter to use to delimit each string.</param>
		/// <returns>The delimited string.</returns>
		public static string Join(ICollection<T> StringCollection, string Delimiter)
		{
			if( StringCollection != null )
			{
				StringBuilder sb = new StringBuilder();
				IEnumerator<T> enumerator = StringCollection.GetEnumerator();
				if( enumerator.MoveNext() )
				{
					sb.Append( enumerator.Current );
					while( enumerator.MoveNext() )
					{
						sb.Append( Delimiter );
						sb.Append( enumerator.Current );
					}
					return sb.ToString();
				}
			}
			return string.Empty;
		}
	}
}
