<?php 
session_start();

// load database connection config file
require 'db_con.php';

//get the wallet from the API

$url='http://localhost/inf1013/WebifPHP/wallet.php';
$ch = curl_init($url);
curl_setopt($ch,CURLOPT_RETURNTRANSFER,1);
curl_setopt($ch,CURLOPT_TIMEOUT,10);
$wallet = curl_exec($ch);
curl_close($ch);

//Prepare to update database with wallet
$sql = "UPDATE users SET wallet = '" . $wallet . "' WHERE email = '" . $_SESSION['email'] . "'";

//Update database
if ($con->query($sql) === TRUE)
	{
		//Success
		$_SESSION['wallet'] = $wallet;
		header('Location:index.php'); 
	}else{
		//Error
		echo "<div>Unable to register your wallet.</div>";
	}
?>