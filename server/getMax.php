<?php
if(!$_POST)
{
	die();
}
include('config.php');

if(isset($_POST["x"]) && isset($_POST["y"]) && isset($_POST["z"]))
{
	$x = htmlspecialchars(trim($_POST["x"]));
	$y = htmlspecialchars(trim($_POST["y"]));
	$z = htmlspecialchars(trim($_POST["z"]));
	if($x>100||$y>100||$z>100)
	{
		die();
	}
	$sql_con = mysqli_connect($db_host, $db_username, $db_password,$db_name)or die('could not connect to database');
	$results = mysqli_query($sql_con,"SELECT id,x,y,z,max,data FROM results WHERE x=$x AND y=$y AND z=$z ORDER BY max DESC LIMIT 1");
	$max = 0;
	$data = "";
	if($results)
	{
		while($row = mysqli_fetch_array($results))
		{
			$max = $row["max"];
			$data = $row["data"];
		}
	}
	echo $max;
	echo "|";
	echo $data;
}