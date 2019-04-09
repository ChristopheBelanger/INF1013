<?php
$timestamp = time(); 
echo hash('sha256', $timestamp);
?>