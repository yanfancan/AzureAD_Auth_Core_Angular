import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MapServicesComponent } from './map-services.component';

describe('MapServicesComponent', () => {
  let component: MapServicesComponent;
  let fixture: ComponentFixture<MapServicesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MapServicesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MapServicesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
