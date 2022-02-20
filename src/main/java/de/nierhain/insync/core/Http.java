package de.nierhain.insync.core;

import java.io.IOException;
import java.net.http.HttpClient;


public class Http {
    public void makeRequest(String token, String secret) throws IOException {
        String baseURL = "http://localhost:5000/api/ItemList/";
        var client = HttpClient.newHttpClient();
    }
}
