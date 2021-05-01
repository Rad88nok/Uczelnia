/**
 *
 *  @author Kazimierczyk Konrad S18361
 *
 */
package zad1;

import java.awt.*;
import java.awt.event.*;
import java.io.IOException;
import javax.swing.*;
import javax.swing.border.EmptyBorder;
import javax.swing.plaf.basic.BasicTabbedPaneUI;

import javafx.scene.layout.StackPane;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;
import javafx.application.Platform;
import javafx.embed.swing.JFXPanel;
import javafx.scene.Scene;
import javafx.scene.layout.VBox;
import javafx.scene.web.WebEngine;
import javafx.scene.web.WebView;

public class GUI extends JFrame implements Runnable {

    private final Service service;

    public GUI(Service s) {
        super("gui");
        try {
            UIManager.setLookAndFeel(
                    UIManager.getSystemLookAndFeelClassName());
        } catch (ClassNotFoundException | InstantiationException | IllegalAccessException | UnsupportedLookAndFeelException e) {
            e.printStackTrace();
        }
        service = s;
        initializeForm();


    }

    private void initializeForm() {
        setPreferredSize(new Dimension(900, 600));
        setDefaultCloseOperation(WindowConstants.EXIT_ON_CLOSE);
        setResizable(false);

        JTabbedPane tabbedPane = new JTabbedPane();
        tabbedPane.setUI(new BasicTabbedPaneUI() {
            @Override
            protected int calculateTabWidth(int tabPlacement, int tabIndex, FontMetrics metrics) {
                return 200;
            }

            @Override
            protected int calculateTabHeight(int tabPlacement, int tabIndex, int fontHeight) {
                return 50;
            }
        });

        Weather weather = service.getWeather();

        JComponent tab1 = new JPanel();
        initializeFormForTab1(tab1, tabbedPane, weather);

        JComponent tab2 = new JPanel();
        initializeFormForTab2(tab2, tabbedPane, weather);

        JComponent tab3 = new JPanel();
        tabbedPane.addTab("Wiki " + weather.getCity(), null, tab3);

        final JFXPanel fxPanel = new JFXPanel();
        tab3.add(fxPanel, BorderLayout.CENTER);

        Platform.runLater(() -> {
            StackPane root = new StackPane();
            Scene scene = new Scene(root, tab3.getWidth(), tab3.getHeight() - 5);
            WebView webView = new WebView();
            WebEngine webEngine = webView.getEngine();
            webEngine.load("https://wikipedia.org/wiki/" + weather.getCity());
            root.getChildren().add(webView);
            fxPanel.setScene(scene);
        });


        add(tabbedPane);
        pack();
        setLocationRelativeTo(null);

    }

    private void initializeFormForTab1(JComponent tab1, JTabbedPane tabbedPane, Weather weather) {

        tabbedPane.addTab("Weather: " + weather.getCity(), null, tab1,
                "Weather " + weather.getCity() + "," + weather.getCountry());

        tab1.setLayout(new BorderLayout());

        JLabel cityL = new JLabel(weather.getCity() + " - " + weather.getCountry() + " (" + weather.getCountryCode() + ")");
        cityL.setHorizontalAlignment(SwingConstants.CENTER);
        cityL.setFont(new Font("TimesRoman", Font.BOLD | Font.ITALIC, 20));
        tab1.add(cityL, BorderLayout.NORTH);


        JComponent contentLeft = new JPanel();
        contentLeft.setLayout(new BoxLayout(contentLeft, BoxLayout.PAGE_AXIS));

        JLabel temperatureL = new JLabel("Temp");
        temperatureL.setHorizontalAlignment(SwingConstants.CENTER);
        temperatureL.setFont(new Font("TimesRoman", Font.BOLD | Font.ITALIC, 20));
        contentLeft.add(temperatureL);

        JLabel temperatureDigitL = new JLabel(String.format("%.1f", weather.getTemperature()) );
        temperatureDigitL.setHorizontalAlignment(SwingConstants.CENTER);
        temperatureDigitL.setFont(new Font("TimesRoman", Font.BOLD | Font.ITALIC, 20));
        contentLeft.add(temperatureDigitL);

        contentLeft.setBorder(new EmptyBorder(100, 50, 10, 0));
        tab1.add(contentLeft, BorderLayout.WEST);

        JComponent contentCenter = new JPanel();
        contentCenter.setLayout(new BoxLayout(contentCenter, BoxLayout.PAGE_AXIS));


        JLabel descL = new JLabel("weather:");
        descL.setHorizontalAlignment(SwingConstants.CENTER);

        descL.setFont(new Font("TimesRoman", Font.BOLD | Font.ITALIC, 20));
        contentCenter.add(descL);

        JLabel descTextL = new JLabel(weather.gettemperatureDescription());
        descTextL.setHorizontalAlignment(SwingConstants.CENTER);

        descTextL.setFont(new Font("TimesRoman", Font.BOLD | Font.ITALIC, 20));
        contentCenter.add(descTextL);

        contentCenter.setBorder(new EmptyBorder(100, 50, 10, 0));
        tab1.add(contentCenter, BorderLayout.CENTER);

        JComponent contentRight = new JPanel();
        contentRight.setLayout(new BoxLayout(contentRight, BoxLayout.PAGE_AXIS));

        JLabel pressureL = new JLabel("pressure:");
        pressureL.setHorizontalAlignment(SwingConstants.CENTER);

        pressureL.setFont(new Font("TimesRoman", Font.BOLD | Font.ITALIC, 20));
        contentRight.add(pressureL);

        JLabel pressureMb = new JLabel(weather.getAtmosphericPressure());
        pressureMb.setHorizontalAlignment(SwingConstants.CENTER);

        pressureMb.setFont(new Font("TimesRoman", Font.BOLD | Font.ITALIC, 20));
        contentRight.add(pressureMb);

        contentRight.setBorder(new EmptyBorder(100, 0, 10, 50));
        tab1.add(contentRight, BorderLayout.EAST);
    }

