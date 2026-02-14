using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SolutionOne : MonoBehaviour
{
    [Header("Character Sheet")]
    public string characterName;
    [Range(1,20)] // only allows values 1 to 20 inclusive for level
    public int level;
    [Range(1,30)] // only allows values 1 to 30 inclusive for con
    public int con;
    
    // establishes a drop down selection for race selection in Inspector
    public enum races 
    {
        Aasimar,
        Dragonborn, 
        Dwarf,
        Elf,
        Gnome,
        Goliath,
        Halfling,
        Human,
        Orc,
        Tiefling
    }
    public races race;
    // dictionary associates bonus hit points per level to player race
    Dictionary<races, int> selectedRace = new Dictionary<races, int>()
    {
        {races.Aasimar, 0},
        {races.Dragonborn, 0}, 
        {races.Dwarf, 2},
        {races.Elf, 0},
        {races.Gnome, 0},
        {races.Goliath, 1},
        {races.Halfling, 0},
        {races.Human, 0},
        {races.Orc, 1},
        {races.Tiefling, 0}
    };
    
    // establishes a drop down selection for player class in Inspector
    public enum classes 
    {   Artificer,
        Barbarian,
        Bard,
        Cleric,
        Druid,
        Fighter,
        Monk,
        Ranger,
        Rogue,
        Paladin,
        Sorcerer,
        Wizard,
        Warlock
    }
    public classes playerClass;
    // dictionary associates hit dice to player class example: Artificer - D8
    Dictionary<classes, int> selectedClass = new Dictionary<classes, int>()
    {
       {classes.Artificer, 8},
       {classes.Barbarian, 12},
       {classes.Bard, 8},
       {classes.Cleric, 8},
       {classes.Druid, 8},
       {classes.Fighter, 10},
       {classes.Monk, 8},
       {classes.Ranger, 10},
       {classes.Rogue, 8},
       {classes.Paladin, 10},
       {classes.Sorcerer, 6},
       {classes.Wizard, 6},
       {classes.Warlock, 8}
    };

    // establishes a drop down selection for hit point traits in Inspector
    public enum traitsHP 
    {
        None, 
        Stout, 
        Tough
    }
    public traitsHP trait;
    // dictionary associates HP gain per level to HP trait
    Dictionary<traitsHP, int> selectedTrait = new Dictionary<traitsHP, int>()
    {
        {traitsHP.None, 0},
        {traitsHP.Stout, 1}, 
        {traitsHP.Tough, 2}
    };

    // establishes a drop down selection for how dice are treated in Inspector
    public enum diceBehavior {Averaged, Random};
    public diceBehavior dice;

    void Start()
    {
        int totalHP = RollDie(level);
        FinalHP(totalHP);
    }

    public int RollDie(int level)
    {
        // Function that rolls die based on how many levels
        // loopNum counts how many times the loop runs
        // This loop stops when the players level is equal to loopNum
        // This loop adds 1 to loopNum after each run
        // This loop starts at 0
        // Class die type
        // Roll die Averaged
        // Roll die Rolled

        int totalHP = 0;

        if (dice == diceBehavior.Averaged)
        {
             // Roll die Averaged (1+N)/2 gives average die value
             // having totalHP as an int will truncate odd numbers divided by 2 (round down)
            totalHP = level * ((selectedClass[playerClass]) + 1) / 2;
            // Debug.Log(selectedClass[playerClass] + " " + totalHP);
        }
        else
        {
            // Role die Random
            int loopNum;
            for (loopNum=0; loopNum<level; loopNum++)
            {
                int roll = Random.Range(1,(selectedClass[playerClass]) + 1);
                totalHP += roll;
                // Debug.Log(selectedClass[playerClass] + " " + roll + " " + totalHP);
            }
        }
        return totalHP;
    }

    public void FinalHP(int totalHP)
    {
        // This function will print the final HP with everything added up
        // This function is named FinalHP

        totalHP = ((con/2)-5)*level + totalHP + (selectedRace[race])*level + (selectedTrait[trait])*level;
        
        if (totalHP < 1) totalHP = 1; // sets HP to 1 if low con results in HP less than 1.
        
        Debug.Log($"Welcome {characterName}! Your starting HP is : {totalHP}");
        return;
    }
}