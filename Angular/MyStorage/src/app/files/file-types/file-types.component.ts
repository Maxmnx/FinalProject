import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { FileTypesService } from 'src/app/services/file-types.service';

@Component({
  selector: 'app-file-types',
  templateUrl: './file-types.component.html'
})
export class FileTypesComponent {
  fileTypesList$!:Observable<any[]>;

  public addModel={
    Extension: null,
    MIMEType: null
  }

  constructor(private service:FileTypesService) { }

  ngOnInit(): void {
    this.fileTypesList$ = this.service.getAllFileTypesList();
  }

  onSubmit(){
    this.service.addFileType(this.addModel).subscribe();

    this.fileTypesList$ = this.service.getAllFileTypesList();
  }

  onDelete(id: string){
    this.service.deleteFileType(id).subscribe();

    this.fileTypesList$ = this.service.getAllFileTypesList();

    console.log(id);
  }

}
