import { ActionTree } from 'vuex'
import { EditorState } from './types'
import { RootState } from '../types'

export const actions: ActionTree<EditorState, RootState> = {
  updateYearRange({ commit }, { yearRange }) {
    commit('updateYearRange', yearRange)
  },

  updateAreaFromLayer({ commit }, { layer }) {
    commit('updateAreaFromLayer', layer)
  }
}
