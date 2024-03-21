int pin = -1;

void setup() {
  Serial.begin(9600);
}

void loop() {
  if (Serial.available() > 0) {
    char cmd = Serial.read();
    if (cmd == 'p') {
      pin = Serial.parseInt();
      pinMode(pin, OUTPUT);
    } 
    else if (cmd == '1') {
      digitalWrite(pin, HIGH);
    } 
    else if (cmd == '0') {
      digitalWrite(pin, LOW);
    }
    else if (cmd == 'c'){
      Serial.println("y");
    }
  }
}
