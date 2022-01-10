bool isLighting = true;

void setup() {
  // put your setup code here, to run once:
  pinMode(13, OUTPUT);
  digitalWrite(13, LOW);
  Serial.begin(19200);
}

void loop() {
  // put your main code here, to run repeatedly:
  if(Serial.available()){
    Serial.read();
    isLighting = !isLighting;
    digitalWrite(13, isLighting);
  }
}
