namespace Caseimport
{
using System.Collections;
using System.Collections.Generic;

public class Case
{
    public List<int> characters;

    public int healthpoints;

    public Case()
    {
        characters = new List<int>();
        this.healthpoints = 0;
    }

    public Case(int NumberOfCharacters)
    {
        characters = new List<int>();
        for (int i= 0; i<NumberOfCharacters;i++)
        {
            this.characters.Add(0);
        }
        this.healthpoints = 100;
    }

}
}