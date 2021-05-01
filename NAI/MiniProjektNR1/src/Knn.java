import java.io.BufferedReader;
import java.io.FileReader;
import java.io.IOException;
import java.util.*;

public class Knn {
    private List<MySet> Training = new ArrayList<>();
    private List<MySet> Test = new ArrayList<>();
    HashMap<String,Integer> answerMap;
    HashMap<Integer,String> tmpMap;

    public Knn() {}

    public void comparingTest(int k) {
        double accuracy = 0;
        answerMap = new HashMap<>();
        tmpMap=new HashMap<>();
        List<String> names=new ArrayList<>();
        for (MySet msTe : Test) {
            List<Distance> distanceList = new ArrayList<>();
            String answer="";
            int t = 0;
            for (MySet msTr : Training) {
                distanceList.add(new Distance(getDistance(msTe, msTr), msTe, msTr));
                if (!answerMap.containsKey((msTr.getName()))) {
                    answerMap.put(msTr.getName(), 0);
                    tmpMap.put(t++,msTr.getName());
                }
                //distanceList- lista obiektów klasy Distance, przyjmuje dwa obiekty klasy MySet i dystans mniędzy nimi
                //msTe-Lista obiektów MySet z listy testowej
                //msTr-Lista obiektów MySet z listy treningowej
                //getDistance-metoda z klasy Knn, pobiera dwa obiekty MySet, jeden testowy drugi treningowy
            }
            Collections.sort(distanceList);//Sortowanie listy dystansow

            for(int i=0;i<k;i++) {
                //System.out.println(distanceList.get(i).getMsTr().getName());
                names.add(distanceList.get(i).getMsTr().getName());
            }
            Collections.sort(names);
            int[] tt=new int[tmpMap.size()];//counter
            for(int i=0;i<tt.length;i++){
                tt[i]=0;
            }
            String [] nn=new String[tmpMap.size()];//individualName
            for(int i=0;i<tmpMap.size();i++) {
                nn[i] = String.valueOf(tmpMap.get(i));
            }
            String individualNameFC="";
            String individualNameSC="";
            String individualNameTC="";
            String theMostCommonName="";
            int counterFC=0;
            int counterSC=0;
            int counterTC=0;
            int tmp=0;
            int s=0;
            for(String n:names){
                //System.out.println(n);
//                if(!n.equals(individualNameFC)&&!n.equals(individualNameSC)){
//                    if(counterTC>=tmp) {
//                        tmp = counterTC;
//                        theMostCommonName=individualNameTC;
//                    }
//                    individualNameTC=n;
//                    counterTC+=1;
//                }
//                if(!n.equals(individualNameSC)&&!n.equals(individualNameTC)) {
//                    if(counterFC>=tmp) {
//                        tmp = counterFC;
//                        theMostCommonName=individualNameFC;
//                    }
//                    individualNameFC=n;
//                    counterFC+=1;
//                }
//                if(!n.equals(individualNameTC)&&!n.equals(individualNameFC)) {
//                    if(counterSC>=tmp) {
//                        tmp = counterSC;
//                        theMostCommonName=individualNameSC;
//                    }
//                    individualNameSC=n;
//                    counterSC+=1;
//                }
                for(int i=0;i<nn.length;i++) {
                    if (n.equals(nn[i])){
                        if(tt[i]>=tmp){
                            tmp = tt[i];
                            theMostCommonName=nn[i];
                        }
                        tt[i]=tt[i]+1;
                    }
                }
                tmp=0;
                //System.out.println(theMostCommonName);
            }
            double similarity = 0;
            for (int l = 0; l < k/2; l++) {
                if (distanceList.get(l).getMsTr().getName().equals(theMostCommonName)) {
                    similarity++;
                }
            }

            if (similarity >= k/2) {
                accuracy++;
            }
        }
            System.out.println("the accuracy for " + k + " is: " + ((accuracy / Test.size()) * 100) + "%");

    }

    public void newTest() {
        Scanner scan = new Scanner(System.in);
        List<Distance> distanceList=new ArrayList<>();
        List<String> names=new ArrayList<>();
        int s= Training.get(0).getValue().size();
        List<Double> zmienne=new ArrayList<>();
        for(int i=0;i<s;i++){
            System.out.println("podaj wartość ");
            zmienne.add(scan.nextDouble());
        }
        MySet mySetNewTest=new MySet(zmienne,"urzytkownika");
        System.out.println("podaj parametr k");
        int k = scan.nextInt();
        for(MySet msTr: Training){
            distanceList.add(new Distance(getDistance(mySetNewTest,msTr),mySetNewTest,msTr));
        }
        Collections.sort(distanceList);
        System.out.println("neighbors: ");
        for(int i=0;i<k;i++) {
            //System.out.println(distanceList.get(i).getMsTr().getName());
            names.add(distanceList.get(i).getMsTr().getName());
        }
        Collections.sort(names);
        String individualNameFC="";
        String individualNameSC="";
        String individualNameTC="";
        String theMostCommonName="";
        int counterFC=0;
        int counterSC=0;
        int counterTC=0;
        int tmp=0;
        //int s=0;
        for(String n:names){
            //System.out.println(n);
            if(!n.equals(individualNameFC)&&!n.equals(individualNameSC)){
                if(counterTC>=tmp) {
                    tmp = counterTC;
                    theMostCommonName=individualNameTC;
                }
                individualNameTC=n;
                counterTC+=1;
            }
            if(!n.equals(individualNameSC)&&!n.equals(individualNameTC)) {
                if(counterFC>=tmp) {
                    tmp = counterFC;
                    theMostCommonName=individualNameFC;
                }
                individualNameFC=n;
                counterFC+=1;
            }
            if(!n.equals(individualNameTC)&&!n.equals(individualNameFC)) {
                if(counterSC>=tmp) {
                    tmp = counterSC;
                    theMostCommonName=individualNameSC;
                }
                individualNameSC=n;
                counterSC+=1;
            }}
        System.out.println("assignment to : "+theMostCommonName);

    }
public void readFile(String path,String mode) throws IOException {
    FileReader fileReader = new FileReader(path);
    BufferedReader bufferedReader = new BufferedReader(fileReader);
    String textLine = bufferedReader.readLine();
    if(mode.equals("training")) {
        do {
            String[] tmp=textLine.split(",");
            List<Double> value=new ArrayList<>();
            for (int i = 0; i < tmp.length-1 ; i++)
                value.add(Double.parseDouble(tmp[i]));
            Training.add(new MySet(value,tmp[tmp.length-1]));
            textLine = bufferedReader.readLine();
        } while (textLine != null);
        bufferedReader.close();
    }else if(mode.equals("test")){
        do {
            String[] tmp=textLine.split(",");
            List<Double> value=new ArrayList<>();
            for (int i = 0; i < tmp.length-1 ; i++)
                value.add(Double.parseDouble(tmp[i]));
            Test.add(new MySet(value,tmp[tmp.length-1]));
            textLine = bufferedReader.readLine();
        } while (textLine != null);
        bufferedReader.close();
    }
}
public double getDistance(MySet m1, MySet m2){//wymagał implementacji Comparable w klasie Distance
    double distance=0;
    for (int i = 0; i <m1.getValue().size() ; i++) {//dystans=sqrt((a1-a2)^2+(b1-b2)^2... itd.
        distance += Math.pow((m1.getValue().get(i))-(m2.getValue().get(i)),2);
    }
    return Math.sqrt(distance);
}
}