import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class FilesService {

  readonly baseUrl = "https://localhost:5001/mystorage";

  constructor(private http:HttpClient) { }

  getAllFilesList():Observable<any[]>{
    return this.http.get<any>(this.baseUrl + "/files")
  }

  getAllPublicFilesList():Observable<any[]>{
    return this.http.get<any>(this.baseUrl + "/files/public")
  }

  getFilesListByFilter(StartDate?:any,EndDate?:any, FileType?:any, MinSize?:any, MaxSize?:any, CreatorUserName?:any):Observable<any[]>{

    return this.http.get<any>(this.baseUrl + "/files/filter", { 
        params: {StartDate : StartDate || '',
        EndDate : EndDate || '',
        FileType : FileType || '',
        MinSize : MinSize || '',
        MaxSize : MaxSize || '',
        CreatorUserName : CreatorUserName || ''}
    }) 
  }

  getFile(id:string):any{
    return this.http.get(this.baseUrl + `/files/${id}`)
  }

  addFile(fileName:any, description:any, accessLevel: any, file: any){

    return this.http.post(this.baseUrl + `/files/upload/${fileName}/${accessLevel}`, 
    file, {params: {description : description || ''}} )
  }

  updateFile(id:string, data:any){
    return this.http.put(this.baseUrl + `/files/${id}`, data)
  }

  deleteFile(id:string){
    return this.http.delete(this.baseUrl + `/files/${id}`);
  }

  fileLink(username:string, filename: string):any{
    return this.http.get<any>(this.baseUrl + `/files/link/${username}/${filename}`)
  }

  public downloadFile(id:string):any{
    return this.http.get(this.baseUrl + `/files/export/${id}`,
    {observe: 'response', responseType: 'blob'})
  }

  getMyFilesList():Observable<any>{
    return this.http.get<any>(this.baseUrl + "/files/my-files");
  }

}
