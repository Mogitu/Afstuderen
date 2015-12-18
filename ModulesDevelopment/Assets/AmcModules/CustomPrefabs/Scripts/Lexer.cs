using System;
using System.IO;
using UnityEngine;


public class Lexer : IDisposable 
{
	StreamReader input;
	MemoryStream stream;
	private string token;
	private TokenType tokenType;

	//tokens define the language, they allow us to know how to parse the script
	public enum TokenType{
		IdentifierToken,
		KeywordToken,
		IntToken,
		RealToken,
		StringToken,
		OtherToken,
		EndOfInput,
		IntRangeToken,
		RealRangeToken,
		ComponentToken,
		BoolToken,
		Vec3,
		Vec3Range
	}

	public Lexer (string fileInput)
	{
		//generate a new stream reader to use our lexer on
		input = new StreamReader(GenerateStreamFromString(fileInput));
	}

	public Stream GenerateStreamFromString(string s)
	{
		stream = new MemoryStream();
		StreamWriter writer = new StreamWriter(stream);
		writer.Write(s);
		writer.Flush();
		stream.Position = 0;
		return stream;
	}

	public void Dispose ()
	{
		stream.Dispose();
	}

	private void SkipWhiteSpace() {
		//peek ahead each character and consume whitespace characters, otherwise we're done.
		int c = input.Peek();
		while(Char.IsWhiteSpace((char)c)) {
			input.Read();

			if(input.EndOfStream)
				return;

			c = input.Peek();
		}
	}

	private int GetNextFromInput() {
		input.Read();
		return input.Peek();
	}

	private int GetNextNonWhiteSpaceFromInput() {
		input.Read();
		SkipWhiteSpace();
		return input.Peek();
	}

	public void NextToken() {
		try{
			token = "";
			SkipWhiteSpace();
			if(input.EndOfStream) {
				token = "<eof>";
				tokenType = TokenType.EndOfInput;
				return;
			}
			int c = input.Peek();
			if(char.IsDigit((char)c) || c == '-') {
				if(c == '-') {
					token = token + (char) c;
					c = GetNextFromInput();
				}
				tokenType = TokenType.IntToken;
				while (char.IsDigit((char) c)) {
					token = token + (char) c;
					c = GetNextFromInput();
				}
				if (c == '.') {
					tokenType = TokenType.RealToken;
					token = token + (char) c;
					c = GetNextFromInput();
					while (char.IsDigit((char) c)) {
						token = token + (char) c;
						c = GetNextFromInput();
					}
				}
				if (c == ':') {
					token = token + (char) c;
					if (tokenType == TokenType.IntToken) {
						tokenType = TokenType.IntRangeToken;
						c = GetNextFromInput();
						if(c == '-') {
							token = token + (char) c;
							c = GetNextFromInput();
						}
						while (char.IsDigit((char) c)) {
							token = token + (char) c;
							c = GetNextFromInput();
						}
					} else {// real token
						tokenType = TokenType.RealRangeToken;
						c = GetNextFromInput();
						if(c == '-') {
							token = token + (char) c;
							c = GetNextFromInput();
						}
						while (char.IsDigit((char) c)) {
							token = token + (char) c;
							c = GetNextFromInput();
						}
						if (c == '.') {
							token = token + (char) c;
							c = GetNextFromInput();
							while (char.IsDigit((char) c)) {
								token = token + (char) c;
								c = GetNextFromInput();
							}
						}
					}
				}
				//first comma of vector 3
				if(c == ',' && 
				   (tokenType == TokenType.IntToken || tokenType == TokenType.RealToken ||
				    tokenType == TokenType.IntRangeToken || tokenType == TokenType.RealRangeToken)) { 
					//vector support ranges for any of their values.
					if(tokenType == TokenType.IntRangeToken || tokenType == TokenType.RealRangeToken)
						tokenType = TokenType.Vec3Range;
					else
						tokenType = TokenType.Vec3;

					token = token + (char) c;
					c = GetNextNonWhiteSpaceFromInput();
					if(c == '-') {
						token = token + (char) c;
						c = GetNextNonWhiteSpaceFromInput();
					}
					while (char.IsDigit((char) c) || c =='.' || c ==':' || c == '-') {
						if(c == ':')
							tokenType = TokenType.Vec3Range;

						token = token + (char) c;
						c = GetNextNonWhiteSpaceFromInput();
					}
					if(c == ',') { //second comma of vector3
						token = token + (char) c;
						c = GetNextNonWhiteSpaceFromInput();
						if(c == '-') {
							token = token + (char) c;
							c = GetNextNonWhiteSpaceFromInput();
						}
						while (char.IsDigit((char) c) || c =='.' || c ==':' || c == '-') {
							if(c == ':')
								tokenType = TokenType.Vec3Range;

							token = token + (char) c;
							c = GetNextNonWhiteSpaceFromInput();
						}
					} else {
						throw new Exception("Invalid_Syntax: Expected second comma to define Vector3");
					}
				}
			} else if (c == '[') {
				tokenType = TokenType.ComponentToken;
				c = GetNextFromInput();
				while (char.IsLetter((char)c)) {
					token = token + (char) c;
					c = GetNextFromInput();
				}
				if (c != ']') {
					throw new Exception("Invalid_Syntax: Component tokens must not contain any non-alphabet characters and must be closed with ']'");
				}
			} else if (char.IsLetter((char) c)) {
				tokenType = TokenType.IdentifierToken;
				while (char.IsLetter((char) c) || char.IsDigit((char) c)) {
					token = token + (char) c;
					c = GetNextFromInput();
				}
				if (token.ToLower().Equals("true")
				    || token.ToLower().Equals("false")) {
					tokenType = TokenType.BoolToken;
				}
			} else if (c == '"') {
				tokenType = TokenType.StringToken;
				c = GetNextFromInput();
				while (c != '"') {
					token = token + (char) c;
					c = GetNextFromInput();
					if (c == -1)
						throw new Exception("Invalid_Syntax: must close '\"'");
				}
				//Clear the " so it's not waiting for the next token
				GetNextFromInput();
			} else {
				tokenType = TokenType.OtherToken;
				token = token + (char) c;
					int d = GetNextFromInput();
				if ((c == '<') && (d == '='))
					token = token + (char) d;
				else if ((c == '<') && (d == '<'))
					token = token + (char) d;
				else if ((c == '>') && (d == '='))
					token = token + (char) d;
				else if ((c == '=') && (d == '='))
					token = token + (char) d;
				else if ((c == '!') && (d == '='))
					token = token + (char) d;
			} 
		} catch (Exception ex) {
			Debug.Log(ex.Message);
		}
	} 

