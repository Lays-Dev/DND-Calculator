using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class SolutionTwo : MonoBehaviour
{
    [Header("Character Sheet")]
    public Character myCharacter;
    public diceBehavior dice;

    // instantiate the struct to access the dictionaries
    private CharacterData data = new CharacterData();

    
    // establishes races types
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
  
    
    // Establishes classes character can have
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
   

    // Establishes traits character can have
    public enum traitsHP 
    {
        None, 
        Stout, 
        Tough
    }


    // Establishes dice behavior options
    public enum diceBehavior {Averaged, Random};
 

    //level * dice + conBonus * level + feat * level + race * level
    void Start()
    {
        
        int totalHP = RollDie(myCharacter.level);
        FinalHP(totalHP);
    }

    public int RollDie(int level)
    {
      
        int totalHP = 0;
        // Accessing the dictionary via the struct
        int dieType = data.selectedClass.ContainsKey(myCharacter.playerClass) ? data.selectedClass[myCharacter.playerClass] : 8;


        // Function that rolls die based on how many levels
        if (dice == diceBehavior.Averaged)
        {
    
            totalHP = level * (dieType + 1) / 2;
           
        }
      
        else
        {
            for (int i = 0; i < level; i++)
            {
                totalHP += Random.Range(1, dieType + 1);
            }
          
        } 
        return totalHP;
    }

    // This function will print the final HP with everything added up
    // This function is named finalHP

    public void FinalHP(int totalHP)
    {
        int conBonus = ((myCharacter.con / 2) - 5) * myCharacter.level;
        int raceBonus = (data.selectedRace.ContainsKey(myCharacter.race) ? data.selectedRace[myCharacter.race] : 0) * myCharacter.level;
        int traitBonus = data.selectedTrait[myCharacter.trait] * myCharacter.level;

        totalHP += conBonus + raceBonus + traitBonus;
        
        if (totalHP < 1) totalHP = 1;

        Debug.Log($"Welcome {myCharacter.characterName}! Your starting HP is : {totalHP}");
        return;
    }
}


// Struct for data managment
public struct CharacterData
{
    public Dictionary<SolutionTwo.traitsHP, int> selectedTrait;
    public Dictionary<SolutionTwo.races, int> selectedRace;
    public Dictionary<SolutionTwo.classes, int> selectedClass;

    // Constructor to fill the dictionaries
    public CharacterData(bool initialize = true)
    {
        selectedTrait = new Dictionary<SolutionTwo.traitsHP, int>()
        {
            {SolutionTwo.traitsHP.None, 0}, {SolutionTwo.traitsHP.Stout, 1}, {SolutionTwo.traitsHP.Tough, 2}
        };

        selectedRace = new Dictionary<SolutionTwo.races, int>()
        {
            {SolutionTwo.races.Dwarf, 2}, 
            {SolutionTwo.races.Goliath, 1}, 
            {SolutionTwo.races.Orc, 1},
            {SolutionTwo.races.Aasimar, 0},
            {SolutionTwo.races.Dragonborn, 0}, 
            {SolutionTwo.races.Elf, 0},
            {SolutionTwo.races.Gnome, 0},
            {SolutionTwo.races.Halfling, 0},
            {SolutionTwo.races.Human, 0},
            {SolutionTwo.races.Tiefling, 0}
        };

        selectedClass = new Dictionary<SolutionTwo.classes, int>()
        {
            {SolutionTwo.classes.Artificer, 8}, 
            {SolutionTwo.classes.Barbarian, 12}, 
            {SolutionTwo.classes.Fighter, 10},
            {SolutionTwo.classes.Wizard, 6},
            {SolutionTwo.classes.Bard, 8},
            {SolutionTwo.classes.Cleric, 8},
            {SolutionTwo.classes.Druid, 8},
            {SolutionTwo.classes.Monk, 8},
            {SolutionTwo.classes.Ranger, 10},
            {SolutionTwo.classes.Rogue, 8},
            {SolutionTwo.classes.Paladin, 10},
            {SolutionTwo.classes.Sorcerer, 6},
            {SolutionTwo.classes.Warlock, 8}
        };
    }
}

// The Character class must be placed below the monobehavior class
// Tells Unity to display the class in the inspector
[System.Serializable]

//Declares a new blueprint named Character
public class Character
{
    public string characterName;
    // This struct groups stats together
    // Range is a slider for values
    [Range(1, 20)] public int level;
    [Range(1, 30)] public int con;
    // Selections for character
    public SolutionTwo.races race;
    public SolutionTwo.classes playerClass;
    public SolutionTwo.traitsHP trait;
}
