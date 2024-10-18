namespace ElementalWords.Exceptions;

public class ElementalWordsValidationException : Exception
{
    public ElementalWordsValidationException()
    { 
    }

    public ElementalWordsValidationException(string message)
        : base(message)
    { 
    }

    public ElementalWordsValidationException(string message, Exception innerException)
        : base(message, innerException)
    { 
    }
}
