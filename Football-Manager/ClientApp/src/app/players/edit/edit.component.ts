import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { Player } from "../player";
import { Position } from "../position";
import { PlayersService } from "../players.service";
import { PositionsService } from "../positions.service";

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.css']
})
export class EditComponent implements OnInit {

  playerId: number;
  player: Player;
  positions: Position[] = [];
  editForm;

  constructor(
    public playersService: PlayersService,
    public positionsService: PositionsService,
    private route: ActivatedRoute,
    private router: Router,
    private formBuilder: FormBuilder
  ) {
    this.editForm = this.formBuilder.group({
      playerId: [''],
      shirtNo: ['', Validators.required],
      name: ['', Validators.required],
      positionId: [''],
      appearances: [''],
      goals: [''],
    });
  }

  ngOnInit(): void {
    this.playerId = this.route.snapshot.params['playerId'];

    this.positionsService.getPositions().subscribe((data: Position[]) => {
      this.positions = data;
    });

    this.playersService.getPlayer(this.playerId).subscribe((data: Player) => {
      this.player = data;
      this.editForm.patchValue(data);
    });
  }

  onSubmit(formData) {
    this.playersService.updatePlayer(this.playerId, formData.value).subscribe(res => {
      this.router.navigateByUrl('players/list');
    });
  }
}
