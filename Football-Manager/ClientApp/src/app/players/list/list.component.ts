import { Component, OnInit } from '@angular/core';
import { Player } from "../player";
import { PlayersService } from "../players.service";

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css']
})
export class ListComponent implements OnInit {

  players: Player[] = [];

  constructor(public playersService: PlayersService) { }

  ngOnInit(): void {
    this.playersService.getPlayers().subscribe((data: Player[]) => {
      this.players = data;
    });
  }

  deletePlayer(id) {
    this.playersService.deletePlayer(id).subscribe(res => {
      this.players = this.players.filter(item => item.PlayerId !== id);
    });
  }
}
