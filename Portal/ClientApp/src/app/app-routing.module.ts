import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './components/login/login.component';

const routes: Routes = [
  {
    path: 'login',
    component: LoginComponent
  },
  //{
  //  path: 'dashboard',
  //  component: DashboardComponent,
  //  children: [
  //    {
  //      path: 'user',
  //      component: UserComponent
  //    },
  //    {
  //      path: 'application',
  //      component: ApplicationComponent
  //    },
  //    {
  //      path: 'profile',
  //      component: ProfileComponent
  //    },
  //  ]
  //},
];
@NgModule(
  {
    imports: [
      RouterModule.forRoot(
        routes,
        {
          useHash: true
        }
      )
    ],
    exports: [
      RouterModule
    ]
  }
)
export class AppRoutingModule { }
