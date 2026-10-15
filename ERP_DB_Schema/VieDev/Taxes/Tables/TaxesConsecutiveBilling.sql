CREATE TABLE [Taxes].[TaxesConsecutiveBilling] (
    [Id]               INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Name]             VARCHAR (200) NOT NULL,
    [InvoicePrefix]    VARCHAR (5)   NOT NULL,
    [InitialInvoice]   INT           NOT NULL,
    [FinalInvoice]     INT           NOT NULL,
    [Consecutive]      INT           NOT NULL,
    [Status]           TINYINT       NOT NULL,
    [CreationUser]     VARCHAR (20)  NOT NULL,
    [CreationDate]     DATETIME      NOT NULL,
    [ModificationUser] VARCHAR (20)  NULL,
    [ModificationDate] DATETIME      NULL,
    [TimeStamp]        ROWVERSION    NOT NULL,
    CONSTRAINT [PK_TaxesConsecutiveBilling] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Estado del regitro   1 activo  0 inactivo', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesConsecutiveBilling', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacena el consecutivo de la facturación de impuestos.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesConsecutiveBilling', @level2type = N'COLUMN', @level2name = N'Consecutive';

