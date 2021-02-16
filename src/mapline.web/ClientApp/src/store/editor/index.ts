import { Module } from 'vuex'
import { getters } from './getters'
import { actions } from './actions'
import { mutations } from './mutations'
import { EditorState, Language } from './types'
import { RootState } from '../types'
import { LatLngBounds } from 'leaflet'

export const state: EditorState = {
  language: {
    name: "",
    yearRange: [ -3000, 1500 ],
    area: []
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
