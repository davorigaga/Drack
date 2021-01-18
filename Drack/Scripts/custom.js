$(document).ready(function () {
    console.log("hi Drack");
});

function printContent(domElement) {
    var contents = $(domElement).html();
    var printWindow = window.open("", "", "width=300,height=400");
    printWindow.document.write($("head").html() + contents + "<script>window.focus();window.print();window.close();</script>");
}
