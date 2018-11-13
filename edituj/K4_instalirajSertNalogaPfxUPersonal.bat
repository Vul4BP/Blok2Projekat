set "putanjaSertifikata=%~dp0Sertifikati\"

:: OVO JE KOMENTAR, DODAJ NA POCETAK AKO NECES NEKI SERTIFIKAT DA SE INSTALIRA

set username1=testReader

set username2=testWriter

set username3=testServis

certutil -f -importpfx -p "12345" "%putanjaSertifikata%%username1%.pfx"
certutil -f -importpfx -p "12345" "%putanjaSertifikata%%username2%.pfx"
certutil -f -importpfx -p "12345" "%putanjaSertifikata%%username3%.pfx"