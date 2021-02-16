import { MutationTree } from 'vuex'
import { EditorState } from './types'

export const mutations: MutationTree<EditorState> = {
  addLanguage(state) {
    console.log("add language. the state")
    console.log(state);

    state.editor++
  }
}
