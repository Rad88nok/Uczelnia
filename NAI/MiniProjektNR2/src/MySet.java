import java.util.List;

public class MySet {
    private final List<Double> values;
    private final String name;

    MySet(List<Double> values, String name){
        this.values=values;
        this.name=name;

    }
    public List<Double> getValues(){
        return  values;
    }
    public String getName(){
        return name;
    }
}
