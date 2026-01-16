<?php  
    require_once('gameFlow.php');
?> 
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Lab 01 - Othello</title>
    <link rel="stylesheet" href="styles.css">
    <script src="https://code.jquery.com/jquery-3.7.1.min.js"></script>
    <script src="script.js" defer></script>
</head>
<body>
    <div class="container">
        <div class = "header">
            <h1>CMPE2550 - Lab 01 - Othello</h1>
        </div>
        <form action="index.php" method="post"> 
            <label id="label">Enter your names below:</label><br>
            <p></p>
            
            <input type="text" name="name1"  placeholder="Player one name here!" id="firstplayer">
            <input type="text" name="name2"  placeholder="Player two name here!" id="secondplayer"><br><br>
            <input type="button" value="New Game" id="submitbtn">
            <input type="reset" value="Quit Game" id="resetbtn">
        </form>
        <div class="board">
            
        </div>
        <div class="footer">
            <p>© 2025</p>
        </div>
    </div>
</body>
</html>