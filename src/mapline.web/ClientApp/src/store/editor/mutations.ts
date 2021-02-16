import { MutationTree } from 'vuex'
import { EditorState } from './types'

export const mutations: MutationTree<EditorState> = {
  incrementEditor(state) {
    console.log("inside the store. the state")
    console.log(state);

    state.editor++
  }
}
