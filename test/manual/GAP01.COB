       IDENTIFICATION DIVISION.
       PROGRAM-ID. GAP01.
       PROCEDURE DIVISION.
       MAIN.
        DISPLAY ':' WITH NO ADVANCING.
        PERFORM A THROUGH B.
        DISPLAY ';' WITH NO ADVANCING.
       AGAIN.
        ALTER PATH TO PROCEED TO B.
        PERFORM A THROUGH B.
        DISPLAY '!'.
       FINISH.
        STOP RUN.
       A.
        DISPLAY 'A' WITH NO ADVANCING.
       PATH.
        GO TO SKIP.
       B.
        DISPLAY 'B' WITH NO ADVANCING.
       SKIP.
        DISPLAY '-' WITH NO ADVANCING.
       F.
        GO TO AGAIN.