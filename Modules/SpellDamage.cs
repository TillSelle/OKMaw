using Oasys.Common.Enums.GameEnums;
using Oasys.Common.GameObject;
using Oasys.Common.GameObject.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ok_Maw.Modules;
using Oasys.SDK;
using Oasys.Common.GameObject.Clients.ExtendedInstances.Spells;
using Oasys.Common.GameObject.ObjectClass;
using Oasys.SDK.Tools;

namespace Ok_Maw.Modules.Spells
{
    internal enum UsedStats
    {
        Total = 0,
        Bonus = 1,
        Base = 2
    }

    internal enum DamageType
    {
        AD = 0,
        AP = 1,
        Mixed = 2,
        True = 3
    }

    internal class _Spell
    {
        
    }

    internal class BaseChamp
    {
        internal float[] QADScalingPerLevel;
        internal float[] QBonusADScalingPerLevel;
        internal float[] QbaseADScalingPerLevel;
        internal float[] QAPScalingPerLevel;
        internal float[] QBaseDamagePerLevel;
        internal float[] WADScalingPerLevel;
        internal float[] WBonusADScalingPerLevel;
        internal float[] WbaseADScalingPerLevel;
        internal float[] WAPScalingPerLevel;
        internal float[] WBaseDamagePerLevel;
        internal float[] EADScalingPerLevel;
        internal float[] EBonusADScalingPerLevel;
        internal float[] EbaseADScalingPerLevel;
        internal float[] EAPScalingPerLevel;
        internal float[] EBaseDamagePerLevel;
        internal float[] RADScalingPerLevel;
        internal float[] RBonusADScalingPerLevel;
        internal float[] RbaseADScalingPerLevel;
        internal float[] RAPScalingPerLevel;
        internal float[] RBaseDamagePerLevel;

        internal bool AppliesBuff = false;
        internal bool AppliesDebuff = false;
        internal string? BuffName;
        internal int QLevel;
        internal int WLevel;
        internal int ELevel;
        internal int RLevel;
        internal string ModelName { set; private get; }
        internal AIHeroClient? ChampionClient => GetChampClient();
        internal bool IsMe => UnitManager.MyChampion == ChampionClient;


        private AIHeroClient? GetChampClient()
        {
            if (UnitManager.MyChampion.ModelName == ModelName)
            {
                return UnitManager.MyChampion;
            }

            foreach (Hero champ in UnitManager.EnemyChampions)
            {
                if (champ.ModelName == ModelName)
                {
                    return champ;
                }
            }

            foreach (Hero champ in UnitManager.AllyChampions)
            {
                if (champ.ModelName == ModelName)
                {
                    return champ;
                }
            }

            return null;
        }

        internal T Setlevel<T>() where T : BaseChamp
        {
            var spellbook = ChampionClient.GetSpellBook();
            var spellClasses = new List<SpellClass>() {
                spellbook.GetSpellClass(SpellSlot.Q),
                spellbook.GetSpellClass(SpellSlot.W),
                spellbook.GetSpellClass(SpellSlot.E),
                spellbook.GetSpellClass(SpellSlot.R)
            };
            QLevel = spellClasses[0].Level;
            WLevel = spellClasses[1].Level;
            ELevel = spellClasses[2].Level;
            RLevel = spellClasses[3].Level;
            return (T)this;
        }

        internal BaseChamp SetLevel(AIHeroClient champ)
        {
            var spellbook = champ.GetSpellBook();
            var spellClasses = new List<SpellClass>() {
                spellbook.GetSpellClass(SpellSlot.Q),
                spellbook.GetSpellClass(SpellSlot.W),
                spellbook.GetSpellClass(SpellSlot.E),
                spellbook.GetSpellClass(SpellSlot.R)
            };
            QLevel = spellClasses[0].Level;
            WLevel = spellClasses[1].Level;
            ELevel = spellClasses[2].Level;
            RLevel = spellClasses[3].Level;
            return this;
        }
        internal BaseChamp SetLevel(SpellBook spellbook)
        {
            var spellClasses = new List<SpellClass>() {
                spellbook.GetSpellClass(SpellSlot.Q),
                spellbook.GetSpellClass(SpellSlot.W),
                spellbook.GetSpellClass(SpellSlot.E),
                spellbook.GetSpellClass(SpellSlot.R)
            };
            QLevel = spellClasses[0].Level;
            WLevel = spellClasses[1].Level;
            ELevel = spellClasses[2].Level;
            RLevel = spellClasses[3].Level;
            return this;
        }
    }

    internal enum CalculationType
    {
        Min = 0,
        Max = 1,
        WithCrit = 2,
        None = 3
    }

    internal class KogMaw : BaseChamp
    {
        internal float[] PercentPerXAP = new float[2] { 1, 100 };
        internal float[] WMaxHealthPerLevel = new float[6] { 0, 3, 4, 5, 6, 7 };

