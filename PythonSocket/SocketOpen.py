#!/usr/bin/env python
# -*- coding: utf-8 -*-
#http://www.python-exemplarisch.ch/index_de.php?inhalt_links=navigation_de.inc.php&inhalt_mitte=raspi/de/communication.inc.php
from threading import Thread
import socket
import time
import RPi.GPIO as GPIO
import sys

IP_PORT = 22000

# ---------------------- class SocketHandler ------------------------
class SocketHandler(Thread):
    def __init__(self, conn):
        Thread.__init__(self)
        self.conn = conn
        
    def run(self):
        while True:
            global isConnected
            cmd = ""
            try:
                cmd = self.conn.recv(1024).decode()
                cmd=str(cmd)
            except:
                # happens when connection is reset from the peer
                break
            if cmd == "":
                break
            self.executeCommand(cmd)
        conn.close()
        print ("Verbinung wurde getrennt")
        isConnected = False

    def executeCommand(self, cmd):
        print ("Kommando: "+ cmd)
        self.conn.sendall(bytes(cmd + "\n", "utf-8"))
# ----------------- End of SocketHandler -----------------------

serverSocket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
serverSocket.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1) 
HOSTNAME = "" # Symbolic name meaning all available interfaces
try:
    serverSocket.bind((HOSTNAME, IP_PORT))
except socket.error as msg:
    print ("Fehler Socket bind")
    sys.exit()
serverSocket.listen(10)

print ("Warten auf eingehende Verbinung")
isConnected = False
while True:
    conn, addr = serverSocket.accept()
    print ("Client verbunden ip-> " + addr[0])
    isConnected = True
    socketHandler = SocketHandler(conn)
    socketHandler.setDaemon(True)  
    socketHandler.start()
    while isConnected:
        test=conn._closed
        test=conn._real_close
        time.sleep(1)