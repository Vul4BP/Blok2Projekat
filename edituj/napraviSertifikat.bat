cd C:\Program Files (x86)\Windows Kits\10\bin\10.0.16299.0\x86

set CertFajlIme=MocniciBackePalanke

makecert -n "CN=%CertFajlIme%" -r -sv %CertFajlIme%.pvk %CertFajlIme%.cer

makecert -sv WCFService.pvk -iv %CertFajlIme%.pvk -n "CN=wcfservice,OU=" -pe -ic %CertFajlIme%.cer WCFService.cer -sr localmachine
pvk2pfx.exe /pvk WCFService.pvk /pi 12345 /spc WCFService.cer /pfx WCFService.pfx
winhttpcertcfg -g -c LOCAL_MACHINE\My -s wcfservice -a wcfservice

makecert -sv WCFClient.pvk -iv %CertFajlIme%.pvk -n "CN=wcfclient" -pe -ic %CertFajlIme%.cer WCFClient.cer -sr localmachine
pvk2pfx.exe /pvk WCFClient.pvk /pi 12345 /spc WCFClient.cer /pfx WCFClient.pfx
winhttpcertcfg -g -c LOCAL_MACHINE\My -s wcfclient -a wcfclient



makecert -sv WCFService.pvk -iv %CertFajlIme%.pvk -n "CN=wcfservice" -pe -ic %CertFajlIme%.cer WCFService.cer -sr localmachine
pvk2pfx.exe /pvk WCFService.pvk /pi 12345 /spc WCFService.cer /pfx WCFService.pfx
winhttpcertcfg -g -c LOCAL_MACHINE\My -s wcfservice -a wcfservice

makecert -sv WCFClient.pvk -iv %CertFajlIme%.pvk -n "CN=wcfclient" -pe -ic %CertFajlIme%.cer WCFClient.cer -sr localmachine
pvk2pfx.exe /pvk WCFClient.pvk /pi 12345 /spc WCFClient.cer /pfx WCFClient.pfx
winhttpcertcfg -g -c LOCAL_MACHINE\My -s wcfclient -a wcfclient