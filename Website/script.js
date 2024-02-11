//let xCoord = 0;
//let yCoord = 0;
//let diffx = 4.180940;
//let diffy = 4.251709;
//let startx = 41.979306 + diffx;
//let starty = 18.777649;
let mesto="";

$(document).ready(function() {
    $("img").on("click", function(event) {
        var x = event.pageX - this.offsetLeft;
        var y = event.pageY - this.offsetTop;
        createPin(event.pageX, event.pageY);
        //xCoord = startx - (x/578 * diffx);
        //yCoord = starty + (y/900 * diffy);
    });
});

function createPin(x, y) {
    debugger;
    var pin = document.createElement("div");
    pin.classList.add("pin");
    pin.style.width = "10px";
    pin.style.height = "10px";
    pin.style.backgroundColor = "red";
    pin.style.zIndex = "1000";

    pin.style.position = "absolute";
    pin.style.left = x + "px";
    pin.style.top = y + "px";

    let chList = document.getElementsByClassName("pin");
    if(chList.length > 0) {
        for (let i = 0; i < chList.length; i++) {
            chList[i].remove();
        }
    }
    document.getElementById("container").appendChild(pin);
}
$(document).ready(function() {
	
	$('#sidebar ul li a').click(function() {
		if($(this).next().attr("style") == "display: block; ") {
			//alert($(this).parent().parent().attr("class"));
			//if($(this).parent().parent().attr("class") != "sublist") {
				if($(this).parent().attr("class") == "odd") {
					$(this).parent().css("background", "#eaf5fc url(images/arrow.png) no-repeat 8px 9px");
				} else {
					$(this).parent().css("background", "#d4e5f0 url(images/arrow.png) no-repeat 8px 9px");
				}
			//}
		} else {
			if($(this).parent().parent().attr("class") != "sublist") {
				if($(this).parent().attr("class") == "odd") {
					$(this).parent().css("background", "#eaf5fc url(images/arrow_down.png) no-repeat 5px 12px");
				} else {
					$(this).parent().css("background", "#d4e5f0 url(images/arrow_down.png) no-repeat 5px 12px");
				}
			}
		}
		$(this).next().toggle("normal");
		return false;
	}).next().hide();
	
	$('#sidebar ul li ul li a').click(function() {
		window.location = $(this).attr("href");
	});
	
	function opstinaShow(klasa) {
		$("div."+klasa+" a").css("display", "inline");
	}
	
	function opstinaHide(klasa) {
		$("div."+klasa+" a").css("display", "none");
	}
	
	/*
$("#sidebar").hover(function() {
		$("#map div a").hide();
	});
*/
	
	$("area").hover(function() {
		$("#map div a").hide();
		opstinaShow($(this).attr("alt"));
	});
	
	$("ul.sublist li a").hover(function() {
		var klasa = $(this).attr("class");
		var opstine = klasa.split(" ");
		//alert(opstine[1]);
		if(klasa == "Serbia ") {
			$("#map div a").fadeIn("normal");
		} else {
			for(var i=0; i<opstine.length-1; i++) {
				var opstina = "div." + opstine[i] + " a";
				$(opstina).fadeIn("normal");
			}
		}
	}, function() {
		$("#map div a").hide();
	});
	
});
function IDClick(id, event)
{
    mesto = id;
    var x = event.pageX - this.offsetLeft;
    var y = event.pageY - this.offsetTop;
    createPin(event.pageX, event.pageY);
    //xCoord = startx - (x/578 * diffx);
    //yCoord = starty + (y/900 * diffy);
}
function SendData(name)
{
    // send object {"ime":name, "lokacija":mesto} as post method to localhost:5171/api/Sensors
    var data = {"ime":name, "lokacija":mesto};
    if(name=="" || mesto=="")
    {
        alert("Niste uneli ime ili lokaciju");
        return;
    }
    $.ajax({
        type: "POST",
        url: "http://localhost:5171/api/Sensors",
        data: JSON.stringify(data),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function(data){
            alert(data);
        },
        failure: function(errMsg) {
            alert(errMsg);
        }
    });


}
function GetSensors()
{
    // get all sensors from localhost:5171/api/Sensors
    $.ajax({
        type: "GET",
        url: "http://localhost:5171/api/Sensors",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function(data){
            var ul = document.getElementById("sensorlist");
            ul.innerHTML = "";
            for(var i=0; i<data.length; i++)
            {
                var li = document.createElement("li");
                li.appendChild(document.createTextNode(data[i].ime + " [" + data[i].lokacija + "] : " + data[i].vrednost));
                ul.appendChild(li);
            }
        },
        failure: function(errMsg) {
            alert(errMsg);
        }
    });

}