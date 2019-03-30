<?php 
error_reporting(0);
ini_set('display_errors', 0);
session_start();
$TRANSACTIONS_URL = "http://localhost/inf1013/WebifPHP/emulateurWebService/transactions.json?" . $_SESSION['wallet'];
$WALLET_URL = "http://localhost/inf1013/WebifPHP/emulateurWebService/wallet.php";
?>
