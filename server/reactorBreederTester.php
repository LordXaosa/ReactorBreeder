<?php
if(!$_POST)
{
	die();
}
include('config.php');

$reactorsChecked;
$reactors;
$groups = Array();
$maxX = 0;
$maxY = 0;
$maxZ = 0;
$max = 0;

//$test = "[[[true,false,false,true,true,false,true,true,false],[true,false,true,true,false,true,false,true,true],[false,true,false,true,false,true,false,true,false],[false,false,true,false,true,false,true,true,false],[true,true,true,false,true,true,false,false,false],[false,true,true,true,true,true,true,true,false],[false,false,false,false,true,false,false,false,false],[false,false,false,true,true,false,true,true,true],[false,true,true,true,false,true,true,false,true]],[[false,false,true,false,true,true,true,true,true],[false,false,false,false,false,false,false,false,true],[true,true,true,false,false,true,true,true,false],[false,false,false,true,false,false,false,true,true],[true,true,true,true,true,false,true,false,false],[false,true,false,false,true,false,true,true,true],[false,false,false,true,true,false,false,true,false],[false,false,false,true,false,false,false,false,false],[true,false,true,true,true,true,true,false,true]],[[true,false,true,true,false,false,true,false,true],[true,true,false,true,false,false,true,false,false],[false,true,true,false,true,false,false,true,true],[true,false,false,true,false,false,false,false,false],[true,true,false,true,true,true,true,true,false],[false,false,false,true,true,false,false,false,true],[false,true,false,false,true,false,true,false,true],[false,true,true,false,true,true,true,true,true],[false,true,true,true,false,true,false,true,true]],[[true,true,false,false,false,true,false,false,false],[true,true,true,true,true,true,false,true,false],[true,false,false,false,true,false,true,false,true],[false,false,true,false,false,true,true,false,false],[false,false,false,true,true,true,false,true,true],[true,false,true,false,true,true,true,false,true],[false,false,true,false,false,false,false,false,true],[true,true,true,false,false,true,false,false,false],[true,false,false,true,false,false,false,true,false]],[[true,true,true,true,false,true,true,false,true],[false,true,true,false,false,false,true,false,false],[false,true,false,false,false,false,false,false,false],[false,false,false,true,true,true,false,false,true],[true,true,false,true,false,true,true,true,false],[true,false,true,false,false,true,true,false,false],[false,false,true,false,true,false,true,false,false],[true,true,false,false,false,true,false,true,false],[false,false,false,false,true,false,false,false,false]],[[false,false,false,true,false,true,false,true,false],[true,true,false,false,false,true,true,false,false],[false,true,true,true,true,false,false,false,true],[false,true,true,true,false,false,true,false,false],[true,false,false,false,true,false,true,true,false],[false,false,false,false,true,false,false,false,true],[false,true,false,false,false,true,false,false,false],[false,false,true,false,false,false,true,true,false],[true,true,true,false,true,false,false,false,false]],[[true,true,false,false,false,true,true,false,true],[false,true,true,true,false,true,false,true,true],[true,false,true,false,true,true,false,true,false],[true,true,true,false,true,false,true,false,false],[false,true,true,true,false,true,false,false,false],[true,false,true,false,false,false,false,true,true],[false,true,false,false,false,true,true,false,false],[true,false,true,false,false,true,false,false,false],[true,true,true,false,true,false,false,true,false]],[[true,false,true,true,true,false,false,false,true],[false,true,false,false,false,false,false,true,false],[true,false,false,true,false,false,true,true,true],[false,false,false,true,false,false,false,true,true],[true,true,true,false,true,true,false,true,false],[true,false,false,false,true,true,true,false,false],[false,true,false,true,false,true,true,false,false],[true,true,false,false,false,false,false,true,true],[true,true,true,false,true,false,true,true,false]],[[false,true,false,false,false,true,true,true,false],[true,false,true,true,true,true,true,false,false],[true,true,false,false,false,false,false,false,true],[true,true,true,true,true,false,false,false,false],[true,true,false,true,false,false,true,false,true],[true,true,false,false,true,true,false,true,false],[false,false,true,false,false,true,true,false,true],[true,false,false,true,true,false,false,true,true],[true,false,true,true,false,true,false,false,false]]]";
//max 23905
$sql_con = mysqli_connect($db_host, $db_username, $db_password,$db_name)or die('could not connect to database');
if(isset($_POST["x"]) && isset($_POST["y"]) && isset($_POST["z"]) && isset($_POST["data"]) && isset($_POST["max"]))
{
	$maxX = htmlspecialchars(trim($_POST["x"]));
	$maxY = htmlspecialchars(trim($_POST["y"]));
	$maxZ = htmlspecialchars(trim($_POST["z"]));
	$max = htmlspecialchars(trim($_POST["max"]));
	if($x>100||$y>100||$z>100)
	{
		die();
	}
	$results = mysqli_query($sql_con,"SELECT id,x,y,z,max,data FROM results WHERE x=$maxX AND y=$maxY AND z=$maxZ AND max>=$max ORDER BY max DESC LIMIT 1");
	$counter = 0;
	if($results)
	{
		while($row = mysqli_fetch_array($results))
		{
			$counter++;
		}
	}
	if($counter == 0)
	{
		$data = $_POST["data"];
		GetInfo($data, $maxX, $maxY, $maxZ);
	}
}
else
{
	die();
}

