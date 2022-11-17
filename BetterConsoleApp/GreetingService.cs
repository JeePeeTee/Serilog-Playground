﻿#region MIT License

// ==========================================================
// 
// ConsoleWithLogging project - Copyright (c) 2022 JeePeeTee
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
// 
// ===========================================================

#endregion

#region usings

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

#endregion

namespace BetterConsoleApp;

public interface IGreetingService {
    void Run();
}

public class GreetingService : IGreetingService {
    private readonly IConfiguration _config;
    private readonly ILogger<GreetingService> _log;

    public GreetingService(ILogger<GreetingService> log, IConfiguration config) {
        _log = log;
        _config = config;
    }

    public void Run() {
        for (var i = 0; i < _config.GetValue<int>("LoopTimes"); i++) {
            _log.LogInformation("Run number {RunNumber}", i);
        }

        try {
            throw new Exception("This is an Exception...");
        }
        catch (Exception exception) {
            _log.LogError(exception, "This is an Exception");
        }
    }
}