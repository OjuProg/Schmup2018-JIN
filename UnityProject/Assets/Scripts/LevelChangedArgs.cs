using System;

public class LevelChangedArgs : EventArgs {

	public LevelChangedArgs(Level level)
    {
        this.Level = level;
    }

    public Level Level
    {
        get;
        private set;
    }
}
