﻿
namespace Anycmd.Repl
{
	using Edi.Application;
	using Ef;
	using Engine.Host;
	using Engine.Host.Impl;
	using Jint;
	using Jint.Native;
	using Jint.Runtime;
	using Logging;
	using System;
	using System.Collections.Generic;
	using System.Diagnostics;
	using System.IO;
	using System.Reflection;

	class Program
	{
		private static IAcDomain _acDomain;

		static void Main(string[] args)
		{
			//程序没有运行
			try
			{
				EfContext.InitStorage(new SimpleEfContextStorage());
				// 环境初始化
				var acDomain = new DefaultAcDomain();
				_acDomain = acDomain;
				_acDomain.AddService(typeof(ILoggingService), new Log4NetLoggingService(_acDomain));
				_acDomain.AddService(typeof(IUserSessionStorage), new SimpleUserSessionStorage());
				acDomain.Init();
				_acDomain.RegisterRepository(new List<string> {
						"EdiEntities",
						"AcEntities",
						"InfraEntities",
						"IdentityEntities"
					}, typeof(AcDomain).Assembly);
				_acDomain.RegisterEdiCore();

				Run(args);
			}
			catch (Exception ex)
			{
				_acDomain.LoggingService.Error(ex);
				Console.WriteLine(ex.Message);
				Console.WriteLine(@"按任意键退出");
				Console.ReadKey();
			}
		}

		private static void Run(string[] args)
		{
			var engine = new Engine(cfg => cfg.AllowClr())
				.SetValue("print", new Action<object>(Console.WriteLine))
				.SetValue("ac", _acDomain);

			var filename = args.Length > 0 ? args[0] : "";
			if (!String.IsNullOrEmpty(filename))
			{
				if (!File.Exists(filename))
				{
					Console.WriteLine(@"Could not find file: {0}", filename);
				}

				var script = File.ReadAllText(filename);
				var result = engine.GetValue(engine.Execute(script).GetCompletionValue());
				return;
			}

			Assembly assembly = Assembly.GetExecutingAssembly();
			FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
			string version = fvi.FileVersion;

			Console.WriteLine(@"Welcome to Anycmd ({0})", version);
			Console.WriteLine(@"Type 'exit' to leave, 'print()' to write on the console.");
			Console.WriteLine();

			var defaultColor = Console.ForegroundColor;
			while (true)
			{
				Console.ForegroundColor = defaultColor;
				Console.Write(@"anycmd> ");
				var input = Console.ReadLine();
				if (input == "exit")
				{
					return;
				}

				try
				{
					var result = engine.GetValue(engine.Execute(input).GetCompletionValue());
					if (result.Type != Types.None && result.Type != Types.Null && result.Type != Types.Undefined)
					{
						var str = TypeConverter.ToString(engine.Json.Stringify(engine.Json, Arguments.From(result, Undefined.Instance, "  ")));
						Console.WriteLine(@"=> {0}", str);
					}
				}
				catch (JavaScriptException je)
				{
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine(je.ToString());
				}
				catch (Exception e)
				{
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine(e.Message);
				}
			}
		}
	}
}
