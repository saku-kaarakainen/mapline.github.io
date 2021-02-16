import { MutationTree } from 'vuex'
import { CounterState } from './types'

export const mutations: MutationTree<CounterState> = {
  incrementCounter(state) {
    console.log("inside the store")

    state.counter++
  },
  resetCounter(state) {
    state.counter = 0
  }
}
