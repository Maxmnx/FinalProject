import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-files',
  templateUrl: './files.component.html',
})
export class FilesComponent {
  constructor(private router: Router, private service: AuthService) { }

  public role = localStorage.getItem('role')

  ngOnInit() {
  }


  public onLogout() {
    localStorage.removeItem('token');
    localStorage.removeItem('role');
    this.router.navigate(['/auth/login']);
  }
}
