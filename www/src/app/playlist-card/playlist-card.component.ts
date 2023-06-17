import { Component, Input, SimpleChanges, OnChanges } from '@angular/core';
import { Playlist } from '../YouFy/playlist';

@Component({
  selector: 'app-playlist-card',
  templateUrl: './playlist-card.component.html',
  styleUrls: ['./playlist-card.component.css']
})

export class PlaylistCardComponent { //implements OnChanges
  @Input() playlistInfo: Playlist;

  constructor() {
  }


  /*
  ngOnChanges(changes: SimpleChanges) {
    if (changes['playlistInfo']) {
      this.playlistInfo = changes['playlistInfo'].currentValue;
      console.log('Informações da playlist:', this.playlistInfo);
    }
  }
  */
}
