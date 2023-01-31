using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryLetters : MonoBehaviour
{
    string[] Story1;

    // To create a story, use the syntax :-  string[] Story(storyNumber) ; , eg:- string Story1 ;

    // To write a letter for a particular story, use the syntax :- Story[ (Letter Number) ] =  "  Content  " ;  

    // Important points to note :
      /*
       1) Type according to the syntax given above.
       2) Content can be written anywhere as long as its inside the double quotation marks.
       3) Don't forget the semicolon at the end of every syntax ;
       4) All letter numbers of a story starts from zero.
      
      */

    public void StoryWriter()
    {
        // First Letter
        Story1[0] = " write Content here .....  ";
    }
}
