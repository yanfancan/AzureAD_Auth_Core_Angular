import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { DboDevice } from '../_models/dboDevice';
import { environment } from '../../environments/environment';

@Component({
  selector: 'app-units',
  templateUrl: './units.component.html',
  styleUrls: ['./units.component.css']
})
export class UnitsComponent implements OnInit {
  public dboDevice: DboDevice;
  public initialValues: DboDevice[];
  public changedRecords: DboDevice[] = new Array<DboDevice>();
  public currentUser;
  p: number = 1;
  public values: DboDevice[];
  public newRecords: DboDevice[] = new Array<DboDevice>();
  public deletedRecords: DboDevice[] = new Array<DboDevice>();
  public filter: any;
  private apiURLRoot: string = environment.apiURLRoot;

  constructor(private http: HttpClient) {
    this.dboDevice = new DboDevice;

    this.http.get('/api/devices/devices').subscribe(result => {
      this.values = result as DboDevice[];;
      console.log(this.values);
      this.initialValues = JSON.parse(JSON.stringify(this.values));
    }, error => console.error(error));
  }

  ngOnInit() {
    var localUser = JSON.parse(localStorage.getItem('currentUser'));
    this.currentUser = localUser.email;
  }

  addRow() {

    var temp: DboDevice = { id: this.dboDevice.id, address: this.dboDevice.address, vehicleId: this.dboDevice.vehicleId, vehicleName: this.dboDevice.vehicleName, deviceType: this.dboDevice.deviceType, serviceId: this.dboDevice.serviceId, addedBy: this.currentUser, modifiedBy: null }
    this.newRecords.push(temp);

    this.dboDevice.id = null;
    this.dboDevice.address = null;
    this.dboDevice.vehicleId = null;
    console.log(this.values);
    console.log(this.newRecords);
  }

  deleteRow(i) {
    if (confirm("Are you sure to delete [ Device: " + this.values[i].id + ",  Unit: " + this.values[i].vehicleId + " ]")) {
      this.deletedRecords.push(this.values[i]);
      this.values.splice(i, 1);
    }
  }
  deleteNew(i) {
    if (confirm("Are you sure to delete [ Device: " + this.values[i].id + ",  Unit: " + this.values[i].vehicleId + " ]")) {
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
      this.http.post<any>('/api/devices/update-devices', this.changedRecords).subscribe(result => {
        this.values = result as DboDevice[];
      }, error => console.error(error));
    }

    // post the new records
    if (this.newRecords.length !== 0) {
      this.http.post<any>('/api/devices/new-devices', this.newRecords).subscribe(result => {
        this.values = result as DboDevice[];
      }, error => console.error(error));
    }

    //if (this.deletedRecords != null) {
    if (this.deletedRecords.length !== 0) {
      this.http.post<any>('/api/devices/delete-devices', this.deletedRecords).subscribe(result => {
        this.values = result as DboDevice[];
      }, error => console.error(error));
    }

    this.newRecords = null;
    console.log("After Save");
    console.log(this.values);
    console.log(this.newRecords)
    //location.reload();
  }

}
