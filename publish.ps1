$signTool="C:\Program Files (x86)\Microsoft SDKs\ClickOnce\SignTool\signtool.exe"
$certFolder = "C:\temp\cert\CodeSign\"
$cert = "$($certFolder)kd.pfx"
$pass = Get-Content "$($certFolder)kd.pass"
$exe = ".\publish\Safetensors.Inspector.UX.exe"
$md5 = ".\publish\Safetensors.Inspector.UX.md5"

Remove-Item ".\publish\*" -Recurse -Force

dotnet publish ".\solution\Safetensors.Inspector.sln" /p:PublishProfile=FolderProfile -v q

Remove-Item ".\publish\*.config" -Force

& $signTool sign /tr http://timestamp.digicert.com /f $cert /p $pass /td SHA256 /fd SHA256 $exe
(Get-FileHash $exe -Algorithm MD5).Hash > $md5