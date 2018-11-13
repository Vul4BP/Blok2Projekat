set "putanjaSertifikata=%~dp0Sertifikati\"

:: OVO JE KOMENTAR, DODAJ NA POCETAK AKO NECES NEKI SERTIFIKAT DA SE INSTALIRA

set username1=testReader

set username2=testWriter

set username3=testServis

certutil -addstore -f -user My "%putanjaSertifikata%%username1%.cer"
certutil -addstore -f -user My "%putanjaSertifikata%%username2%.cer"
certutil -addstore -f -user My "%putanjaSertifikata%%username3%.cer"