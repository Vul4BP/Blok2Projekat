@echo off

net localgroup "Krompir" /add

net user "testReader" "12345" /add
net user "testWriter" "12345" /add
net user "testServis" "12345" /add

net user "ClientUser" "12345" /add

net localgroup "Krompir" "ClientUser" /add

WMIC USERACCOUNT WHERE "Name='ClientUser'" SET PasswordExpires=FALSE

WMIC USERACCOUNT WHERE "Name='ClientUser'" SET Passwordchangeable=FALSE