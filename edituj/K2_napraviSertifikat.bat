cd C:\Program Files (x86)\Windows Kits\10\bin\10.0.16299.0\x86

set "putanjaSertifikata=%~dp0Sertifikati\"

set CertFajlIme=MocniciBackePalanke
set username1=testReader
set username1L=testreader

set username2=testWriter
set username2L=testwriter

set username3=testServis
set username3L=testservis


makecert -n "CN=%CertFajlIme%" -r -sv "%putanjaSertifikata%%CertFajlIme%.pvk" "%putanjaSertifikata%%CertFajlIme%.cer"

makecert -sv "%putanjaSertifikata%%username1%.pvk" -iv "%putanjaSertifikata%%CertFajlIme%.pvk" -n "CN=%username1L%,OU=Rider" -pe -ic "%putanjaSertifikata%%CertFajlIme%.cer" "%putanjaSertifikata%%username1%.cer" -sr localmachine -ss My -sky exchange
pvk2pfx.exe /pvk "%putanjaSertifikata%%username1%.pvk" /pi 12345 /spc "%putanjaSertifikata%%username1%.cer" /pfx "%putanjaSertifikata%%username1%.pfx"

makecert -sv "%putanjaSertifikata%%username2%.pvk" -iv "%putanjaSertifikata%%CertFajlIme%.pvk" -n "CN=%username2L%,OU=Vrajter" -pe -ic "%putanjaSertifikata%%CertFajlIme%.cer" "%putanjaSertifikata%%username2%.cer" -sr localmachine -ss My -sky exchange
pvk2pfx.exe /pvk "%putanjaSertifikata%%username2%.pvk" /pi 12345 /spc "%putanjaSertifikata%%username2%.cer" /pfx "%putanjaSertifikata%%username2%.pfx"

makecert -sv "%putanjaSertifikata%%username3%.pvk" -iv "%putanjaSertifikata%%CertFajlIme%.pvk" -n "CN=%username3L%,OU=Servis" -pe -ic "%putanjaSertifikata%%CertFajlIme%.cer" "%putanjaSertifikata%%username3%.cer" -sr localmachine -ss My -sky exchange
pvk2pfx.exe /pvk "%putanjaSertifikata%%username3%.pvk" /pi 12345 /spc "%putanjaSertifikata%%username3%.cer" /pfx "%putanjaSertifikata%%username3%.pfx"