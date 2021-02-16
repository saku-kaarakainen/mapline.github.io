import { MutationTree } from 'vuex'
import { EditorState } from './types'

export const mutations: MutationTree<EditorState> = {
  updateYearRange(state, yearRange) {
    state.language.yearRange = yearRange;
  },

  updateAreaFromLayer(state, layer) {
    state.language.area = layer.getLatLngs();
  }
}
