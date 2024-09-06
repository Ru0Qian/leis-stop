using Auto;
    string username = "";
    string password = "";

    var tokenResult = await Token.GetToken(username, password);
    if (tokenResult.Item1)
    {
        Console.WriteLine(tokenResult.Item2);
    }
    else
    {
        Console.WriteLine(tokenResult.Item2);
    }
