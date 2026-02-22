import { Routes } from '@angular/router';
import { Login } from './pages/login/login';
import { Register } from './pages/register/register';
import { Weather } from './components/weather/weather';
import { authGuard } from './guards/auth-guard';

export const routes: Routes = [
    {path: "login", component: Login},
    {path: "register", component: Register},
    {path: "weather", component: Weather, canActivate: [authGuard]}
];
