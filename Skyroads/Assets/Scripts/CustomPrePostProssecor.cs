using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class CustomPrePostProssecor : AssetPostprocessor
{
    private const int min = 200;
    private const int max = 3000;
    void OnPreprocessAudio()
    {
        if (assetPath.Contains("bg"))
        {   
            var settings = new AudioImporterSampleSettings();
            var audioImporter = (AudioImporter)assetImporter;

            audioImporter.preloadAudioData = true;
            audioImporter.loadInBackground = true;
            var fileInfo = new System.IO.FileInfo(assetPath);
            var size = fileInfo.Length / 1024;
            if (size < min)
            {
                settings.loadType = AudioClipLoadType.DecompressOnLoad;
                settings.compressionFormat = AudioCompressionFormat.Vorbis;
            }
            else
            {
                settings.loadType = AudioClipLoadType.CompressedInMemory;
                settings.compressionFormat = AudioCompressionFormat.Vorbis;
            }

            if (size > max)
            {
                settings.compressionFormat = AudioCompressionFormat.Vorbis;
                settings.loadType = AudioClipLoadType.Streaming;
                settings.quality = 70f;
            }
           
          
            
            audioImporter.SetOverrideSampleSettings("Standalone", settings);

        }
    }
    
}
