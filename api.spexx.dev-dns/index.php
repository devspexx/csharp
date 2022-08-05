<!DOCTYPE html>
<html lang="en">
   
    <head>
        <title>DNS | spexx Dev</title>
        <link rel="stylesheet" href="assets/css/body.css">
        <link rel="preconnect" href="https://fonts.googleapis.com">
        <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
        <link rel="shortcut icon" href="https://spexx.dev/assets/images/branding/favicon.ico">
        <link rel="preconnect" href="https://fonts.googleapis.com">
        <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
        <link href="https://fonts.googleapis.com/css2?family=Ruda:wght@400;500;800" rel="stylesheet">
    </head>
    
    <body id="root">

        <div class="home-top">
            <img src="//spexx.dev/assets/images/branding/favicon.ico" alt="favicon">
            <a href="https://www.spexx.dev/">DNS | spexx dev</a>
        </div>

        <article>
            <table>
                <tr class="heading">
                    <th>COUNTRY</th>
                    <th>PROVIDER</th>
                    <th>HOSTNAME</th>
                    <th>IPv4</th>
                    <th>IPv6</th>
                </tr>
                
                <?php
                    
                    $config = json_decode(file_get_contents("dns.json"), true);

                    $content = "<tr><td><img src=\"assets/images/{COUNTRY}.svg\">{COUNTRY-NAME}</td>
                                <td>{PROVIDER}</td>
                                <td>{HOSTNAME}</td>
                                <td>{IPv4}</td>
                                <td>{IPv6}</td></tr>";

                    foreach ($config as $key => $value) {
                        $combined = str_replace("{COUNTRY}", $value["COUNTRY"], 
                                    str_replace("{COUNTRY-NAME}", $value["COUNTRY-NAME"], 
                                    str_replace("{PROVIDER}", $value["PROVIDER"], 
                                    str_replace("{HOSTNAME}", $value["HOSTNAME"], 
                                    str_replace("{IPv4}", $value["IPv4"], 
                                    str_replace("{IPv6}", $value["IPv6"], $content))))));
                        echo $combined;

                    }   
                ?>

            </table>

            <a href="dns.json">dns.json</a>
        </article>    
    </body>
</html>
