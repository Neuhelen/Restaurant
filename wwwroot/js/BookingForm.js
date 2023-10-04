const enableContinueArray = [];

enableContinueArray.push(0, 0, 0, 0, 0);

const continueButton = document.getElementById("continueButton");

//This part disables the button by default. 

continueButton.disabled = true;

//This function enables the "continueButton" if the different needed textfields' requirements are fulfilled. 
function enableContinueButton() {
	let enableCounter = 0;
	for (let i = 0; i < enableContinueArray.length; i++) {
    		if (enableContinueArray[i] == 1){
			enableCounter += 1;
        	} else {
			continueButton.disabled = true;
        }
	}
	if (enableCounter == 5){
		continueButton.disabled = false;
    }	
}

function checkDate() {
	let date = document.getElementById("bookingDate");
	var isConditionMet = true;

	if (!date.value || isNaN(Date.parse(date.value))) {
		isConditionMet = false;
	}

	var now = new Date();
	now.setHours(0, 0, 0, 0);
	date.setHours(0, 0, 0, 0);
	if (date < now) {
		isConditionMet = false;
	}

	if (isConditionMet) {
		enableContinueArray[0] = 1;
		enableContinueButton();
		return;
	}
}

function checkName() {
	let textbox = document.getElementById("nameTextbox");

	//When a button is pressed in the textfield, the border becomes red, 
	//unless there is text in the textfield. 

	document.getElementById("nameTextbox").style.borderColor = "red";
    
	if (textbox.value.length > 0) {
		document.getElementById("nameTextbox").style.borderColor = "green";

		//This part activates the "Continue"-button if all the textfields fulfill their requirements. 
		enableContinueArray[1] = 1;
		enableContinueButton();

		return; 
    }
	enableContinueArray[1] = 0;
	enableContinueButton(); 
}

function checkEmail() {
	let textbox = document.getElementById("emailTextbox");

	//When a button is pressed in the textfield, the border becomes red, 
	//unless the text in the textfield fullfils all the conditions in the if-statements. 

	document.getElementById("emailTextbox").style.borderColor = "red";

	//These if-statements check whether the text in the textfield passes our criteria for an email. 
	//There must be 2 characters before the "@@", 2 more characters, a "." and finally 2 more characters 
	//in the textfield to pass the if-statements and make the border of the textfield green. 

	var isConditionMet = true;

½	//If the text in the textfield is less than 8 characters in length, the format is not unacceptable by our parameters. 
	if (textbox.value.length < 8) {
		isConditionMet = false;
	}

	//If the text in the textfield doesn't include an "@", the text is not an email and thus unacceptable. 
	if (!textbox.value.includes("@")) {
		isConditionMet = false;
	}

	//If there is no text before the "@", the text is not an email and thus unacceptable. 
	if (textbox.value.substring(0, textbox.value.indexOf("@")).length < 2) {
		isConditionMet = false;
	}

	//If the text in the textfield doesn't include an "." after the "@", the text is not an email and thus unacceptable. 
	if (!textbox.value.substring(textbox.value.indexOf("@@")).includes(".")) {
		isConditionMet = false;
	}

	//If the in the textfield doesn't include 2 characters after the "@", but before the last ".", we don't accept it. 
	if (textbox.value.substring(textbox.value.indexOf("@@") + 1, textbox.value.lastIndexOf(".")).length < 2) {
		isConditionMet = false;
	}

	//If the in the textfield doesn't include 2 characters after the last ".", we don't accept it. 
	if (textbox.value.substring(textbox.value.lastIndexOf(".") + 1).length < 2) {
		isConditionMet = false;
	}

	if (isConditionMet) {
		document.getElementById("emailTextbox").style.borderColor = "green";
		enableContinueArray[2] = 1;
		enableContinueButton();
	} 
}

function checkPhoneNumber() {

	//If there is text in the the textbox that includes " ", as well as text before and after the " ", 
	//the box fulfills one of the 5 requirements to continue.
	let textbox = document.getElementById("phoneTextbox");
	document.getElementById("phoneTextbox").style.borderColor = "red";

	if (textbox.value.length == 8) {
		document.getElementById("phoneTextbox").style.borderColor = "green";

		//This part activates the "Continue"-button if all the textfields fulfill their requirements. 
		enableContinueArray[3] = 1;
		enableContinueButton();

		return;
    }
	enableContinueArray[3] = 0;
	enableContinueButton(); 
}

function checkParty() {
	let textbox = document.getElementById("partyTextbox");

	document.getElementById("partyTextbox").style.borderColor = "red";
    
	if (textbox.value.length > 0) {
		document.getElementById("partyTextbox").style.borderColor = "green";

		//This part activates the "Continue"-button if all the textfields fulfill their requirements. 
		enableContinueArray[4] = 1;
		enableContinueButton();

		return; 
    }
	enableContinueArray[4] = 0;
	enableContinueButton(); 
}

function checkMeals() {
	let textbox = document.getElementById("mealTextbox");

	//When a button is pressed in the textfield, the border becomes red, 
	//unless there is text in the textfield. 

	document.getElementById("mealTextbox").style.borderColor = "red";
    
	if (textbox.value.length > 0) {
		document.getElementById("mealTextbox").style.borderColor = "green";

		//This part activates the "Continue"-button if all the textfields fulfill their requirements. 
		enableContinueArray[5] = 1;
		enableContinueButton();

		return; 
    }
	enableContinueArray[5] = 0;
	enableContinueButton(); 
}