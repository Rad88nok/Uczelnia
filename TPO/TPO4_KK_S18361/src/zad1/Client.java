/**
 *
 *  @author Kazimierczyk Konrad S18361
 *
 */

package zad1;


import java.io.*;
import java.net.InetSocketAddress;
import java.net.Socket;
import java.net.UnknownHostException;
import java.nio.ByteBuffer;
import java.nio.CharBuffer;
import java.nio.channels.SocketChannel;
import java.nio.charset.Charset;
import java.nio.charset.StandardCharsets;
import java.util.ArrayList;
import java.util.List;

public class Client implements Cloneable {
    private String host;
    private String id;
    private int port;
    private SocketChannel sChanel;
    private StringBuilder logBuilder;
    private static Charset charset = StandardCharsets.UTF_8;
    private SocketChannel channel;

    public Client(String host, int port, String id){
        this.host = host;
        this.port = port;
        this.id = id;
        this.logBuilder = new StringBuilder();
    }

    public void connect(){
        try {
            sChanel = SocketChannel.open();
            sChanel.configureBlocking(false);
            sChanel.connect(new InetSocketAddress(host, port));
            while((!sChanel.finishConnect()));
        } catch (IOException e){
            e.printStackTrace();
        }
    }

    public String send(String req){
        ByteBuffer outBBuffer = ByteBuffer.allocateDirect(req.getBytes().length);
        ByteBuffer byteBuffer = ByteBuffer.allocateDirect(1024);
        StringBuilder response = new StringBuilder();
        try {

            outBBuffer.put(charset.encode(req));
            outBBuffer.flip();
            sChanel.write(outBBuffer);

        } catch (IOException e) {
            e.printStackTrace();
        }
        byteBuffer.clear();

        try {
            int readBytes;

            while((readBytes = sChanel.read(byteBuffer)) < 1);

            for( ; readBytes > 0 ; readBytes = sChanel.read(byteBuffer) ){
                byteBuffer.flip();
                CharBuffer cbuf = charset.decode(byteBuffer);
                response.append(cbuf);
            }

        }catch (IOException e){
            e.printStackTrace();
        }

        return response.toString();
    }

    public String getId() {
        return id;
    }

    public StringBuilder getLog() {
        return logBuilder;
    }

    @Override
    protected Object clone() throws CloneNotSupportedException {
        return super.clone();
    }
}
