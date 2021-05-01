/**
 *
 *  @author Kazimierczyk Konrad S18361
 *
 */

package zad1;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;
import org.json.XML;
import org.jsoup.Jsoup;
import org.jsoup.nodes.Document;
import org.jsoup.select.Elements;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.net.MalformedURLException;
import java.net.URL;
import java.net.URLConnection;
import java.util.Currency;
import java.util.Locale;

public class Service {
    private final Weather w;
    private double rateCustom;
    private String rateCustomName;
    private JSONObject nbpRateObj;

    public Service(String c) {
        w = new Weather(c);
    }


    String getWeather(String c) {

        String json;
        //http://api.openweathermap.org/data/2.5/weather?q=Warsaw,Poland&APPID=ef684ca343ba36cb7f605a7c518f7f70
        json = getContentFromUrl("http://api.openweathermap.org/data/2.5/weather?q=" + c + "," + w.getCountry() + "&APPID=ef684ca343ba36cb7f605a7c518f7f70");
        //assert json != null;
        if(json==null){
            throw new AssertionError();
        }

        if (!json.contains("\"cod\":\"404\""))
            w.initialize(json);


        return json;
    }

    Double getRateFor(String v) {

        double tmp = 0;
        String jsonData = getContentFromUrl("https://api.exchangerate.host/latest?base=" + w.getCountryCurrencyCode());

        if (jsonData != null) {
            JSONObject jsonObject = new JSONObject(jsonData);
            tmp = jsonObject.getJSONObject("rates").optDouble(v);
            rateCustom = tmp;
            rateCustomName = v;
        }
        return tmp;
    }

    Double getNBPRate() {//zwraca kurs złotego wobec waluty danego kraju

        String nbpA = getContentFromUrl("http://www.nbp.pl/kursy/xml/a057z170322.xml");
        String nbpB = getContentFromUrl("http://www.nbp.pl/kursy/xml/b012z170322.xml");

        if (!nbpA.isEmpty() && !nbpB.isEmpty()) {
            JSONObject jsonA = XML.toJSONObject(nbpA);
            JSONObject jsonB = XML.toJSONObject(nbpB);
            JSONArray jsonArray = new JSONArray();
            jsonArray.put(jsonA);
            jsonArray.put(jsonB);
            double rateToPLN = 0.0;

//          JSONObject obj = jsonObjectA.optJSONObject(weather.getCountryCurrencyCode());
            JSONObject objCurrency = findCurrencyRateNBP(jsonArray);
            if (objCurrency != null) {
                rateToPLN = objCurrency.optDouble("kurs_sredni");
                nbpRateObj = objCurrency;
            }
            return rateToPLN;
        }
        return null;
    }

    private JSONObject findCurrencyRateNBP(JSONArray json) {
        if (w.getCountryCurrencyCode().equals("PLN")) {
            return new JSONObject("{\"kurs_sredni\":\"1\",\"kod_waluty\":\"PLN\",\"nazwa_waluty\":\"polski zloty\",\"przelicznik\":1}");
        }
        JSONObject objTmp;
        for (int i = 0; i < json.length(); i++) {
            for (int j = 0; j < json.getJSONObject(i).optJSONObject("tabela_kursow").getJSONArray("pozycja").length(); j++) {
                objTmp = json.getJSONObject(i).optJSONObject("tabela_kursow").getJSONArray("pozycja").getJSONObject(j);
                if (objTmp.optString("kod_waluty").equals(w.getCountryCurrencyCode())) {
                    return objTmp;
                }
            }
        }
        return null;
    }

    static public String getContentFromUrl(String url) {
        StringBuffer textData = null;
        URLConnection urlConnection;
        URL myURL;

        try {
            myURL = new URL(url);
            urlConnection = myURL.openConnection();
            urlConnection.connect();
            BufferedReader rd = new BufferedReader(new InputStreamReader(urlConnection.getInputStream()));

            textData = new StringBuffer();
            String line;

            while ((line = rd.readLine()) != null) {
                textData.append(line);
            }
        } catch (IOException e) {
            e.printStackTrace();
        }
        return textData != null ? textData.toString() : "";
    }

    public Weather getWeather() {
        return w;
    }

    public JSONObject getNbpRateObj() {
        return nbpRateObj;
    }

    public double getRateCustom() {
        return rateCustom;
    }

    public String getRateCustomName() {
        return rateCustomName;
    }
}
