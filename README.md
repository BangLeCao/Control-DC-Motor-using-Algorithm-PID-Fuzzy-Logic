# Control-DC-Motor-using-Algorithm-PID-Fuzzy-Logic
This project uses STM32F4 microcontroller to apply PID algorithm to control DC motor position and speed.
PID algorithm applies fuzzy logic to increase control accuracy and control quality.


Via USB communication between microcontroller and PC to transmit and receive data.
The PC will perform the control by sending commands to the microcontroller, then receiving the data sent back from the microcontroller to collect data and graph the response.
USB communication uses handshaking rules between PC and microcontroller to avoid the loss of data packets for transmission and reception.

Source code Keil C : Control-DC-Motor-using-Algorithm-PID-Fuzzy-Logic/Keil/Src/main.c

Source code C# applocation : Control-DC-Motor-using-Algorithm-PID-Fuzzy-Logic/C# application/fuzzy_control
