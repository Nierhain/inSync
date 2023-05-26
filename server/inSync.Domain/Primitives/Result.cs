public class Result<T> where T : class{

    public Result(Error error){
        IsSuccess = false;
        Error = error;
    }
    public Result(T value){
        Value = value;
        IsSuccess = true;
    }

    public T Value {get; set;}
    public Error Error {get; set;}

    public bool IsSuccess {get; init;}
    public bool IsFailure => !IsSuccess;
}

public class Result {

    public static Result<T> Success<T>(T value) where T: class{
        return new Result<T>(value);
    }

    public static Result<T> Failure<T>(Error error) where T: class {
        return new Result<T>(error);
    }
}


public class Error {
    public Error(string code, string message)
    {
        Code = code;
        Message = message;
    }

    public string Code {get;set;}
    public string Message {get; set;}
}
