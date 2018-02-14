using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Microsoft.ProjectOxford.Vision;
using Microsoft.ProjectOxford.Vision.Contract;

namespace epicture
{
    class Analyze
    {
        private VisionServiceClient visionClient;
        public AnalysisResult result { get; private set; }

        public Analyze()
        {
            visionClient = new VisionServiceClient("API KEY vision", @"https://westeurope.api.cognitive.microsoft.com/vision/v1.0");
        }

        public async Task<Description> AnalyzePictureAsync(Stream inputFile)
        {
            if (inputFile.Length == 0)
                return null;
            //VisualFeature[] visualFeatures = new VisualFeature[] { VisualFeature.Adult,
            //        VisualFeature.Categories, VisualFeature.Color, VisualFeature.Description,
            //        VisualFeature.Faces, VisualFeature.ImageType, VisualFeature.Tags };
            var _res = await visionClient.DescribeAsync(inputFile);
            return (_res.Description);
            //result = await visionClient.AnalyzeImageAsync(inputFile, visualFeatures);
            //result = await visionClient.DescribeAsync(inputFile, 3);
            //return (analysisResult);
        }
    }
}
