CREATE TABLE [Portfolio].[PortfolioNote] (
    [Id]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                VARCHAR (20)  NOT NULL,
    [NoteDate]            DATETIME      NOT NULL,
    [CustomerId]          INT           NULL,
    [Observations]        VARCHAR (MAX) NULL,
    [Nature]              TINYINT       NOT NULL,
    [NoteType]            TINYINT       NOT NULL,
    [PortfolioAdvanceId]  INT           NULL,
    [OperatingUnitId]     INT           NOT NULL,
    [Status]              TINYINT       NOT NULL,
    [CreationUser]        VARCHAR (20)  NOT NULL,
    [CreationDate]        DATETIME      NOT NULL,
    [ModificationUser]    VARCHAR (20)  NULL,
    [ModificationDate]    DATETIME      NULL,
    [ConfirmationUser]    VARCHAR (20)  NULL,
    [ConfirmationDate]    DATETIME      NULL,
    [AnnulmentUser]       VARCHAR (20)  NULL,
    [AnnulmentDate]       DATETIME      NULL,
    [TimeStamp]           ROWVERSION    NOT NULL,
    [PortfolioTransferId] INT           NULL,
    [CurrencyId]          INT           CONSTRAINT [DF__Portfolio__Curre__02A3A76F] DEFAULT ((1)) NOT NULL,
    [EntityName]          VARCHAR (250) NULL,
    CONSTRAINT [PK_PortfolioNote] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PortfolioNote_Currency] FOREIGN KEY ([CurrencyId]) REFERENCES [Common].[Currency] ([Id]),
    CONSTRAINT [FK_PortfolioNote_Customer] FOREIGN KEY ([CustomerId]) REFERENCES [Common].[Customer] ([Id]),
    CONSTRAINT [FK_PortfolioNote_OperatingUnit] FOREIGN KEY ([OperatingUnitId]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [FK_PortfolioNote_PortfolioAdvance] FOREIGN KEY ([PortfolioAdvanceId]) REFERENCES [Portfolio].[PortfolioAdvance] ([Id]),
    CONSTRAINT [FK_PortfolioNote_PortfolioTransfer] FOREIGN KEY ([PortfolioTransferId]) REFERENCES [Portfolio].[PortfolioTransfer] ([Id])
);


GO
ALTER TABLE [Portfolio].[PortfolioNote] NOCHECK CONSTRAINT [FK_PortfolioNote_Currency];


GO
ALTER TABLE [Portfolio].[PortfolioNote] NOCHECK CONSTRAINT [FK_PortfolioNote_Customer];


GO
ALTER TABLE [Portfolio].[PortfolioNote] NOCHECK CONSTRAINT [FK_PortfolioNote_OperatingUnit];


GO
ALTER TABLE [Portfolio].[PortfolioNote] NOCHECK CONSTRAINT [FK_PortfolioNote_PortfolioAdvance];


GO
ALTER TABLE [Portfolio].[PortfolioNote] NOCHECK CONSTRAINT [FK_PortfolioNote_PortfolioTransfer];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'La nota aplica: 
1 - Factura Total (Se cargan las facturas que tengan saldos en una o mas de una cuenta contable asociada y que solo tenga una cuota)  
2 - Factura Cuota (Se cargan las facturas que tengan saldo solo en una cuenta contable pero que tenga mas de una cuota)  
3 - Anticipo  4 - Distribucion de Anticipo (Cuando sea de este tipo la naturaleza siempre va hacer de tipo credito)  
5 - Reversión Anticipo vs CxC 
6 - Factura Detallada
7 - Factura glosada', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNote', @level2type = N'COLUMN', @level2name = N'NoteType';

