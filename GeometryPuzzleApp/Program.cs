//Assumptions:

//Points on the vertices of the polygon is not considered WITHIN the polygon
//Only integer inputs are accepted
//The limit for random coordinates is between -100 and 100

using GeometryPuzzleApp.Interfaces;
using GeometryPuzzleApp.RunMode;
using GeometryPuzzleApp.ShapeGenerators;
using GeometryPuzzleApp.Util;
using PolygonUtility.Utils;

ChooseMode();

void ChooseMode()
{
    ConsoleMessageUtil messageUtil = new ConsoleMessageUtil();
    ProcessInputUtil inputUtil = new ProcessInputUtil();
    CheckPointWithinPolygonUtil checkPointUtil = new CheckPointWithinPolygonUtil();
    IRunMode runMode;
    while (true)
    {
        messageUtil.WelcomeMessage();
        var input = Console.ReadKey();
        if (int.TryParse(input.KeyChar.ToString(), out int option))
        {
            if (option == 1)
            {
                var shapeGenerator = new CustomShapeGenerator();
                runMode = new CustomShapeRunMode(shapeGenerator, messageUtil, inputUtil, checkPointUtil);
                runMode.Start();
            }
            else if (option == 2)
            {
                //runMode = new RandomShapeGenerator();
                //runMode.Start();
            }
            else messageUtil.NotAValidInput();
        }
        else messageUtil.NotAValidInput();
    }
}
