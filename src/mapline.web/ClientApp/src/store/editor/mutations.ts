import { MutationTree } from 'vuex'
import { EditorState } from './types'

export const mutations: MutationTree<EditorState> = {
  incrementEditor(state) {
    console.log("inside the store")

    state.editor++
  },
  resetEditor(state) {
    state.editor = 0
  }
}
