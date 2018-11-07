import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { DboServiceAccount } from '../_models/dboServiceAccount';
import { DboService } from '../_models/dboService';
import { DboServiceAccountServices } from '../_models/dboServiceAccountServices';
import { fail } from 'assert';

@Component({
  selector: 'app-map-services',
  templateUrl: './map-services.component.html',
  styleUrls: ['./map-services.component.css']
})

export class MapServicesComponent implements OnInit {

  public currentUser;
  p: number = 1;
  public serviceAccounts: DboServiceAccount[];
  public selectedSA: DboServiceAccount;
  public withService: DboServiceAccount[] = new Array<DboServiceAccount>();
  public withoutService: DboServiceAccount[] = new Array<DboServiceAccount>();
  public initialServiceAccounts: DboServiceAccount[];
  public services: DboService[];
  public selectedService: DboService;
  public temp: DboService[];
  public newSAS: DboServiceAccountServices[] = new Array<DboServiceAccountServices>();
  public deleteSAS: DboServiceAccountServices[] = new Array<DboServiceAccountServices>();

  constructor(private http: HttpClient) {

    //get service accounts
    this.http.get('/api/users/serviceAccounts').subscribe(result => {
      this.serviceAccounts = result as DboServiceAccount[];
      console.log(this.serviceAccounts);

      //populate service accounts with currently associated services
      for (var i = 0; i < this.serviceAccounts.length; i++) {
        this.getAssosciatedServices(i);
      }
      //set initial service accounts to keep a record of it
      console.log("initialServiceAccounts");
      console.log(this.initialServiceAccounts);

    }, error => console.error(error));

    //get all services
    this.http.get('/api/users/services').subscribe(result => {
      this.services = result as DboService[];
      console.log(this.services);

    }, error => console.error(error));


  }


  ngOnInit() {
    var localUser = JSON.parse(localStorage.getItem('currentUser'));
    this.currentUser = localUser.email;


  }

  getAssosciatedServices(i) {
    console.log(this.serviceAccounts[i].loginName);
    this.http.get('/api/users/associatedServices/' + this.serviceAccounts[i].id).subscribe(result => {
      this.temp = result as DboService[];
      this.serviceAccounts[i].services = this.temp
      //console.log(this.serviceAccounts[i].name);
      console.log(this.serviceAccounts[i]);
      //console.log(this.serviceAccounts[i].services);
      if (i == this.serviceAccounts.length - 1) {
        this.initialServiceAccounts = JSON.parse(JSON.stringify(this.serviceAccounts))
      }
    }, error => console.error(error));
  }

  hasService(serviceAccount: DboServiceAccount, service: DboService) {
    if (serviceAccount.services.map(function (s) { return s.id }).includes(service.id)) { return "checked" }
    return ""
  }
  
  onCheck(e, sA: DboServiceAccount, s: DboService) {
    if (e.target.checked) {
      sA.services.push(s);
      console.log(sA.services)
    } else {
      sA.services = sA.services.filter(service => service.id !== s.id); 
      console.log(sA.services)
    }
  } 

  // fill the list boxes displaying the assosciated service accounts
  getLists() {
    var s = this.selectedService;
    this.withService = [];
    this.withoutService = [];

    for (var serviceAccount of this.serviceAccounts) {
      if (serviceAccount.services.map(function (s) { return s.id }).includes(s.id)) {
        this.withService.push(serviceAccount);
      }
      else {
        this.withoutService.push(serviceAccount);
      }
    }
    console.log(s.serviceName)
  }

  select(sa) {
    this.selectedSA = sa;
    console.log(this.selectedSA)
  }

  addAccount() {
    this.selectedSA.services.push(this.selectedService);
    this.getLists()
  }

  removeAccount() {
    console.log(this.selectedSA)
    console.log(this.selectedService)
    for (var i = 0; i < this.selectedSA.services.length; i++) {
      if (this.selectedSA.services[i].id == this.selectedService.id) {
        this.selectedSA.services.splice(i, 1);
      }
    }
    this.getLists()
  }


  save() {
    console.log(this.serviceAccounts.length)
    for (var i = 0; i < this.serviceAccounts.length; i++) {
      console.log("--------initial---" + this.initialServiceAccounts[i].loginName)

      console.log(this.initialServiceAccounts[i])
      console.log("-----" + this.serviceAccounts[i].loginName)
      console.log(this.serviceAccounts[i])
    }

    for (var i = 0; i < this.serviceAccounts.length; i++) {
      for (var service of this.serviceAccounts[i].services) {
        if (!(this.initialServiceAccounts[i].services.map(function (s) { return s.id }).includes(service.id))) {
          var temp: DboServiceAccountServices = { id: null, serviceAccountId: this.serviceAccounts[i].id, serviceId: service.id, addedBy: this.currentUser }
          console.log(temp)
          this.newSAS.push(temp)
        }
      }
      for (var service of this.initialServiceAccounts[i].services) {
        if (!(this.serviceAccounts[i].services.map(function (s) { return s.id }).includes(service.id))) {
          var temp: DboServiceAccountServices = { id: 0, serviceAccountId: this.serviceAccounts[i].id, serviceId: service.id, addedBy: this.currentUser }
          console.log(temp)
          this.deleteSAS.push(temp)
        }
      }

      if (this.newSAS.length !== 0) {
        this.http.post<any>('/api/users/new-users', this.newSAS).subscribe(result => {
        }, error => console.error(error));

      }

      // post the records to delete
      //if (this.deletedRecords != null) {
      if (this.deleteSAS.length !== 0) {
        this.http.post<any>('/api/users/delete-users', this.deleteSAS).subscribe(result => {
        }, error => console.error(error));

      }

    }
  }
}
