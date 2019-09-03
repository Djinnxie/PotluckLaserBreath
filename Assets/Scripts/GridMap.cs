using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct wallLoc
{
    public int[] wallFacePos;
    public int[] wallBackPos;
}

public class PathLogic
{
    List<int[]> path = new List<int[]>();
    public int pathCount { get; set; }

    public PathLogic ()
    {
        pathCount = 0;
    }

    public int[] getPathPoint(int pos)
    {
        if (pos > pathCount)
        {
            return new int[2] { -1, -1 };
        }
        else
        {
            return path[pos];
        }
    }
        

    public int[] getPathPoint(int w, int l)
    {
        foreach (int[] wl in path)
        {
            if(wl[0] == w && wl[1] == l)
            {
                return wl;
            }
        }
        return new int[2] { -1, -1 };
    }

    public void addPathPoint(int w, int l)
    {
        if (pathCount < 1)
        {
            path.Add(new int[2] { w, l });
            pathCount++;
        }
        else if (checkPath(w, l))
        {
            path.Add(new int[2] { w, l });
            pathCount++;
            
        }
    }

    public bool checkPath(int w, int l)
    {
        int[] previousPoint = new int[2] { path[path.Count - 1][0], path[path.Count - 1][1] };

        if (previousPoint[0] == w || previousPoint[1] == l)
        {
            return true;
        }

        return false;
    }
}


public class GridMap : MonoBehaviour
{
    public int gridWidth;
    public int gridLength;
    public GameObject corridorObject;
    public GameObject tJunctionObject;
    public GameObject cornerObject;
    public int roomSize;
    private GridSection[,] gridMap;
    private List<wallLoc> wallLocations;
    private PathLogic corridorPath;

    public GridMap(int w, int l, GameObject corridor, GameObject tJunction, GameObject corner)
    {
        gridWidth = w;
        gridLength = l;
        corridorObject = corridor;
        tJunctionObject = tJunction;
        cornerObject = corner;
    }

