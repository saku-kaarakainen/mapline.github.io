import { MutationTree } from 'vuex'
import { EditorState } from './types'

export const mutations: MutationTree<EditorState> = {
  updateLanguage(state, language) {
    state.language = language;
  }
}
