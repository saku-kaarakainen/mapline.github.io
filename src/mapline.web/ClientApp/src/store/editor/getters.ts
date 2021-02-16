import { GetterTree } from 'vuex'
import { EditorState } from './types'
import { RootState } from '../types'

export const getters: GetterTree<EditorState, RootState> = {
  currentCount(state): number {
    return state.editor
  }
}
