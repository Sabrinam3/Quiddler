/*
 * Interface name: IDeck
 * Purpose: Defines the interface for a Deck 
 * Author: Sabrina Tessier
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuiddlerLibrary
{
    public interface IDeck
    {
         string About { get; }
         int CardCount { get; }

         int CardsPerPlayer{get; set;}

         string Discard { get; }

         IPlayer NewPlayer();
    }
}
