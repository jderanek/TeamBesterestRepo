using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PersonalityTags {

    public enum Tag
    {
        BLOODTHIRSTY,
        VIOLENT,
        SPOOKY,
        ANCIENT,
        SICKLY,
        GREEDY,
        HEAVY,
        TINY,
        WEAK,
        AGGRESSIVE,
        BURLY,
        POSITIVE,
        MOTIVATED,
        SOCIAL,
        NEGATIVE,
        LAZY,
        MEAN,
        FRIENDLY,
        DEFENSIVE,
        WILD,
        GROSS,
        ANTISOCIAL,
        ANGRY,
        COWARDLY,
        HELPFUL,
        DILIGENT,
        DETAILED,
        ENDURING,
        THREATENING,
        TALKATIVE,
        KEEN,
        HARDWORKING,
        EGOCENTRIC,
        INDEPENDENT,
        STRONG,
        OLDFASHIONED,
        FOLLOWER,
        PROUD,
        NULL
    }

    public static Tag StringToTag(string input)
    {
        input = input.ToUpper().Trim('-','_',' ');

        switch (input)
        {
            case "BLOODTHIRSTY":
                return Tag.BLOODTHIRSTY;
            case "VIOLENT":
                return Tag.VIOLENT;
            case "SPOOKY":
                return Tag.SPOOKY;
            case "ANCIENT":
                return Tag.ANCIENT;
            case "SICKLY":
                return Tag.SICKLY;
            case "GREEDY":
                return Tag.GREEDY;
            case "HEAVY":
                return Tag.HEAVY;
            case "TINY":
                return Tag.TINY;
            case "WEAK":
                return Tag.WEAK;
            case "AGGRESSIVE":
                return Tag.AGGRESSIVE;
            case "BURLY":
                return Tag.BURLY;
            case "POSITIVE":
                return Tag.POSITIVE;
            case "MOTIVATED":
                return Tag.MOTIVATED;
            case "SOCIAL":
                return Tag.SOCIAL;
            case "NEGATIVE":
                return Tag.NEGATIVE;
            case "LAZY":
                return Tag.LAZY;
            case "MEAN":
                return Tag.MEAN;
            case "FRIENDLY":
                return Tag.FRIENDLY;
            case "DEFENSIVE":
                return Tag.DEFENSIVE;
            case "WILD":
                return Tag.WILD;
            case "GROSS":
                return Tag.GROSS;
            case "ANTISOCIAL":
                return Tag.ANTISOCIAL;
            case "ANGRY":
                return Tag.ANGRY;
            case "COWARDLY":
                return Tag.COWARDLY;
            case "HELPFUL":
                return Tag.HELPFUL;
            case "DILIGENT":
                return Tag.DILIGENT;
            case "DETAILED":
                return Tag.DETAILED;
            case "ENDURING":
                return Tag.ENDURING;
            case "THREATENING":
                return Tag.THREATENING;
            case "TALKATIVE":
                return Tag.TALKATIVE;
            case "KEEN":
                return Tag.KEEN;
            case "HARDWORKING":
                return Tag.HARDWORKING;
            case "EGOCENTRIC":
                return Tag.EGOCENTRIC;
            case "INDEPENDENT":
                return Tag.INDEPENDENT;
            case "STRONG":
                return Tag.STRONG;
            case "OLDFASHIONED":
                return Tag.OLDFASHIONED;
            case "FOLLOWER":
                return Tag.FOLLOWER;
            case "PROUD":
                return Tag.PROUD;
            default:
                Debug.LogWarning("Tag not found");
                return Tag.NULL;
        }
    }
}
