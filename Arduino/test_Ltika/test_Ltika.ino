void setup() {
    // put your setup code here, to run once:
    pinMode(OUTPUT, 13);
//    Serial.begin(19200);
    digitalWrite(13, LOW);
}

void loop() {
    // put your main code here, to run repeatedly:
    digitalWrite(13, LOW);
    delay(1000);
    digitalWrite(13, LOW);
    delay(1000);
}
