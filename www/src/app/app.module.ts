import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms'

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { PlaylistService } from './playlist.service';
import { PlaylistCardComponent } from './playlist-card/playlist-card.component';

@NgModule({
  declarations: [
    AppComponent,
    PlaylistCardComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [PlaylistService],
  bootstrap: [AppComponent, PlaylistCardComponent]
})
export class AppModule { }
