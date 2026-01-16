<?php
    
    session_start(); // Remove the incorrect argument
    error_log("Inside function");

    function checkblocks($row, $column, $x, $y, $playernum)
    {
        // error_log("Inside function");
        // error_log("Before:".$row);
        // error_log("Before:".$column);
        // error_log($x);
        // error_log($y);
        
        $row += $x;
        $column += $y;

        // error_log("Inside function");
        // error_log("After".$row);
        // error_log("After".$column);
        // error_log($x);
        // error_log($y);

        if ($row > 7 || $row < 0 || $column > 7 || $column < 0) {
            return false;
        }

        $current = $_SESSION['arrayData'][$row][$column];

        if ($current == 0) {
            return false;
        }

        if ($current == $playernum) {
            return true;
        }

        if (checkblocks($row , $column , $x, $y, $playernum)) {
            error_log("iftrue");
            $_SESSION['arrayData'][$row][$column] = $playernum;
            $_SESSION['arrayData'][$row - $x][$column - $y] = $playernum;
            return true;
        }

        return false;
    }

    function checktestblocks(&$tempArray,$row, $column, $x, $y, $playernum)
    {
        // error_log("Inside function");
        // error_log("Before:".$row);
        // error_log("Before:".$column);
        // error_log($x);
        // error_log($y);
        
        $row += $x;
        $column += $y;

        // error_log("Inside function");
        // error_log("After".$row);
        // error_log("After".$column);
        // error_log($x);
        // error_log($y);

        if ($row > 7 || $row < 0 || $column > 7 || $column < 0) {
            return false;
        }

        $current = $tempArray[$row][$column];

        if ($current == 0) {
            return false;
        }

        if ($current == $playernum) {
            return true;
        }

        if (checktestblocks($tempArray,$row, $column, $x, $y, $playernum)) {
            error_log("iftrue");
            $tempArray[$row][$column] = $playernum;
            $tempArray[$row - $x][$column - $y] = $playernum;
            return true;
        }

        return false;
    }
?>