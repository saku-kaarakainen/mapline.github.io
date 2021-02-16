export interface Language {
  yearRange: Array<number>
    // [0]: yearStart
    // [1]: yearEnd
}

export interface EditorState {
  language: Language
}