        internal KogMaw()
        {
            // Set Model Name
            ModelName = "KogMaw";

            // Q
            QADScalingPerLevel = new float[6] { 0,0,0,0,0,0 };
            QAPScalingPerLevel = new float[6] { 0, 70, 70, 70, 70, 70 };
            QBaseDamagePerLevel = new float[6] { 0, 90, 140, 190, 240, 290 };
            AppliesDebuff = true;

            // W
            WADScalingPerLevel = new float[6] { 0, 0, 0, 0, 0, 0 };
            WAPScalingPerLevel = new float[6] { 0, 0, 0, 0, 0, 0 };
            WBaseDamagePerLevel = new float[6] { 0, 0, 0, 0, 0, 0 };
            AppliesBuff = true;

            // E
            EADScalingPerLevel = new float[6] { 0, 0, 0, 0, 0, 0 };
            EAPScalingPerLevel = new float[6] { 0, 70, 70, 70, 70, 70 };
            EBaseDamagePerLevel = new float[6] { 0, 75, 120, 165, 210, 255 };
            AppliesDebuff = true;

            // R
            RADScalingPerLevel = new float[4] { 0, 0, 0, 0};
            RBonusADScalingPerLevel = new float[4] { 0, 65, 65, 65 };
            RAPScalingPerLevel = new float[4] { 0, 35, 35, 35 };
            RBaseDamagePerLevel = new float[4] { 0, 100, 140, 180 };
            AppliesDebuff = true;
            Setlevel<KogMaw>();
        }

        internal float CalculateActualDamage(GameObjectBase target, SpellSlot spell)
        {
            return spell switch
            {
                SpellSlot.Q => CalculateActualQDamage(target),
                SpellSlot.W => CalculateActualWDamagePerAA(target),
                SpellSlot.E => CalculateActualEDamage(target),
                SpellSlot.R => CalculateActualRDamage(target),
                SpellSlot.BasicAttack => CalculateFullAA(target, CalculationType.Min)
            };
        }

        internal float CalculateFullAA(GameObjectBase target, CalculationType type=CalculationType.Min)
        {
            /*
            if (type == CalculationType.Min)
            {
                
            }
            else if (type == CalculationType.Max) {

            } else if (type == CalculationType.None)
            {

            } else if (type == CalculationType.WithCrit)
            {

            }
            */
            float RawDamage = DamageCalculator.GetNextBasicAttackDamage(ChampionClient, target);
            if (ChampionClient.BuffManager.HasBuff("KogMawBioArcaneBarrageBuff"))
            {
                if (ChampionClient.BuffManager.GetBuffByName("KogMawBioArcaneBarrageBuff").IsActive)
                {
                    RawDamage += CalculateActualWDamagePerAA(target);
                }
            }
            Logger.Log(RawDamage);
            return RawDamage;
        }

        internal float CalculateActualQDamage(GameObjectBase target)
        {
            float RawAPDamage = QBaseDamagePerLevel[QLevel] + (ChampionClient.UnitStats.TotalAbilityPower * (QAPScalingPerLevel[QLevel] / 100));
            Logger.Log($"Q Damage - {target.ModelName}:" + DamageCalculator.CalculateActualDamage(ChampionClient, target, 0, RawAPDamage, 0) + $" Raw Damage: {RawAPDamage}");
            return DamageCalculator.CalculateActualDamage(ChampionClient, target, 0, RawAPDamage, 0);
        }

        internal float CalculateActualWDamagePerAA(GameObjectBase target)
        {
            float RawAPDamage = (target.MaxHealth * ((WMaxHealthPerLevel[WLevel] + ((float)decimal.Round((decimal)(ChampionClient.UnitStats.TotalAbilityPower / 100), 0))) / 100));
            Logger.Log("W/AA Damage:" + DamageCalculator.CalculateActualDamage(ChampionClient, target, 0, RawAPDamage, 0) + $" Raw Damage: {RawAPDamage}");
            return DamageCalculator.CalculateActualDamage(ChampionClient, target, 0, RawAPDamage, 0);
        }

        internal float CalculateActualEDamage(GameObjectBase target)
        {
            float RawAPDamage = EBaseDamagePerLevel[ELevel] + (ChampionClient.UnitStats.TotalAbilityPower * (EAPScalingPerLevel[ELevel] / 100));
            Logger.Log("E Damage:" + DamageCalculator.CalculateActualDamage(ChampionClient, target, 0, RawAPDamage, 0) + $" Raw Damage: {RawAPDamage}");
            return DamageCalculator.CalculateActualDamage(ChampionClient, target, 0, RawAPDamage, 0);
        }

        internal float CalculateActualRDamage(GameObjectBase target)
        {
            float RawAPDamage = RBaseDamagePerLevel[RLevel] + (ChampionClient.UnitStats.BonusAttackDamage * (RBonusADScalingPerLevel[RLevel] / 100)) + (ChampionClient.UnitStats.TotalAbilityPower * (RAPScalingPerLevel[RLevel] / 100));
            float MissingHealth = target.MaxHealth - target.Health;
            float MissingHealthInPercent = (MissingHealth / target.MaxHealth) * 100;
            float RawPlusMissHealth = 0;
            if (MissingHealthInPercent >= 60)
                RawPlusMissHealth = RawAPDamage * 2;
            else {
                RawPlusMissHealth = RawAPDamage + (MissingHealthInPercent * 0.83333333333333333333333333333F);
            }
            Logger.Log("R Damage:" + DamageCalculator.CalculateActualDamage(ChampionClient, target, 0, RawPlusMissHealth, 0) + $" Raw Damage: {RawAPDamage} : MissHealth {RawPlusMissHealth}");
            return DamageCalculator.CalculateActualDamage(ChampionClient, target, 0, RawPlusMissHealth, 0);
        }
    }
}
