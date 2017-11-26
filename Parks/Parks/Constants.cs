public static class Constants
{
    public const string AppName = "Parks";
    // ==================================================================
    // PARAMETROS DE CONFIGURAÇÃO PARA AUTENTICAÇÃO NA GOOGLE
    // Para fazer login via Google, configure em
    // https://console.developers.google.com/

    public const string iOSGoogleClientId = "833822264754-9mu9n2unb4n8pvv14o810eur98br27qi.apps.googleusercontent.com";

    public const string AndroidGoogleClientId = "833822264754-srqnb0715inv6lvk65j2uru8p99lfjd7.apps.googleusercontent.com";

    public const string OutroGoogleClientId = "833822264754-vr5efmm7fblqiigh921hmbadbbsrh07b.apps.googleusercontent.com";
    public const string OutroGoogleClientSecret = "xGk-2tmS6vhNfuW_cHYDBZ8G";

    // Esses valores não precisam serem alterados
    // lista dos Google Scope:
    // https://developers.google.com/identity/protocols/googlescopes
    public const string GoogleScope = "https://www.googleapis.com/auth/userinfo.email";
    public const string GoogleAuthorizeUrl = "https://accounts.google.com/o/oauth2/auth";
    public const string GoogleAccessTokenUrl = "https://www.googleapis.com/oauth2/v4/token";
    public const string GoogleUserInfoUrl = "https://www.googleapis.com/oauth2/v2/userinfo";
    // Inverter os Client IDs para iOS/Android,
    // adicionando ":/oauth2redirect" no final
    public const string iOSGoogleRedirectUrl = "com.googleusercontent.apps.833822264754-9mu9n2unb4n8pvv14o810eur98br27qi:/oauth2redirect";

    public const string AndroidGoogleRedirectUrl = "com.googleusercontent.apps.833822264754-srqnb0715inv6lvk65j2uru8p99lfjd7:/oauth2redirect";
    // ==================================================================
}