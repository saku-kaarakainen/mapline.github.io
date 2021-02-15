import { GetterTree } from 'vuex'
import { EditorState } from './types'
import { RootState } from '../types'

export const getters: GetterTree<EditorState, RootState> = {
  yearStart(state): number {
    console.log("TODO: What to do here.  The state:");
    console.log(state);

    return state.yearStart
  }
}
