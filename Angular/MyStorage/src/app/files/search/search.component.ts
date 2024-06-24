import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { FilesService } from 'src/app/services/files.service';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html'
})
export class SearchComponent {

  filesList$!:Observable<any[]>;

  public filterModel={
    StartDate: null,
    EndDate: null,
    FileType: null,
    MinSize: null,
    MaxSize: null,
    CreatorUserName: null
  }

  constructor(private service:FilesService) { }

  ngOnInit(): void {
    this.filesList$ = this.service.getAllPublicFilesList();
  }

  onFilter(){
    this.filesList$ = this.service.getFilesListByFilter(
      this.filterModel.StartDate,
      this.filterModel.EndDate,
      this.filterModel.FileType,
      this.filterModel.MinSize,
      this.filterModel.MaxSize,
      this.filterModel.CreatorUserName);
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
}
