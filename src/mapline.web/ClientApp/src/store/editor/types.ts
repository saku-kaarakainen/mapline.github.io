import { GeoJSON } from 'leaflet';

export interface Language {
  name: string
  yearRange: Array<number>
    // [0]: yearStart
    // [1]: yearEnd
  area: GeoJSON
}

export interface EditorState {
  language: Language
}
