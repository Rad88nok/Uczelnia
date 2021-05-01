import java.util.List;

public class MySet {
    private List<Double> values;
    private String name;

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
    public void setName(String name){
        this.name=name;
    }
}
