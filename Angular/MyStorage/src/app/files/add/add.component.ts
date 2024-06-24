import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { FilesService } from 'src/app/services/files.service';

@Component({
  selector: 'app-add',
  templateUrl: './add.component.html',
})
export class AddComponent {

  fileModel={
    FileName: '',
    Description: null,
    AccessLevel: 0,
  }

  

  accessOptions= ['Public', 'OnlyByLink', 'Private']

  constructor(private service : FilesService, private router: Router){

  }

  onSubmit(files:any){

    if(files.length === 0){
      return;
    }

    let fileToUpload = <File>files[0];
    var formData = new FormData();
    formData.append("file", fileToUpload, fileToUpload.name)

    this.service.addFile(this.fileModel.FileName,this.fileModel.Description, this.fileModel.AccessLevel, formData).subscribe(
      r => console.log(r),
      err => console.log(err)
      );
  }

}
