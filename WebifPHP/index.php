<?php 
define("TRANSACTIONS_URL", "http://localhost/inf1013/WebifPHP/transactions.json");
define("WALLLET_URL", "http://localhost/inf1013/WebifPHP/wallet.php");
session_start();
IF(ISSET($_SESSION['name'])){
header("Location:account.php");
	die();
}else{
	header("Location:welcome.php");
	die();
}
?>