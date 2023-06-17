import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient, HttpParams } from '@angular/common/http';

@Injectable()
export class PlaylistService {
  baseUrl: string = 'https://localhost:7144/api/YouFy/'
  constructor(private http: HttpClient) { }

  getPlaylist(urlPlaylist: string): Observable<any> {
    const params = new HttpParams()
      .set('urlPlaylist', urlPlaylist);

    return this.http.get(this.baseUrl + 'GetPlaylist', {'params': params })
  }
}
