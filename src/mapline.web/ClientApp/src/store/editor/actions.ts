import { ActionTree } from 'vuex'
import { EditorState } from './types'
import { RootState } from '../types'

export const actions: ActionTree<EditorState, RootState> = {
  updateEditorData({ commit }, { yearRange }) {
    commit('updateEditorData', yearRange)
  },

  updateAreaFromLayer({ commit }, { layer }) {
    commit('updateAreaFromLayer', layer)
  }
}
