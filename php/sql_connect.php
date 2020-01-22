<?php

function connectDB(){

	try {
		$pdo = new PDO('mysql:host=127.0.0.1;dbname=test905;charset=utf8',getenv(DBUSER),getenv(DBPASS));
	} catch (PDOException $e) {
		exit('' . $e->getMessage());
	}

	 return $pdo;
}

?>
