$(document).ready(function(){
  var table = $('#dataTable').DataTable( {
			"paging": false,
			"searching": false,
			"iDisplayLength": -1,
			"order": [[ 0, "desc" ]]
  });
  
  $('#btn-export').on('click', function(){
	 var table2excel = new Table2Excel();
     table2excel.export(document.querySelectorAll('table'), "TransactionsBITtruq");
  });      
})