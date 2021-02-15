import { MutationTree } from 'vuex'
import { EditorState } from './types'

export const mutations: MutationTree<EditorState> = {
  saveMap(state) {
    console.log("we are saving...")
    console.log("inside the store. The state is:")
    console.log(state);
  }
}
