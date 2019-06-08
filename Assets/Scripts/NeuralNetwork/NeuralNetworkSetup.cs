using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngineInternal;

namespace Assets.Scripts.NeuralNetwork
{
	public class NeuralNetworkDataCollector
	{
		private string _path;
		private string _fileName;

		public string _fullPath
		{
			get { return _path + _fileName; }
		}

		public NeuralNetworkDataCollector(string path = @"c:\temp\", string fileName = "trainingdata.txt")
		{
			_path = path;
			_fileName = fileName;
			Directory.CreateDirectory(_path);

		}
		
		public void StoreData(NormalizableTrainingData[] trainingData)
		{
			NormalizeData(trainingData);

			var dataString = Environment.NewLine;

			string.Join(",", trainingData.Select(t => t.Value.ToString()).ToArray());

			File.AppendAllText(_fullPath, dataString);
		}

		private void NormalizeData(NormalizableTrainingData[] trainingData)
		{
			foreach (var data in trainingData)
			{
				data.Value = (data.Value - data.Min) /
					  (data.Max - data.Min);
			}

		}
		
	}

	public class NormalizableTrainingData
	{
		public string Label { get; set; }
		public double Value { get; set; }
		public double Min { get; set; }
		public double Max { get; set; }
	}

	public class TrainingData
	{
		public NormalizableTrainingData Health { get; set; }
		public NormalizableTrainingData Damage { get; set; }
		public List<NormalizableTrainingData> Tiles { get; set; }
	}
}
