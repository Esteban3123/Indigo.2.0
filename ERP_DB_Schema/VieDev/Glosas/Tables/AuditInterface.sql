CREATE TABLE [Glosas].[AuditInterface] (
    [Id]             INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [OptionRegister] VARCHAR (50) NOT NULL,
    [NumberGlosa]    VARCHAR (50) NOT NULL,
    [InvoiceNumber]  VARCHAR (50) NOT NULL,
    [ActionCode]     INT          NOT NULL,
    [Concept]        VARCHAR (50) NOT NULL,
    [NewConsecutive] VARCHAR (50) NOT NULL,
    [TimeStamp]      ROWVERSION   NOT NULL,
    [Date]           DATETIME     CONSTRAINT [DF_AuditInterface_Date] DEFAULT ([Common].[getdate]()) NULL,
    CONSTRAINT [PK_AuditInterface] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 Comprobante Contable  2 Nota Credito  3 Nota Debito', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AuditInterface', @level2type = N'COLUMN', @level2name = N'ActionCode';

