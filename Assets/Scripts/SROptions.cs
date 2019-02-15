using System.Collections;
using System.Collections.Generic;

using System.ComponentModel;
using UnityEngine;

public partial class SROptions
{

    [Category("Scores")]
    public void ClearLeaderboardEntries()
    {
        GameManager.Instance.ClearLeaderboardEntries();
    }

    [Category("General")]
    public void Quit() {
        Application.Quit();
    }
}