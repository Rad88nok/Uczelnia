import java.util.ArrayList;
import java.util.List;

public class MyPerceptron {
    private List<Double> vectorW;
    private double theta;
    private double alpha;

    public MyPerceptron(int vectorSize,double alpha){
        this.alpha=alpha;
        this.vectorW=new ArrayList<>();
        for(int i=0;i<vectorSize;i++){
            this.vectorW.add(Math.random()*1);
        }
        this.theta=Math.random()*1;
    }

    public List<Double> getVectorW(){
        return vectorW;
    }

    public void setVectorW(List<Double> vectorW) {
        this.vectorW = vectorW;
    }

    public double getTheta(){
        return theta;
    }

    public void setTheta(double theta){
        this.theta=theta;
    }

    public void setAlpha(double alpha){
        this.alpha=alpha;
    }
    public double getAlpha(){
        return this.alpha;
    }

    public void learn(MySet mySet, int answer){
        double net =0;

        for (int i = 0; i < mySet.getValues().size() ; i++) {
            net += mySet.getValues().get(i) * this.vectorW.get(i);
        }

        int y=(net>=this.theta?1:0);

        if(y!=answer){
            List<Double> wTMP=new ArrayList<>(this.vectorW);
            for(int i=0;i<mySet.getValues().size();i++){
                wTMP.set(i,(this.vectorW.get(i)+((answer-y)*alpha*mySet.getValues().get(i))));
                //weights[i] = weights[i] + learningConstant * (d - y) * input[i];
                // w = w +(d − y)αx
            }
            this.vectorW=wTMP;
            this.theta=theta+(answer-y)*alpha;
            //θ = θ −(d − y) α
        }
    }
    public int ecaluate(MySet mySet){
        double net=0;
        for(int i=0;i<mySet.getValues().size();i++){
            net=net+mySet.getValues().get(i)*this.vectorW.get(i);
            //X * W
        }
        return (net>=this.theta?1:0);
    }
}
