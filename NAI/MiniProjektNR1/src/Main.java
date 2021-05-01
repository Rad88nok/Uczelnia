/**
 *  @author Kazimierczyk Konrad S18361
 */
import java.io.IOException;
import java.util.Scanner;

public class Main {

    public static void main(String[] args) {
        Knn knnTest = new Knn();
        Scanner scan = new Scanner(System.in);

        String test = ".\\data\\iris.test.data";
        String training = ".\\data\\iris.data";
        try {
            knnTest.readFile(test,"test");
        } catch (IOException e) {
            e.printStackTrace();
        }
        try {
            knnTest.readFile(training,"training");
        } catch (IOException e) {
            e.printStackTrace();
        }
        System.out.println("enter the parameter k");
        int k = scan.nextInt();
        knnTest.comparingTest(k);
        scan.nextLine();
        String answer;
            do{
                System.out.println("Do you want to enter another object to check?(yes)");
                answer = scan.nextLine();
               if(answer.equals("yes")) {
                   knnTest.newTest();
               }
            }while(answer.equals("yes"));

    }
}
