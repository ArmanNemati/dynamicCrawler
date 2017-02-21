// --- loader ---
function showLoader() {
    $(".loader").show();
    $(".loader").css("z-index","999999");
    $(".loader").fadeIn("slow");
}

function hideLoader() {
    $(".loader").fadeOut("slow");
}