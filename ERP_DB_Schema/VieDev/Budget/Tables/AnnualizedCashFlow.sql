CREATE TABLE [Budget].[AnnualizedCashFlow] (
    [Id]                         INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CategoryId]                 INT             NOT NULL,
    [DocumentSource]             TINYINT         NOT NULL,
    [Month]                      TINYINT         NOT NULL,
    [InitialValue]               NUMERIC (18, 2) NOT NULL,
    [DebitModificationValue]     NUMERIC (18, 2) NOT NULL,
    [CreditModificationValue]    NUMERIC (18, 2) NOT NULL,
    [DebitTransferValue]         NUMERIC (18, 2) NOT NULL,
    [CreditTransferValue]        NUMERIC (18, 2) NOT NULL,
    [TotalScheduled]             NUMERIC (18, 2) NOT NULL,
    [ExecutedValue]              NUMERIC (18, 2) NOT NULL,
    [Balance]                    NUMERIC (18, 2) NOT NULL,
    [ReserveValue]               NUMERIC (18, 2) NOT NULL,
    [DebitReserveModValue]       NUMERIC (18, 2) NOT NULL,
    [CreditReserveModValie]      NUMERIC (18, 2) NOT NULL,
    [DebitReserveTransValue]     NUMERIC (18, 2) NOT NULL,
    [CreditReserveTransValue]    NUMERIC (18, 2) NOT NULL,
    [ExecutedReserveValue]       NUMERIC (18, 2) NOT NULL,
    [CxPValue]                   NUMERIC (18, 2) NOT NULL,
    [DebitCxPModificationValue]  NUMERIC (18, 2) NOT NULL,
    [CreditCxPModificationValue] NUMERIC (18, 2) NOT NULL,
    [DebitCxPTransferValue]      NUMERIC (18, 2) NOT NULL,
    [CreditCxPTransferValue]     NUMERIC (18, 2) NOT NULL,
    [ExecutedCxPValue]           NUMERIC (18, 2) NOT NULL,
    [Status]                     TINYINT         NOT NULL,
    [CreationUser]               VARCHAR (20)    CONSTRAINT [DF_AnnualizedCashFlow_CreationUser] DEFAULT ((999)) NOT NULL,
    [CreationDate]               DATETIME        CONSTRAINT [DF_AnnualizedCashFlow_CreationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [ModificationUser]           VARCHAR (20)    NULL,
    [ModificationDate]           DATETIME        NULL,
    [ConfirmationUser]           VARCHAR (20)    NULL,
    [ConfirmationDate]           DATETIME        NULL,
    [AnnulmentUser]              VARCHAR (20)    NULL,
    [AnnulmentDate]              DATETIME        NULL,
    [TimeStamp]                  ROWVERSION      NOT NULL,
    CONSTRAINT [PK_PAC] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PAC_Category] FOREIGN KEY ([CategoryId]) REFERENCES [Budget].[Category] ([Id])
);


GO
ALTER TABLE [Budget].[AnnualizedCashFlow] NOCHECK CONSTRAINT [FK_PAC_Category];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el saldo del PAC, este se calcula de la siguiente manera  (TotalScheduled - ExecutedValue )', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'Balance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Total Programado, este se calcula de la siguiente manera  (IniitalValue - DebitModificationValue + CreditModificationValue - DebitTransferValue + CreditTransferValue)', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'TotalScheduled';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Mes del PAC (ENERO = 1,FEBRERO = 2,MARZO =  3,ABRIL = 4,MAYO = 5,JUNIO = 6,JULIO = 7,AGOSTO =  8,SEPTIEMBRE = 9,OCTUBRE = 10,NOVIEMBRE =  11,DICIEMBRE = 12)', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'Month';

