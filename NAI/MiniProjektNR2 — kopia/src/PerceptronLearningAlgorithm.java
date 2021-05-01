import java.io.BufferedReader;
import java.io.FileReader;
import java.io.IOException;
import java.util.*;

public class PerceptronLearningAlgorithm  {
    private List<MySet> Training = new ArrayList<>();
    private List<MySet> Test = new ArrayList<>();
    private List<Double> listOfWeights = new ArrayList<>();
    private List<Double> listOfWeight = new ArrayList<>();
    int setNumb = 10;
    int teta = 1;
    double alpha;
    int limit = 1;
    HashMap<String,Integer> answerMap;
    MyPerceptron myPerceptron;

    public PerceptronLearningAlgorithm(){ }

    public void treining(double alpha){
        answerMap = new HashMap<>();
        int n=0;
        for(MySet mySet:Training){
            if(!answerMap.containsKey((mySet.getName())))
                answerMap.put(mySet.getName(),n++);
            if(answerMap.size()==2)
                break;
        }
        myPerceptron= new MyPerceptron(Training.get(0).getValues().size(),alpha);//wielkosc wiektora i alpha
        for(MySet mySet:Training){
            myPerceptron.learn(mySet,answerMap.get(mySet.getName()));
        }

    }

    public void test(){
        int n=0;
        String[] names=new String[answerMap.size()];
        for(String key:answerMap.keySet()){
            names[n]=key;
            n++;
        }
        int correctAnswerFC=0;
        int correctAnswerSC=0;
        for(MySet mySet:Test){
            int y=myPerceptron.ecaluate(mySet);
            if(answerMap.get(names[0])==y && y==answerMap.get(mySet.getName())){
                correctAnswerFC++;
            }
            if(answerMap.get(names[1])==y && y==answerMap.get(mySet.getName())){
                correctAnswerSC++;
            }
        }
        System.out.println("accuracy: "+ (double) (correctAnswerFC+correctAnswerSC)/Test.size()*100+"%");
        System.out.println("W:"+myPerceptron.getVectorW()+" Theta: "+myPerceptron.getTheta());
    }
    public void newTest() {
        Scanner scan = new Scanner(System.in);
        List<Double> zmienne = new ArrayList<>();
        int s = Training.get(0).getValues().size();
        for (int i = 0; i < s; i++) {
            System.out.println("podaj wartość ");
            zmienne.add(scan.nextDouble());
        }
        MySet mySetNewTest = new MySet(zmienne, "urzytkownika");
        int y = myPerceptron.ecaluate(mySetNewTest);
        for (String key : answerMap.keySet()){
            if (answerMap.get(key)==y) {
                System.out.println("assignment to : "+ key);
            }
        }
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
}
