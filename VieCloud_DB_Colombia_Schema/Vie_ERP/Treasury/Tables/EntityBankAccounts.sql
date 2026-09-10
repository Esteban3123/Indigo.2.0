CREATE TABLE [Treasury].[EntityBankAccounts] (
    [Id]                          INT                                                                       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                        VARCHAR (20)                                                              NOT NULL,
    [IdBank]                      INT                                                                       NOT NULL,
    [IdCity]                      INT                                                                       NOT NULL,
    [Type]                        TINYINT                                                                   NOT NULL,
    [Prefix]                      VARCHAR (4)                                                               NOT NULL,
    [Number]                      VARCHAR (100) MASKED WITH (FUNCTION = 'partial(0, "Number_Ofuscado", 0)') NOT NULL,
    [InitialBalance]              DECIMAL (18, 2)                                                           NOT NULL,
    [CurrentBalance]              DECIMAL (18, 2)                                                           NOT NULL,
    [Rate]                        DECIMAL (18)                                                              NOT NULL,
    [Quota]                       DECIMAL (18)                                                              NOT NULL,
    [InitialDate]                 DATE                                                                      NOT NULL,
    [IdMainAccount]               INT                                                                       NOT NULL,
    [IdCostCenter]                INT                                                                       NULL,
    [ThirdPartyId]                INT                                                                       NULL,
    [FMGCounterpartMainAccountId] INT                                                                       NOT NULL,
    [FMGCounterpartThirdPartyId]  INT                                                                       NULL,
    [FMGCounterpartCostCenterId]  INT                                                                       NULL,
    [FMGExpenseMainAccountId]     INT                                                                       NOT NULL,
    [FMGExpenseThirdPartyId]      INT                                                                       NULL,
    [FMGExpenseCostCenterId]      INT                                                                       NULL,
    [FinancialSourceId]           INT                                                                       NULL,
    [Status]                      BIT                                                                       CONSTRAINT [DF_EntityBankAccount_Status] DEFAULT ((1)) NOT NULL,
    [CreationUser]                VARCHAR (20)                                                              NOT NULL,
    [CreationDate]                DATETIME                                                                  NOT NULL,
    [ModificationUser]            VARCHAR (20)                                                              NULL,
    [ModificationDate]            DATETIME                                                                  NULL,
    [TimeStamp]                   ROWVERSION                                                                NOT NULL,
    [CurrencyId]                  INT                                                                       CONSTRAINT [DF__EntityBan__Curre__74207DEE] DEFAULT ((1)) NULL,
    [BalanceLastRevaluation]      DECIMAL (18, 2)                                                           CONSTRAINT [DF_EntityBankAccounts_BalanceLastRevaluation] DEFAULT ((0)) NOT NULL,
    [PeriodLastRevaluation]       INT                                                                       CONSTRAINT [DF_EntityBankAccounts_PeriodLastRevaluation] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_EntityBankAccounts__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_EntityBankAccount_Bank] FOREIGN KEY ([IdBank]) REFERENCES [Payroll].[Bank] ([Id]),
    CONSTRAINT [FK_EntityBankAccount_City] FOREIGN KEY ([IdCity]) REFERENCES [Common].[City] ([Id]),
    CONSTRAINT [FK_EntityBankAccount_CostCenter] FOREIGN KEY ([IdCostCenter]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_EntityBankAccount_MainAccount] FOREIGN KEY ([IdMainAccount]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_EntityBankAccounts_Currency] FOREIGN KEY ([CurrencyId]) REFERENCES [Common].[Currency] ([Id]),
    CONSTRAINT [FK_EntityBankAccounts_FinancialSource] FOREIGN KEY ([FinancialSourceId]) REFERENCES [Budget].[FinancialSource] ([Id]),
    CONSTRAINT [FK_EntityBankAccounts_MainAccounts] FOREIGN KEY ([FMGCounterpartMainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_EntityBankAccounts_MainAccounts1] FOREIGN KEY ([FMGExpenseMainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_EntityBankAccounts_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [Treasury].[EntityBankAccounts].[Number]
    WITH (LABEL = 'Sensitive - Financial', INFORMATION_TYPE = 'Financial');




GO

CREATE UNIQUE NONCLUSTERED INDEX [UQ_EntityBankAccounts__Code]
    ON [Treasury].[EntityBankAccounts]([Code] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Período de última revalorización de la cuenta bancaria en formato YYYYmm (ej: 202301). Almacena el mes y año del último ajuste por inflación o revaluación de saldos.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'PeriodLastRevaluation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo para almacenar el ultimo periodo que fue revalorizada la Cuenta bancaria, el formato es YYYYmm  ejemplo: 202301', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'PeriodLastRevaluation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'PeriodLastRevaluation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Saldo registrado en la última revalorización de la cuenta bancaria. Decimal(18,2), valor por defecto 0.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'BalanceLastRevaluation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Saldo de la última revalorización.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'BalanceLastRevaluation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'BalanceLastRevaluation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la moneda de la cuenta bancaria (peso, dólar, euro, etc.). Referencia a Common.Currency. Por defecto moneda local (1).', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'CurrencyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Moneda de la nota deb/cred', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'CurrencyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'CurrencyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal automática (timestamp) que registra el instante de creación, modificación o evento crítico del registro de cuenta bancaria.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de la última modificación del registro de la cuenta bancaria. Tipo DATETIME, nullable.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario o identificación del operario que realizó la última modificación del registro. Varchar(20), nullable.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro de la cuenta bancaria. Tipo DATETIME, requerido.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario o identificación del operario que creó el registro de la cuenta bancaria. Varchar(20), requerido.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de la cuenta bancaria: 1=Activa, 0=Inactiva. Booleano, por defecto 1 (activo).', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del registro: True-Activo, False-Inactivo', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la fuente de financiación (presupuesto público, transferencias, etc.). Requerido solo para entidades públicas o municipales. Referencia a Budget.FinancialSource.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'FinancialSourceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la fuente de financiación, solo se pida cuando el tipo de compañía sea pública o de alcaldías', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'FinancialSourceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'FinancialSourceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del centro de costo asociado a la cuenta de gasto para Gravamen Movimientos Financieros (4x1000). Solicitado cuando la cuenta contable de gasto maneja centros de costo.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'FMGExpenseCostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del centro de costo que se va a solicitar cuando la cuenta de contrapartida maneje centro de costo', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'FMGExpenseCostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'FMGExpenseCostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tercero (proveedor, acreedor) asociado a la cuenta de gasto para Gravamen Movimientos Financieros (4x1000). Solicitado cuando la cuenta contable de gasto maneja terceros.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'FMGExpenseThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tercero que se va a solicitar cuando la cuenta del gasto maneje tercero', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'FMGExpenseThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'FMGExpenseThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cuenta contable de gasto para Gravamen Movimientos Financieros (4x1000). Referencia a GeneralLedger.MainAccounts, requerido.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'FMGExpenseMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Gravamen Movimientos Financieros (4x1000) -  Id Cuenta Contable del Gasto', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'FMGExpenseMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'FMGExpenseMainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del centro de costo asociado a la cuenta contable de contrapartida para Gravamen Movimientos Financieros (4x1000). Solicitado cuando la contrapartida maneja centros de costo.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'FMGCounterpartCostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del centro de costo que se va a solicitar cuando la cuenta de contrapartida maneje centro de costo', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'FMGCounterpartCostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'FMGCounterpartCostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tercero (acreedor, proveedor) asociado a la cuenta de contrapartida para Gravamen Movimientos Financieros (4x1000). Solicitado cuando la contrapartida maneja terceros.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'FMGCounterpartThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tercero que se va a solicitar cuando la cuenta de contrapartida maneje tercero', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'FMGCounterpartThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'FMGCounterpartThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cuenta contable de contrapartida para Gravamen Movimientos Financieros (4x1000). Referencia a GeneralLedger.MainAccounts, requerido.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'FMGCounterpartMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Gravamen Movimientos Financieros (4x1000) -  Id Cuenta Contable de la contrapartida', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'FMGCounterpartMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'FMGCounterpartMainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tercero, proveedor o acreedor asociado a la cuenta bancaria. Se solicita cuando la cuenta contable maneja terceros. Referencia a Common.ThirdParty.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Tercero (aparece si la cuenta contable maneja tercero)', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del centro de costo o unidad funcional asociada a la cuenta bancaria. Referencia a Payroll.CostCenter, nullable.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'IdCostCenter';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del centro de costo', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'IdCostCenter';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'IdCostCenter';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cuenta contable del mayor general asociada a la cuenta bancaria. Referencia a GeneralLedger.MainAccounts, requerido.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'IdMainAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta contable', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'IdMainAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'IdMainAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de apertura o inicio de operación de la cuenta bancaria. Tipo DATE.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'InitialDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha inicial', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'InitialDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'InitialDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cupo de sobregiro permitido en la cuenta bancaria. Valor decimal(18,2), límite de descubierto autorizado.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'Quota';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cupo de sobregiro', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'Quota';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'Quota';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tasa por mil (0.4%) aplicable a movimientos financieros de la cuenta. Valor decimal(18).', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'Rate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tasa por mil', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'Rate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'Rate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Saldo disponible actual de la cuenta bancaria. Cuantía en decimal(18,2), refleja activos menos pasivos.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'CurrentBalance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuantia disponible', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'CurrentBalance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'CurrentBalance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Saldo inicial de apertura de la cuenta bancaria en fecha de inicio. Valor decimal(18,2) de referencia histórica.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'InitialBalance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Saldo inicial de la cuenta', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'InitialBalance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'InitialBalance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de la cuenta bancaria. Varchar(100) ENMASCARADO (PII: Identificación_Ofuscado), solo primeros y últimos dígitos visibles.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'Number';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de la cuenta bancaria', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'Number';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'Number';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Prefijo de 4 caracteres usado en comprobantes de egreso y transferencias bancarias de la cuenta. Varchar(4).', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'Prefix';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el prefijo que se deberia usar cuando se realiza un comprobante de egreso', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'Prefix';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'Prefix';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de cuenta bancaria: 1=Cuenta de Ahorro, 2=Cuenta Corriente. Tinyint.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'Type';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de cuenta: 1-Ahorro, 2-Corriente', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'Type';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'Type';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la ciudad donde está radicar o domiciliada la cuenta bancaria. Referencia a Common.City.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'IdCity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la ciudad de radicación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'IdCity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'IdCity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del banco emisor o titular de la cuenta bancaria. Referencia a Payroll.Bank.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'IdBank';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del banco al que pertenece la cuenta', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'IdBank';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'IdBank';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único de la cuenta bancaria en el sistema ERP/EHR Indigo Vie Cloud. Varchar(20), identificador de negocio.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de la cuenta bancaria', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (clave primaria) del registro de cuenta bancaria. INT IDENTITY, autonumérico.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del registro', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuentas bancarias registradas por la entidad: bancos, tipos de cuenta (corriente, ahorros, crédito), saldos iniciales y actuales, cupos, tasas y sus contrapartidas contables para el módulo de tesorería.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts';
