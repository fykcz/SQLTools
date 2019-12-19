namespace FYK.ITTools.BCPGUI
{
    public class BCPParams
    {
        public string Action { get; set; }
        public string SourceOrTarget { get; set; }
        public string DataFile { get; set; }
        public bool WithFormatFile { get; set; }
        public string FormatFile { get; set; }
        public bool WithXMLFormatFile { get; set; }
        public string Server { get; set; }
        public string Database { get; set; }
        public string Authentication { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public bool WithPacketSize { get; set; }
        public long PacketSize { get; set; }
        public bool WithBatchSize { get; set; }
        public long BatchSize { get; set; }
        public bool WithFirstRow { get; set; }
        public long FirstRow { get; set; }
        public bool WithLastRow { get; set; }
        public long LastRow { get; set; }
        public bool WithMaxErrors { get; set; }
        public long MaxErrors { get; set; }
        public bool WithErrorFile { get; set; }
        public string ErrorFile { get; set; }
        public bool WithInputFile { get; set; }
        public string InputFile { get; set; }
        public bool WithOutputFile { get; set; }
        public string OutputFile { get; set; }
        public string Format { get; set; }
        public bool WithCodePage { get; set; }
        public string CodePage { get; set; }
        public bool WithDataTypeVersion { get; set; }
        public int DataTypeVersion { get; set; }
        public bool WithRowTerminator { get; set; }
        public string RowTerminator { get; set; }
        public bool WithFieldTerminator { get; set; }
        public string FieldTerminator { get; set; }
        public bool WithImportIdentity { get; set; }
        public bool WithKeepNull { get; set; }
        public bool WithQuotedIdentifiers { get; set; }
        public bool WithRegionalFormat { get; set; }
        public bool WithHintOrder { get; set; }
        public string HintOrder { get; set; }
        public bool WithHintRowsPerBatch { get; set; }
        public long HintRowsPerBatch { get; set; }
        public bool WithHintKilobytesPerBatch { get; set; }
        public long HintKilobytesPerBatch { get; set; }
        public bool WithHintTablock { get; set; }
        public bool WithHintCheckContraints { get; set; }
        public bool WithHintFireTriggers { get; set; }
    }
}
