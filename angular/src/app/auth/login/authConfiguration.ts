export const authConfiguration = {
  loginUrl: 'https://gitlab.com/oauth/authorize',
  tokenEndpoint: 'https://gitlab.com/oauth/token ',
  userinfoEndpoint: 'https://gitlab.com/oauth/userinfo',

  clientId: '7842564f2ee9e1df05665dfa3611d429ddd87537dc4c63015db788a7ec2eef32',

  // URL of the SPA to redirect the user to after login
  redirectUri: window.location.origin + '/auth/login',

  // set the scope for the permissions the client should request
  // The first four are defined by OIDC.
  // Important: Request offline_access to get a refresh token
  // The api scope is a usecase specific one
  scope: 'openid profile email',
};
