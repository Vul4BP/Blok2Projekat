cd C:\Program Files (x86)\Windows Resource Kits\Tools

set cnWriterSignSertifikata=testwriter_sign
set username4L=testwriter

set cnServisSignSertifikata=testservis_sign
set username5L=testservis


winhttpcertcfg -g -c LOCAL_MACHINE\My -s %cnServisSignSertifikata% -a %username5L%
winhttpcertcfg -g -c LOCAL_MACHINE\My -s %cnWriterSignSertifikata% -a %username4L%