import { Position } from "./position";

export interface Player {
  PlayerId: number;
  ShirtNo?: number;
  Name: string;
  Appearances?: number;
  Goals?: number;
  GoalsPerMatch?: number;
  Position: Position;
}
