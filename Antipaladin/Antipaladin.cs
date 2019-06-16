// Copyright (c) 2019 Jennifer Messerly
// This code is licensed under MIT license (see LICENSE for details)

using System;
using System.Collections.Generic;
using System.Linq;
using Kingmaker;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Blueprints.Facts;
using Kingmaker.Blueprints.Root;
using Kingmaker.Blueprints.Validation;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Class.LevelUp.Actions;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.Utility;
using Newtonsoft.Json;

namespace EldritchArcana
{
    static class AntipaladinClass
    {
        static LibraryScriptableObject library => Main.library;

        internal static BlueprintCharacterClass antipaladin;
        internal static BlueprintCharacterClass[] antipaladinArray;

        internal static void Load()
        {
            if (AntipaladinClass.antipaladin != null) return;

            var paladin = Helpers.GetClass("bfa11238e7ae3544bbeb4d0b92e897ec");
            var cleric = Helpers.GetClass("67819271767a9dd4fbfd4ae700befea0");

            var antipaladin = AntipaladinClass.antipaladin = Helpers.Create<BlueprintCharacterClass>();
            antipaladinArray = new BlueprintCharacterClass[] { antipaladin };
            antipaladin.name = "AntipaladinClass";
            library.AddAsset(antipaladin, "8bdbd26755764d348fc9fab894b8d167");
            antipaladin.LocalizedName = Helpers.CreateString("Antipaladin.Name", "Antipaladin");
            antipaladin.LocalizedDescription = Helpers.CreateString("Antipaladin.Description", "Although it is a rare occurrence, paladins do sometimes stray from the path of righteousness. Most of these wayward holy warriors seek out redemption and forgiveness for their misdeeds, regaining their powers through piety, charity, and powerful magic. Yet there are others, the dark and disturbed few, who turn actively to evil, courting the dark powers they once railed against in order to take vengeance on their former brothers. It's said that those who climb the farthest have the farthest to fall, and antipaladins are living proof of this fact, their pride and hatred blinding them to the glory of their forsaken patrons.\n" +
                "Antipaladins become the antithesis of their former selves. They make pacts with fiends, take the lives of the innocent, and put nothing ahead of their personal power and wealth. Champions of evil, they often lead armies of evil creatures and work with other villains to bring ruin to the holy and tyranny to the weak. Not surprisingly, paladins stop at nothing to put an end to such nefarious antiheroes.\n" +
                "Role: Antipaladins are villains at their most dangerous. They care nothing for the lives of others and actively seek to bring death and destruction to ordered society. They rarely travel with those that they do not subjugate, unless as part of a ruse to bring ruin from within.\n" + 
                "Alignment: Chaotic Evil");
            antipaladin.m_Icon = paladin.Icon;
            antipaladin.SkillPoints = 2;
            antipaladin.HitDie = DiceType.D10;
            antipaladin.BaseAttackBonus = paladin.BaseAttackBonus;
            antipaladin.FortitudeSave = paladin.FortitudeSave;
            antipaladin.ReflexSave = paladin.ReflexSave;
            antipaladin.WillSave = paladin.WillSave;

            var spellbook = Helpers.Create<BlueprintSpellbook>();
            spellbook.name = "AntipaladinSpellbook";
            library.AddAsset(spellbook, "6705c8ea12ca4140a11a4ec0b6b204e2");
            spellbook.Name = antipaladin.LocalizedName;
            var paladinSpellLevels = library.Get<BlueprintSpellsTable>("9aed4803e424ae8429c392d8fbfb88ff");
            spellbook.SpellsPerDay = paladinSpellLevels;
            spellbook.SpellList = paladin.Spellbook.SpellList;
            spellbook.Spontaneous = false;
            spellbook.IsArcane = false;
            spellbook.AllSpellsKnown = true;
            spellbook.CanCopyScrolls = false;
            spellbook.CastingAttribute = StatType.Charisma;
            spellbook.CasterLevelModifier = -3;
            spellbook.CharacterClass = antipaladin;
            antipaladin.Spellbook = spellbook;

            antipaladin.ClassSkills = new StatType[]
            {
                // Antipaladin has Bluff, Intimidate, and Sense Motive
                StatType.SkillPersuasion,
                // Antipaladin has Stealth
                StatType.SkillStealth,
                // Religion Lore
                StatType.SkillLoreReligion,
                // Spellcraft
                StatType.SkillLoreArcana,
                // Disguise
                StatType.SkillLoreWorld
            };

            antipaladin.IsDivineCaster = true;
            antipaladin.IsArcaneCaster = false;
            antipaladin.StartingGold = paladin.StartingGold;
            var rogue = library.Get<BlueprintCharacterClass>("299aa766dee3cbf4790da4efb8c72484");
            antipaladin.PrimaryColor = rogue.PrimaryColor;
            antipaladin.SecondaryColor = rogue.SecondaryColor;

            antipaladin.RecommendedAttributes = paladin.RecommendedAttributes;
            antipaladin.NotRecommendedAttributes = paladin.NotRecommendedAttributes;

            antipaladin.EquipmentEntities = paladin.EquipmentEntities;
            antipaladin.MaleEquipmentEntities = paladin.MaleEquipmentEntities;
            antipaladin.FemaleEquipmentEntities = paladin.FemaleEquipmentEntities;

            var prerequisiteAlignment = Helpers.Create<PrerequisiteAlignment>();
                prerequisiteAlignment.Alignment = Kingmaker.UnitLogic.Alignments.AlignmentMaskType.ChaoticEvil;
                prerequisiteAlignment.name = "$PrerequisiteAlignment$f011d13b-6e58-47aa-9a03-a0ca82ce196e";
            var noAtheist = ;
            var noAnimal = ;
            antipaladin.ComponentsArray = new Component[] { noAnimal, noAtheist, prerequisiteAlignment };
            antipaladin.StartingItems = paladin.StartingItems;

            var progression = Helpers.CreateProgression("AntipaladinProgression",
                antipaladin.Name,
                antipaladin.Description,
                "6d4fa3d7af79422ba2ef5db0940b61ec",
                antipaladin.Icon,
                FeatureGroup.None);
            progression.Classes = antipaladinArray;
            var entries = new List<LevelEntry>();

            var 
        }
    }
}