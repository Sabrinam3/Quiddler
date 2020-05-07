/*
 * Interface name: IPlayer
 * Purpose: Defines the interface for a Player 
 * Author: Cory Watson
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuiddlerLibrary
{
    public interface IPlayer
    {
      
        int CardCount { get; }

        int TotalPoints { get; }

        string DrawTopCard();

        bool DropDiscard(string letter);

        string PickupDiscard();

        int PlayWord(string candidate);

        int TestWord(string candidate);

        string ToString();
    }
}
