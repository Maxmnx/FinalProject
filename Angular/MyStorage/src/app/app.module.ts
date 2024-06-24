import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule} from '@angular/forms'
import { ToastrModule } from 'ngx-toastr';
import { HttpClientModule, HTTP_INTERCEPTORS} from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { RegistrationComponent } from './auth/registration/registration.component';

import { AppRoutingModule } from './app-routing.module';
import { AuthService } from './services/auth.service';
import { FilesService } from './services/files.service';
import { LoginComponent } from './auth/login/login.component';
import { FilesComponent } from './files/files.component';
import { HomeComponent } from './files/home/home.component';
import { SearchComponent } from './files/search/search.component';
import { AddComponent } from './files/add/add.component';
import { MyFilesComponent } from './files/my-files/my-files.component';
import { AuthComponent } from './auth/auth.component';
import { LinkComponent } from './files/link/link.component';
import { UpdateComponent } from './files/update/update.component';
import { FileTypesComponent } from './files/file-types/file-types.component';
import { FileTypesService } from './services/file-types.service';
import { AuthInterceptor } from './auth/auth.interceptor';



@NgModule({
  declarations: [
    AppComponent,
    RegistrationComponent,
    LoginComponent,
    FilesComponent,
    HomeComponent,
    SearchComponent,
    AddComponent,
    MyFilesComponent,
    AuthComponent,
    LinkComponent,
    UpdateComponent,
    FileTypesComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule,
    AppRoutingModule,
    HttpClientModule,
    ToastrModule.forRoot({
      progressBar: true
    }),
  ],
  providers: [AuthService, {
    provide: HTTP_INTERCEPTORS,
    useClass: AuthInterceptor,
    multi: true
  }, FilesService, FileTypesService],
  bootstrap: [AppComponent]
})
export class AppModule { }
