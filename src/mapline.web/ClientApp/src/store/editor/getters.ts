import { GetterTree } from 'vuex'
import { EditorState, Language } from './types'
import { RootState } from '../types'

export const getters: GetterTree<EditorState, RootState> = {
  currentLanguage(state): Language {
    return state.language
  }
}
