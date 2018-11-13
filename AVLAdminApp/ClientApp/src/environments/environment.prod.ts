
export const environment = {
  state: 'production',
  production: true,
  apiURLRoot: "http://on34c02211293.cihs.ad.gov.on.ca:2888",
  adalConfig: {
    tenant: 'cddc1229-ac2a-4b97-b78a-0e5cacb5865c',
    clientId: '49a46b72-d4c3-4481-a8bc-d1da537fa284',
    redirectUri: "http://on34c02211293.cihs.ad.gov.on.ca/AVLAdminAngularApp/auth-callback",
    postLogoutRedirectUri: "http://on34c02211293.cihs.ad.gov.on.ca/AVLAdminAngularApp/signedout",
    endpoints: {
      "http://on34c02211293.cihs.ad.gov.on.ca:2888": "5bc457e1-d064-4392-a133-ed9b399eb872"
    }
  }
};
