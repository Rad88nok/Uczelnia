/**
 *
 *  @author Kazimierczyk Konrad S18361
 *
 */

package zad1;


import org.yaml.snakeyaml.Yaml;

import java.io.FileInputStream;
import java.io.InputStream;
import java.util.List;
import java.util.Map;

public class Tools {
    public static Options createOptionsFromYaml(String fileName) throws Exception{
        Yaml yaml = new Yaml();
        InputStream inputStream = null;
        inputStream = new FileInputStream(fileName);

        Map<String, Object> map = yaml.load(inputStream);
        Map<String, List<String>> clientsMap = (Map<String, List<String>>) map.get("clientsMap");//Map<String, List<String>> clientsMap

        return new Options(
                map.get("host").toString(),
                Integer.parseInt(map.get("port").toString()),
                Boolean.parseBoolean(map.get("concurMode").toString()),
                Boolean.parseBoolean(map.get("showSendRes").toString()),
                clientsMap
        );
    }
}
