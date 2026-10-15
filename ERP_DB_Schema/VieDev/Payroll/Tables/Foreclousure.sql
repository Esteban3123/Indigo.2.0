CREATE TABLE [Payroll].[Foreclousure] (
    [Id]                        INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                      VARCHAR (20)   NOT NULL,
    [IdEmployee]                INT            NOT NULL,
    [InitialContract]           INT            NOT NULL,
    [IdContract]                INT            NOT NULL,
    [ForeclousureType]          TINYINT        NOT NULL,
    [Comment]                   VARCHAR (500)  NULL,
    [State]                     TINYINT        NOT NULL,
    [IdApplicant]               INT            NOT NULL,
    [DiscountType]              TINYINT        NOT NULL,
    [DiscountClass]             TINYINT        NOT NULL,
    [QuoteValue]                NUMERIC (18)   NULL,
    [QuoteNumber]               INT            NULL,
    [Percentage]                DECIMAL (6, 2) NULL,
    [TotalValue]                NUMERIC (18)   NULL,
    [CurrentBalance]            NUMERIC (18)   NULL,
    [IdConcept]                 INT            NOT NULL,
    [InitialDate]               DATE           NOT NULL,
    [AffectVacation]            BIT            NOT NULL,
    [Affect1erIncentivePayment] BIT            NOT NULL,
    [Affect2doIncentivePayment] BIT            NOT NULL,
    [AffectUnemploymentValue]   BIT            NOT NULL,
    [IdJudgment]                INT            NOT NULL,
    [IdCity]                    INT            NOT NULL,
    [CodeDestinationOffice]     VARCHAR (5)    CONSTRAINT [DF_Foreclousure_CodeDestinationOffice] DEFAULT ((0)) NOT NULL,
    [TradeNumber]               VARCHAR (100)  NOT NULL,
    [TradeDate]                 DATE           NOT NULL,
    [IdBeneficiary]             INT            NOT NULL,
    [ProcessNumber]             VARCHAR (100)  NOT NULL,
    [CreationDate]              DATE           NOT NULL,
    [CreationUser]              VARCHAR (20)   NOT NULL,
    [ConfirmationDate]          DATE           NULL,
    [ConfirmationUser]          VARCHAR (20)   NULL,
    [AnnulmentDate]             DATE           NULL,
    [AnnulmentUser]             VARCHAR (20)   NULL,
    [AnnulmentComment]          VARCHAR (100)  NULL,
    [SuspendDate]               DATE           NULL,
    [SuspendUser]               VARCHAR (20)   NULL,
    [SupendComment]             VARCHAR (100)  NULL,
    [TimeStamp]                 ROWVERSION     NOT NULL,
    CONSTRAINT [PK_Foreclousure] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Foreclousure_City] FOREIGN KEY ([IdCity]) REFERENCES [Common].[City] ([Id]),
    CONSTRAINT [FK_Foreclousure_Company] FOREIGN KEY ([IdJudgment]) REFERENCES [Payroll].[Company] ([Id]),
    CONSTRAINT [FK_Foreclousure_Concept] FOREIGN KEY ([IdConcept]) REFERENCES [Payroll].[Concept] ([Id]),
    CONSTRAINT [FK_Foreclousure_Contract] FOREIGN KEY ([IdContract]) REFERENCES [Payroll].[Contract] ([Id]),
    CONSTRAINT [FK_Foreclousure_Employee] FOREIGN KEY ([IdEmployee]) REFERENCES [Payroll].[Employee] ([Id]),
    CONSTRAINT [FK_Foreclousure_ThirdParty] FOREIGN KEY ([IdApplicant]) REFERENCES [Common].[ThirdParty] ([Id]),
    CONSTRAINT [FK_Foreclousure_ThirdParty1] FOREIGN KEY ([IdBeneficiary]) REFERENCES [Common].[ThirdParty] ([Id])
);


GO
ALTER TABLE [Payroll].[Foreclousure] NOCHECK CONSTRAINT [FK_Foreclousure_City];


GO
ALTER TABLE [Payroll].[Foreclousure] NOCHECK CONSTRAINT [FK_Foreclousure_Company];


GO
ALTER TABLE [Payroll].[Foreclousure] NOCHECK CONSTRAINT [FK_Foreclousure_Concept];


GO
ALTER TABLE [Payroll].[Foreclousure] NOCHECK CONSTRAINT [FK_Foreclousure_Contract];


GO
ALTER TABLE [Payroll].[Foreclousure] NOCHECK CONSTRAINT [FK_Foreclousure_Employee];


GO
ALTER TABLE [Payroll].[Foreclousure] NOCHECK CONSTRAINT [FK_Foreclousure_ThirdParty];


GO
ALTER TABLE [Payroll].[Foreclousure] NOCHECK CONSTRAINT [FK_Foreclousure_ThirdParty1];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Clase de Descuento:  1. Porcentaje  2. Valor Fijo  3. Valor Fijo con Saldo  4. Porcentaje con Saldo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'DiscountClass';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Clase de Descuento:  1. Porcentaje  2. Valor Fijo  3. Valor Fijo con Saldo  4. Porcentaje con Saldo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'DiscountType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado:  1. Registrado   - 2. Confirmado   - 3. Suspendido -   4. Anulado - 5. Terminado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Embargo  1. Embargos por Obligaciones Comunes  2. Embargos por Obligaciones Alimentarias y/o Cooperativas  3. Embargos Judiciales de Prestaciones Sociales', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Foreclousure', @level2type = N'COLUMN', @level2name = N'ForeclousureType';