    private void initializeFormForTab2(JComponent tab2, JTabbedPane tabbedPane, Weather weather) {

        tabbedPane.addTab("rate" + weather.getCountryCurrencyCode(), null, tab2,
                "Rate" + weather.getCity() + "," + weather.getCountry());

        tab2.setLayout(new BorderLayout());
        JLabel cityL = new JLabel(service.getRateCustomName() + " - " + weather.getCountry() + " (" + weather.getCountryCode() + ")");
        cityL.setHorizontalAlignment(SwingConstants.CENTER);
        cityL.setFont(new Font("TimesRoman", Font.BOLD | Font.ITALIC, 20));
        tab2.add(cityL, BorderLayout.NORTH);

        JComponent contentLeft = new JPanel();
        contentLeft.setLayout(new BoxLayout(contentLeft, BoxLayout.PAGE_AXIS));

        JLabel exchangeRate = new JLabel("rate: ");
        exchangeRate.setHorizontalAlignment(SwingConstants.CENTER);
        exchangeRate.setFont(new Font("TimesRoman", Font.BOLD | Font.ITALIC, 20));
        contentLeft.add(exchangeRate);

        JLabel rateCurr = new JLabel();
        rateCurr.setText("1 " + weather.getCountryCurrencyCode() + " = " + service.getRateCustom() + " " + service.getRateCustomName());
        rateCurr.setHorizontalAlignment(SwingConstants.CENTER);
        rateCurr.setFont(new Font("TimesRoman", Font.BOLD | Font.ITALIC, 20));
        contentLeft.add(rateCurr);
        contentLeft.setBorder(new EmptyBorder(100, 20, 0, 0));

        JComponent contentRight = new JPanel();
        contentRight.setLayout(new BoxLayout(contentRight, BoxLayout.PAGE_AXIS));

        JLabel nbpRate = new JLabel("nbp rate: ");
        nbpRate.setHorizontalAlignment(SwingConstants.CENTER);
        nbpRate.setFont(new Font("TimesRoman", Font.BOLD | Font.ITALIC, 20));
        contentRight.add(nbpRate);

        JLabel nbpRateValue = new JLabel(service.getNbpRateObj().optString("kurs_sredni") + " PLN" + " = " + service.getNbpRateObj().optString("przelicznik") + " " + service.getWeather().getCountryCurrencyCode());
        nbpRateValue.setHorizontalAlignment(SwingConstants.CENTER);
        nbpRateValue.setFont(new Font("TimesRoman", Font.BOLD | Font.ITALIC, 20));
        contentRight.add(nbpRateValue);
        contentRight.setBorder(new EmptyBorder(100, 0, 0, 20));

        tab2.add(contentLeft, BorderLayout.WEST);
        tab2.add(contentRight, BorderLayout.EAST);

    }


    @Override
    public void run() {

        setVisible(true);
    }

}