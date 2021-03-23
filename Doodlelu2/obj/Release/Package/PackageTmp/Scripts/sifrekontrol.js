var password = document.getElementById("sifre")
    , confirm_password = document.getElementById("sifrekontrol");

function validatePassword() {
    if (password.value != confirm_password.value) {
        confirm_password.setCustomValidity("Girdiğiniz şifreler eşleşmiyor. Lütfen tekrar deneyiniz.");
    } else {
        confirm_password.setCustomValidity('');
    }
}

password.onchange = validatePassword;
confirm_password.onkeyup = validatePassword;