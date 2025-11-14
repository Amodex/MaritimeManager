export interface PointOfInterestInterface {
  id?: number;
  name: string;
  description: string;
  latitude: number;
  longitude: number;
  type: 'Energy Infrastructure' | 'Wreck';
}
