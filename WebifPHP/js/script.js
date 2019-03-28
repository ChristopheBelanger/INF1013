$(document).ready(function(){
  var table = $('#dataTable').DataTable();
  
  $('#btn-export').on('click', function(){
	 var table2excel = new Table2Excel();
     table2excel.export(document.querySelectorAll('table'), "TransactionsBITtruq");
  });      
})