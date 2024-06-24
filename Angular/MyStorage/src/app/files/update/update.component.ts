import { Component } from '@angular/core';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { FilesService } from 'src/app/services/files.service';

@Component({
  selector: 'app-update',
  templateUrl: './update.component.html'
})
export class UpdateComponent {

  id:any;

  fileModel={
    name: '',
    description: null,
    accessLevel: 0,
  }

  accessOptions= ['Public', 'OnlyByLink', 'Private']

  constructor(private service : FilesService, private route: ActivatedRoute){
    this.id = route.snapshot.params['id'];
    this.service.getFile(this.id).subscribe(
      (response : any) => {
        this.id = response.id;
        this.fileModel.name = response.name;
        this.fileModel.accessLevel = response.accessLevel;
        this.fileModel.description = response.description;
      }
    );
  }

  onSubmit(){
    this.service.updateFile(this.id, this.fileModel);
  }

}
