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


public class Weather {
    private String city;
    private final String country;
    private String countryCode;
    private String countryCurrencyCode;
    private String temperatureDescription;
    private double temperature;
    private String atmosphericPressure;

    public Weather(String c) {
        this.country = c;
    }

    public String getCity() {
        return city;
    }

    private void setCity(String c) {
        this.city = c;
    }

    public String getCountry() {
        return country;
    }

    public String gettemperatureDescription() {
        return temperatureDescription;
    }

    private void settemperatureDescription(String d) {
        this.temperatureDescription = d;
    }


    public double getTemperature() {
        return temperature;
    }

    private void setTemperature(double t) {
        this.temperature = t - 273.2;//Kelvin
    }

    public String getAtmosphericPressure() {
        return atmosphericPressure;
    }

    private void setAtmosphericPressure(String a) {
        this.atmosphericPressure = a;
    }

    public String getCountryCode() {
        return countryCode;
    }

    private void setCountryCode(String cCode) {
        this.countryCode = cCode.toUpperCase();
    }

    private void setCountryCurrencyCode() {
        if (!countryCode.isEmpty() && countryCurrencyCode == null) {

            String json = Service.getContentFromUrl("http://country.io/currency.json");
            if (json != null) {
                JSONObject jsonObject = new JSONObject(json);
                countryCurrencyCode = jsonObject.getString(countryCode);
            }
        }
    }

    public String getCountryCurrencyCode() {
        return countryCurrencyCode;
    }
//http://jsonviewer.stack.hu/
    void initialize(String jsonDataString) {
        if (jsonDataString != null) {
            JSONObject jsonObject = new JSONObject(jsonDataString);
            setCity(jsonObject.getString("name"));
            setCountryCode(jsonObject.getJSONObject("sys").optString("country"));
            settemperatureDescription(jsonObject.getJSONArray("weather").getJSONObject(0).optString("main"));
            setAtmosphericPressure(jsonObject.getJSONObject("main").optString("pressure"));
            setTemperature(jsonObject.getJSONObject("main").optDouble("temp"));
            setCountryCurrencyCode();
        }
    }

}