CREATE TABLE [Contract].[BlockRecordContract] (
    [Id]        INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [FormId]    INT           NOT NULL,
    [RecordId]  INT           NOT NULL,
    [CodUser]   VARCHAR (250) NOT NULL,
    [NameUser]  VARCHAR (250) NOT NULL,
    [BlockDate] DATETIME      NOT NULL,
    CONSTRAINT [PK_BlockRecordContract] PRIMARY KEY CLUSTERED ([Id] ASC)
);

