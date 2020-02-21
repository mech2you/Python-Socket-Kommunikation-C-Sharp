#!/usr/bin/env python
# -*- coding: utf-8 -*-
#http://www.python-exemplarisch.ch/index_de.php?inhalt_links=navigation_de.inc.php&inhalt_mitte=raspi/de/communication.inc.php
from threading import Thread
import socket
import time
import RPi.GPIO as GPIO
import sys
import Adafruit_BMP.BMP085 as BMP085
import time
import Adafruit_PCA9685
pwm = Adafruit_PCA9685.PCA9685()
servo_min = 150  # Min pulse length out of 4096
servo_max = 600  # Max pulse length out of 4096
def set_servo_pulse(channel, pulse):
    pulse_length = 1000000    # 1,000,000 us per second
    pulse_length //= 60       # 60 Hz
    print('{0}us per period'.format(pulse_length))
    pulse_length //= 4096     # 12 bits of resolution
    print('{0}us per bit'.format(pulse_length))
    pulse *= 1000
    pulse //= pulse_length
    pwm.set_pwm(channel, 0, pulse)

pwm.set_pwm_freq(60)

IP_PORT = 22000
sensor = BMP085.BMP085()

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
        #print ("Kommando: "+ cmd)
        if cmd=="Druck":
            tmp=str(sensor.read_pressure())
            self.conn.sendall(bytes(tmp+ "\n", "utf-8"))
        
        if cmd=="Temp":
            tmp=str(sensor.read_temperature())
            self.conn.sendall(bytes(tmp+ "\n", "utf-8"))
        if cmd=="Servo0":
            pwm.set_pwm(0, 0, servo_min)
            self.conn.sendall(bytes("Servo zu\n", "utf-8"))
        if cmd=="Servo1":
            pwm.set_pwm(0, 0, servo_max)
            self.conn.sendall(bytes("Servo auf\n", "utf-8"))
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