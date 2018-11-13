cd C:\Program Files (x86)\Windows Kits\10\bin\10.0.16299.0\x86

set "putanjaSertifikata=%~dp0Sertifikati\"
set CertFajlIme=MocniciBackePalanke

set username2L=testwriter

set username3L=testservis

set certName1=SignWriter
set certName2=SignServis


makecert -sv "%putanjaSertifikata%%certName2%.pvk" -iv "%putanjaSertifikata%%CertFajlIme%.pvk" -n "CN=%username3L%_sign" -pe -ic "%putanjaSertifikata%%CertFajlIme%.cer" "%putanjaSertifikata%%certName2%.cer" -sr localmachine -ss My -sky signature
pvk2pfx.exe /pvk "%putanjaSertifikata%%certName2%.pvk" /pi 12345 /spc "%putanjaSertifikata%%certName2%.cer" /pfx "%putanjaSertifikata%%certName2%.pfx"

makecert -sv "%putanjaSertifikata%%certName1%.pvk" -iv "%putanjaSertifikata%%CertFajlIme%.pvk" -n "CN=%username2L%_sign" -pe -ic "%putanjaSertifikata%%CertFajlIme%.cer" "%putanjaSertifikata%%certName1%.cer" -sr localmachine -ss My -sky signature
pvk2pfx.exe /pvk "%putanjaSertifikata%%certName1%.pvk" /pi 12345 /spc "%putanjaSertifikata%%certName1%.cer" /pfx "%putanjaSertifikata%%certName1%.pfx"