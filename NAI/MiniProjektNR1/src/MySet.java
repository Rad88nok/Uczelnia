import java.util.List;

public class MySet {
    List<Double> value;
    String name;
    MySet(List <Double> value, String name){
        this.value=value;//Lista wartości
        this.name=name;//Atrybut decyzyjny
    }
    public List<Double> getValue(){
        return value;
    }
    public String toString(){
        return this.name;
    }
    public String getName(){
        return this.name;
    }


}
