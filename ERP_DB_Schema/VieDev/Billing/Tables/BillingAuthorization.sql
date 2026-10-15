CREATE TABLE [Billing].[BillingAuthorization] (
    [Id]               INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]             VARCHAR (20)  NOT NULL,
    [Name]             VARCHAR (100) NOT NULL,
    [ResolutionNumber] VARCHAR (50)  NOT NULL,
    [ResolutionDate]   DATE          NOT NULL,
    [InvoicePrefix]    VARCHAR (5)   NOT NULL,
    [InitialInvoice]   BIGINT        NOT NULL,
    [FinalInvoice]     BIGINT        NOT NULL,
    [InvoiceType]      TINYINT       NOT NULL,
    [Consecutive]      BIGINT        NOT NULL,
    [Status]           BIT           NOT NULL,
    [CreationUser]     VARCHAR (20)  NOT NULL,
    [CreationDate]     DATETIME      NOT NULL,
    [ModificationUser] VARCHAR (20)  NULL,
    [ModificationDate] DATETIME      NULL,
    [TimeStamp]        ROWVERSION    NOT NULL,
    [TechnicalKey]     VARCHAR (500) NULL,
    [InitialDate]      DATE          NULL,
    [FinalDate]        DATE          NULL,
    CONSTRAINT [PK_BillingAuthorization__Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del de la entidad 1 - Activo  0 - Inactivo', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingAuthorization', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el tipo de factura   1 - Digitalizada  2 - Manual  3 - Electrónica  4 - documento soporte', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingAuthorization', @level2type = N'COLUMN', @level2name = N'InvoiceType';

