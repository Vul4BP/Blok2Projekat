cd C:\Program Files (x86)\Windows Kits\10\bin\10.0.16299.0\x86

set CertFajlIme=MocniciBackePalanke
set username1=testReader
set username1L=testreader

set username2=testWriter
set username2L=testwriter

makecert -n "CN=%CertFajlIme%" -r -sv %CertFajlIme%.pvk %CertFajlIme%.cer

makecert -sv %username1%.pvk -iv %CertFajlIme%.pvk -n "CN=%username1L%,OU=Rider" -pe -ic %CertFajlIme%.cer %username1%.cer -sr localmachine
pvk2pfx.exe /pvk %username1%.pvk /pi 12345 /spc %username1%.cer /pfx %username1%.pfx
winhttpcertcfg -g -c LOCAL_MACHINE\My -s %username1L% -a %username1L%

makecert -sv %username2%.pvk -iv %CertFajlIme%.pvk -n "CN=%username2L%,OU=Vrajter" -pe -ic %CertFajlIme%.cer %username2%.cer -sr localmachine
pvk2pfx.exe /pvk %username2%.pvk /pi 12345 /spc %username2%.cer /pfx %username2%.pfx
winhttpcertcfg -g -c LOCAL_MACHINE\My -s %username2L% -a %username2L%