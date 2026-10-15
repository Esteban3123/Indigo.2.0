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
ALTER TABLE [Treasury].[EntityBankAccounts] NOCHECK CONSTRAINT [FK_EntityBankAccount_Bank];


GO
ALTER TABLE [Treasury].[EntityBankAccounts] NOCHECK CONSTRAINT [FK_EntityBankAccount_City];


GO
ALTER TABLE [Treasury].[EntityBankAccounts] NOCHECK CONSTRAINT [FK_EntityBankAccount_CostCenter];


GO
ALTER TABLE [Treasury].[EntityBankAccounts] NOCHECK CONSTRAINT [FK_EntityBankAccount_MainAccount];


GO
ALTER TABLE [Treasury].[EntityBankAccounts] NOCHECK CONSTRAINT [FK_EntityBankAccounts_Currency];


GO
ALTER TABLE [Treasury].[EntityBankAccounts] NOCHECK CONSTRAINT [FK_EntityBankAccounts_FinancialSource];


GO
ALTER TABLE [Treasury].[EntityBankAccounts] NOCHECK CONSTRAINT [FK_EntityBankAccounts_MainAccounts];


GO
ALTER TABLE [Treasury].[EntityBankAccounts] NOCHECK CONSTRAINT [FK_EntityBankAccounts_MainAccounts1];


GO
ALTER TABLE [Treasury].[EntityBankAccounts] NOCHECK CONSTRAINT [FK_EntityBankAccounts_ThirdParty];


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_EntityBankAccounts__Code]
    ON [Treasury].[EntityBankAccounts]([Code] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo para almacenar el ultimo periodo que fue revalorizada la Cuenta bancaria, el formato es YYYYmm  ejemplo: 202301', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'PeriodLastRevaluation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Saldo de la última revalorización.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'BalanceLastRevaluation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Moneda de la nota deb/cred', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'CurrencyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del registro: True-Activo, False-Inactivo', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la fuente de financiación, solo se pida cuando el tipo de compañía sea pública o de alcaldías', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'FinancialSourceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del centro de costo que se va a solicitar cuando la cuenta de contrapartida maneje centro de costo', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'FMGExpenseCostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del tercero que se va a solicitar cuando la cuenta del gasto maneje tercero', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'FMGExpenseThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Gravamen Movimientos Financieros (4x1000) -  Id Cuenta Contable del Gasto', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'FMGExpenseMainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del centro de costo que se va a solicitar cuando la cuenta de contrapartida maneje centro de costo', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'FMGCounterpartCostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del tercero que se va a solicitar cuando la cuenta de contrapartida maneje tercero', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'FMGCounterpartThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Gravamen Movimientos Financieros (4x1000) -  Id Cuenta Contable de la contrapartida', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'FMGCounterpartMainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Tercero (aparece si la cuenta contable maneja tercero)', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del centro de costo', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'IdCostCenter';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuenta contable', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'IdMainAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha inicial', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'InitialDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cupo de sobregiro', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'Quota';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tasa por mil', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'Rate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuantia disponible', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'CurrentBalance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Saldo inicial de la cuenta', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'InitialBalance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número de la cuenta bancaria', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'Number';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el prefijo que se deberia usar cuando se realiza un comprobante de egreso', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'Prefix';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de cuenta: 1-Ahorro, 2-Corriente', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'Type';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la ciudad de radicación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'IdCity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del banco al que pertenece la cuenta', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'IdBank';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de la cuenta bancaria', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del registro', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'Id';

