/**
 *
 *  @author Kazimierczyk Konrad S18361
 *
 */

package zad1;


import java.time.*;
import java.time.format.DateTimeFormatter;
import java.time.format.DateTimeParseException;
import java.time.temporal.ChronoUnit;
import java.util.Locale;
import java.util.Map;
import java.util.stream.Collectors;
import java.util.stream.Stream;

public class Time {
    public static String passed(String from, String to) {
        Locale.setDefault(Locale.ENGLISH);

        try {
            if (from.contains("T") && to.contains("T")) {//dateTime
                return info(LocalDateTime.parse(from), LocalDateTime.parse(to));
            }
            return infoWithoutT(LocalDate.parse(from), LocalDate.parse(to));

        } catch (DateTimeParseException e) {
            return "*** java.time.format.DateTimeParseException: " + e.getMessage();
        }
    }

    private static String info(LocalDateTime from, LocalDateTime to) {
        ZonedDateTime zFrom = ZonedDateTime.of(from, ZoneId.of("Europe/Warsaw"));
        ZonedDateTime zTo = ZonedDateTime.of(to, ZoneId.of("Europe/Warsaw"));

        long days = ChronoUnit.DAYS.between(zFrom.toLocalDate(), zTo.toLocalDate());
        double weeks = days / 7.0;
        long hour = Duration.between(zFrom, zTo).toHours();
        long min = Duration.between(zFrom, zTo).toMinutes();

        return "Od " + zFrom.getDayOfMonth() + " " + month.get(zFrom.getMonth()) + " " + zFrom.getYear() + " " + week.get(zFrom.getDayOfWeek()) +
                " godz. " + zFrom.format(DateTimeFormatter.ofPattern("hh:mm")) +
                " do " + zTo.getDayOfMonth() + " " + month.get(zTo.getMonth()) + " " + zTo.getYear() + " " + week.get(zTo.getDayOfWeek()) +
                " godz. " + zTo.format(DateTimeFormatter.ofPattern("hh:mm")) + "\n- mija: " + days +
                (days == 1 ? " dzień, " : " dni, ") + "tygodni " +(days % 7.0 !=0 && days!=1 ?String.format("%.2f", weeks) :
                (days == 1 ? String.format("%.2f", weeks): String.valueOf(weeks).split("\\.")[0])) +
                "\n- godzin: " + hour + ", minut: " + min + getCalendar(zFrom.toLocalDate(), zTo.toLocalDate());
    }

    private static String infoWithoutT(LocalDate from, LocalDate to) {
        long days = ChronoUnit.DAYS.between(from, to);
        double weeks = days / 7.0;

        return "Od " + from.getDayOfMonth() + " " + month.get(from.getMonth()) + " " + from.getYear() + " " + week.get(from.getDayOfWeek()) +
                " do " + to.getDayOfMonth() + " " + month.get(to.getMonth()) + " " + to.getYear() + " " + week.get(to.getDayOfWeek()) + "\n- mija: " + days +
                (days == 1 ? " dzień, " : " dni, ") + "tygodni " + (days % 7.0 == 0 ? String.valueOf(weeks).split("\\.")[0] : String.format("%.2f", weeks)) +
                getCalendar(from, to);
    }

    private static String getCalendar(LocalDate from, LocalDate to) {

        long days = ChronoUnit.DAYS.between(from, to);
        String calendar = "";

        if (days != 0) {
            int r = Period.between(from, to).getYears();
            int m = Period.between(from, to).getMonths();
            int d = Period.between(from, to).getDays();

            calendar = "\n- kalendarzowo: ";
            if (r != 0)
                calendar+=(r == 1 ? r + " rok, ": (r < 5 ? r + " lata, ": r + " lat, "));
            if (m != 0)
                calendar+=(m == 1 ? m + " miesiąc, ": (m < 5 ? m + " miesiące, ": m + " miesięcy, "));
            if (d != 0)
                calendar+=(d == 1 ? d + " dzień, ": d + " dni, ");
            calendar = calendar.substring(0, calendar.length() - 2);
        }
        return calendar;
    }
    private static Map<DayOfWeek, String> week = Stream.of(new Object[][]{
            {DayOfWeek.MONDAY, "(Poniedziałek)"},
            {DayOfWeek.TUESDAY, "(Wtorek)"},
            {DayOfWeek.WEDNESDAY, "(Sroda)"},
            {DayOfWeek.THURSDAY, "(Czwartek)"},
            {DayOfWeek.FRIDAY, "(Piątek)"},
            {DayOfWeek.SATURDAY, "(Sobota)"},
            {DayOfWeek.SUNDAY, "(Niedziela)"},
    }).collect(Collectors.toMap(data -> (DayOfWeek) data[0], data -> (String) data[1]));

    private static Map<Month, String> month = Stream.of(new Object[][]{
            {Month.JANUARY, "Stycznia"},
            {Month.FEBRUARY, "Lutego"},
            {Month.MARCH, "Marca"},
            {Month.APRIL, "Kwietnia"},
            {Month.MAY, "Maja"},
            {Month.JUNE, "Czerwca"},
            {Month.JULY, "Lipca"},
            {Month.AUGUST, "Sierpnia"},
            {Month.SEPTEMBER, "Srześnia"},
            {Month.OCTOBER, "Października"},
            {Month.NOVEMBER, "Listopada"},
            {Month.DECEMBER, "Grudnia"}
    }).collect(Collectors.toMap(data -> (Month) data[0], data -> (String) data[1]));
}