export interface PointOfInterestInterface {
  identifier: string;
  name: string;
  description: string;
  latitude: number;
  longitude: number;
  type: 'Energy Infrastructure' | 'Wreck';
}
