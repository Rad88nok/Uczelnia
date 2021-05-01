import java.io.BufferedReader;
import java.io.FileReader;
import java.io.IOException;
import java.util.ArrayList;
import java.util.Collections;
import java.util.List;
import java.util.Scanner;

public class Knn {
    private List<String> Training = new ArrayList<>();
    private List<String> Test = new ArrayList<>();
    double testSize;
    double accuracy;
    public Knn() {}

    public void comparingTest(int k) {
        String wynik = null;

        for (String s : Test) {
            List<String> neighbor = new ArrayList<>();
            String[] line = s.split(",");
            int similarity = 0;

            double firstValue = Double.parseDouble(line[0]);
            double secondValue = Double.parseDouble(line[1]);
            double thirdValue = Double.parseDouble(line[2]);
            double fourthValue = Double.parseDouble(line[3]);
            String fifthValue = line[4];

            com(neighbor,firstValue,secondValue,thirdValue,fourthValue);

            for (int i = 0; i < k; i++) {
                if (neighbor.get(i).contains(fifthValue)) {
                    similarity++;
                }
            }
            if (similarity >= k) {
                accuracy++;
            }

            wynik = "the accuracy is: " + ((accuracy * 100) / testSize) + "%";


        }
        System.out.println("correctly classified from the test file based on the training file: " + (int) accuracy);
        System.out.println(wynik);

    }

    public void newTest() {
        Scanner scan = new Scanner(System.in);

        System.out.println("enter the length of the leaf ");
        double firstValue = scan.nextDouble();
        System.out.println("enter the width of the leaf ");
        double secondValue = scan.nextDouble();
        System.out.println("enter the length of the petal ");
        double thirdValue = scan.nextDouble();
        System.out.println("enter the length of the petal ");
        double fourthValue = scan.nextDouble();
        System.out.println("enter the parameter k ");
        int k = scan.nextInt();
        int irisS = 0;
        int irisVe = 0;
        int irisVi = 0;
        List<String> neighbor = new ArrayList<>();

        com(neighbor,firstValue,secondValue,thirdValue,fourthValue);

        System.out.println("neighbors: ");
        for(int i=0;i<k;i++) {
            System.out.println(neighbor.get(i));
        }
        for (int i = 0; i < k; i++) {
            if (neighbor.get(i).contains("Iris-setosa")) {
                irisS++;
            } else if (neighbor.get(i).contains("Iris-versicolor")) {
                irisVe++;
            } else if (neighbor.get(i).contains("Iris-virginica")) {
                irisVi++;
            }
        }
        if (irisS > irisVe && irisS > irisVi) {
            System.out.println("assignment to : iris-setosa");
        } else if (irisVe > irisS && irisVe > irisVi) {
            System.out.println("assignment to : iris-versicolor");
        } else if(irisVi > irisVe && irisVi > irisS){
            System.out.println("assignment to : iris-virginica");
        }
    }
    //Reading data files
    public void readFile(String path,String mode) throws IOException {
        FileReader fileReader = new FileReader(path);
        BufferedReader bufferedReader = new BufferedReader(fileReader);
        String textLine = bufferedReader.readLine();
        if(mode.equals("training")) {
            do {
                Training.add(textLine);
                textLine = bufferedReader.readLine();
            } while (textLine != null);
            bufferedReader.close();
        }else if(mode.equals("test")){
            do {
                //System.out.println(textLine);
                Test.add(textLine);
                textLine = bufferedReader.readLine();
                //amount of data in the test file
                testSize++;
            } while (textLine != null);
            bufferedReader.close();
        }
    }
    //Calculation of the Euclidean distance from points
    public void com(List<String> neighbor,double v1,double v2, double v3, double v4){
        for (String a :Training) {
            String[] line = a.split(",");

            double first = Math.pow((v1 - Double.parseDouble(line[0])), 2);
            double second = Math.pow((v2 - Double.parseDouble(line[1])), 2);
            double third = Math.pow((v3 - Double.parseDouble(line[2])), 2);
            double fourth = Math.pow((v4 - Double.parseDouble(line[3])), 2);

            double distance = Math.sqrt(first + second + third + fourth);

            neighbor.add(distance + " " + line[4]);
            //sorts, the numbers are in the first position so it arranges them in ascending order
            Collections.sort(neighbor);
        }
    }
}