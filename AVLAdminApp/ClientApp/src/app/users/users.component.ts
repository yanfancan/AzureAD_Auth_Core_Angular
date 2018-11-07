import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { DboUser } from '../_models/dboUser';
import { FormControl } from '@angular/forms';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent implements OnInit {
  public currentUser;
  p: number = 1;
  public dboUser: DboUser;
  title = 'AVLAdminClient';
  public values: DboUser[];
  public initialValues: DboUser[];
  public changedRecords: DboUser[] = new Array<DboUser>();
  public newRecords: DboUser[] = new Array<DboUser>();
  public deletedRecords: DboUser[] = new Array<DboUser>();
  public vehicles: Object[];
  public filter: any;

  constructor(private http: HttpClient) {
    this.dboUser = new DboUser;
    this.dboUser.accountLocked = false;
    this.dboUser.accountDisabled = false;


    this.http.get('/api/users/users').subscribe(result => {
      this.values = result as DboUser[];
      this.initialValues = JSON.parse(JSON.stringify(this.values))
    }, error => console.error(error));
  }
  ngOnInit() {
    var localUser = JSON.parse(localStorage.getItem('currentUser'));
    this.currentUser = localUser.email;
  }


  addUser() {
    var currentTime = new Date();
    var temp: DboUser = { id: this.dboUser.id, firstName: this.dboUser.firstName, lastName: this.dboUser.lastName, department: this.dboUser.department, email: this.dboUser.email, position: this.dboUser.position, accessLevel: this.dboUser.accessLevel, password: this.dboUser.password, accountLocked: this.dboUser.accountLocked, accountDisabled: this.dboUser.accountDisabled, addedBy: this.currentUser, modifiedDateTime: currentTime, modifiedBy: this.currentUser  }
    this.newRecords.push(temp);

    //this.dboUser.id = null;
    this.dboUser.firstName = null;
    this.dboUser.lastName = null;
    this.dboUser.department = null;
    this.dboUser.email = null;
    this.dboUser.position = null;
    this.dboUser.accessLevel = null;
    this.dboUser.password = null;
    this.dboUser.accountLocked = false;
    this.dboUser.accountDisabled = false;

    console.log(this.values);
    console.log(this.newRecords);
  }

  deleteRow(i) {
    if (confirm("Are you sure to delete [ User: " + this.values[i].firstName + ",  ID: " + this.values[i].id + " ]")) {
      this.deletedRecords.push(this.values[i]);
      this.values.splice(i, 1);
    }
  }
  deleteNew(i) {
    if (confirm("Are you sure to delete [ User: " + this.values[i].firstName + ",  ID: " + this.values[i].id + " ]")) {
      this.newRecords.splice(i, 1);
    }
  }


  save() {
    console.log('Save is run');
    //check for changed records
    /*for (var i = 0; i < this.initialValues.length; i++) {
      console.log(JSON.stringify(this.values[i]) === JSON.stringify(this.initialValues[i]));
     if (JSON.stringify(this.values[i]) !== JSON.stringify(this.initialValues[i])) {
        this.changedRecords.push(this.values[i]);
      }
      
    }*/
    for (var newValue of this.values) {
      if (this.initialValues.map(function (v) { return JSON.stringify(v); }).indexOf(JSON.stringify(newValue)) === -1) {
        newValue.modifiedBy = this.currentUser;
        this.changedRecords.push(newValue);
      }

    }

    // post the changed records
    if (this.changedRecords.length !== 0) {
      //this.http.post<any>('/api/users/update-users', testRecords).subscribe(result => {
      this.http.post<any>('/api/users/update-users', this.changedRecords).subscribe(result => {
        this.values = result as DboUser[];
      }, error => console.error(error));
    }

    // post the new records
    if (this.newRecords.length !== 0) {
      this.http.post<any>('/api/users/new-users', this.newRecords).subscribe(result => {
        this.values = result as DboUser[];
      }, error => console.error(error));

    }
    // post the records to delete
    //if (this.deletedRecords != null) {
    if (this.deletedRecords.length !== 0) {
      this.http.post<any>('/api/users/delete-users', this.deletedRecords).subscribe(result => {
        this.values = result as DboUser[];
      }, error => console.error(error));

    }

    this.newRecords = new Array<DboUser>();

    //this.newRecords = null;
    location.reload();
  }




  

  test() {
    console.log("changed records: " + this.changedRecords);
    console.log(this.changedRecords);
    console.log("initial records: " + this.changedRecords);
    console.log(this.initialValues);
    console.log("records records: " + this.changedRecords);
    console.log(this.values);
  }

}
