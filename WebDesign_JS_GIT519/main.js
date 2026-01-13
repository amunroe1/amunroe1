// This is the main JavaScript file for the project
"use strict";


//Add event handler for guessing game
document.getElementById("gamePlay").addEventListener("click", guessNumber);

// Function to handle the game logic
function guessNumber() {
    let gameMessage = document.getElementById("gameResult");
    let guess = parseInt(document.getElementById("guess").value);
    let randomNumber = Math.floor(Math.random() * 10) + 1;
    let gameError = document.getElementById("gameError");
    
    gameMessage.innerHTML = ""; // Clear previous messages
    gameError.innerHTML = ""; // Clear previous error messages
    
    // Validate guess
    if (guess < 1 || guess > 10 || isNaN(guess)) {
        gameError.innerHTML = "Please enter a valid number between 1 and 10.";
        document.getElementById("guess").value = "";
        return;
    }

    if (guess === randomNumber) {
        gameMessage.innerHTML = `Congratulations! You guessed the correct number: <strong>${randomNumber}</strong>.`;
    }
    else {
        gameMessage.innerHTML = `Sorry, the correct number was <strong>${randomNumber}</strong> and you guessed <strong>${guess}</strong>. Try again!`;
    }

    // Clear the input field
    document.getElementById("guess").value = "";
}

//Add event handler for dark mode toggle
document.getElementById("darkModeButton").addEventListener("click", toggleDarkMode)

// Function to toggle dark mode by toggling the class "darkMode" on the body element which has an alternate color palette
// and changes the button text to "Light Mode" or "Dark Mode" accordingly
function toggleDarkMode() {
    document.body.classList.toggle("darkMode");


    let button = document.getElementById("darkModeButton");
    if (document.body.classList.contains("darkMode")) {
        button.innerHTML = "Light Mode";
    } else {
        button.innerHTML = "Dark Mode";
    }
}

//Add event handler for contact form
document.getElementById("submitContact").addEventListener("click", checkSignup)

// Function to validate the contact form
function checkSignup() {
    let error = false;
    let customer = {
        name: "",
        email: "",
        phone: "",
        comments: "",
    }
    
    //Verify the name input
    let name = document.getElementById("fullName");
    let nameError = document.getElementById("nameError");
    if (name.value.trim() === "") {
        nameError.innerHTML = "Please enter your name.";
        name.classList.add("error");
        error = true;
    }

    //Remove error message if name is valid and assign to customer object
    if(name.value.trim() !== "") {
        nameError.innerHTML = "";
        name.classList.remove("error");
        customer.name = name.value.trim();
    }

    //Verify the email input with regex
    const emailRgx =  /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
    let email = document.getElementById("email");
    let emailChecked = document.getElementById("emailRadio").checked;
    let emailError = document.getElementById("emailError");

    //Check to see if email radio is checked
    if (!emailRgx.test(email.value) && emailChecked) {
        emailError.innerHTML = "Please enter a valid email address.";
        email.classList.add("error");
        error = true;
    }
    //Remove error message if email is valid and assign to customer object
    if(!emailChecked || emailRgx.test(email.value)) {
        emailError.innerHTML = "";
        email.classList.remove("error");
        customer.email = email.value.trim();

    }

    //Verify the phone input with regex
    const phoneRgx = /^\d{3}-?\d{3}-?\d{4}$/;
    let phone = document.getElementById("phoneNum");
    let phoneError = document.getElementById("phoneError");
    let phoneChecked = document.getElementById("phoneRadio").checked;

    //Check to see if phone radio is checked
    if (!phoneRgx.test(phone.value) && phoneChecked) {
        phoneError.innerHTML = "Please enter a valid phone number in the format XXX-XXX-XXXX.";
        phone.classList.add("error");
        error = true;
    }

    //Remove error message if phone is valid and assign to customer object
    if(!phoneChecked || phoneRgx.test(phone.value)) {
        phoneError.innerHTML = "";
        phone.classList.remove("error");
        customer.phone = phone.value.trim();
    }

    //Verify the comments input
    let comments = document.getElementById("comments");
    let commentsError = document.getElementById("commentsError");
    if (comments.value.trim() === "") {
        commentsError.innerHTML = "Please enter your comments.";
        comments.classList.add("error");
        error = true;
    }
    //Remove error message if comments is valid and assign to customer object
    if(comments.value.trim() !== "") {
        commentsError.innerHTML = "";
        comments.classList.remove("error");
        customer.comments = comments.value.trim();
    }

    //construct the success message
    let successMessage = document.getElementById("successMessage");
    successMessage.innerHTML = ""; // Clear previous messages
    if (!error) {
        // Reset the form
        name.value = "";
        email.value = "";
        phone.value = "";
        comments.value = "";
        
        // Output for phone only
        if(phoneChecked) {
            successMessage.innerText = "Form submitted successfully!\n Here's a summary of your information: \n" + `
            Name: ${customer.name}
            Phone: ${customer.phone}
            Comments: ${customer.comments}`;
        }

        // Output for email only
        if(emailChecked) {
            successMessage.innerText = "Thank you! Here's a summary of your information: \n" + `
            Name: ${customer.name}
            Email: ${customer.email}
            Comments: ${customer.comments}`;
        }

    }
}


//Add event handler for product selection
document.getElementById("b1").addEventListener("click", changeProduct1);
document.getElementById("b2").addEventListener("click", changeProduct2);
document.getElementById("b3").addEventListener("click", changeProduct3);

// Get the product elements
let prod1 = document.getElementById("product1");
let prod2 = document.getElementById("product2");
let prod3 = document.getElementById("product3");

// Functions to change the product displayed to products 1, 2, or 3
function changeProduct1(){
    prod1.classList.remove("hide");
    prod2.classList.add("hide");
    prod3.classList.add("hide");
}

function changeProduct2(){
    prod1.classList.add("hide");
    prod2.classList.remove("hide");
    prod3.classList.add("hide");
}

function changeProduct3(){
    prod1.classList.add("hide");
    prod2.classList.add("hide");
    prod3.classList.remove("hide");
}
