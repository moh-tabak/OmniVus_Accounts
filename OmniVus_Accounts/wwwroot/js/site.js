
let isNameValid = false
let isEmailValid = false
let isSubjectValid = false
const formElements = document.getElementsByClassName('form-control')

if (formElements.namedItem("name-input")!=null)
formElements.namedItem("name-input").addEventListener('blur', e => {
    validateName(e.target, e.target.value)
});

if (formElements.namedItem("email-input") != null)
formElements.namedItem("email-input").addEventListener('blur', e => {
    validateEmail(e.target, e.target.value)
});

if (formElements.namedItem("subject-input") != null)
formElements.namedItem("subject-input").addEventListener('blur', e => {
    validateSubject(e.target, e.target.value)
});

const loginUrl = document.getElementById("login-link").getAttribute("href")
const summary = document.getElementById("summary");
if (summary != null) {
    let msg = summary.innerText;
    if (msg.includes("taken")) {
        msg = `You're already a user.<a href="${loginUrl}"> Log in instead!</a>`;
        summary.innerHTML = msg;
    }
}




function validateName(input, name) {
    let regex = /^[a-zäåöâçéèêëîïôûùüÿñæœ .-]{2,100}$/i
    let parent = input.parentElement
    if (regex.test(name) == false) {
        isNameValid = false;
        input.classList.add("is-invalid")
        input.parentElement.classList.add("is-invalid")
        parent.nextElementSibling.classList.remove("is-hidden")
    }
    else {
        isNameValid = true;
        input.classList.remove("is-invalid")
        input.parentElement.classList.remove("is-invalid")
        parent.nextElementSibling.classList.add("is-hidden")
    }
}

function validateEmail(input, email) {
    let regex = /^[a-zA-Z0-9_.+-]{2,}@[a-zA-Z0-9-]{2,}\.[a-zA-Z0-9-.]{2,}$/i
    let parent = input.parentElement
    if (regex.test(email) == false) {
        isEmailValid = false;
        input.classList.add("is-invalid")
        parent.classList.add("is-invalid")
        parent.nextElementSibling.classList.remove("is-hidden")
    }
    else {
        isEmailValid = true;
        input.classList.remove("is-invalid")
        input.parentElement.classList.remove("is-invalid")
        parent.nextElementSibling.classList.add("is-hidden")
    }
}

function validateSubject(input, subject) {
    let regex = /^.{8,400}$/
    if (regex.test(subject) == false) {
        isSubjectValid = false;
        input.classList.add("is-invalid")
        input.parentElement.classList.add("is-invalid")
    }
    else {
        isSubjectValid = true;
        input.classList.remove("is-invalid")
        input.parentElement.classList.remove("is-invalid")
    }
}