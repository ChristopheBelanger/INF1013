<?php 
error_reporting(0);
ini_set('display_errors', 0);
session_start();
$TRANSACTIONS_URL = "http://localhost:5000/api/Client/" . $_SESSION['wallet'];
$TRANSFER_URL = "http://localhost:5000/api/Client/";
$WALLET_URL = "http://localhost:5000/api/Wallet";
?>
