import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Player } from "../player";
import { PlayersService } from "../players.service";

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css']
})
export class ListComponent implements OnInit {

  players: Player[] = [];

  constructor(
    public playersService: PlayersService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.playersService.getPlayers().subscribe((data: Player[]) => {
      this.players = data;
    });
  }

  deletePlayer(playerId) {
    this.playersService.deletePlayer(playerId).subscribe(res => {
      this.players = this.players.filter(item => item.PlayerId !== playerId);
      this.router.navigateByUrl('/', { skipLocationChange: true }).then(() =>
        this.router.navigate(['players/list']));
    });
  }
}
