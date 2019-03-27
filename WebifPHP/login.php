<!DOCTYPE HTML>
<html>
<head>
<title>Bootstrap Login</title>

<!-- bootstrap-3.3.7 -->
<link rel="stylesheet" href="bootstrap-3.3.7/css/bootstrap.min.css">
<script src="bootstrap-3.3.7/js/bootstrap.min.js"></script>

<!-- JQUERY -->
<script type="text/javascript" language="javascript" src="jquery/jquery.js"></script>
<link href="style/style.css" rel="stylesheet" type="text/css" media="all"/>
<script type="text/javascript" language="javascript" src="style/style.js"></script>

</head>
<body>

<nav class="navbar navbar-inverse">
  <div class="container-fluid">
    <div class="navbar-header">
      <a class="navbar-brand" href="index.php">BITtruq</a>
    </div>
    <ul class="nav navbar-nav">
      <!--  <li class="active"><a href="#">Accueil</a></li> -->
    </ul>
    <ul class="nav navbar-nav navbar-right">
	  <li><a href="login.php"><span class="glyphicon glyphicon-log-in"></span> Se connecter</a></li>
      <li><a href="register.php"><span class="glyphicon glyphicon-user"></span> S'enregister</a></li>
    </ul>
  </div>
</nav>

<div class="bg">
	<div class="container">
			<div class="card card-container">
			<div class="panel-heading">
				<h3 class="panel-title">Connectez-vous à votre compte !</h3>
			</div>
			<p id="profile-name" class="profile-name-card"></p>
				<form class="form-signin" action="" method="POST">
					<span id="reauth-email" class="reauth-email"></span>
					<input type="email" id="inputEmail" name="email" class="form-control" placeholder="Email address" required autofocus>
					<input type="password" id="inputPassword" name="password" class="form-control" placeholder="Password" required>
					<br>
					<button class="btn btn-lg btn-primary btn-block btn-signin" type="submit"  name="login">Sign in</button>
				</form>
				
			</div>
	</div>
</div>
</body>
</html>
<?php
// load database connection cnfig file
require 'db_con.php';

IF(ISSET($_POST['login'])){
	$email = $_POST['email'];
	$password = $_POST['password'];
	
	$result = mysqli_query($con, "SELECT * FROM users WHERE email='$email'");
	$cek = $result->num_rows;
	$data = mysqli_fetch_array(mysqli_query($con,"SELECT * FROM users WHERE email='$email'"));
	IF($cek > 0)
	{
		
		if(password_verify($password, $data["password"]))  
		{  
		  //return true;  
		  session_start();
		$_SESSION['email'] = $data['email'];
		$_SESSION['name'] = $data['full_name'];
		header('Location:index.php');  
		}  
		else  
		{  
		  //return false;  
			echo "<div>Access denied, check variable is false. <br></br> $password <br></br>  <a href='login.php'>Back.</a></div>";
		}
		
	}else{
	echo "<div>Access denied, no rows returned. <a href='login.php'>Back.</a></div>";	}
}
?>