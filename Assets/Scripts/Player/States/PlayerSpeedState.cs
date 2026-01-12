using UnityEngine;

public abstract class PlayerSpeedState
{
    public abstract float SpeedMultiplier { get; }
    public abstract int Level { get; }
}

public class SpeedLevelMinus1 : PlayerSpeedState
{
    public override float SpeedMultiplier => 0.5f;
    public override int Level => -1;
}

public class SpeedLevel0 : PlayerSpeedState
{
    public override float SpeedMultiplier => 1f;
    public override int Level => 0;
}

public class SpeedLevel1 : PlayerSpeedState
{
    public override float SpeedMultiplier => 1.25f;
    public override int Level => 1;
}

public class SpeedLevel2 : PlayerSpeedState
{
    public override float SpeedMultiplier => 1.5f;
    public override int Level => 2;
}

public class SpeedLevel3 : PlayerSpeedState
{
    public override float SpeedMultiplier => 1.75f;
    public override int Level => 3;
}

public class SpeedLevel4 : PlayerSpeedState
{
    public override float SpeedMultiplier => 2f;
    public override int Level => 4;
}

//public interface PlayerSpeedState { float GetSpeedMultiplier(); }
//public class SpeedLevel0 : PlayerSpeedState
//{
//    public float GetSpeedMultiplier() => 0.5f;
//}
//public class SpeedLevel1 : PlayerSpeedState
//{
//    public float GetSpeedMultiplier() => 1f;
//}
//public class SpeedLevel2 : PlayerSpeedState
//{
//    public float GetSpeedMultiplier() => 1.25f;
//}
//public class SpeedLevel3 : PlayerSpeedState
//{
//    public float GetSpeedMultiplier() => 1.5f;
//}

//public class SpeedLevel4 : PlayerSpeedState
//{
//    public float GetSpeedMultiplier() => 2.0f;
//}
