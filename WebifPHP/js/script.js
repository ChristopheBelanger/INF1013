$(document).ready(function(){
  var table = $('#dataTable').DataTable( {
			"aLengthMenu": [[25, 50, 75, -1], [25, 50, 75, "All"]],
			"iDisplayLength": -1
  });
  
  $('#btn-export').on('click', function(){
	 var table2excel = new Table2Excel();
     table2excel.export(document.querySelectorAll('table'), "TransactionsBITtruq");
  });      
})