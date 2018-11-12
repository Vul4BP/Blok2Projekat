cd C:\Program Files (x86)\Windows Resource Kits\Tools

set username4=testwriter_sign
set username4L=testwriter

set username5=testservis_sign
set username5L=testservis


winhttpcertcfg -g -c LOCAL_MACHINE\My -s %username5% -a %username5L%
winhttpcertcfg -g -c LOCAL_MACHINE\My -s %username4% -a %username4L%