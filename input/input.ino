void setup() {
    // put your setup code here, to run once:
    for(int i = 0; i < 3; i++) pinMode(11+i, OUTPUT);
    // for(int i = 0; i < 3; i++) pinMode(2+i, INPUT);
    for(int i = 0; i < 3; i++) digitalWrite(11+i, LOW);
    Serial.begin(19200);/
}

void loop() {
    for(int i = 0; i < 3; i++){
        digitalWrite(13-i, HIGH);
        delay(5);
        for(int j = 0; j < 3; j++){
            int r = digitalRead(4-j);
            // int r = analogRead(4-j);
            // Serial.print(r);
            // Serial.print(", ");

            Serial.print(i);
            Serial.print("\t");
            Serial.print(j);
            Serial.print("\t");
            if(r == HIGH) Serial.print(1);
            else Serial.print(0);
            Serial.println();
        }
        // Serial.println();
        digitalWrite(13-i, LOW);
        // delay(10);
    }
    // Serial.println();
    
    delay(10);
}
