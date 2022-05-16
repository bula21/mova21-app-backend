import { NgModule } from '@angular/core';
import { AuthModule } from 'angular-auth-oidc-client';


@NgModule({
  imports: [AuthModule.forRoot({
    config: {
      authority: 'https://auth.bula21.ch/auth/realms/Test/protocol/openid-connect/auth',
      redirectUrl: window.location.origin,
      postLogoutRedirectUri: window.location.origin,
      unauthorizedRoute: '/unauthorized',
      clientId: 'test-app-velo',
      scope: 'openid profile email', // 'openid profile ' + your scopes
      responseType: 'code',
      silentRenew: true,
      authWellknownEndpointUrl: "https://auth.bula21.ch/auth/realms/Test/.well-known/openid-configuration",
      silentRenewUrl: window.location.origin + '/silent-renew.html',
      renewTimeBeforeTokenExpiresInSeconds: 10
    }
  })],
  exports: [AuthModule],
})
export class AuthConfigModule { }
