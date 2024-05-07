using System;

public interface IDamagable
{
    event EventHandler<float> OnHealthChanged;
}
