import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthComponent} from './auth/auth.component';
import { RegistrationComponent } from './auth/registration/registration.component';

import { RouterModule, Routes, ParamMap } from '@angular/router';
import { LoginComponent } from './auth/login/login.component';
import { FilesComponent } from './files/files.component';
import { HomeComponent } from './files/home/home.component';
import { SearchComponent } from './files/search/search.component';
import { AddComponent } from './files/add/add.component';
import { MyFilesComponent } from './files/my-files/my-files.component';
import { LinkComponent } from './files/link/link.component';
import { UpdateComponent } from './files/update/update.component';
import { FileTypesComponent } from './files/file-types/file-types.component';

const routes: Routes = [
  {path: '', redirectTo:'auth/registration', pathMatch:'full'},
  {
  path:'auth', component: AuthComponent,
  children:[
    {path:'registration', component: RegistrationComponent},
    {path:'login', component: LoginComponent}
  ]
},
  {path: 'files', component: FilesComponent,
  children:[
    {path: 'home', component: HomeComponent},
    {path: 'search', component: SearchComponent},
    {path: 'add', component: AddComponent},
    {path: 'update/:id', component: UpdateComponent},
    {path: 'my-files', component: MyFilesComponent},
    {path: 'link/:username/:filename', component: LinkComponent},
    {path: 'file-types', component: FileTypesComponent}
  ]
}
]

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forRoot(routes)
  ],
  exports :[
    RouterModule
  ]
})
export class AppRoutingModule { }
