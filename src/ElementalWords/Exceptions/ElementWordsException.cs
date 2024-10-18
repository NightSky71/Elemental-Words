namespace ElementalWords.Exceptions;

public class ElementWordsException : Exception
{
    public ElementWordsException()
    { 
    }

    public ElementWordsException(string message)
        : base(message)
    { 
    }

    public ElementWordsException(string message, Exception innerException)
        : base(message, innerException)
    { 
    }
}
