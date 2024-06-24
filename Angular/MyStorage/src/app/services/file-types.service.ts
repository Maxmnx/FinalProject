import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class FileTypesService {

  readonly baseUrl = "https://localhost:5001/mystorage";

  constructor(private http:HttpClient) { }

  getAllFileTypesList():Observable<any[]>{
    return this.http.get<any>(this.baseUrl + "/fileTypes")
  }

  deleteFileType(id:string){
    return this.http.delete(this.baseUrl + `/fileTypes/${id}`);
  }

  addFileType(data: any){

    return this.http.post(this.baseUrl + "/fileTypes", data)
  }

}
