<?php
$chkpdo = new PDO('mysql:host=127.0.0.1;dbname=test905;charset=utf8',getenv(DBUSER),getenv(DBPASS));
if($chkpdo == null){
	echo "0";
} else {
	echo "1";
}
?>
