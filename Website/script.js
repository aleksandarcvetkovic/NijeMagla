let xCoord = 0;
let yCoord = 0;
let diffx = 4.180940;
let diffy = 4.251709;
let startx = 41.979306 + diffx;
let starty = 18.777649;
let mesto="";
let currentId="";

$(document).ready(function() {
    GetSensorsWithLocation();
});

function createPin(x, y, color, id) {
    var pin = document.createElement("div");
    pin.id = id;
    pin.classList.add("pin");
    pin.style.width = "10px";
    pin.style.height = "10px";
    pin.style.backgroundColor = color;
    pin.style.zIndex = "1000";

    pin.onclick = function() {
        var details = document.getElementById("sensorinfo");
        currentId = pin.id;
        var iddetails = document.getElementById("sensoridinfo");
        iddetails.innerHTML = "ID: " + pin.id;
        details.innerHTML = "Nema podataka";
        $.ajax({
            type: "GET",
            url: "http://localhost:5171/api/Values/" + pin.id,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function(data){
                details.innerHTML = data.time + " : " + data.value;
            },
            failure: function(errMsg) {
                details.innerHTML = "Nema podataka";
            }
        });
    }

    pin.style.position = "absolute";
    pin.style.left = x + "px";
    pin.style.top = y + "px";

    document.getElementById("container").appendChild(pin);
}
function clearPins() {
    let chList = document.getElementsByClassName("pin");
    if(chList.length > 0) {
        for (let i = 0; i < chList.length; i++) {
            chList[i].remove();
        }
    }
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
    let img = document.getElementById("mapa");
    var x = event.pageX - img.offsetLeft;
    var y = event.pageY - img.offsetTop;
    clearPins();
    GetSensorsWithLocation();
    createPin(event.pageX, event.pageY, "red");
    xCoord = startx - (x/578 * diffx);
    yCoord = starty + (y/900 * diffy);
}
function SendData(name)
{
    // send object {"ime":name, "lokacija":mesto} as post method to localhost:5171/api/Sensors
    var data = {"ime":name, "lokacija":mesto, "lat":xCoord, "lng":yCoord};
    if(name=="" || mesto=="")
    {
        alert("Niste uneli ime ili lokaciju");
        return;
    }
    console.log(data);
    $.ajax({
        type: "POST",
        url: "http://localhost:5171/api/Sensors",
        data: JSON.stringify(data),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function(data){
            clearPins();
            GetSensorsWithLocation();
        },
        failure: function(errMsg) {
            alert(errMsg);
        }
    });


}
function GetSensors()
{
    $.ajax({
        type: "GET",
        url: "http://localhost:5171/api/Values",
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
function GetSensorsWithLocation()
{
    $.ajax({
        type: "GET",
        url: "http://localhost:5171/api/Sensors",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function(data){
            let img = document.getElementById("mapa");
            for(var i=0; i<data.length; i++)
            {
                var x = (startx-data[i].lat) / diffx * 578 + img.offsetLeft;
                var y = (data[i].lng-starty) / diffy * 900 + img.offsetTop;
                createPin(x, y, "blue", data[i].id);
            }
        },
        failure: function(errMsg) {
            alert(errMsg);
        }
    });
}
function UpdateSensor()
{
    var data = {"id":currentId, lat:xCoord, lng:yCoord, lokacija:mesto};
    $.ajax({
        type: "PUT",
        url: "http://localhost:5171/api/Sensors/" + currentId,
        data: JSON.stringify(data),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function(data){
            clearPins();
            GetSensorsWithLocation();
        },
        failure: function(errMsg) {
            alert(errMsg);
        }
    });
}
function DeleteSensor()
{
    $.ajax({
        type: "DELETE",
        url: "http://localhost:5171/api/Sensors/" + currentId,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function(data){
            clearPins();
            GetSensorsWithLocation();
        },
        failure: function(errMsg) {
            alert(errMsg);
        }
    });
}


(function(window, document, undefined) {

    // code that should be taken care of right away
  
    window.onload = init;
  
    function init(){
        
    }
  
  })(window, document, undefined);



