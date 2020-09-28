<template>
    <div class="voteOnPlayer" :style="{ flexBasis: flexBasis + '%' }">
        <div class="playerSection" :style="{ backgroundColor: '#' + color }">
            <div class="votesNumber">{{votesWithText}}</div>
            <div class="spacer"/>
            <div class="playerName">
                {{playerName}}
            </div>
        </div>
        <div :class="collapsableClass">
            <div class="arrowSection">
                <img class="arrowImg" src="@/assets/voting-arrow.png"/>
                <div class="arrowLabel">głosuje na</div>
            </div>
            <div class="votedSection" :style="{ backgroundColor: '#' + voteOnPlayerColor }">
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
        numberOfVotes: Number,
        isToggled: Boolean,
        flexBasis: Number
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
        },

        collapsableClass() {
            return this.isToggled ? 'collapsable' : 'collapsable-hidden';
        }
    }
}
</script>

<style scoped>
    .voteOnPlayer {
        display: flex;
        align-items: center;
        margin-top: .1rem;
        margin-bottom: .1rem;
        width: 100%;

        justify-content: center;

        margin-top: 0.3rem;
        margin-bottom: 0.3rem;

    }

    .votedSection {
        font-size: 1.7rem;
        color: white;
        padding-top: .3rem;
        padding-bottom: .3rem;
        border-radius: .5rem;

        width: 12rem;
        text-align: center;
        
    }

    .playerSection {

        display: flex;
        align-items: center;
        padding-right: 1rem;
        font-size: 2rem;
        width: 20rem;
        max-width: 25rem;
        border-radius: 1rem;
        overflow: hidden;
    }

    .playerName {
        color: white;
        flex: 0;
    }

    .votesNumber {
        background-color: #bedcf2;
        color: #1d5d8f;
        height: 100%;
        padding-top: .5rem;
        padding-bottom: .5rem;
        padding-left: 1rem;
        min-width: 8rem;
        display: flex;
        align-items: center;
        flex: 0;
    }

    .collapsable,
    .collapsable-hidden {
        display: flex;
        align-items: center;
        transition: transform 0.5s cubic-bezier(.7,0,.3,1), opacity 0.5s ease-in-out;
    }

    .collapsable {
        opacity: 1;
    }

    .collapsable-hidden {
        display: flex;
        align-items: center;
        opacity: 0;
        transform: translateX(-2rem);
    }

    .arrowSection {
        position: relative;
        margin-left: 2rem;
        margin-right: 2rem;
    }

    .arrowLabel {
        font-size: 0.8rem;

        color: white;

        position: absolute;
        top: 50%;
        left: 10%;
        margin-top: -.75rem;
    }

    .arrowImg {
        max-width: 7rem;
    }

    .spacer {
        flex: 1;
    }
</style>
