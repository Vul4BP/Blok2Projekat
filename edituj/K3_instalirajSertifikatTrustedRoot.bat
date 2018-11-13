set "putanjaSertifikata=%~dp0Sertifikati\"

set CertFajlIme=MocniciBackePalanke

certutil -addstore -f -enterprise "Root" "%putanjaSertifikata%%CertFajlIme%.cer"