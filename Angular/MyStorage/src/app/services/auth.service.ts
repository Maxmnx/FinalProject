import { Injectable } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { HttpClient, HttpHeaders } from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(public fb: FormBuilder, private http : HttpClient) { }

  readonly BaseURI = 'http://localhost:5215/mystorage';

  formModel = this.fb.group({
    UserName:['', Validators.required],
    Email:['', Validators.email],
    PhoneNumber:[''],
    FirstName:[''],
    MiddleName:[''],
    LastName:[''],
    Passwords: this.fb.group({
      Password: ['', [Validators.required, Validators.minLength(10)]],
      ConfirmPassword: ['', Validators.required]
    })
  })

  register() {
    var body = {
      UserName: this.formModel.value.UserName,
      Email: this.formModel.value.Email,
      PhoneNumber: this.formModel.value.PhoneNumber,
      FirstName: this.formModel.value.FirstName,
      MiddleName: this.formModel.value.MiddleName,
      LastName: this.formModel.value.LastName,
      Password: this.formModel.value.Passwords?.Password
    };
    return this.http.post(this.BaseURI + '/Auth/Register', body);
  }

  login(formData: any) {
    return this.http.post(this.BaseURI + '/Auth/Login', formData);
  }
}
