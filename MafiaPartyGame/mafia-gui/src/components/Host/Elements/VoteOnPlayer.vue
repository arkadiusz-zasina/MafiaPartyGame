<template>
    <div class="voteOnPlayer">
        <div class="playerSection" :style="{ backgroundColor: color }">
            <div class="votesNumber">{{votesWithText}}</div>
            <div class="spacer"/>
            <div class="playerName">
                {{playerName}}
            </div>
        </div>
        <div class="collapsable">
            <div class="arrowSection">
                <img src="@/assets/voting-arrow.png"/>
                <div class="arrowLabel">głosuje na</div>
            </div>
            <div class="votedSection" :style="{ backgroundColor: voteOnPlayerColor }">
            {{ voteOnPlayerName }}
            </div>
        </div>
    </div>
</template>

<script>
export default {
    name: 'VoteOnPlayer',
    props: {
        playerName: String,
        color: String,
        voteOnPlayerName: String,
        voteOnPlayerColor: String,
        numberOfVotes: Number
    },
    computed: {
        votesWithText() {
            let addedText = "";
            console.log(this.numberOfVotes);
            switch (this.numberOfVotes) {
                case 1:
                    addedText = "głos";
                    break;
                case 2: 
                case 3:
                case 4:
                    addedText = "głosy";
                    break;
                default:
                    addedText = "głosów"
            }
            return this.numberOfVotes + " " + addedText;
        }
    }
}
</script>

<style scoped>
    .voteOnPlayer {
        display: flex;
    }

    .playerSection {
        display: flex;
    }
</style>
