public class Distance implements Comparable<Distance>{

    double distance;
    MySet msTr;
    MySet msTe;
    public Distance(double distance, MySet msTe, MySet msTr){
        this.distance = distance;//dystans
        this.msTe=msTe;//obiekt MySet, wartości i nazwa
        this.msTr=msTr;//obiekt MySet, wartości i nazwa
    }
    @Override
    public int compareTo(Distance o) {
        return Double.compare(this.distance, o.distance);
    }
    public MySet getMsTr(){
        return msTr;
    }

}
