import { Component, Output } from '@angular/core';
import { PlaylistService } from './playlist.service';
import { Playlist } from './YouFy/playlist';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})

export class AppComponent {
  title = "YouFy";
  urlPlaylist: string = "";

  loading: boolean = false;

  constructor(private playlistService: PlaylistService) {
  }

  @Output() playlist: Playlist = new Playlist();

  public async getPlaylist() {
    this.loading = true;
    await this.playlistService.getPlaylist(this.urlPlaylist)
      .subscribe(
        (response) => {                           //next() callback
          this.playlist.UrlThumbnail = response.urlThumbnail;
          this.playlist.Titulo = response.nomePlaylist;
          this.playlist.Descricao = response.descricao;
          this.playlist.QtdFaixa = response.qtdFaixa;
          this.playlist.NomeCriador = response.nomeCriador;
          this.playlist.UrlPlaylist = this.urlPlaylist;

          this.playlist = { ...this.playlist };
        },
        (error) => {                              //error() callback
          console.error('Request falhou! ' + error);
          this.loading = false;
        },
        () => {                                   //complete() callback
          this.loading = false;
        })
  }
}


