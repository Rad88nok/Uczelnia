/**
 *
 *  @author Kazimierczyk Konrad S18361
 *
 */

package zad1;

import java.io.IOException;
import java.net.InetSocketAddress;
import java.net.ServerSocket;
import java.net.Socket;
import java.nio.ByteBuffer;
import java.nio.CharBuffer;
import java.nio.channels.SelectionKey;
import java.nio.channels.Selector;
import java.nio.channels.ServerSocketChannel;
import java.nio.channels.SocketChannel;
import java.nio.charset.Charset;
import java.nio.charset.StandardCharsets;
import java.text.SimpleDateFormat;
import java.time.LocalTime;
import java.time.format.DateTimeFormatter;
import java.util.*;
import java.util.concurrent.locks.Lock;
import java.util.concurrent.locks.ReentrantLock;

import static java.nio.channels.SelectionKey.OP_WRITE;


public class Server {
    private String host;
    private int port;
    private volatile boolean isRunning;
    private Selector selector;
    InetSocketAddress inetSocketAddress;
    private SelectionKey selectionKey;
    private Map<SocketChannel, Connection> map;
    private StringBuilder log;
    Thread thread;
    StringBuilder serverLog;
    private final Lock lock = new ReentrantLock();
    private ServerSocketChannel serverSocketChannel;
    private ServerSocketChannel SocketChannel;
    private ByteBuffer byteBuffer = ByteBuffer.allocateDirect(1024);
    private static Charset charset = StandardCharsets.UTF_8;
    private StringBuilder req = new StringBuilder();
    Map<SocketChannel, String> clients;

    public Server(String host, int port) throws IOException {
        this.host = host;
        this.port = port;
        this.serverSocketChannel = ServerSocketChannel.open();
        this.serverSocketChannel.socket().bind(new InetSocketAddress(this.host, this.port));
        this.serverSocketChannel.configureBlocking(false);
        this.selector = Selector.open();
        this.selectionKey = serverSocketChannel.register(selector, SelectionKey.OP_ACCEPT);
        this.log = new StringBuilder();
        this.map = new HashMap<>();
    }

    public void startServer(){
        new Thread(()->{
            isRunning = true;
            while(isRunning){
                try {
                    selector.select();

                    Set<SelectionKey> keySet = selector.selectedKeys();
                    Iterator<SelectionKey> i = keySet.iterator();

                    while(i.hasNext()){
                        SelectionKey k = i.next();
                        i.remove();

                        if(k.isAcceptable()){
                            SocketChannel client = serverSocketChannel.accept();
                            client.configureBlocking(false);
                            client.register(selector, SelectionKey.OP_READ | SelectionKey.OP_WRITE);
                            continue;
                        }
                        if(k.isReadable()){
                            SocketChannel c = (SocketChannel)k.channel();
                            if(!c.isOpen()) return;
                            req.setLength(0);
                            byteBuffer.clear();

                            for(int bytesRead = c.read(byteBuffer); bytesRead > 0; bytesRead = c.read(byteBuffer)){
                                byteBuffer.flip();
                                CharBuffer charBuffer = charset.decode(byteBuffer);
                                req.append(charBuffer);
                            }

                            StringBuilder response = new StringBuilder();
                            String[] d = new String[4];
                            d[3] = "";
                            String status;
                            if(req.toString().contains("login")){
                                status = "logged in";
                                response.append(status);
                                map.put(c, new Connection(req.toString().split(" ")[1]));
                                putLogClient(c, status);
                                d[1] = status;
                            } else if(req.toString().equals("bye and log transfer")){
                                status = "logged out";
                                putLogClient(c,status);
                                map.get(c).close();
                                response.append(map.get(c));
                                d[1] = status;
                            } else {
                                putLogClient(c,"Request: " + req);
                                String[] split = req.toString().split(" ");
                                String result = Time.passed(split[0],split[1]);
                                putLogClient(c,"Result:");
                                putLogClient(c,result);
                                response.append(result);
                                d[1] = "request";
                                d[3] = ": \"" + req + "\"";
                            }
                            d[0] = map.get(c).id;
                            d[2] = "at " + LocalTime.now();
                            log(String.format("%s %s %s%s", d[0], d[1], d[2], d[3]));
                            ByteBuffer out = ByteBuffer.allocateDirect(response.toString().getBytes().length);
                            out.put(charset.encode(response.toString()));
                            out.flip();
                            c.write(out);
                        }
                    }

                } catch (IOException ex){
                    ex.printStackTrace();
                }
            }
        }).start();
    }

    private void putLogClient(SocketChannel client, String log){
        if(!map.containsKey(client)){
            map.put(client, new Connection(log));
        } else {
            map.get(client).log.append(log).append("\n");
        }
    }

    private static class Connection {
        StringBuilder log;
        String id;

        Connection(String id){
            this.id = id;
            log = new StringBuilder("\n=== " + id + " log start ===\n");
        }

        public void close(){
            log.append("=== ").append(id).append(" log end ===\n");
        }

        @Override
        public String toString() {
            return log.toString();
        }
    }

    public void stopServer(){
        isRunning = false;
    }

    private void log(String log){
        this.log.append(log).append("\n");
    }

    public String getServerLog() {
        return log.toString();
    }
}