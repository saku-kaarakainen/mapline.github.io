import { MutationTree } from 'vuex'
import { EditorState } from './types'

export const mutations: MutationTree<EditorState> = {
  // Validate parameters before!
  // just passing a value "-" in yearRange, will crash this...
  updateEditorData(state, payLoad) {
    state.language.name = payLoad.name;
    state.language.yearRange = payLoad.yearRange;    
  },

  updateAreaFromLayer(state, layer) {
    state.language.area = layer.toGeoJSON();
  }
}
