<?php 
$hostname = "localhost";
$username = "inf1013";
$password = "inf1013";
$database = "inf1013_tp_webif";

$con=mysqli_connect($hostname,$username,$password,$database);
// Check connection
if (mysqli_connect_errno())
  {
  echo "Failed to connect to MySQL: " . mysqli_connect_error();
  }
?>