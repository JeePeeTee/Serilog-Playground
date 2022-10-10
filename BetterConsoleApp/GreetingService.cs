﻿#region Copyright (c) 2000-2022 Sultan CRM BV

// ==========================================================
// 
// ConsoleUI project - Copyright (c) 2000-2022 Sultan CRM BV
// ALL RIGHTS RESERVED
// 
// The entire contents of this file is protected by Dutch and
// International Copyright Laws. Unauthorized reproduction,
// reverse-engineering, and distribution of all or any portion of
// the code contained in this file is strictly prohibited and may
// result in severe civil and criminal penalties and will be
// prosecuted to the maximum extent possible under the law.
// 
// RESTRICTIONS
// 
// THIS SOURCE CODE AND ALL RESULTING INTERMEDIATE FILES
// ARE CONFIDENTIAL AND PROPRIETARY TRADE
// SECRETS OF SULTAN CRM BV. THE REGISTERED DEVELOPER IS
// NOT LICENSED TO DISTRIBUTE THE PRODUCT AND ALL ACCOMPANYING
// CODE AS PART OF AN EXECUTABLE PROGRAM.
// 
// THE SOURCE CODE CONTAINED WITHIN THIS FILE AND ALL RELATED
// FILES OR ANY PORTION OF ITS CONTENTS SHALL AT NO TIME BE
// COPIED, TRANSFERRED, SOLD, DISTRIBUTED, OR OTHERWISE MADE
// AVAILABLE TO OTHER INDIVIDUALS WITHOUT EXPRESS WRITTEN CONSENT
// AND PERMISSION FROM SULTAN CRM BV.
// 
// CONSULT THE LICENSE AGREEMENT FOR INFORMATION ON
// ADDITIONAL RESTRICTIONS
// 
// ===========================================================

#endregion

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace BetterConsoleApp;

public interface IGreetingService {
    void Run();
}

public class GreetingService : IGreetingService {
    private readonly ILogger<GreetingService> _log;
    private readonly IConfiguration _config;

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