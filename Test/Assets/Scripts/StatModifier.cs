namespace riadh.Stats
{
    public class StatModifier
    {
        public enum StatModType
        {
            Flat,
            PercentAdd,
            PercentMult,
        }
        public readonly float Value; //value of how much you want to change the stat
        public readonly StatModType Type; // value of HOW you want to change the stat ( * percent or + add flat value) 
        public readonly int Order;


        public StatModifier(float value, StatModType type, int order)
        {
            Value = value;
            Type = type;
            Order = order;
        }

        //A Constructor that add a default Order value (similar in Unity where you chose the way you use a contructor) in case the user doesn't want to manually define it for some reason
        //calling a constructor from another constructor , like extending the constructor, so it takes the things we did in the other constructor and adds to it
        // (int) type is getting the index of the type in the enum StatModType , which decides for us the order
        public StatModifier (float value, StatModType type) : this(value, type, (int)type) { }
    }
}