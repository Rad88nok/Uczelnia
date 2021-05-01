import java.io.File;
import java.io.IOException;
import java.util.Scanner;

public class Main {

    public static void main(String[] args) {

        PerceptronLearningAlgorithm perceptron = new PerceptronLearningAlgorithm();
        String test = "."+ File.separator+"Data"+ File.separator+"perceptron.test.data";
        String training = "."+ File.separator+"Data"+ File.separator+"perceptron.data";
        Scanner scan = new Scanner(System.in);


        try {
            perceptron.readFile(test,"test");
        } catch (IOException e) {
            e.printStackTrace();
        }
        try {
            perceptron.readFile(training,"training");
        } catch (IOException e) {
            e.printStackTrace();
        }
        System.out.println("Podaj stałą uczenia(alpha)(przecinek)");
        double alpha=scan.nextDouble();
        perceptron.treining(alpha);
        perceptron.test();
        scan.nextLine();
        String answer;
        do{
            System.out.println("Czy chcesz podać nowy objekt do klasyfikacji?(T/N)");
            answer = scan.nextLine();
            if(answer.equals("T")) {
                perceptron.newTest();
            }
        }while(answer.equals("T"));

    }



}