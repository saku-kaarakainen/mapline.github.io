import { MutationTree } from 'vuex'
import { EditorState } from './types'

export const mutations: MutationTree<EditorState> = {
  updateEditorData(state, payLoad) {
    console.log("payLoad");
    console.log(payLoad);
    state.language.name = payLoad.name;
    state.language.yearRange = payLoad.yearRange;    
  },

  updateAreaFromLayer(state, layer) {
    state.language.area = layer.toGeoJSON();
  }
}
