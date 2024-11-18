import { CanActivateFn, Router } from '@angular/router';

export const authGuard: CanActivateFn = (route, state) => {
  const isLoggedIn = !!localStorage.getItem('token');
  const router = new Router();

  const targetUrl = state.url;

  if (isLoggedIn && (targetUrl === '/login' || targetUrl === '/signup')) {
    // Redirect logged-in users to the home page
    return router.createUrlTree(['/home']);
  }

  if (!isLoggedIn && targetUrl.startsWith('/home')) {
    // Redirect unauthenticated users to the login page
    return router.createUrlTree(['/login']);
  }

  return true; // Allow navigation if the conditions above are not met
};
