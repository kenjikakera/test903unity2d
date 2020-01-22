<?php

require_once('sql_connect.php');
$pdo = connectDB();
$stmt = $pdo->query("SELECT COUNT(*) FROM hiscore");
echo $stmt->fetchColumn();

?>
