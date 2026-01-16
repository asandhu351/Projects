<?php  

    session_start();

    require_once('External.php');

    if(!isset($_SESSION['currentplayer']))
    {
        $_SESSION['currentplayer'] = 0;
    }

    if($_SERVER['REQUEST_METHOD'] == "POST")
    {
        error_log("New game");
        if($_POST['action'] == "newGame")                                           // post request if new game has been started
        {
            // New blank array
            $_SESSION['arrayData'] =  array([0,0,0,0,0,0,0,0],
                                            [0,0,0,0,0,0,0,0],
                                            [0,0,0,0,0,0,0,0],
                                            [0,0,0,1,2,0,0,0],
                                            [0,0,0,2,1,0,0,0],
                                            [0,0,0,0,0,0,0,0],
                                            [0,0,0,0,0,0,0,0],
                                            [0,0,0,0,0,0,0,0]);


            error_log($_POST['player1']);

            //cleaning the names and storing those in session variables
            $_SESSION['name1'] = strip_tags(trim($_POST['player1']));
            $_SESSION['name2'] = strip_tags(trim($_POST['player2']));

            $temp = rand(0, 2);

            // determining the starting player based on a random value
            if($temp == 1)
            {
                $_SESSION['currentplayer'] = 1;
                $string = $_SESSION['name1']."'s turn with the yellow stones";
            }
            else
            {
                $_SESSION['currentplayer'] = 2;
                $string = $_SESSION['name2']."'s turn with the brown stones";
            }

            // sending the string and array back
            $data = (object)[
                'reqarray' => $_SESSION['arrayData'],
                'string' => $string
            ];

            error_log(json_encode($data));
            
            echo json_encode($data);  
        }
         
    }

    if($_SERVER['REQUEST_METHOD'] == "GET")
    {
        //request whenever a cell is clicked
        if($_GET['action'] == "click")
        {
            error_log("On click");
            $row = $_GET['row'];
            $column = $_GET['column'];
            $player = $_SESSION['currentplayer'];

            // Check all 8 directions
            $directions = [
                [0, 1],  // Right
                [0, -1], // Left
                [1, 0],  // Down
                [-1, 0], // Up
                [1, 1],  // Down-Right
                [-1, -1], // Up-Left
                [1, -1], // Down-Left
                [-1, 1]  // Up-Right
            ];

            $temp = array();
            $temp = $_SESSION['arrayData'];
            if($_SESSION['arrayData'][$row][$column] == 0)                              // check for if cell clicked is empty
            {   
                //checking in all directions
                foreach ($directions as $dir) {
                    checkblocks($row, $column, $dir[0], $dir[1], $player);
                }

                if($_SESSION['arrayData'] == $temp)
                {
                    $string = "Invalid move!! Need to click on an empty block.";
                }
                else
                {
                    // switching the players
                    if($_SESSION['currentplayer'] == 1)
                    {
                        $_SESSION['currentplayer'] = 2;
                        $string = $_SESSION['name2']."'s turn with the brown stones";
                    }
                    else
                    {
                        $_SESSION['currentplayer'] = 1;
                        $string = $_SESSION['name1']."'s turn with the yellow stones";
                    }
                }
            }
            else                                                                        // if cell clicked is not empty
            {
                $string = "Invalid move!! Need to click on an empty block.";
            }
            
            $flag = 0;                                                                  // flag for checking zero values
            $p1blocks = 0;                                                              // number of player1 blocks
            $p2blocks = 0;                                                              // number of player2 blocks

        
            $p1check = 0;
            $p2check = 0;
            for($i = 0; $i < 8; $i++)
            {
                for($j = 0; $j < 8; $j++)
                {
                    $temp = $_SESSION['arrayData'];

                    if($_SESSION['arrayData'][$i][$j] == 0)
                    {
                        $flag = 1;
                    }

                    // Counting the player 1 blocks
                    if($_SESSION['arrayData'][$i][$j] == 1)
                    {
                        $p1blocks++;
                    }

                    // Counting the player 2 blocks
                    if($_SESSION['arrayData'][$i][$j] == 2)
                    {
                        $p2blocks++;
                    }

                    if($player == 1)
                    {
                        foreach ($directions as $dir) {
                            checktestblocks($temp ,$i, $j, $dir[0], $dir[1], 2);
                        }

                        if($temp != $_SESSION['arrayData'])
                        {
                            $p2check = 1;
                        }
                    }

                    if($player == 2)
                    {
                        foreach ($directions as $dir) {
                            checktestblocks($temp ,$i, $j, $dir[0], $dir[1], 1);
                        }

                        if($temp != $_SESSION['arrayData'])
                        {
                            $p1check = 1;
                        }
                    }
                }
            }

            $_SESSION['arrayData'] = $temp;

            if($player == 1)
            {
                if($p2check == 0)
                {
                    $string = "No valid moves left!!.".$_SESSION['name1']." wins with ".$p1blocks." blocks";
                }
            }
            if($player == 2)
            {
                if($p1check == 0)
                {
                    $string = "No valid moves left!!.".$_SESSION['name2']." wins with ".$p2blocks." blocks";
                }
            }

            if($flag == 0)                                                                          // if no zeroes are encountered
            {
                // determing the winner
                if($p1blocks > $p2blocks)
                {
                    $string = $_SESSION['name1']." wins with ".$p1blocks." blocks";
                    $data = (object)[
                        'reqarray' => $_SESSION['arrayData'],
                        'nextplayer' => $_SESSION['currentplayer'],
                        'string' => $string
                    ];
                    session_unset();
                    session_destroy();
                }
                else
                {
                    $string = $_SESSION['name2']." wins with ".$p2blocks." blocks";
                    $data = (object)[
                        'reqarray' => $_SESSION['arrayData'],
                        'nextplayer' => $_SESSION['currentplayer'],
                        'string' => $string
                    ];
                    session_unset();
                    session_destroy();
                }
            }
            else
            {
                // sending the data back
                $data = (object)[
                    'reqarray' => $_SESSION['arrayData'],
                    'nextplayer' => $_SESSION['currentplayer'],
                    'string' => $string
                ];
            }
            
            error_log(json_encode($data));
            
            echo json_encode($data);
        }

        if($_GET['action'] == "quitsession")
        {
            session_unset();
            session_destroy();
        }
    }
?> 