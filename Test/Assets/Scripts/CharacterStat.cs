using System.Collections.Generic;
using System.Collections;
using System;

namespace riadh.Stats
{
    public class CharacterStat
    {

        public float BaseValue;
        private bool isDirty = true; // to avoid calling CalculateFinalValue() every single time and call it only when a change has occured
        private float _value;

        public float Value
        {
            get
            {
                if (isDirty)
                {
                    _value = CalculateFinalValue();
                    isDirty = false;
                }
                return _value;
            }
        }

        private readonly List<StatModifier> statModifiers;

        public CharacterStat(float baseValue)
        {
            BaseValue = baseValue;
            statModifiers = new List<StatModifier>();
        }

        public bool RemoveModifier(StatModifier mod)
        {
            isDirty = true;
            return statModifiers.Remove(mod);
        }

        public void AddModifier(StatModifier mod)
        {
            isDirty = true;
            statModifiers.Add(mod);
            statModifiers.Sort(CompareModifierOrder);
        }

        private float CalculateFinalValue()
        {
            float finalValue = BaseValue;
            float sumPercentAdd = 0; // This will hold the sum of our "PercentAdd" modifiers
            for (int i = 0; i < statModifiers.Count; i++)
            {
                StatModifier mod = statModifiers[i];

                if (mod.Type == StatModifier.StatModType.Flat)
                {
                    finalValue += statModifiers[i].Value;
                }
                else if (mod.Type == StatModifier.StatModType.PercentAdd)
                {
                    sumPercentAdd += mod.Value; //add all percentAdd modifiers we meet

                    // if we reached end of list or the modifier li 3anna isn't percentAdd
                    if (i+ 1 >=statModifiers.Count || statModifiers[i+1].Type != StatModifier.StatModType.PercentAdd)
                    {
                        finalValue *= 1 + sumPercentAdd; // Multiply the sum with the final value , kima percentMult
                        sumPercentAdd = 0; // reset, rajja3 kima kenet, for next uses
                    }
                }
                else if (mod.Type == StatModifier.StatModType.PercentMult)
                {
                    finalValue *= 1 + mod.Value;
                    //This is the equivalent of "finalValue += finalValue * mod.Value" 
                }
            }
            //Rounding to 4 digits after the Zero 
            return (float)Math.Round(finalValue, 4);
        }

        private int CompareModifierOrder (StatModifier a, StatModifier b) // if returns 1 , it will go the standard order in the enum, else it will go opposite order and sort it in that order
        {
            if (a.Order < b.Order)
                return -1;
            else if (a.Order > b.Order)
                return 1;
            return 0; // if both are equal, so same priority
        }

    }
}
