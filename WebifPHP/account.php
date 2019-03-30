<?php 
include 'urls.php';
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
  <link href="vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet">
  <!-- Custom fonts for this template-->
  <link href="vendor/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css">
  <!-- Custom styles for this template-->
  <link href="css/sb-admin.css" rel="stylesheet">
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
            <span class="nav-link-text">Compte</span>
          </a>
        </li>
        <li class="nav-item" data-toggle="tooltip" data-placement="right" title="Transactions">
          <a class="nav-link nav-link-collapse" data-toggle="collapse" href="#collapseExamplePages" data-parent="#exampleAccordion">
            <i class="fa fa-fw fa-file"></i>
            <span class="nav-link-text">Transactions</span>
          </a>
          <ul class="sidenav-second-level collapse" id="collapseExamplePages">
            <li>
              <a href="tables.php" class="nav-link-text">Tableau</a>
            </li>
            <li>
              <a href="charts.php" class="nav-link-text">Graphique</a>
            </li>
          </ul>
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
      <!-- Breadcrumbs-->
      <ol class="breadcrumb">
        <li class="breadcrumb-item">
          <a href="index.php">Compte</a>
        </li>
        <li class="breadcrumb-item active">Porte feuille</li>
      </ol>
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
					<div class=\"form-row\">
						<div class=\"col-md-8 mb-3 text-center\">
						<br>
						  <input type=\"text\" class=\"form-control\" id=\"validationCustom03\" placeholder=\"Numero de portefeuille\" required>
						</div>
						<div class=\"col-md-2 mb-3 text-center\">
						<br>
						  <input type=\"text\" class=\"form-control\" id=\"validationCustom04\" placeholder=\"Montant\" required>
						</div>
						<div class=\"col-md-2 mb-3 text-center\">
						<br>
							<a class=\"btn btn-primary pull-center\" href=\"#\" role=\"button\">Transferer</a>

						</div>
					  </div>
					</div>
				</div>
			</div></div></div></div>
			
			";
		} else {
			echo "
			<p>Aucun porte feuille configuré pour ce compte.</p>
			<br>
			<p>Cliquer sur le bouton pour obtenir un porte feuille BITtruq.</p>
			<div class=\"col-md-5 col-sm-5 col-xs-12 gutter\">
                <a class='btn btn-primary' href='getWallet.php' role='button'>Obtenir un Porte feuille</a>
            </div>
			";
		} ?>
        <!--  
		<h1>Blank</h1>
          <p>This is an example of a blank page that you can use as a starting point for creating new ones.</p>
		  -->
        </div>
      </div>
    </div>
    <!-- Bootstrap core JavaScript-->
    <script src="vendor/jquery/jquery.min.js"></script>
    <script src="vendor/bootstrap/js/bootstrap.bundle.min.js"></script>
    <!-- Core plugin JavaScript-->
    <script src="vendor/jquery-easing/jquery.easing.min.js"></script>
    <!-- Custom scripts for all pages-->
    <script src="js/sb-admin.min.js"></script>
  </div>
</body>

</html>
<?php 
}else{
	header("Location:welcome.php");
	die();
}
?>