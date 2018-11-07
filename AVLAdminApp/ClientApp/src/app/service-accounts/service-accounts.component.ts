import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { DboServiceAccount } from '../_models/dboServiceAccount';

@Component({
  selector: 'app-service-accounts',
  templateUrl: './service-accounts.component.html',
  styleUrls: ['./service-accounts.component.css']
})
export class ServiceAccountsComponent implements OnInit {
  public dboServiceAccount: DboServiceAccount;
  public initialValues: DboServiceAccount[];
  public changedRecords: DboServiceAccount[] = new Array<DboServiceAccount>();
  public currentUser;
  p: number = 1;

  title = 'ServiceAccounts';
  //public values: string[];
  public values: DboServiceAccount[];
  public newRecords: DboServiceAccount[] = new Array<DboServiceAccount>();
  public deletedRecords: DboServiceAccount[] = new Array<DboServiceAccount>();
  public vehicles: Object[];
  public filter: any;
  constructor(private http: HttpClient) {
    this.dboServiceAccount = new DboServiceAccount;
    this.http.get('/api/users/serviceAccounts').subscribe(result => {
      this.values = result as DboServiceAccount[];
      this.initialValues = JSON.parse(JSON.stringify(this.values));
      console.log(this.values);
    }, error => console.error(error));
  }
  ngOnInit() {
    var localUser = JSON.parse(localStorage.getItem('currentUser'));
    this.currentUser = localUser.email;
  }


  addRowInline() {

    var temp: DboServiceAccount = { id: this.dboServiceAccount.id, loginName: this.dboServiceAccount.loginName, name: this.dboServiceAccount.name, password: this.dboServiceAccount.password, roles: this.dboServiceAccount.roles, status: this.dboServiceAccount.status, TimeZone: this.dboServiceAccount.TimeZone, AddedBy: this.currentUser, modifiedBy: null, services: null }
    this.newRecords.push(temp);

    this.dboServiceAccount.id = null;
    this.dboServiceAccount.name = null;
    this.dboServiceAccount.password = null;
    this.dboServiceAccount.roles = null;
    this.dboServiceAccount.status = null;
    this.dboServiceAccount.TimeZone = null;
    console.log(this.values);
    console.log(this.newRecords);
  }

  deleteRow(i) {
    if (confirm("Are you sure to delete [ Service Account: " + this.values[i].name + ",  ID: " + this.values[i].id + " ]")) {
      this.deletedRecords.push(this.values[i]);
      this.values.splice(i, 1);
    }
  }
  deleteNew(i) {
    if (confirm("Are you sure to delete [ Service Account: " + this.values[i].name + ",  ID: " + this.values[i].id + " ]")) {
      this.newRecords.splice(i, 1);
    }
  }


  save() {

    console.log("Before Save:");
    console.log(this.values);
    console.log(this.newRecords)

    console.log('Save is run');
    for (var newValue of this.values) {
      if (this.initialValues.map(function (v) { return JSON.stringify(v); }).indexOf(JSON.stringify(newValue)) === -1) {
        newValue.modifiedBy = this.currentUser;
        this.changedRecords.push(newValue);
      }
    }

    // post the changed records
    if (this.changedRecords.length !== 0) {
      //this.http.post<any>('/api/users/update-users', testRecords).subscribe(result => {
      this.http.post<any>('/api/users/update-serviceAccounts', this.changedRecords).subscribe(result => {
        this.values = result as DboServiceAccount[];
      }, error => console.error(error));
    }

    // post the new records
    if (this.newRecords.length !== 0) {
      this.http.post<any>('/api/users/new-serviceAccounts', this.newRecords).subscribe(result => {
        this.values = result as DboServiceAccount[];
      }, error => console.error(error));
    }

    //if (this.deletedRecords != null) {
    if (this.deletedRecords.length !== 0) {
      this.http.post<any>('/api/users/delete-serviceAccounts', this.deletedRecords).subscribe(result => {
        this.values = result as DboServiceAccount[];
      }, error => console.error(error));
    }

    this.newRecords = null;
    console.log("After Save");
    console.log(this.values);
    console.log(this.newRecords)
    //location.reload();
  }

}
