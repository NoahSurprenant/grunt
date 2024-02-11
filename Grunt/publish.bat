echo off
CALL clean.bat
dotnet pack Grunt
for /f "delims=" %%a in ('dir "Grunt\bin\Release\Surprenant.Grunt.*.nupkg" /b') do set file=%%a
echo Press any key to push %file%
pause
dotnet nuget push Grunt\bin\Release\%file% --source https://api.nuget.org/v3/index.json
pause