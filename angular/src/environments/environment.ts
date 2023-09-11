
const baseUrl = 'http://localhost:4200';

export const environment = {
  production: false,
  application: {
    baseUrl,
    name: 'NightMarket.Admin',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://localhost:5000/',
    redirectUri: baseUrl,
    clientId: 'NightMarket_Admin',
    dummyClientSecret:'1q2w3e*',
    responseType: 'code',
    scope: 'offline_access NightMarket.Admin',
    requireHttps: true,
  },
  apis: {
    default: {
      url: 'https://localhost:5001',
      rootNamespace: 'NightMarket.Admin',
    },
  },
};
