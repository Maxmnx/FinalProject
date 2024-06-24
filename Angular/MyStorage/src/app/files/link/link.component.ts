import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { FilesService } from 'src/app/services/files.service';

@Component({
  selector: 'app-link',
  templateUrl: './link.component.html',
})
export class LinkComponent implements OnInit{

  file = {
    id: '',
    name : '',
    size : '',
    fileTypeExtension : '',
    creationDate : '',
    creatorUserName : ''
  }

  username : any;
  filename: any;

  constructor(
    private route: ActivatedRoute, public service : FilesService
  ) 
  {
    this.username = route.snapshot.params['username'];
    this.filename = route.snapshot.params['filename'];
  }

  ngOnInit() {
    this.service.fileLink(this.username, this.filename).subscribe(
      (response: any)=>{
        this.file.id = response.id;
        this.file.name = response.name;
        this.file.size = response.size;
        this.file.fileTypeExtension = response.fileTypeExtension;
        this.file.creationDate = response.creationDate;
        this.file.creatorUserName = response.creatorUserName;
      }
    )
  }

  onDownload(){
    this.service.downloadFile(this.file.id).subscribe(
      (      response: { body: Blob; }) =>
      {
        let blob:Blob = response.body as Blob;
        let a = document.createElement('a');
        a.download = this.file.name;
        a.href=window.URL.createObjectURL(blob);
        a.click();

      }
    )
  }

}

