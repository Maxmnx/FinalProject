import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { FilesService } from 'src/app/services/files.service';

@Component({
  selector: 'app-my-files',
  templateUrl: './my-files.component.html'
})
export class MyFilesComponent {

  filesList$!:Observable<any[]>;

  accessOptions= ['Public', 'OnlyByLink', 'Private']

  constructor(private service:FilesService, private routing: Router) { }

  ngOnInit(): void {
    this.filesList$ = this.service.getMyFilesList();
  }

  onDownload(id:string, name: string):void{
    this.service.downloadFile(id).subscribe(
      (      response: { body: Blob; }) =>
      {
        let blob:Blob = response.body as Blob;
        let a = document.createElement('a');
        a.download = name;
        a.href=window.URL.createObjectURL(blob);
        a.click();

      }
    )
  }

  onDelete(id: string){
    this.service.deleteFile(id).subscribe();

    this.filesList$ = this.service.getMyFilesList();

    console.log(id);
  }

  onUpdate(fileModel: any){
    this.routing.navigateByUrl('/files/update/' + fileModel.id);

    console.log('update');
  }
}
