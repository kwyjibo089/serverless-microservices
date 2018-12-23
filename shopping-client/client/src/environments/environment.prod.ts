import { AuthConfig } from "angular-oauth2-oidc";

export const environment = {
  production: true,
  hmr: false,

  loginRoute: "/login",
  ordersApiBaseUrl: "https://orders.serverlessmicroservices.net/api/",
  productsApiBaseUrl: "https://products.serverlessmicroservices.net/api/"
};

export const resourceOwnerConfig: AuthConfig = {
  issuer: "https://tt-identityserver4-demo.azurewebsites.net",
  clientId: "resourceowner",
  dummyClientSecret: "no-really-a-secret",
  scope: "openid profile email api",
  oidc: false
};
