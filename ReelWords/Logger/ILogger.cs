using System;
namespace ReelWords.Logger
{
	public interface ILogger
	{
		void Error(string error);
        void Error(string error, Exception ex);

        void Info(String info);


	}
}

