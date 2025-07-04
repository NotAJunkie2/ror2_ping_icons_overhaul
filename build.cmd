@echo off
set MODE=Debug

if /I "%1"=="Release" (
    set MODE=Release
)

echo Building in %MODE% mode...

copy .\src\PingIconsOverhaul\pingiconsoverhaul .\artifacts\bin\PingIconsOverhaul\%MODE%\pingiconsoverhaul
dotnet build -c %MODE% -v:d ^
    /p:DebugSymbols=true ^
    /p:DebugType=portable

if /I "%MODE%"=="Release" (
    dotnet build -c Release -t:PackTS -v:d ^
    /p:DebugSymbols=true ^
    /p:DebugType=portable
)