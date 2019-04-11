<?php 
include 'urls.php';
session_start();

// load database connection config file
require 'db_con.php';

//get the wallet from the API
$ch = curl_init($WALLET_URL);
curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);
curl_setopt($ch, CURLINFO_HEADER_OUT, true);
curl_setopt($ch, CURLOPT_POST, true);
curl_setopt($ch, CURLOPT_POSTFIELDS, $_SESSION['email']);
curl_setopt($ch, CURLOPT_FOLLOWLOCATION, true);
curl_setopt($ch, CURLOPT_SSL_VERIFYHOST, 0);
curl_setopt($ch, CURLOPT_SSL_VERIFYPEER, 0);
 
// Set HTTP Header for POST request 
curl_setopt($ch, CURLOPT_HTTPHEADER, array(
    'Content-Type: text/plain',
    'Content-Length: ' . strlen($_SESSION['email']))
);
 
$wallet = curl_exec($ch);
curl_close($ch);

//Prepare to update database with wallet
$sql = "UPDATE users SET wallet = '" . $wallet . "' WHERE email = '" . $_SESSION['email'] . "'";

//Update database
if ($con->query($sql) === TRUE)
	{
		//Success
		$_SESSION['wallet'] = $wallet;
		header('Location:../index.php'); 
	}else{
		//Error
		echo "<div>Unable to register your wallet:  " . $_SESSION['wallet'] . "</div>";
	}
?>