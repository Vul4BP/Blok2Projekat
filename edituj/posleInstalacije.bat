cd C:\Program Files (x86)\Windows Resource Kits\Tools

set username1=testReader
set username1L=testreader

set username2=testWriter
set username2L=testwriter

set username3=testServis
set username3L=testservis

winhttpcertcfg -g -c LOCAL_MACHINE\My -s %username1L% -a %username1L%

winhttpcertcfg -g -c LOCAL_MACHINE\My -s %username2L% -a %username2L%

winhttpcertcfg -g -c LOCAL_MACHINE\My -s %username3L% -a %username3L%