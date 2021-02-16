import { Module } from 'vuex'
import { getters } from './getters'
import { actions } from './actions'
import { mutations } from './mutations'
import { EditorState, Language } from './types'
import { RootState } from '../types'

export const state: EditorState = {
  language: {
    yearStart: 0
  }
}

const namespaced = true

export const editor: Module<EditorState, RootState> = {
  namespaced,
  state,
  getters,
  actions,
  mutations
}
