// #include <Arduino.h>
// 通信量を削減
// 接続:1, 非接続:0としてバイナリデータで表現
// 9通りの接続状態を9bitで通信。通信量は1/18に。
void setup() {
    for(int i = 0; i < 3; i++) pinMode(11+i, OUTPUT);
    for(int i = 0; i < 3; i++) digitalWrite(11+i, LOW);
    Serial.begin(19200);
}

void loop() {
    // put your main code here, to run repeatedly:
    int send = 0;

    // D13, D12, D11を1つずつHIGHにしてdigitalReadを行う
    for(int i = 0; i < 3; i++){
        digitalWrite(13-i, HIGH);
        delay(5);
        for(int j = 0; j < 3; j++){
            int r = digitalRead(4-j);
            int index = 3*i + j;
            if(r == HIGH) send += (1 << index); // (1 << index) は 「2のindex乗」と等しい。理由はシフト演算で検索。
        }
        digitalWrite(13-i, LOW);
    }
    Serial.print(send);
    Serial.println();
    
    delay(10);
}
