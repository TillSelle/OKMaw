using Oasys.Common.Enums.GameEnums;
using Oasys.Common.GameObject;
using Oasys.Common.GameObject.Clients;
using Oasys.Common.GameObject.Clients.ExtendedInstances.Spells;
using Oasys.Common.Menu;
using Oasys.Common.Menu.ItemComponents;
using Oasys.SDK;
using Oasys.SDK.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ok_Maw
{
    public static partial class Extensions
    {

        public static float TotalShieldPlusHealth(this GameObjectBase entity) => (float)(entity.Health + entity.NeutralShield + entity.MagicalShield + entity.PhysicalShield);

        public static bool HasUndyingBuff(this AIHeroClient target)
        {
            if (target.BuffManager.GetBuffList().Any(
                b => b.IsActive &&
                    (b.Name == "ChronoShift" ||
                     b.Name == "FioraW" ||
                     b.Name == "BardRStasis" ||
                     b.Name == "JudicatorIntervention" ||
                     b.Name == "UndyingRage"
                    )
               ))
                return true;
            return !target.IsTargetable;
        }

        public static SpellBook MyBook()
        {
            return Use.Me.GetSpellBook();
        }

        public static SpellClass MySpell(SpellSlot spellslot)
        {
            return MyBook().GetSpellClass(spellslot);
        }

        public static SpellClassData MySpellData(SpellSlot spellSlot)
        {
            return MySpell(spellSlot).SpellData;
        }

        public static bool IsInRange(this AIHeroClient targHero, float range)
        {
            return targHero.Distance <= range + targHero.UnitComponentInfo.UnitBoundingRadius + 5;
        }

        public static bool SpellReady(this AIHeroClient Hero, SpellSlot spellslot)
        {
            return MySpell(spellslot).IsSpellReady;
        }

        public static float SpellRadius(this AIHeroClient Hero, SpellSlot spellslot)
        {
            return MySpellData(spellslot).SpellRadius;
        }

        public static float SpellMissileSpeed(this AIHeroClient Hero, SpellSlot spellslot)
        {
            return (MySpellData(spellslot).MissileMinSpeed == 0) ? ((MySpellData(spellslot).MissileMaxSpeed == 0) ? 0 : MySpellData(spellslot).MissileMaxSpeed) : MySpellData(spellslot).MissileMinSpeed;
        }

        public static bool SwitchItemOn(this string TabName, string itemName)
        {
            return MenuManager.GetTab(TabName).GetItem<Switch>(itemName).IsOn;
        }

        public static bool SwitchItemOn(this Tab Tab, string itemName)
        {
            return Tab.GetItem<Switch>(itemName).IsOn;
        }

        public static bool SwitchItemOn(this string TabName, int itemIndex)
        {
            return MenuManager.GetTab(TabName).GetItem<Switch>(itemIndex).IsOn;
        }

        public static bool SwitchItemOn(this Tab Tab, int itemIndex)
        {
            return Tab.GetItem<Switch>(itemIndex).IsOn;
        }

        public static bool ModeSelected(this string TabName, string itemName, string Mode)
        {
            return MenuManager.GetTab(TabName).GetItem<ModeDisplay>(itemName).SelectedModeName == Mode;
        }

        public static bool ModeSelected(this Tab Tab, string itemName, string Mode)
        {
            return Tab.GetItem<ModeDisplay>(itemName).SelectedModeName == Mode;
        }

        public static bool ModeSelected(this string TabName, int itemIndex, string Mode)
        {
            return MenuManager.GetTab(TabName).GetItem<ModeDisplay>(itemIndex).SelectedModeName == Mode;
        }

        public static bool ModeSelected(this Tab Tab, int itemIndex, string Mode)
        {
            return Tab.GetItem<ModeDisplay>(itemIndex).SelectedModeName == Mode;
        }

        public static float CastRange(this AIHeroClient Hero, SpellSlot spellslot)
        {
            return Hero.GetSpellBook().GetSpellClass(spellslot).SpellData.CastRange;
        }
    }
}
