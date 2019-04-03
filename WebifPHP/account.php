<?php 
include 'include/urls.php';
IF(ISSET($_SESSION['name'])){
?>

<!DOCTYPE html>
<html lang="en">

<head>
  <meta charset="utf-8">
  <meta http-equiv="refresh" content="30"/>
  <meta http-equiv="X-UA-Compatible" content="IE=edge">
  <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
  <meta name="description" content="">
  <meta name="author" content="">
  <title>Compte BITtruq</title>
  <!-- Bootstrap core CSS-->
  <link href="framework/bootstrap/css/bootstrap.min.css" rel="stylesheet">
  <!-- Custom fonts for this template-->
  <link href="framework/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css">
  <!-- Custom styles for this template-->
  <script src="framework/jquery/jquery.table2excel.js"></script>
  <link href="css/sb-admin.css" rel="stylesheet">
  <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js"></script>
  <script src="https://cdn.datatables.net/1.10.7/framework/js/jquery.dataTables.min.js"></script>
  <script src="framework/js/sb-admin-table2excel.js"></script>
</head>

<body class="fixed-nav sticky-footer bg-dark" id="page-top">
  <!-- Navigation-->
  <nav class="navbar navbar-expand-lg navbar-dark bg-dark fixed-top" id="mainNav">
    <a class="navbar-brand" href="index.php">BITtruq</a>
    <button class="navbar-toggler navbar-toggler-right" type="button" data-toggle="collapse" data-target="#navbarResponsive" aria-controls="navbarResponsive" aria-expanded="false" aria-label="Toggle navigation">
      <span class="navbar-toggler-icon"></span>
    </button>
    <div class="collapse in navbar-collapse" id="navbarResponsive">
      <ul class="navbar-nav navbar-sidenav" id="exampleAccordion">
	    <li class="nav-item" data-toggle="tooltip" data-placement="right" title="Account">
          <a class="nav-link" href="account.php">
            <i class="fa fa-fw fa-dashboard"></i>
            <span class="nav-link-text">Tableau de bord</span>
          </a>
        </li>

      </ul>
      <ul class="navbar-nav ml-auto">
	    <li class="nav-item">
			<a class="nav-link" href="account.php">
				<i class="fa fa-fw fa-user"></i> <?=$_SESSION['name'];?> </a>	
        </li>
        <li class="nav-item">
          <a class="nav-link" href="logout.php?destroy">
            <i class="fa fa-fw fa-sign-out"></i> Déconnexion</a>
        </li>
      </ul>
    </div>
  </nav>
  <div class="content-wrapper">
    <div class="container-fluid">
      <div class="row">
        <div class="col-12">
		
		<?php 
		IF(ISSET($_SESSION['wallet'])) {
					$ch = curl_init();
					curl_setopt($ch, CURLOPT_SSL_VERIFYPEER, false);
					curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);
					curl_setopt($ch, CURLOPT_URL, $TRANSACTIONS_URL);
					$result = curl_exec($ch);
					curl_close($ch);

					$obj = json_decode($result);
					$elementCount  = count($obj->transactions);
					$solde = 0;
					for($i = 0; $i < $elementCount; $i++) {
						if($obj->transactions[$i]->Action == "send to"){
							$solde -= $obj->transactions[$i]->Montant;
						} else {
							$solde += $obj->transactions[$i]->Montant;
						}
					}
					if($solde > 1){
						$BITtruq = "BITtruqs";
					} else {
						$BITtruq = "BITtruq";
					}

			echo "			
			<div role=\"tabpanel\" class=\"tab-pane active container-fluid\" id=\"home\">
				<div class=\"row\">
					<div class=\"col-md-8\"><div class=\"panel panel-default\">
					<div class=\"panel-heading\">Numéro de porte feuille</div>
					<div class=\"panel-body\">
						<p><center><strong><big>" . $_SESSION['wallet'] . "</big></strong></center></p>
					</div>
				</div>
			</div>
				<div class=\"col-md-4\"><div class=\"panel panel-default\">
					<div class=\"panel-heading\">Solde du porte feuille</div>
					<div class=\"panel-body\">
						<p><center><big><strong>" . $solde . "</strong>  " . $BITtruq . "</big></center></p>
					</div>
			</div></div></div></div>
			
			<!-- second row *************************************************************************************** -->
			
			<div role=\"tabpanel\" class=\"tab-pane active container-fluid\" id=\"home\">
				<div class=\"row\">
					<div class=\"col-md-12\"><div class=\"panel panel-default\">
						<div class=\"panel-heading\">Transferts</div>
					<div class=\"panel-body\">
					<form action=\"\" method=\"post\">
					<div class=\"form-row\">
						<div class=\"col-md-8 mb-3 text-center\">
						<br>
						  <input type=\"text\" class=\"form-control\" id=\"sendTo\" placeholder=\"Numero de portefeuille\" required>
						</div>
						<div class=\"col-md-2 mb-3 text-center\">
						<br>
						  <input type=\"text\" class=\"form-control\" id=\"montant\" placeholder=\"Montant\" required>
						</div>
						<div class=\"col-md-2 mb-3 text-center\">
						<br>
							<input type=\"button\" class=\"btn btn-primary pull-center\" onclick=\"window.location.href = '";
							echo $TRANSFER_URL;
							echo "'\" value= \"Transferer\"/>
						</div>
					  </div>
					  </form>
					</div>
				</div>
			</div></div>
			
			
			      <!-- Example DataTables Card-->
      <div class=\"card mb-3\">
        <div class=\"card-header\">
          <i class=\"fa fa-table\"></i> Tableau des transactions
			<button id=\"btn-export\" type=\"button\" class=\"btn btn-light pull-right\">Exporter au format XLS </button>
		  </div>
        <div class=\"card-body\">
          <div class=\"table-responsive\">
            <table class=\"table table-bordered data-page-length='-1'\" id=\"dataTable\" width=\"100%\" cellspacing=\"0\">
              <thead>
                <tr>
                  <th>Id</th>
                  <th>Action</th>
                  <th>Porte feuille</th>
                  <th>Montant</th>
                  <th>Date</th>
                  <th>Status</th>
                </tr>
              </thead>
              <tbody>";
			  
					for($i = $elementCount - 1; $i > -1; $i--) {
						echo "<tr>";	
						  echo "<td>" . $obj->transactions[$i]->Id . "</td>";
						  echo "<td>" . $obj->transactions[$i]->Action . "</td>";
						  echo "<td>" . $obj->transactions[$i]->Portefeuille . "</td>";
						  echo "<td>" . $obj->transactions[$i]->Montant . "</td>";
						  echo "<td>" . $obj->transactions[$i]->Date . "</td>";
						  echo "<td>" . $obj->transactions[$i]->Status . "</td>";
						echo "</tr>";
					} 
					
					echo "
              </tbody>
            </table>
          </div>
        </div>
        <div class=\"card-footer small text-muted\">
		"; 
			date_default_timezone_set('America/New_York');
			$today = date("d-m-Y"); 
			$time = date("H:i:s");
			echo "Derniere mise a jour le: " . $today . " a " . $time;  
		echo "
  </div>
      </div>
    </div></div></div>
			";
		} else {
			echo "
			<p>Aucun porte feuille configuré pour ce compte.</p>
			<br>
			<p>Cliquer sur le bouton pour obtenir un porte feuille BITtruq.</p>
			<div class=\"col-md-5 col-sm-5 col-xs-12 gutter\">
                <a class='btn btn-primary' href='include/getWallet.php' role='button'>Obtenir un Porte feuille</a>
            </div>
			";
		} ?>
        </div>
      </div>
    </div>
    <!-- Bootstrap core JavaScript-->
    <script src="framework/jquery/jquery.min.js"></script>
    <script src="framework/bootstrap/framework/js/bootstrap.bundle.min.js"></script>
    <!-- Core plugin JavaScript-->
    <script src="framework/jquery-easing/jquery.easing.min.js"></script>
	<!-- Page level plugin JavaScript-->
    <script src="framework/datatables/jquery.dataTables.js"></script>
    <script src="framework/datatables/dataTables.bootstrap4.js"></script>
    <!-- Custom scripts for all pages-->
    <script src="framework/js/sb-admin.min.js"></script>
	    <!-- Custom scripts for this page-->
    <script src="framework/js/sb-admin-datatables.min.js"></script>
  </div>
</body>

</html>
<?php 
}else{
	header("Location:welcome.php");
	die();
}
?>