    // Start is called before the first frame update
    public void Start()
    {
        /*
        wallLocations = new List<wallLoc>();

        gridMap = new GridSection[gridWidth,gridLength];

        GameObject previousCorridorSection;
        GameObject newCorridorSection;
        int rot;
        bool firstPlacement = false;
        // generates a starting room tile
        int randCorridor = Random.Range(1, 3);
        randCorridor = 1;
        switch (randCorridor)
        {
            default:
            case 1:
                print("working");
                newCorridorSection = corridorObject;
                rot = 0;

                // adds to the wall locations list

                //corridors are viewed as having a left and right wall, no matter the rotation

                // left wall 
                wallLoc leftCorridorWallLoc = new wallLoc();
                leftCorridorWallLoc.wallFacePos = new int[2] { 0, 0 };
                leftCorridorWallLoc.wallBackPos = new int[2] { -1, 0 };
                print("case 0 left X " + leftCorridorWallLoc.wallBackPos[0] +
                                    " case 0 left Y " + leftCorridorWallLoc.wallBackPos[1]);
                wallLocations.Add(leftCorridorWallLoc);

                //right wall
                wallLoc rightCorridorWallLoc = new wallLoc();
                rightCorridorWallLoc.wallFacePos = new int[2] { 0, 0 };
                rightCorridorWallLoc.wallBackPos = new int[2] { 1, 0 };
                print("case 0 right X " + rightCorridorWallLoc.wallBackPos[0] +
                                   " case 0 right Y " + rightCorridorWallLoc.wallBackPos[1]);
                wallLocations.Add(rightCorridorWallLoc);
                break;
            case 2:
                newCorridorSection = tJunctionObject;
                rot = 0;

                //t Junctions are viewed as having a forward wall no matter the rotation
                wallLoc forwardTjunctWallLoc = new wallLoc();
                forwardTjunctWallLoc.wallFacePos = new int[2] { 0, 0 };
                forwardTjunctWallLoc.wallBackPos = new int[2] { -1, 0 };
                wallLocations.Add(forwardTjunctWallLoc);
                break;
            case 3:
                newCorridorSection = cornerObject;
                rot = 3;

                //corners have a forward and right wall no matter how the rotation

                //forward wall
                wallLoc forwardCornerWallLoc = new wallLoc();
                forwardCornerWallLoc.wallFacePos = new int[2] { 0, 0 };
                forwardCornerWallLoc.wallBackPos = new int[2] { -1, 0 };
                wallLocations.Add(forwardCornerWallLoc);

                //right wall
                wallLoc rightCornerWallLoc = new wallLoc();
                rightCornerWallLoc.wallFacePos = new int[2] { 0, 0 };
                rightCornerWallLoc.wallBackPos = new int[2] { 0, 1 };
                wallLocations.Add(rightCornerWallLoc);
                break;
            
                break;
        }

        int count = 0;

        for (int l = 0; l < gridLength; ++l)
        {
            for (int w = 0; w < gridWidth; ++w)
            {
                count++;
                print(count);

                if (firstPlacement == false)
                {
                    gridMap[w, l] = new CorridorSection(newCorridorSection, w, l, 0, rot);
                    Instantiate(gridMap[w, l].gridSectionType, gridMap[w, l].position, gridMap[w, l].rotation);
                    previousCorridorSection = newCorridorSection;

                    w++;
                    
                }
                bool continueLoop = false;
                foreach (wallLoc wl in wallLocations)
                {
                    if (wl.wallBackPos[0] == w &&
                        wl.wallBackPos[1] == l)
                    {
                        continueLoop = true;
                    }
                }

                if (continueLoop)
                {
                    continue;
                }

                randCorridor = Random.Range(1, 4);
                randCorridor = 1;

                switch (randCorridor)
                {
                    case 1:
                        newCorridorSection = corridorObject;
                        rot = Random.Range(0, 4);
                        wallLoc leftCorridorWallLoc = new wallLoc();
                        wallLoc rightCorridorWallLoc = new wallLoc();

                        switch (rot)
                        {
                            
                            case 0:
                                // left wall 
                                leftCorridorWallLoc.wallFacePos = new int[2] { w, l };
                                leftCorridorWallLoc.wallBackPos = new int[2] { w - 1, l };
                                print("case 0 left X " + leftCorridorWallLoc.wallBackPos[0] +
                                    " case 0 left Y " + leftCorridorWallLoc.wallBackPos[1]);
                                wallLocations.Add(leftCorridorWallLoc);

                                //right wall
                                rightCorridorWallLoc.wallFacePos = new int[2] { w, l };
                                rightCorridorWallLoc.wallBackPos = new int[2] { w + 1, l };
                                print("case 0 right X " + rightCorridorWallLoc.wallBackPos[0] +
                                    " case 0 right Y " + rightCorridorWallLoc.wallBackPos[1]);
                                wallLocations.Add(rightCorridorWallLoc);
                                break;

                            case 1:
                                // left wall 
                                leftCorridorWallLoc.wallFacePos = new int[2] { w, l };
                                leftCorridorWallLoc.wallBackPos = new int[2] { w, l - 1 };
                                print("case 1 left X " + leftCorridorWallLoc.wallBackPos[0] +
                                    " case 1 left Y " + leftCorridorWallLoc.wallBackPos[1]);
                                wallLocations.Add(leftCorridorWallLoc);

                                //right wall
                                rightCorridorWallLoc.wallFacePos = new int[2] { w, l };
                                rightCorridorWallLoc.wallBackPos = new int[2] { w, l + 1 };
                                print("case 1 right X " + rightCorridorWallLoc.wallBackPos[0] +
                                    " case 1 right Y " + rightCorridorWallLoc.wallBackPos[1]);
                                wallLocations.Add(rightCorridorWallLoc);
                                break;

                            case 2:
                                // left wall 
                                leftCorridorWallLoc.wallFacePos = new int[2] { w, l };
                                leftCorridorWallLoc.wallBackPos = new int[2] { w + 1, l };
                                print("case 2 left X " + leftCorridorWallLoc.wallBackPos[0] +
                                    " case 2 left Y " + leftCorridorWallLoc.wallBackPos[1]);
                                wallLocations.Add(leftCorridorWallLoc);

                                //right wall
                                rightCorridorWallLoc.wallFacePos = new int[2] { w, l };
                                rightCorridorWallLoc.wallBackPos = new int[2] { w - 1, l };
                                print("case 2 right X " + rightCorridorWallLoc.wallBackPos[0] +
                                    " case 2 right Y " + rightCorridorWallLoc.wallBackPos[1]);
                                wallLocations.Add(rightCorridorWallLoc);
                                break;

                            case 3:
                                // left wall 
                                leftCorridorWallLoc.wallFacePos = new int[2] { w, l };
                                leftCorridorWallLoc.wallBackPos = new int[2] { w, l + 1 };
                                print("case 3 left X " + leftCorridorWallLoc.wallBackPos[0] +
                                    " case 3 left Y " + leftCorridorWallLoc.wallBackPos[1]);
                                wallLocations.Add(leftCorridorWallLoc);

                                //right wall
                                rightCorridorWallLoc.wallFacePos = new int[2] { w, l };
                                rightCorridorWallLoc.wallBackPos = new int[2] { w, l - 1 };
                                print("case 3 right X " + rightCorridorWallLoc.wallBackPos[0] +
                                    " case 3 right Y " + rightCorridorWallLoc.wallBackPos[1]);
                                wallLocations.Add(rightCorridorWallLoc);
                                break;
                            default:
                                break;
                        }
                        break;
                    case 2:
                        newCorridorSection = tJunctionObject;
                        rot = Random.Range(0, 4);
                        break;
                    case 3:
                        newCorridorSection = cornerObject;
                        rot = Random.Range(0, 4);
                        break;
                    default:
                        break;
                }
                if (firstPlacement)
                {
                    gridMap[w, l] = new CorridorSection(newCorridorSection, w, l, 0, rot);
                    Instantiate(gridMap[w, l].gridSectionType, gridMap[w, l].position, gridMap[w, l].rotation);
                    previousCorridorSection = newCorridorSection;
                }
                firstPlacement = true;
            }
        }
        */

        int rot = 0;
        wallLocations = new List<wallLoc>();

        corridorPath = new PathLogic();

        gridMap = new GridSection[gridWidth, gridLength];

        for (int l = 0; l < gridLength; l++)
        {
            for (int w = 0; w < gridWidth; w++)
            {

                gridMap[w, l] = generateCorridor(w, l, 0);
                //gridMap[w, l] = new CorridorSection(cornerObject, w, l, 0, rot);
                if (gridMap[w,l].willDraw == true)
                {
                    print("draw");
                    Instantiate(gridMap[w, l].gridSectionType, gridMap[w, l].position, gridMap[w, l].rotation);
                }
                

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    CorridorSection generateCorridor(int w, int l, int count)
    {
        print("generation count: " + count);
        int randDirection; //= Random.Range(0, 4);

        int rot = 0;

        wallLoc forwardCornerWallLoc = new wallLoc();
        wallLoc rightCornerWallLoc = new wallLoc();

        if (corridorPath.pathCount < 1)
        {
            randDirection = 3;
        }
        else
        {
            randDirection = count;
        }

        switch (randDirection)
        {
            case 0:
                rot = 0;
                //corners have a forward and right wall no matter how the rotation

                //forward wall

                forwardCornerWallLoc.wallFacePos = new int[2] { w, l };
                forwardCornerWallLoc.wallBackPos = new int[2] { w, l - 1 };
                

                //right wall
                rightCornerWallLoc.wallFacePos = new int[2] { w, l };
                rightCornerWallLoc.wallBackPos = new int[2] { w - 1, l };
                

                if ((forwardCornerWallLoc.wallBackPos[0] == corridorPath.getPathPoint(corridorPath.pathCount-1)[0] &&
                    forwardCornerWallLoc.wallBackPos[1] == corridorPath.getPathPoint(corridorPath.pathCount - 1)[1])
                    ||
                    (rightCornerWallLoc.wallBackPos[0] == corridorPath.getPathPoint(corridorPath.pathCount - 1)[0] &&
                    rightCornerWallLoc.wallBackPos[1] == corridorPath.getPathPoint(corridorPath.pathCount - 1)[1]))
                {
                    return generateCorridor(w, l, count+1);
                }
                else
                {
                    wallLocations.Add(forwardCornerWallLoc);
                    wallLocations.Add(rightCornerWallLoc);
                }
                break;
            case 1:
                rot = 1;
                //corners have a forward and right wall no matter how the rotation

                //forward wall
                forwardCornerWallLoc.wallFacePos = new int[2] { w, l };
                forwardCornerWallLoc.wallBackPos = new int[2] { w + 1, l };

                //right wall
                rightCornerWallLoc.wallFacePos = new int[2] { w, l };
                rightCornerWallLoc.wallBackPos = new int[2] { w, l - 1 };


                if ((forwardCornerWallLoc.wallBackPos[0] == corridorPath.getPathPoint(corridorPath.pathCount - 1)[0] &&
                    forwardCornerWallLoc.wallBackPos[1] == corridorPath.getPathPoint(corridorPath.pathCount - 1)[1])
                    ||
                    (rightCornerWallLoc.wallBackPos[0] == corridorPath.getPathPoint(corridorPath.pathCount - 1)[0] &&
                    rightCornerWallLoc.wallBackPos[1] == corridorPath.getPathPoint(corridorPath.pathCount - 1)[1]))
                {
                    return generateCorridor(w, l, count+1);
                }
                else
                {
                    wallLocations.Add(forwardCornerWallLoc);
                    wallLocations.Add(rightCornerWallLoc);
                }
                break;
            case 2:
                rot = 2;

                //corners have a forward and right wall no matter how the rotation

                //forward wall
                forwardCornerWallLoc.wallFacePos = new int[2] { w, l };
                forwardCornerWallLoc.wallBackPos = new int[2] { w, l + 1 };

                //right wall

                rightCornerWallLoc.wallFacePos = new int[2] { w, l };
                rightCornerWallLoc.wallBackPos = new int[2] { w + 1, l };


                if ((forwardCornerWallLoc.wallBackPos[0] == corridorPath.getPathPoint(corridorPath.pathCount - 1)[0] &&
                    forwardCornerWallLoc.wallBackPos[1] == corridorPath.getPathPoint(corridorPath.pathCount - 1)[1])
                    ||
                    (rightCornerWallLoc.wallBackPos[0] == corridorPath.getPathPoint(corridorPath.pathCount-1)[0] &&
                    rightCornerWallLoc.wallBackPos[1] == corridorPath.getPathPoint(corridorPath.pathCount-1)[1]))
                {
                    return generateCorridor(w, l, count+1);
                }
                else
                {
                    wallLocations.Add(forwardCornerWallLoc);
                    wallLocations.Add(rightCornerWallLoc);
                }
                break;
            case 3:
                rot = 3;

                //corners have a forward and right wall no matter how the rotation

                //forward wall
                forwardCornerWallLoc.wallFacePos = new int[2] { w, l };
                forwardCornerWallLoc.wallBackPos = new int[2] { w - 1, l };

                //right wall
                rightCornerWallLoc.wallFacePos = new int[2] { w, l };
                rightCornerWallLoc.wallBackPos = new int[2] { w, l + 1 };

                if (corridorPath.pathCount < 1)
                {
                    wallLocations.Add(forwardCornerWallLoc);
                    wallLocations.Add(rightCornerWallLoc);
                }
                else if ((forwardCornerWallLoc.wallBackPos[0] == corridorPath.getPathPoint(corridorPath.pathCount-1)[0] &&
                    forwardCornerWallLoc.wallBackPos[1] == corridorPath.getPathPoint(corridorPath.pathCount-1)[1])
                    ||
                    (rightCornerWallLoc.wallBackPos[0] == corridorPath.getPathPoint(corridorPath.pathCount-1)[0] &&
                    rightCornerWallLoc.wallBackPos[1] == corridorPath.getPathPoint(corridorPath.pathCount-1)[1]))
                {
                    return generateCorridor(w, l, count+1);
                }
                else
                {
                    wallLocations.Add(forwardCornerWallLoc);
                    wallLocations.Add(rightCornerWallLoc);
                }

                break;
            case 5:
                return new CorridorSection(cornerObject, w, l, 0, rot, false);
        }
        corridorPath.addPathPoint(w, l);
        print(corridorPath.pathCount);
        print("Path X " + corridorPath.getPathPoint(corridorPath.pathCount - 1)[0] + " Path Y " + corridorPath.getPathPoint(corridorPath.pathCount - 1)[1]);
        return new CorridorSection(cornerObject, w,l,0,rot, true);
    }
}
