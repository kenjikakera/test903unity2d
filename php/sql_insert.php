<?php
require_once('sql_connect.php');
$pdo = connectDB();
$n = $_POST['name'];
$s = $_POST['score'];
$stmt = $pdo->query("INSERT INTO hiscore (name,score) VALUES('$n',$s)");
$stmt = $pdo->query("COMMIT");
?>
