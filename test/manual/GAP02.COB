       IDENTIFICATION DIVISION.
       PROGRAM-ID. GAP02.
       PROCEDURE DIVISION.
       MAIN.
        DISPLAY ':' WITH NO ADVANCING.
        PERFORM A THROUGH C.
        DISPLAY ';' WITH NO ADVANCING.
       AGAIN.
        ALTER PATH TO PROCEED TO ALT-FIN.
        PERFORM A THROUGH C.
        DISPLAY '!'.
       FINISH.
        STOP RUN.
       A.
        DISPLAY 'A' WITH NO ADVANCING.
       PATH.
        GO TO C.
       B.
        DISPLAY 'B' WITH NO ADVANCING.
       C.
        DISPLAY 'C' WITH NO ADVANCING.
       ALT-FIN.
        DISPLAY '-' WITH NO ADVANCING.
        STOP RUN.
