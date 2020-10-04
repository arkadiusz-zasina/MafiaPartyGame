<template>
  <div class="draw">
    <div class="draw-labels">
      <div class="draw-firstLabel"><div :style="{ color: PlayerTypesColors.AGENT, display: 'inline' }">Dogrywka</div>!</div>
      <div class="draw-secondLabel">Jeżeli nie dojdziecie do porozumienia, wyeliminujemy kogoś <div :style="{ color: PlayerTypesColors.MAFIA, display: 'inline' }">losowo!</div></div>
    </div>
    <div class="draw-possible-votes-container">
      <VoteOnPlayer v-for="p in possibleVotesCounted" :key="p.playerName" 
          :isToggled="p.isToggled" 
          :flexBasis="0" 
          :playerName="p.playerName" 
          :color="p.color"
          :numberOfVotes="p.numberOfVotes"
          :withoutVote="true"
        />
    </div>
    <div class="draw-votes-container">
      <div class="spacer"/>
      <div class="draw-votes">
        <VoteOnPlayer v-for="p in votes" :key="p.playerName" 
          :isToggled="p.isToggled" 
          :flexBasis="flexBasisValue" 
          :playerName="p.playerName" 
          :color="p.color" 
          :voteOnPlayerName="p.voteOnPlayerName" 
          :voteOnPlayerColor="p.voteOnPlayerColor" 
          :numberOfVotes="0"
          :withoutNumberOfVotes="true"
        />
      </div>
      <div class="spacer"/>
    </div>
  </div>
</template>

<script>
import { PlayerTypesColors } from "../../enums/PlayerTypesColors";
import VoteOnPlayer from "./Elements/VoteOnPlayer"
export default {
  name: 'Draw',
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
    possibleVotes() {
      return this.$store.state.Voting.votingDrawPossibleVotes;
    },
    votingMain() {
      return this.$store.state.Voting.votingMain;
    },
    votes() {
      return this.playersList.filter(x => !this.possibleVotes.filter(y => {console.log(x); console.log(y); return y.name == x.name}).length > 0).map((player) => {
        const isToggled = this.votingMain.filter(x => x.voting.name == player.name).length > 0;
        return {
          isToggled: isToggled,
          playerName: player.name,
          color: player.color,
          voteOnPlayerName: isToggled ? this.votingMain.find(x => x.voting.name == player.name).voted.name : '',
          voteOnPlayerColor:  isToggled ? this.votingMain.find(x => x.voting.name == player.name).voted.color : '',
          numberOfVotes: this.votingMain.filter(x => x.voted.name == player.name).length
        }
      });
    },
    possibleVotesCounted() {
      return this.possibleVotes.map((player) => {
        return {
          isToggled: false,
          playerName: player.name,
          color: player.color,
          numberOfVotes: this.votingMain.filter(x => x.voted.name == player.name).length
        }
      });
    }
  },
  created() {
    
  }
}
</script>

<style scoped>

  .draw {
    display: flex;
    flex-direction: column;
    width: 100%;

    animation: slideIn .3s;
  }

  .draw-votes-container {
    display: flex;
    flex-direction: column;
    flex: 1;
    overflow: auto;
  }

  .draw-possible-votes-container {
    display: flex;
    justify-content: center;
    flex-wrap: wrap;
    margin-top: 2rem;
  }
  
  .draw-possible-votes-container > div {
    margin-left: 2rem;
    margin-right: 2rem;
  }

  .spacer {
    flex: 1;
  }

  .draw-votes {

    width: 100%;
    display: flex;
    flex-direction: row;
    flex-wrap: wrap;

    justify-content:space-around;

    overflow: auto;
  }

  .draw-labels {
    text-align: right;
    margin-top: 3rem;
    margin-right: 4rem; 
  }
  .draw-firstLabel {
    font-size: 3rem;
  }

  .draw-secondLabel {
    font-size: 2rem;
  }

.flip-list-item {
   transition: all 1s;
}
</style>
