



 <?php
               

echo $_POST["name"];
    $myfile = fopen("stato.txt", "w") or die("Unable to open file!");
              $txt = $_POST["name"];
                 fwrite($myfile, $txt);
                fclose($myfile);

                
header( 'Location: /index.php' ) ; 

          ?> 


