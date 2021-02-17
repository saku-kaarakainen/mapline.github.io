import { GeoJSON } from 'leaflet'

export class SaveLanguage {
  public Name?: string;
  public GeoJson?: GeoJSON;
  public YearStart?: number;
  public YearEnd?: number;
  public Features?: JSON;
  public AdditionalDetails?: JSON;
}
