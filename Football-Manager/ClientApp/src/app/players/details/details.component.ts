import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { Player } from "../player";
import { PlayersService } from "../players.service";

@Component({
  selector: 'app-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.css']
})
export class DetailsComponent implements OnInit {

  id: number;
  player: Player;

  constructor(
    public playersService: PlayersService,
    private route: ActivatedRoute,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.id = this.route.snapshot.params['playerId'];
    this.playersService.getPlayer(this.id).subscribe((data: Player) => {
      this.player = data;
    });
  }
}
