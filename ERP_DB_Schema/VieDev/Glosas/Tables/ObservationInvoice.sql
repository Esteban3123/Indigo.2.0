CREATE TABLE [Glosas].[ObservationInvoice] (
    [Id]          INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]        VARCHAR (5)   NOT NULL,
    [Name]        VARCHAR (50)  NOT NULL,
    [Description] VARCHAR (100) NULL,
    [TimeStamp]   ROWVERSION    NOT NULL,
    CONSTRAINT [PK_ObservationInvoice] PRIMARY KEY CLUSTERED ([Id] ASC)
);

