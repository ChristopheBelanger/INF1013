<?php 
include 'urls.php';
session_start();

$url = $TRANSFER_URL . $_POST['sendTo'] . "/" . $_POST['montant'];



//get the wallet from the API
$ch = curl_init($url);
curl_setopt($ch, CURLOPT_RETURNTRANSFER, false);
curl_setopt($ch, CURLINFO_HEADER_OUT, true);
curl_setopt($ch, CURLOPT_POST, false);
curl_setopt($ch, CURLOPT_FOLLOWLOCATION, true);
curl_setopt($ch, CURLOPT_SSL_VERIFYHOST, 0);
curl_setopt($ch, CURLOPT_SSL_VERIFYPEER, 0);
 
$result = curl_exec($ch);
curl_close($ch);

if(!$result)
	echo "Houston, we have a problem !";

header('Location:../index.php');
?>