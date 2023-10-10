const enableContinueArray = [];

enableContinueArray.push(0, 0, 0, 0, 0);

const continueButton = document.getElementById("continueButton");

//This part disables the button by default. 

continueButton.disabled = true;

//This function enables the "continueButton" if the different needed textfields' requirements are fulfilled. 
function enableContinueButton() {
	let enableCounter = 0;
	for (let i = 0; i < enableContinueArray.length; i++) {
		if (enableContinueArray[i] == 1) {
			enableCounter += 1;
		} else {
			continueButton.disabled = true;
		}
	}
	if (enableCounter == 5) {
		continueButton.disabled = false;
	}
}


function checkDate() {

	let dateInput = document.getElementById("bookingDate");

	dateInput.style.borderColor = "red";

	var isConditionMet = true;

	//This part parses the input date as a Date object. 
	let selectedDate = new Date(dateInput.value);

	//If the input is empty or an invalid date, the selected date is unacceptable and the user is unable to continue. 
	if (!dateInput.value || isNaN(selectedDate)) {
		isConditionMet = false;
	}

	//This part gets the current date. 
	let currentDate = new Date();
	currentDate.setHours(0, 0, 0, 0);

	//If the selected is earlier than the current date, the selected is once again unacceptable. 
	if (selectedDate < currentDate) {
		isConditionMet = false;
	}

	//If all the criteria are met, this part passes the criteria to enable the continueButton. 
	if (isConditionMet) {
		dateInput.style.borderColor = "green";
		enableContinueArray[0] = 1;
		enableContinueButton();
		return;
	} else {
		enableContinueArray[0] = 0;
		enableContinueButton();
		return;
	}
}

function checkName() {
	let textbox = document.getElementById("nameTextbox");

	//When a button is pressed in the textfield, the border becomes red, 
	//unless there is text in the textfield. 

	textbox.style.borderColor = "red";

	if (textbox.value.length > 0) {
		textbox.style.borderColor = "green";

		//This part activates the "Continue"-button if all the textfields and the selected date fulfill their requirements. 
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
	textbox.style.borderColor = "red";

	//These if-statements check whether the text in the textfield passes our criteria for an email.
	var isConditionMet = true;

	//If the text in the textfield is less than 8 characters in length, the format is unacceptable by our parameters.
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

	//If the text in the textfield doesn't include a "." after the "@", the text is not an email and thus unacceptable.
	if (!textbox.value.substring(textbox.value.indexOf("@")).includes(".")) {
		isConditionMet = false;
	}

	//If the text in the textfield doesn't include 2 characters after the last ".", we don't accept it.
	if (textbox.value.substring(textbox.value.lastIndexOf(".") + 1).length < 2) {
		isConditionMet = false;
	}

	//If all the text in the textfield passes all the if-statements, we accept the text as an email
	//this part passes the criteria to enable the continueButton.
	if (isConditionMet) {
		textbox.style.borderColor = "green";
		enableContinueArray[2] = 1;
		enableContinueButton();
	}
}

function checkPhoneNumber() {

	//If there is text in the the textbox that includes " ", as well as text before and after the " ", 
	//the box fulfills one of the 5 requirements to continue.
	let textbox = document.getElementById("phoneTextbox");
	textbox.style.borderColor = "red";

	if (textbox.value.length == 8) {
		textbox.style.borderColor = "green";

		//This part activates the "Continue"-button if all the textfields and the selected date fulfill their requirements. 
		enableContinueArray[3] = 1;
		enableContinueButton();

		return;
	}
	enableContinueArray[3] = 0;
	enableContinueButton();
}

function checkParty() {
	let textbox = document.getElementById("partyTextbox");

	textbox.style.borderColor = "red";

	if (textbox.value.length > 0) {
		textbox.style.borderColor = "green";

		//This part activates the "Continue"-button if all the textfields and the selected date fulfill their requirements.
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

	textbox.style.borderColor = "red";

	if (textbox.value.length > 0) {
		textbox.style.borderColor = "green";

		//This part activates the "Continue"-button if all the textfields and the selected date fulfill their requirements.
		enableContinueArray[5] = 1;
		enableContinueButton();

		return;
	}
	enableContinueArray[5] = 0;
	enableContinueButton();
}