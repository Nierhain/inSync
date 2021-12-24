package de.nierhain.insync.core;

import java.util.Base64;
import java.util.Base64.Encoder;
import java.util.Base64.Decoder;

public class ItemEncoder {

    public void encode(){
        Encoder encoder = Base64.getEncoder();
        TestObject test = new TestObject("testString", 1337, true);

    }

    static class TestObject{
        String name;
        int number;
        boolean value;

        public TestObject(String name, int number, boolean value){
            this.name = name;
            this.number = number;
            this.value = value;
        }
    }
}
