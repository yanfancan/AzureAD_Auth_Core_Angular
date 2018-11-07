// This file can be replaced during build by using the `fileReplacements` array.
// `ng build ---prod` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

//local debug
export const environment = {
  production: false,
  apiURLRoot: "http://localhost:44386",
  adalConfig: {
    tenant: 'cddc1229-ac2a-4b97-b78a-0e5cacb5865c',
    clientId: '537384c0-62b9-44bc-948c-199410793e33',
    redirectUri: "http://localhost:44386/auth-callback",
    postLogoutRedirectUri: "http://localhost:44386/signedout",
    //endpoints: {
    //  "http://localhost:44386": "537384c0-62b9-44bc-948c-199410793e33"
    //}
  }
};

//local IIS deploy
//export const environment = {
//  production: false,
//  adalConfig: {
//    tenant: 'cddc1229-ac2a-4b97-b78a-0e5cacb5865c',
//    clientId: '5bc457e1-d064-4392-a133-ed9b399eb872',
//    redirectUri: "http://on34c02211293.cihs.ad.gov.on.ca:2888/auth-callback",
//    postLogoutRedirectUri: "http://on34c02211293.cihs.ad.gov.on.ca:2888/signedout",
//    //endpoints: {
//    //  "http://on34c02211293.cihs.ad.gov.on.ca:1888": "537384c0-62b9-44bc-948c-199410793e33"
//    //}
//  }
//};

/*
 * In development mode, for easier debugging, you can ignore zone related error
 * stack frames such as `zone.run`/`zoneDelegate.invokeTask` by importing the
 * below file. Don't forget to comment it out in production mode
 * because it will have a performance impact when errors are thrown
 */
// import 'zone.js/dist/zone-error';  // Included with Angular CLI.
