let name1;
let name2;
let newgame = 0;
$(()=>
{
    console.log("on page load");

    $("#submitbtn").on("click",() =>                                                    // on clicking submit button
    {
        name1 = $("#firstplayer").val();
        name2 = $("#secondplayer").val();

        if(!name1 || !name2)                                                            // checking if either name1 or name2 is null
        {
            $("#label").html("Name must be at least one character!");
        }
        else
        {
            let data = {};
            data['action'] = "newGame"; 
            data['player1'] = name1;
            data['player2'] = name2;
                    
            // Constructing the board
            let boardString = "";
            for(let i = 0; i < 8; i++)
            {
                boardString += "<div>";
                for(let j = 0; j < 8; j++)
                {
                    boardString += `<input type="text" id="${i}_${j}" class="boxes" readonly>`;
                }
                boardString += "</div>";
            }
            $(".board").html(boardString);

            if(newgame == 0)
            {
                newgame = 1;
                // assigning the event handler for the boxes
                $(".board").on("click", ".boxes", function() {
                    let [row, col] = $(this).attr("id").split("_").map(Number);
                    Checkboard(row, col);
                }); 
            }   
            
            MakeAjaxCall("gameFlow.php", "POST", data, "JSON", updateBoard, errorHandler);
        }
    });

    /*************************************************
     * function Checkboard(row, column)
     * objective - sending the ajax call to examine the move made by player
     ****************************************************/
    function Checkboard(row, column)
    {
        console.log("textbox has been clicked" +row+column);
        let rowcolData = {};
        rowcolData['row'] = row;
        rowcolData['column'] = column;
        rowcolData['action'] = 'click'; 
        MakeAjaxCall("gameFlow.php", "GET", rowcolData, "JSON", updateBoard, errorHandler);
    }

    // On pressing the reset button
    $("#resetbtn").on("click",() =>
    {
        let data = {};
        data['action'] = 'quitsession';
        MakeAjaxCall("gameFlow.php", "GET", data, "JSON", successB1, errorHandler);
        window.location.reload();
    });
});

function MakeAjaxCall(serverURL, reqMethod, data, serverResponse, successHandler, errorHandler)
{
    console.log("Inside MakeAjax call");

    let ajaxOptions = {};
    ajaxOptions['url']      = serverURL;        // Destination URL
    ajaxOptions['type']     = reqMethod;        // GET/POST
    ajaxOptions['dataType'] = serverResponse;   // HTML/JSON
    ajaxOptions['data']     = data;             // Client data to be passed to server
    ajaxOptions['success']  = successHandler;   // Callback function to handle successful case
    ajaxOptions['error']    = errorHandler;     // Callback function for error handling

    // Make ajax
    $.ajax(ajaxOptions);
}

function successB1(serverData, serverStatus)
{
    console.log(serverData);
    console.log(serverStatus);
    
}

/*************************************************
 * function updateBoard(data, serverStatus)
 * objective - updating the board after every move
 ****************************************************/
function updateBoard(data, serverStatus)
{
    console.log(data);
    console.log(serverStatus);

    // iterating through the entire array and updating the value and css of every cell
    for(let i = 0; i < 8; i++)
    {
        for(let j = 0; j < 8; j++)
        {
            if(data.reqarray[i][j] == 1)
            {
                $("#"+i+"_"+j).val("\u25CF");
                $("#"+i+"_"+j).css("color", "yellow");
            }
            if(data.reqarray[i][j]  == 2)
            {
                $("#"+i+"_"+j).val("\u25CF");
                $("#"+i+"_"+j).css("color", "brown");
            }
        }
    }

    $("#label").html(data.string);              // Updating the label
}

function errorHandler(ajaxReq, ajaxStatus, errorThrown) 
{
    console.log(ajaxStatus);
    console.log(errorThrown);
}


