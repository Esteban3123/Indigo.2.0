CREATE TABLE [Treasury].[ExpenseConcepts] (
    [Id]                    INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                  VARCHAR (20)  NOT NULL,
    [Description]           VARCHAR (255) NOT NULL,
    [Nature]                TINYINT       NOT NULL,
    [IdMainAccount]         INT           NOT NULL,
    [AffectBudget]          BIT           CONSTRAINT [DF_ExpenseConcept_AffectBudget] DEFAULT ((0)) NOT NULL,
    [Behavior]              TINYINT       NOT NULL,
    [Status]                BIT           CONSTRAINT [DF_ExpenseConcept_Status] DEFAULT ((1)) NOT NULL,
    [CreationUser]          VARCHAR (20)  NOT NULL,
    [CreationDate]          DATETIME      NOT NULL,
    [ModificationUser]      VARCHAR (20)  NULL,
    [ModificationDate]      DATETIME      NULL,
    [TimeStamp]             ROWVERSION    NOT NULL,
    [AffectCashFlowConcept] BIT           NULL,
    [IdCashFlowConcept]     INT           NULL,
    [TaxManagement]         BIT           CONSTRAINT [DF__ExpenseCo__TaxMa__7238357C] DEFAULT ((0)) NOT NULL,
    [BuyOrderManagement]    BIT           CONSTRAINT [DF_ExpenseConcepts_BuyOrderManagement] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_ExpenseConcepts__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ExpenseConcept_MainAccount] FOREIGN KEY ([IdMainAccount]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_ExpenseConcepts_IdCashFlowConcept] FOREIGN KEY ([IdCashFlowConcept]) REFERENCES [Treasury].[CashFlowConcept] ([Id])
);


GO
ALTER TABLE [Treasury].[ExpenseConcepts] NOCHECK CONSTRAINT [FK_ExpenseConcept_MainAccount];


GO
ALTER TABLE [Treasury].[ExpenseConcepts] NOCHECK CONSTRAINT [FK_ExpenseConcepts_IdCashFlowConcept];


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_ExpenseConcepts__Code]
    ON [Treasury].[ExpenseConcepts]([Code] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo booleano que indica si maneja orden de compra', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'ExpenseConcepts', @level2type = N'COLUMN', @level2name = N'BuyOrderManagement';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo booleano que especifica si hay gestión tributaria.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'ExpenseConcepts', @level2type = N'COLUMN', @level2name = N'TaxManagement';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de concepto de flujo de efectivo', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'ExpenseConcepts', @level2type = N'COLUMN', @level2name = N'IdCashFlowConcept';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica si afecta o no concepto de flujo de efectivo', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'ExpenseConcepts', @level2type = N'COLUMN', @level2name = N'AffectCashFlowConcept';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sello de tiempo. Guarda el instante tiempo de la creación, registro o modificación de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'ExpenseConcepts', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'ExpenseConcepts', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'ExpenseConcepts', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'ExpenseConcepts', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'ExpenseConcepts', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del registro: True-Activo, False-Inactivo', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'ExpenseConcepts', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Comportamiento:   2 - Caja Menor;   3 - Pago/Anticipo de Facturas CxP;   4 - Devolutivos de Anticipos RC;   6 - Ninguno', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'ExpenseConcepts', @level2type = N'COLUMN', @level2name = N'Behavior';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Afecta presupuesto', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'ExpenseConcepts', @level2type = N'COLUMN', @level2name = N'AffectBudget';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuenta contable', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'ExpenseConcepts', @level2type = N'COLUMN', @level2name = N'IdMainAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Naturaleza contable: 1-Debito, 2-Credito', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'ExpenseConcepts', @level2type = N'COLUMN', @level2name = N'Nature';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripción del concepto', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'ExpenseConcepts', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del concepto', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'ExpenseConcepts', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del registro', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'ExpenseConcepts', @level2type = N'COLUMN', @level2name = N'Id';