	public System.Object GetValue(TokenType tt) {
		string tokenValuePair = token;
		NextToken();
		if (Match("=")) {
			tokenValuePair += "=";
			NextToken();
			tokenValuePair += token;
			if (tokenType == tt) {
				return GetObject();
			}
		}
		Debug.Log("TokenType mismatch when getting value: " + tokenValuePair);
		return null;
	}

	public System.Object GetValue() {
		string tokenValuePair = token;
		NextToken();
		if (Match("=")) {
			tokenValuePair += "=";
			NextToken();
			tokenValuePair += token;
			return GetObject();
		}
		Debug.Log("TokenType mismatch when getting value: " + tokenValuePair);
		return null;
	}
	
	public System.Object GetObject() {
		switch (tokenType) {
		case TokenType.IntToken:
			return int.Parse(GetToken());
		case TokenType.IntRangeToken:
			string[] iComponents = GetToken().Split(':');
			return new int[]{int.Parse(iComponents[0]), int.Parse(iComponents[1])};
		case TokenType.RealToken:
			return float.Parse(GetToken());
		case TokenType.RealRangeToken:
			string[] fComponents = GetToken().Split(':');
			return new float[]{float.Parse(fComponents[0]), float.Parse(fComponents[1])};
		case TokenType.StringToken:
			return GetToken();
		case TokenType.BoolToken:
			return bool.Parse(GetToken());
		case TokenType.IdentifierToken:
			return GetToken();
		case TokenType.Vec3:
			string[] components = GetToken().Split(',');
			return new Vector3(float.Parse(components[0]), float.Parse(components[1]), float.Parse(components[2]));
		case TokenType.Vec3Range:
			string[] rangeComponents = GetToken().Split(',');
			float[] vec3Range = new float[6];
			int vec3RangePointer = 0;
			//Construct a array of 6 floats, each two defining the min/max for the x,y and z components.
			for(int comp = 0; comp < 3; comp++) {
				if(rangeComponents[comp].Contains(":"))
				{
					int indexOfColon = rangeComponents[comp].IndexOf(':');

					//if there's a range use it
					vec3Range[vec3RangePointer++] = float.Parse(rangeComponents[comp].Substring(0, indexOfColon));
					vec3Range[vec3RangePointer++] = float.Parse(rangeComponents[comp].Substring(indexOfColon + 1, rangeComponents[comp].Length - 1 - indexOfColon));
				} else {
					//if there's not a range, just use the same value
					vec3Range[vec3RangePointer++] = float.Parse(rangeComponents[comp]);
					vec3Range[vec3RangePointer++] = float.Parse(rangeComponents[comp]);
				}
			}
			return vec3Range;
		}
		return null;
	}

	public static void FinializeSpecialTypes(ref System.Object value, TokenType type) {
		switch(type) {
		case Lexer.TokenType.IntRangeToken:
			int[] intRange = (int[])value;
			value = UnityEngine.Random.Range(intRange[0], intRange[1]);
			break;
		case Lexer.TokenType.RealRangeToken:
			float[] floatRange = (float[])value;
			value = UnityEngine.Random.Range(floatRange[0], floatRange[1]);
			break;
		case Lexer.TokenType.Vec3Range:
			float[] vec3Range = (float[])value;
			value = new Vector3(UnityEngine.Random.Range(vec3Range[0], vec3Range[1]),
			                    UnityEngine.Random.Range(vec3Range[2], vec3Range[3]),
								UnityEngine.Random.Range(vec3Range[4], vec3Range[5]));
			break;
		}
	}

	public string GetToken() {
		return token;
	}

	public TokenType GetTokenType() {
		return tokenType;
	}

	public bool Match(string test) {
		return test.ToLower().Equals(token.ToLower());
	}
}

