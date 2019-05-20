
var modal = document.getElementById('viewListModal');
var viewImage = document.getElementById('viewListImage');
var modalImage = document.getElementById("modalListImage");
var modalImageName = document.getElementsByClassName("modalListImageName");
var span = document.getElementsByClassName("close")[0];
var hide = document.getElementById("forListModal");

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