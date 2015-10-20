using UnityEngine;
using System.Collections;

public interface ISpawnable
{
    /// <summary>
    /// Setup the spawnable object to respawn. (Re)create the object [from an object pool preferably]
    /// Used when the game object is being spawned, before the Spawn() call;
    /// </summary>
    void Setup();

    /// <summary>
    /// Get a list (one or more) of possible spawn locations. Can be random or predetermined, or only a single location.
    /// Used for the paramter of Spawn(Vector3);
    /// </summary>
    Vector3[] GetSpawnPoints();

    /// <summary>
    /// Move the object to the spawn position and turn it on [enable it].
    /// Used to (re)spawn a game object in the level. Make sure to call Setup() first.
    /// </summary>
    void Spawn(Vector3 location);

    /// <summary>
    /// Turn off the game object and reset any relevant properties [velocity, sprite renderers, explosions etc]
    /// Used when the game object is turned off (died etc).
    /// </summary>
    void Reset();

}
