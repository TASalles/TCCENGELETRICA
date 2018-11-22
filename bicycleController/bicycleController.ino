#include "Joystick.h"

Joystick_ Joystick;

// Set to true to test "Auto Send" mode or false to test "Manual Send" mode.
//const bool testAutoSendMode = true;
const bool testAutoSendMode = false;

const unsigned long gcCycleDelta = 1000;
const unsigned long gcAnalogDelta = 25;
const unsigned long gcButtonDelta = 500;
unsigned long gNextTime = 0;
unsigned int gCurrentStep = 0;

int potPin = 0; // selecione o pino de entrada ao potenciômetro
int val = 0;    // variável a guardar o valor proveniente do sensor

void setup() {
  if (testAutoSendMode)
  {
    Joystick.begin();
  }
  else
  {
    Joystick.begin(false);
  }
  
  pinMode(A0, INPUT_PULLUP);
  pinMode(13, OUTPUT);

  Joystick.setXAxisRange(0, 1023);
  Joystick.setYAxisRange(0, 1023);

  Joystick.setYAxis(511);
  
  Serial.begin(9600);
}

void loop() {

  // System Disabled
//  if (digitalRead(A0) != 0)
//  {
//    digitalWrite(13, 0);
//    return;
//  }

  // Turn indicator light on.
  digitalWrite(13, 1);

  val = analogRead(potPin); // ler o valor do potenciômetro
  Serial.println(val);  
  Joystick.setXAxis(val);
  
  if (testAutoSendMode == false)
    {
      Joystick.sendState();
    }
}