class Group
{
	public $MaxX = 0;
	public $MaxY = 0;
	public $MaxZ = 0;
	public $MinX = 999;
	public $MinY = 999;
	public $MinZ = 999;
	public $Blocks = 0;	
	public $Key = 0;
}
function GetInfo($str, $mx, $my, $mz)
{
	global $maxX;
	global $maxY;
	global $maxZ;
	global $reactors;
	global $reactorsChecked;
	global $max;
	global $groups;
	global $sql_con;
	
	$maxX = $mx;
	$maxY = $my;
	$maxZ = $mz;
	
	
	for($x = 0; $x < $mx; $x++)
	{
		for($y = 0; $y < $my; $y++)
		{
			for($z = 0; $z < $mz; $z++)
			{
				$reactorsChecked[$x][$y][$z] = false;
			}
		}
	}
	$reactors = json_decode($str, true);
	for($x = 0; $x < $mx; $x++)
	{
		for($y = 0; $y < $my; $y++)
		{
			for($z = 0; $z < $mz; $z++)
			{
				if($reactorsChecked[$x][$y][$z] != true)
				{
					CheckGroup($x,$y,$z,null);
				}
			}
		}
	}
	$total = 0;
	for($i = 0; $i < count($groups); $i++)
	{
		$dims = $groups[$i]->MaxX - $groups[$i]->MinX + $groups[$i]->MaxY - $groups[$i]->MinY + $groups[$i]->MaxZ - $groups[$i]->MinZ + 3;
        $total += (2000000.0 / (1.0 + pow(1.000696, (-0.333 * pow(($dims / 3.0), 1.7)))) - 1000000.0 + 25.0 * $groups[$i]->Blocks);
	}
	
		
	if($total > $max)
	{
		$max1 = 0;
		$results = mysqli_query($sql_con,"SELECT id,x,y,z,max,data FROM results WHERE x=$maxX AND y=$maxY AND z=$maxZ ORDER BY max DESC LIMIT 1");
		$counter = 0;
		if($results)
		{
			while($row = mysqli_fetch_array($results))
			{
				$max1 = $row["max"];
			}
		}
		if($max1 < $total)
		{
			$data = json_encode($reactors);
			$results = mysqli_query($sql_con,"SELECT id,x,y,z,max,data FROM results WHERE x=$maxX AND y=$maxY AND z=$maxZ ORDER BY max DESC LIMIT 1");
			$counter = 0;
			$counter = 0;
			while($row = mysqli_fetch_array($results))
			{
				$counter++;
			}
			if($counter == 0)
			{
				mysqli_query($sql_con,"INSERT INTO results(x,y,z,max,data) VALUES ($maxX,$maxY,$maxZ, $total, '$data')");
			}
			else
			{
				mysqli_query($sql_con,"UPDATE results SET max=$total, data='$data' WHERE x=$maxX AND y=$maxY AND z=$maxZ");
			}
		}
		
		$max = $total;
		//записываем в базу
	}
}
class Point
{
	public $X;
	public $Y;
	public $Z;
	
	function __construct($x,$y,$z) {
       $this->X = $x;
	   $this->Y = $y;
	   $this->Z = $z;
   }
}
function CheckGroup($x, $y, $z, $group)
        {
			global $reactors;
			global $reactorsChecked;
			global $maxX;
			global $maxY;
			global $maxZ;
			global $groups;
            //$reactorsChecked[$x][$y][$z] = true;
            if ($reactors[$x][$y][$z] == true)
            {
                if (!$group)
                {
                    $group = new Group();
                    $group->MaxX = $x;
                    $group->MaxY = $y;
                    $group->MaxZ = $z;
                    $group->MinX = $x;
                    $group->MinY = $y;
                    $group->MinZ = $z;
                    $group->Blocks = 0;
					$group->Key = rand();
                }
				
				$stack = array();
				$point = new Point($x,$y,$z);
				array_push($stack,$point);
				while(count($stack)>0)
				{
					$p = array_pop($stack);
					
					
					if ($p->X >= $maxX || $p->X < 0 || $p->Y >= $maxY || $p->Y < 0 || $p->Z >= $maxZ || $p->Z < 0)
                    {
                        continue;
                    }
                    if ($reactors[$p->X][$p->Y][$p->Z] == true && $reactorsChecked[$p->X][$p->Y][$p->Z] == false)
                    {
                        $group->Blocks++;
                        $reactorsChecked[$p->X][$p->Y][$p->Z] = true;
                        $group->MaxX = max($group->MaxX, $p->X);
                        $group->MaxY = max($group->MaxY, $p->Y);
                        $group->MaxZ = max($group->MaxZ, $p->Z);
                        $group->MinX = min($group->MinX, $p->X);
                        $group->MinY = min($group->MinY, $p->Y);
                        $group->MinZ = min($group->MinZ, $p->Z);
						

                        array_push($stack, new Point($p->X+1,$p->Y,$p->Z));
                        array_push($stack, new Point($p->X-1,$p->Y,$p->Z));

                        array_push($stack, new Point($p->X,$p->Y+1,$p->Z));
                        array_push($stack, new Point($p->X,$p->Y-1,$p->Z));

                        array_push($stack, new Point($p->X,$p->Y,$p->Z+1));
                        array_push($stack, new Point($p->X,$p->Y,$p->Z-1));
                    }
					
				}
				array_push($groups, $group);
            }
        }