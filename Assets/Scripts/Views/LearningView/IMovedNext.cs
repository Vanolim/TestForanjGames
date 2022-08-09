using System;

public interface IMovedNext
{
    public event Action<LearningView> OnNext;
}