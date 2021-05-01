package zad1;

import java.io.IOException;
import java.nio.channels.FileChannel;
import java.nio.charset.Charset;
import java.nio.file.*;
import java.nio.file.attribute.BasicFileAttributes;

public class Futil {


    public static void processDir(String start, String res) {
        Charset csIn=Charset.forName("cp1250");
        Charset csOut=Charset.forName("UTF8");
        try {
            Path startPath=Paths.get(start);
            Path resPath = Paths.get(res);

            Files.deleteIfExists(resPath);
            FileChannel fcOut =  FileChannel.open(resPath,StandardOpenOption.CREATE_NEW,StandardOpenOption.APPEND);
            Files.walkFileTree(startPath, new SimpleFileVisitor<Path>(){
                @Override
                public FileVisitResult visitFile(Path file, BasicFileAttributes attrs)
                        throws IOException {

                    FileChannel fcIn = (FileChannel) Files.newByteChannel(file);
                    fcOut.write(
                            csOut.encode(
                                    csIn.decode(
                                        fcIn.map(FileChannel.MapMode.READ_ONLY,0,fcIn.size())
                                    )
                            )
                    );

                    return FileVisitResult.CONTINUE;
                }
            });
            fcOut.close();
        } catch (IOException e) {
            System.out.println("Nieznaleziono katalogu");//C:\Users\Shini\TPO1dir
        }
    }
}
