import { Component, OnInit } from '@angular/core';
import { UserService } from '../_services/user.service';
import { AlertifyService } from '../_services/alertify.service';
import { User } from '../_models/user';

@Component({
  selector: 'app-researches',
  templateUrl: './researches.component.html',
  styleUrls: ['./researches.component.css']
})
export class ResearchesComponent implements OnInit {

  constructor(private userService: UserService, private alertify: AlertifyService) { }

  ngOnInit() {
  }

  loadResearches() {}

}
