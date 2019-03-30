<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta name="viewport" content="width=device-width,initial-scale=1,maximum-scale=1,user-scalable=no" />
<title>BITtruq Enregistrement</title>
<!-- framework/bootstrap-3.3.7 -->
<link rel="stylesheet" href="framework/bootstrap-3.3.7/css/bootstrap.min.css">
<script src="framework/bootstrap-3.3.7/framework/js/bootstrap.min.js"></script>

<!-- JQUERY -->
<script type="text/javascript" language="javascript" src="jquery/jquery.js"></script>
<link href="css/sb-admin-style.css" rel="stylesheet" type="text/css" media="all"/>
<link type="text/css" rel="stylesheet" href="css/sb-admin-resister.css" />
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
			<h3 class="panel-title">Enregistrez-vous sur BITtruq !</h3>
		</div>
			<div class="panel-body">			
				<?php
				// save the username and password
				if($_POST){
				 
					try{
						// load database connection and password hasher library
						require 'include/db_con.php';
						 
						/* 
						 * -prepare password to be saved
						 */
						 
						 $password = $_POST['password'];
						 
						/* 
						 * hashing password with password_hash()
						 */
						
						$password = password_hash($password, PASSWORD_DEFAULT);  

						// insert command
						$query = "INSERT INTO users SET email = ?, password = ?, full_name = ?" ;
			 
						$stmt = $con->prepare($query);
			 
						$stmt->bind_param("sss", $_POST['email'], $password, $_POST['user']);
			 
						// execute the query
						if($stmt->execute()){
							echo "<div>Enregistrement réussi.</div>";
							session_start();
							$_SESSION['email'] = $_POST['email'];
							$_SESSION['name'] = $_POST['user'];
						 	header('Location:index.php');
						}else{
							echo "<div>Unable to register. <a href='register.php'>Please try again.</a></div>";
						}
					}
					 
					//to handle error
					catch(PDOException $exception){
						echo "Error: " . $exception->getMessage();
					}
				}
				 
				// show the registration form
				else{
				?>
			<form action="register.php" method="post">

				<div class="form-group">
					<input type="text" name="user" id="username" class="form-control input-sm" placeholder="Username" required autofocus>
				</div>

				<div class="form-group">
					<input type="email" name="email" id="email" class="form-control input-sm" placeholder="Email Address" required>
				</div>

				<div class="row">
					<div class="col-xs-6 col-sm-6 col-md-6">
						<div class="form-group">
							<input type="password" name="password" id="password" class="form-control input-sm" placeholder="Password" required>
						</div>
					</div>
					<div class="col-xs-6 col-sm-6 col-md-6">
						<div class="form-group">
							<input type="password" name="password_confirmation" id="password_confirmation" class="form-control input-sm" placeholder="Confirm Password" required>
						</div>
					</div>
				</div>
				
				<input type="submit" value="Register" class="btn btn-lg btn-primary btn-block btn-signin">

			</form>
			<?php
			}
			?>
			</div>
		</div>
	</div>
</div>
</body>
</html>