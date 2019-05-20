
var modalF = document.getElementById("viewFeaturedModal");
var viewImageF = document.getElementById("viewFeaturedImage");
var modalImageF = document.getElementById("modalFeaturedImage");
var modalImageNameF = document.getElementsByClassName("modalFeaturedImageName");
var spanF = document.getElementsByClassName("closeFeatured")[0];
var hideF = document.getElementById("forModalFeatured");

viewImageF.onclick = function () {
  hideF.style.display = "none";
  modalF.style.display = "block";
  modalImageF.src = this.src;
  modalImageNameF.innerHTML = this.alt;
}

modalF.onclick = function () {
    modalF.style.display = "none";
    hideF.style.display = "block";
}

spanF.onclick = function() { 
    modalF.style.display = "none";
    hideF.style.display = "block";
}