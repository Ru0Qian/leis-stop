using Auto;
    string username = "18576268305";
    string password = "gongqwe123";

    var tokenResult = await Token.GetToken(username, password);
    if (tokenResult.Item1)
    {
        Console.WriteLine(tokenResult.Item2);
    }
    else
    {
        Console.WriteLine(tokenResult.Item2);
    }
