import { HttpInterceptorFn } from '@angular/common/http';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const token = localStorage.getItem('token'); // Retrieve token from localStorage

  if (token) {
    const clonedReq = req.clone({
      setHeaders: {
        Authorization: `Bearer ${token}`, // Add Authorization header
      },
    });
    return next(clonedReq); // Pass the modified request
  }

  return next(req); // Pass the unmodified request if no token
};
