using System;

namespace Sigmade.DataGenerator.Model
{
    public class Diagnostic
    {
        public long MemoryBeforeMb { get; set; }
        public long MemoryAfterMb { get; set; }
        public long MemoryAfterGcMb { get; set; }
        public int GenerationBefore { get; set; }
        public int GenerationAfter { get; set; }
        public TimeSpan TimeExecuted { get; set; }
    }
}
