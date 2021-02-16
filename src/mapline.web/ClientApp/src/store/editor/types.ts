import { LatLng } from 'leaflet';

export interface Language {
  yearRange: Array<number>
    // [0]: yearStart
    // [1]: yearEnd
  area: Array<LatLng>
}

export interface EditorState {
  language: Language
}
