########## ADDING A TEAMMATE ##############

#SPECIFY THE URL
$url = "http://localhost:50647/team"

#SPECIFY THE REQUEST BODY
$body = @{
    FirstName= "Bojan"
    LastName= "Bogdanovic"
    BirthDate= "1989-04-18"
    Height= "6-8"
    PrimaryPosition= "PF"
    IsStarter= "true"
    Interests= "Shooting a jumper, pick-n-rollin, throwing it down with authority."
    Address= "5207 South State Street"
    City= "Murray"
    State= "UT"
    Zipcode= "84107"
}

Write-Host "1. Adding teammate defined in this script (modify script to change attributes)"
Write-Host "   ...calling the ADD endpoint, POST ["$Url" ]"

#CAPTURE ERRORS WHEN CALLING THE ENDPOINT
try {
    #CALL THE API ENDPOINT TO ADD TEAMMATE
    $response = Invoke-RestMethod -Method POST -Uri $url -Body ($body|ConvertTo-Json) -ContentType "application/json"

    #DISPLAY RESPONSE ON THE SCREEN FOR ASSESSMENT REVIEW. 
    Write-Host "   teammate successfully added. The response is:"
    $response | Format-Table | Out-String | Write-Host
}
catch {
    #DISPLAY ERROR ON THE SCREEN. Hopefully this block is not needed.
    if ( $_.Exception.Response -eq $null) {
        Write-Host $_
        Exit
    }
    else {
    Write-Host "   Error:" $_.Exception.Response.StatusCode.value__ $_.Exception.Response.StatusDescription "`r`n"
    }
}

#INTENTIONALLY SLEEP TO GIVE TIME TO SEE RESULT
Start-Sleep -Milliseconds 1000

########## SEARCHING FOR A TEAMMATE ##############

Write-Host "2. Now search for a teammate"
$strQuit = "Y"

While ($strQuit -eq "Y")
{
    $term = Read-Host "   Enter characters for either first or last name" 
    $searchUrl = "$($url)/search/$($term)" 

    Write-Host "   ...calling the SEARCH endpoint, GET ["$searchUrl" ]"
    Write-Host "`r`n"

    #CAPTURE ERRORS WHEN CALLING THE ENDPOINT
    try {
      #CALL THE API ENDPOINT TO SEARCH FOR TEAMMATE
      $response = Invoke-RestMethod -Method GET -Uri $searchUrl -ContentType "application/json"

      #DISPLAY RESPONSE ON THE SCREEN FOR ASSESSMENT REVIEW.
      Write-Host "   The response is:" 
      #$response | Format-Table | Out-String | Write-Host
      Write-Output $response
    }
    catch {
      #DISPLAY ERROR ON THE SCREEN.
      if ( $_.Exception.Response -eq $null) {
        Write-Host $_
        Exit
      }
      else {
        Write-Host "   Error:" $_.Exception.Response.StatusCode.value__ $_.Exception.Response.StatusDescription "`r`n"
      }
    }

    #INTENTIONALLY SLEEP TO GIVE TIME TO SEE RESULT
    Start-Sleep -Milliseconds 1000
    $strQuit = Read-Host "   Search again? (Y/N)"
 }  

 Write-Host "`r`n**Thank you!** :)"
 #Exit
