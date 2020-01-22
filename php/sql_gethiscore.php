<?php
require_once('sql_connect.php');
$pdo = connectDB();
$stmt = $pdo->query("SELECT * FROM hiscore where score=(select max(score) from hiscore)");
foreach ($stmt as $row) {
	echo $row['name'];
	echo ',';
	echo $row['score'];
	echo ',';
}
?>
