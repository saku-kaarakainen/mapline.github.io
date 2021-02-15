import { Module } from 'vuex'
import { getters } from './getters'
import { actions } from './actions'
import { mutations } from './mutations'
import { EditorState } from './types'
import { RootState } from '../types'

export const state: EditorState = {
  yearStart: 0,
  yearEnd: 0,
  name: ""//,
  //geoJson: { }
}

const namespaced = true

export const editor: Module<EditorState, RootState> = {
  namespaced,
  state,
  getters,
  actions,
  mutations
}
