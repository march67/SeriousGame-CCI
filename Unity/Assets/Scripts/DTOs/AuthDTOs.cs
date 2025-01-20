using UnityEngine;

public class AuthDTOs
{
    [System.Serializable]
    public class SignInRequest
    {
        public string Email;
        public string Username;
        public string Password;
    }

    public class SignInReponse
    {
        public string accessToken;
    }

    [System.Serializable]
    public class LoginRequest
    {
        public string Username;
        public string Password;
    }

    [System.Serializable]
    public class LoginResponse
    {
        public string accessToken;
        public string tokenType;
    }
}
