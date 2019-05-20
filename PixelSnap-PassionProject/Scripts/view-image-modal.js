
var modal = document.getElementById('viewModal');
var viewImage = document.getElementById('viewImage');
var modalImage = document.getElementById("modalImage");
var modalImageName = document.getElementsByClassName("modalImageName");
var span = document.getElementsByClassName("close")[0];
var hide = document.getElementById("forModal");

viewImage.onclick = function () {
  hide.style.display = "none";
  modal.style.display = "block";
  modalImage.src = this.src;
  modalImageName.innerHTML = this.alt;
}

modal.onclick = function () {
    modal.style.display = "none";
    hide.style.display = "block";
}

span.onclick = function() { 
    modal.style.display = "none";
    hide.style.display = "block";
}