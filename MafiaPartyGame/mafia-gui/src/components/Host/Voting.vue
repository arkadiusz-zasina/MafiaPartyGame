<template>
  <div class="voting">
    <div class="voting-labels">
      <div class="voting-firstLabel">Czas na <div :style="{ color: PlayerTypesColors.AGENT, display: 'inline' }">głosowanie</div>!</div>
      <div class="voting-secondLabel">Kto jest <div :style="{ color: PlayerTypesColors.MAFIA, display: 'inline' }">mafią</div>?</div>
    </div>
    <div class="voting-votes-container">
      <div class="spacer"/>
      <transition-group class="voting-votes" tag="div" name="flip-list">
        <VoteOnPlayer class="flip-list-item" v-for="p in votes" :key="p.playerName" 
          :isToggled="p.isToggled" 
          :flexBasis="flexBasisValue" 
          :playerName="p.playerName" 
          :color="p.color" 
          :voteOnPlayerName="p.voteOnPlayerName" 
          :voteOnPlayerColor="p.voteOnPlayerColor" 
          :numberOfVotes="p.numberOfVotes ? p.numberOfVotes : 0"
        />
      </transition-group>
      <div class="spacer"/>
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
    },
    flexBasisValue() {
      var val = Math.floor(this.playersList.length / 7) + 1;
      var basisValue = 100;
      basisValue /= val;
      return basisValue;
    },
    playersList() {
      return this.$store.state.Players.playersList.filter(x => x.isAlive);
    },
    votingMain() {
      return this.$store.state.Voting.votingMain;
    },
    votes() {
      return this.playersList.map((player) => {
        const isToggled = this.votingMain.filter(x => x.voting.name == player.name).length > 0;
        return {
          isToggled: isToggled,
          playerName: player.name,
          color: player.color,
          voteOnPlayerName: isToggled ? this.votingMain.find(x => x.voting.name == player.name).voted.name : '',
          voteOnPlayerColor:  isToggled ? this.votingMain.find(x => x.voting.name == player.name).voted.color : '',
          numberOfVotes: this.votingMain.filter(x => x.voted.name == player.name).length
        }
      }).sort((a, b) => b.numberOfVotes - a.numberOfVotes);
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

  .voting-votes-container {
    display: flex;
    flex-direction: column;
    flex: 1;
    overflow: auto;
  }

  .spacer {
    flex: 1;
  }

  .voting-votes {

    width: 100%;
    display: flex;
    flex-direction: row;
    flex-wrap: wrap;

    justify-content:space-around;

    overflow: auto;
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

.flip-list-item {
   transition: all 1s;
}
</style>
