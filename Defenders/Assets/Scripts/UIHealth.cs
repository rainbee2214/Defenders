using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public interface UIHealth
{
    void TakeDamage(float damage = -1);
    void ResetHealth();
}
