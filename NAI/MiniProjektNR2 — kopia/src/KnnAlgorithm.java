import java.util.ArrayList;
import java.util.Collections;
import java.util.HashMap;
import java.util.List;

public class KnnAlgorithm {
    private List<MySet> Training = new ArrayList<>();
    private List<MySet> Test = new ArrayList<>();
    HashMap<String, Integer> answerMap;
    HashMap<Integer, String> tmpMap;

    public KnnAlgorithm(List<MySet> Training, List<MySet> Test) {
        this.Test = Test;
        this.Training = Training;
    }

    public void comparingTest(int k) {
        List<String> names=new ArrayList<>();
        for (MySet msTe : Test) {
            List<Distance> distanceList = new ArrayList<>();
            String answer = "";
            int t = 0;
            for (MySet msTr : Training) {
                distanceList.add(new Distance(getDistance(msTe, msTr), msTe, msTr));
                if (!answerMap.containsKey((msTr.getName()))) {
                    answerMap.put(msTr.getName(), t);
                    tmpMap.put(t++, msTr.getName());
                }
            }
            for(int i=0;i<k;i++) {
                names.add(distanceList.get(i).getMsTr().getName());
            }
            Collections.sort(distanceList);
            Collections.sort(names);
        }
    }
    public double getDistance (MySet m1, MySet m2){
        double distance = 0;
        for (int i = 0; i < m1.getValues().size(); i++) {//dystans=sqrt((a1-a2)^2+(b1-b2)^2... itd.
            distance += Math.pow((m1.getValues().get(i)) - (m2.getValues().get(i)), 2);
        }
        return Math.sqrt(distance);
    }

}
