$(document).ready(function(){
  var table = $('#dataTable').DataTable( {
			"paging": false,
			"searching": false,
			"iDisplayLength": -1
  });
  
  $('#btn-export').on('click', function(){
	 var table2excel = new Table2Excel();
     table2excel.export(document.querySelectorAll('table'), "TransactionsBITtruq");
  });      
})