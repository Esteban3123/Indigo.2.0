CREATE TABLE [Treasury].[CashReceipts] (
    [Id]                              INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                            VARCHAR (20)    NOT NULL,
    [IdThirdParty]                    INT             NOT NULL,
    [CollectType]                     TINYINT         NOT NULL,
    [IdMainAccount]                   INT             NOT NULL,
    [IdCostCenter]                    INT             NULL,
    [Detail]                          VARCHAR (2000)  NULL,
    [DocumentDate]                    DATETIME        NOT NULL,
    [IdCashRegister]                  INT             NULL,
    [IdBankAccount]                   INT             NULL,
    [PaymentResponsibles]             VARCHAR (100)   NOT NULL,
    [Value]                           DECIMAL (18, 2) NOT NULL,
    [IdRefund]                        INT             NULL,
    [OperatingUnitId]                 INT             NOT NULL,
    [Status]                          TINYINT         NOT NULL,
    [CreationUser]                    VARCHAR (20)    NOT NULL,
    [CreationDate]                    DATETIME        NOT NULL,
    [ModificationUser]                VARCHAR (20)    NULL,
    [ModificationDate]                DATETIME        NULL,
    [ConfirmationUser]                VARCHAR (20)    NULL,
    [ConfirmationDate]                DATETIME        NULL,
    [AnnulmentUser]                   VARCHAR (20)    NULL,
    [AnnulmentDate]                   DATETIME        NULL,
    [ReversedUser]                    VARCHAR (20)    NULL,
    [ReversedDate]                    DATETIME        NULL,
    [TimeStamp]                       ROWVERSION      NOT NULL,
    [PaymentResponsiblesThirdPartyId] INT             NULL,
    [EntityName]                      VARCHAR (250)   NULL,
    [CurrencyId]                      INT             NULL,
    CONSTRAINT [PK_CashReceipts__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CashReceipt_CashRegisters] FOREIGN KEY ([IdCashRegister]) REFERENCES [Treasury].[CashRegisters] ([Id]),
    CONSTRAINT [FK_CashReceipt_CostCenter] FOREIGN KEY ([IdCostCenter]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_CashReceipt_EntityBankAccounts] FOREIGN KEY ([IdBankAccount]) REFERENCES [Treasury].[EntityBankAccounts] ([Id]),
    CONSTRAINT [FK_CashReceipt_MainAccounts] FOREIGN KEY ([IdMainAccount]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_CashReceipt_ThirdParty] FOREIGN KEY ([IdThirdParty]) REFERENCES [Common].[ThirdParty] ([Id]),
    CONSTRAINT [FK_CashReceipts_OperatingUnit] FOREIGN KEY ([OperatingUnitId]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [FK_CashReceipts_ThirdPartyResponsiblePayment] FOREIGN KEY ([PaymentResponsiblesThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id])
);


GO
ALTER TABLE [Treasury].[CashReceipts] NOCHECK CONSTRAINT [FK_CashReceipt_CashRegisters];


GO
ALTER TABLE [Treasury].[CashReceipts] NOCHECK CONSTRAINT [FK_CashReceipt_CostCenter];


GO
ALTER TABLE [Treasury].[CashReceipts] NOCHECK CONSTRAINT [FK_CashReceipt_EntityBankAccounts];


GO
ALTER TABLE [Treasury].[CashReceipts] NOCHECK CONSTRAINT [FK_CashReceipt_MainAccounts];


GO
ALTER TABLE [Treasury].[CashReceipts] NOCHECK CONSTRAINT [FK_CashReceipt_ThirdParty];


GO
ALTER TABLE [Treasury].[CashReceipts] NOCHECK CONSTRAINT [FK_CashReceipts_OperatingUnit];


GO
ALTER TABLE [Treasury].[CashReceipts] NOCHECK CONSTRAINT [FK_CashReceipts_ThirdPartyResponsiblePayment];


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_CashReceipts__Code]
    ON [Treasury].[CashReceipts]([Code] ASC);


GO
CREATE NONCLUSTERED INDEX [idx_CashReceipts_Status_Includes]
    ON [Treasury].[CashReceipts]([Status] ASC)
    INCLUDE([Code], [DocumentDate]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'nombre del Proceso o entidad (tabla) que genera el rc', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceipts', @level2type = N'COLUMN', @level2name = N'EntityName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Responsables de pago terceros', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceipts', @level2type = N'COLUMN', @level2name = N'PaymentResponsiblesThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceipts', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la fecha en que se reverso el documento', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceipts', @level2type = N'COLUMN', @level2name = N'ReversedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Usuario que reverso el documento', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceipts', @level2type = N'COLUMN', @level2name = N'ReversedUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Anulación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceipts', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Anulación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceipts', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de confirmación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceipts', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Confirmación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceipts', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceipts', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceipts', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceipts', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceipts', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado (registrado = 1,confirmado = 2,anulado = 3,reversado = 4)', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceipts', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'id de la unidad operativa', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceipts', @level2type = N'COLUMN', @level2name = N'OperatingUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del reembolso', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceipts', @level2type = N'COLUMN', @level2name = N'IdRefund';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'valor del documento', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceipts', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'responsable del pago', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceipts', @level2type = N'COLUMN', @level2name = N'PaymentResponsibles';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id cuenta bancaria', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceipts', @level2type = N'COLUMN', @level2name = N'IdBankAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la caja', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceipts', @level2type = N'COLUMN', @level2name = N'IdCashRegister';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha del documento', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceipts', @level2type = N'COLUMN', @level2name = N'DocumentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Detalle', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceipts', @level2type = N'COLUMN', @level2name = N'Detail';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del centro de costo', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceipts', @level2type = N'COLUMN', @level2name = N'IdCostCenter';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuenta contable', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceipts', @level2type = N'COLUMN', @level2name = N'IdMainAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de recaudo: 1 = Caja, 2 = Bancos', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceipts', @level2type = N'COLUMN', @level2name = N'CollectType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del tercero', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceipts', @level2type = N'COLUMN', @level2name = N'IdThirdParty';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del recibo', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceipts', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del registro', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceipts', @level2type = N'COLUMN', @level2name = N'Id';

