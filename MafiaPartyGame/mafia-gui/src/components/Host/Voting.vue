<template>
  <div class="voting">
    <div class="voting-labels">
      <div class="voting-firstLabel">Czas na <div :style="{ color: PlayerTypesColors.AGENT, display: 'inline' }">głosowanie</div>!</div>
      <div class="voting-secondLabel">Kto jest <div :style="{ color: PlayerTypesColors.MAFIA, display: 'inline' }">mafią</div>?</div>
    </div>
    <div class="voting-votes">
      <VoteOnPlayer playerName="Arek" color="eb4034" voteOnPlayerName="Basia" voteOnPlayerColor="eb4034" :numberOfVotes=3 />
      <VoteOnPlayer playerName="Arek" color="eb4034" voteOnPlayerName="Basia" voteOnPlayerColor="eb4034" :numberOfVotes=3 />
      <VoteOnPlayer playerName="Arek" color="eb4034" voteOnPlayerName="Basia" voteOnPlayerColor="eb4034" :numberOfVotes=3 />
      <VoteOnPlayer playerName="Arek" color="eb4034" voteOnPlayerName="Basia" voteOnPlayerColor="eb4034" :numberOfVotes=3 />
    </div>
  </div>
</template>

<script>
import { PlayerTypesColors } from "../../enums/PlayerTypesColors";
import VoteOnPlayer from "./Elements/VoteOnPlayer"
export default {
  name: 'Voting',
  components: {
    VoteOnPlayer
  },
  computed: {
    PlayerTypesColors() {
      return PlayerTypesColors;
    }
  }
}
</script>

<style scoped>

  .voting {
    display: flex;
    flex-direction: column;
    width: 100%;
  }

  .voting-votes {
    flex: 1;
  }

  .voting-labels {
    text-align: right;
    margin-top: 3rem;
    margin-right: 4rem; 
  }
  .voting-firstLabel {
    font-size: 3rem;
  }

  .voting-secondLabel {
    font-size: 2rem;
  }
</style>
