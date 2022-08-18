using OOMetrics.Metrics.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOMetrics.Metrics
{
    public class MetricsCalculator
    {
        private readonly List<IDeclaration> rawData;
        public MetricsCalculator(List<IDeclaration> data)
        {
            rawData = data;
        }
    }
}
