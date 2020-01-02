using UnityEngine;
using UnityEditor;
 
public class CommandBuild
{
    public static void BuildAndroid()
    {
        string[] escenas = {"Assets/Scene/Configuracion.unity", "Assets/Scene/Creditos.unity","Assets/Scene/Estadisticas.unity",
        "Assets/Scene/Home.unity", "Assets/Scene/Juego.unity","Assets/Scene/NuevoJugador.unity","Assets/Scene/Personalizacion.unity"};
        BuildPipeline.BuildPlayer(escenas, "WhastBall.apk", BuildTarget.Android, BuildOptions.None);
    }
}
 