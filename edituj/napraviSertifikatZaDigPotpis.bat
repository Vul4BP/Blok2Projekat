cd C:\Program Files (x86)\Windows Kits\10\bin\10.0.16299.0\x86

set CertFajlIme=MocniciBackePalanke
set username1=testReader
set username1L=testreader

set username2=testWriter
set username2L=testwriter

set username3=testServis
set username3L=testservis

set username4=Sign1
set username5=Sign2


makecert -sv %username5%.pvk -iv %CertFajlIme%.pvk -n "CN=%username3L%_sign" -pe -ic %CertFajlIme%.cer %username5%.cer -sr localmachine -ss My -sky signature
pvk2pfx.exe /pvk %username5%.pvk /pi 12345 /spc %username5%.cer /pfx %username5%.pfx
makecert -sv %username4%.pvk -iv %CertFajlIme%.pvk -n "CN=%username2L%_sign" -pe -ic %CertFajlIme%.cer %username4%.cer -sr localmachine -ss My -sky signature
pvk2pfx.exe /pvk %username4%.pvk /pi 12345 /spc %username4%.cer /pfx %username4%.pfx

