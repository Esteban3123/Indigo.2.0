CREATE TABLE [Budget].[RevenueType] (
    [Id]                        INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [BudgetaryValidityId]       INT           NOT NULL,
    [Code]                      VARCHAR (20)  NOT NULL,
    [Name]                      VARCHAR (100) NULL,
    [Type]                      TINYINT       NULL,
    [IncomeSource]              TINYINT       NULL,
    [ExpenditureDefinition]     TINYINT       NULL,
    [ExpenditureClassification] TINYINT       NULL,
    [Status]                    BIT           CONSTRAINT [DF_EarningsType_Status] DEFAULT ((1)) NOT NULL,
    [CreationUser]              VARCHAR (20)  CONSTRAINT [DF_RevenueType_CreationUser] DEFAULT ((999)) NOT NULL,
    [CreationDate]              DATETIME      CONSTRAINT [DF_RevenueType_CreationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [ModificationUser]          VARCHAR (20)  NULL,
    [ModificationDate]          DATETIME      NULL,
    [TimeStamp]                 ROWVERSION    NOT NULL,
    CONSTRAINT [PK_RevenueType__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RevenueType_BudgetaryValidity] FOREIGN KEY ([BudgetaryValidityId]) REFERENCES [Budget].[BudgetaryValidity] ([Id])
);


GO
ALTER TABLE [Budget].[RevenueType] NOCHECK CONSTRAINT [FK_RevenueType_BudgetaryValidity];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sello de tiempo. Guarda el instante tiempo de la creación, registro o modificación de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'RevenueType', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Clasificacion de Gastos   1 - Funcionamiento  2 - Inversion  3 - Servicios de la deuda  4 - Gastos del personal  5- Gastos Generales  6 - Mantenimiento Hospitalario  7 - Transferencias  8 - Gastos de operacion comercial y de prestacion de servicios  9 - Disponibilidad Final', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'RevenueType', @level2type = N'COLUMN', @level2name = N'ExpenditureClassification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Origen del tipo de ingreso:   1 - Aportes Nacionales  2 - Ingresos propios  3 - Disponibilidad inicial  4 - Venta de servicios  5 - Sistema general de participaciones  6 - Otros ingresos  7 - Ingresos capital  8 - Total ingresos', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'RevenueType', @level2type = N'COLUMN', @level2name = N'IncomeSource';

