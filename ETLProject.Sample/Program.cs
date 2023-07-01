using ETLProject.Common.Common.DIManager;
using ETLProject.Common.Database;
using ETLProject.Common.Table;
using ETLProject.DataSource.Abstractions;
using ETLProject.DataSource.Common;
using ETLProject.DataSource.Common.DIManager;
using ETLProject.Sample.ETLSamples;
using Microsoft.Extensions.DependencyInjection;
using SqlKata;
using System.Data;

await WriteSamplesHelpers.ExtractAndLoad();
