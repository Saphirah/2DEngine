using System;
using System.Collections.Generic;
using System.Text;

namespace GameJam.Assets.Stats
{
    public class CStats : Component
    {
        Action OnStatAdd;

        protected List<Stat> stats = new List<Stat>();

        public CStats(Object parent) : base(parent) {}

        public void AddStat(Stat s)
        {
            stats.Add(s);
            OnStatAdd?.Invoke();
        }

        public void RemoveStat(int index)
        {
            if(index < stats.Count)
                stats.RemoveAt(index);
        }
        public void RemoveStat(Stat s)
        {
            stats.Remove(s);
        }

        public void SetStat(string name, float value)
        {
            foreach (Stat s in stats)
                if (s.name == name)
                    s.value = value;
        }

        public float GetStat(string name)
        {
            foreach (Stat s in stats)
                if (s.name == name)
                    return s.value;
            return -1f;
        }
    }

    public class Stat
    {
        public Action OnValueChange;

        public string name = "default";
        private float Value = 0f;
        public List<StatModifier> modifiers = new List<StatModifier>();

        public float value 
        {
            get
            {
                float v = Value;
                foreach (StatModifier sm in modifiers)
                    v = sm.CalculateModifier(v);
                return v;
            }
            set
            {
                Value = value;
                OnValueChange?.Invoke();
            }
        }
    }

    public abstract class StatModifier
    {
        public Action OnValueChanged;

        public string name = "default";
        public string description = "the default modifier";

        protected float Value;
        public float value
        {
            get
            {
                return Value;
            }
            set
            {
                Value = value;
                OnValueChanged?.Invoke();
            }
        }

        public abstract float CalculateModifier(float value);
    }

    public class StatModifier_Multiplier : StatModifier
    {
        public StatModifier_Multiplier()
        {
            name = "Multiplier";
            OnValueChanged += SetDescription;
            value = 1f;
        }

        public override float CalculateModifier(float value)
        {
            return value * this.value;
        }

        private void SetDescription()
        {
            description = "Multiplies the value by " + value * 100f + "%";
        }
    }

    public class StatModifier_Max : StatModifier
    {
        public StatModifier_Max()
        {
            name = "Max";
            OnValueChanged += SetDescription;
            value = 100f;
        }

        public override float CalculateModifier(float value)
        {
            return MathF.Min(value, this.value);
        }

        private void SetDescription()
        {
            description = "Clamp the max of this value to " + value;
        }
    }

    public class StatModifier_Min : StatModifier
    {
        public StatModifier_Min()
        {
            name = "Min";
            OnValueChanged += SetDescription;
            value = 0f;
        }

        public override float CalculateModifier(float value)
        {
            return MathF.Max(value, this.value);
        }

        private void SetDescription()
        {
            description = "Clamp the min of this value to " + value;
        }
    }
}
