using UnityEngine;

class PowerPelletEatenEventArgs : EventArgs
{
    public PowerPellet Pellet { get; }

    public PowerPelletEatenEventArgs(int pellet)
    {
        Pellet = pellet;
    }
}