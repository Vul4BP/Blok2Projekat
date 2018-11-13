@echo off

net localgroup "Krompir" /add

net user "testreader" "12345" /add
net user "testwriter" "12345" /add
net user "testservis" "12345" /add

net user "clientuser" "12345" /add

net localgroup "Krompir" "clientuser" /add

WMIC USERACCOUNT WHERE "Name='clientuser'" SET PasswordExpires=FALSE

WMIC USERACCOUNT WHERE "Name='clientuser'" SET Passwordchangeable=FALSE