import java.util.*;

public class KnnAlgorithm {
    private List<MySet> Training = new ArrayList<>();
    private List<MySet> Test = new ArrayList<>();
    HashMap<String, Integer> answerMap;
    HashMap<Integer, String> tmpMap;

    public KnnAlgorithm(List<MySet> Training, List<MySet> Test) {
        this.Test = Test;
        this.Training = Training;
    }

    public void comparingTest() {
        int k=7;
        //HashMap<String, Integer> answerMap = new HashMap<>();
        for (MySet msTe : Test) {
            answerMap = new HashMap<>();
            List<Distance> distanceList = new ArrayList<>();
            String answer = "";
            int t = 0;
            for (MySet msTr : Training) {
                distanceList.add(new Distance(getDistance(msTe, msTr), msTe, msTr));
                if (!answerMap.containsKey((msTr.getName()))) {
                    answerMap.put(msTr.getName(), t);
                }
            }
            Collections.sort(distanceList);
            int y=0;
            for(int i=0;i<k;i++){
                y=answerMap.get(distanceList.get(i).getMsTr().getName());
                answerMap.replace(distanceList.get(i).getMsTr().getName(),++y);
            }
            String mostCommonName="";
            int maxValueInMap=(Collections.max(answerMap.values()));
            for(Map.Entry<String, Integer> answerM: answerMap.entrySet()){
                if (answerM.getValue()==maxValueInMap) {
                    mostCommonName=answerM.getKey();
                }
            }
            msTe.setName(mostCommonName);
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
