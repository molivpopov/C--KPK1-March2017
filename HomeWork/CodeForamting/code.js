/*jslint
    es6:true,
	white:true
*/
// this project can't work without 
// appropriate html
'use strict';
var navigator = Object.Navigator;
var window = Object.window;
var document = Object.document;
var event = Object.Event;
var nameOfNavigator = navigator.appName;
var addScroll = false;
var theLayer = Object;

if ((navigator.userAgent.indexOf('MSIE 5') > 0)
	|| (navigator.userAgent.indexOf('MSIE 6')) > 0) {
	addScroll = true;
}

var pX = 0, pY = 0;

if (nameOfNavigator === "Netscape") {
	if (document.layers.ToolTip.visibility === 'show') {
		Object.PopTip();
	}
} else {
	if (document.all.ToolTip.style.visibility === 'visible') {
		Object.PopTip();
	}
}

function mouseMove(evn) {
	if (nameOfNavigator === "Netscape") {
		pX = evn.pageX - 5;
		pY = evn.pageY;
	} else {
		pX = event.x - 5;
		pY = event.y;
	}
}

document.onmousemove = mouseMove;
if (nameOfNavigator === "Netscape") {
	document.captureEvents(event.MOUSEMOVE);
}

function PopTip() {
	if (nameOfNavigator === "Netscape") {
		theLayer = document.layers.ToolTip;
		if ((pX + 120) > window.innerWidth) {
			pX = window.innerWidth - 150;
		}

		theLayer.left = pX + 10;
		theLayer.top = pY + 15;
		theLayer.visibility = 'show';
	} else {
		theLayer = document.all.ToolTip;
		if (theLayer) {
			pX = event.x - 5;
			pY = event.y;
			
			if (addScroll) {
				pX = pX + document.body.scrollLeft;
				pY = pY + document.body.scrollTop;
			}

			if ((pX + 120) > document.body.clientWidth) {
				pX = pX - 150;
			}

			theLayer.style.pixelLeft = pX + 10;
			theLayer.style.pixelTop = pY + 15;
			theLayer.style.visibility = 'visible';
		}
	}
}

function HideTip() {
	if (nameOfNavigator === "Netscape") {
		document.layers.ToolTip.visibility = 'hide';
	} else {
		document.all.ToolTip.style.visibility = 'hidden';
	}
}

function HideMenu1() {
	if (nameOfNavigator === "Netscape") {
		document.layers.menu1.visibility = 'hide';
	} else {
		document.all.menu1.style.visibility = 'hidden';
	}
}

function ShowMenu1() {
	if (nameOfNavigator === "Netscape") {
		theLayer = document.layers.menu1;
		theLayer.visibility = 'show';
	} else {
		theLayer = document.all.menu1;
		theLayer.style.visibility = 'visible';
	}
}

function HideMenu2() {
	if (nameOfNavigator === "Netscape") {
		document.layers.menu2.visibility = 'hide';
	} else {
		document.all.menu2.style.visibility = 'hidden';
	}
}

function ShowMenu2() {
	if (nameOfNavigator === "Netscape") {
		theLayer = document.layers.menu2;
		theLayer.visibility = 'show';
	} else {
		theLayer = document.all.menu2;
		theLayer.style.visibility = 'visible';
	}
}