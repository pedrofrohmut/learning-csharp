# TODOs


## MilanJovanovic

1. Add Results instead of exceptions to UseCases. Exceptions are expensive and should
only be used for except cases in csharp + dotnet. Make something like rust result
to replace the exceptions + happy path

```txt
    Result { error, value }
    Error { code, message }

    Result<User> =>
        bool  Result.IsSuccess (has no errors),
        Error Result.Error     (returns the error)
        User  Result.Value     (returns the success value),

    Result.Error.Code == "User.NotFound"
    Result.Error.Message == "User not found by id"
```

Videos:
- [Get Rid of Exceptions in Your Code With the Result Pattern](https://www.youtube.com/watch?v=WCCkEe_Hy2Y)
- [2 Best Practices for Returning API Errors in ASP.NET Core](https://www.youtube.com/watch?v=YBK93gkGRj8)

2. Make the API response on error to be more than a message with a status code with
a message. Make it a object + status code.

With this format you can have more than one error per response

```json
Error Response: {
    "httpStatus": 400,
    "title": "Sign up has validation errors",
    "errors": [
        {
            "code": "Validation.Email",
            "description": "E-mail format is not valid"
        },
        {
            "code": "Validation.Password",
            "description": "Password is too short. Must be at least 6 characters"
        }
    ]
}
```
