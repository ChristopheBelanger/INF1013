<?php 
session_start();
$TRANSACTIONS_URL = "http://localhost/inf1013/WebifPHP/transactions.json?" . $_SESSION['wallet'];
$WALLET_URL = "http://localhost/inf1013/WebifPHP/wallet.php";
?>
