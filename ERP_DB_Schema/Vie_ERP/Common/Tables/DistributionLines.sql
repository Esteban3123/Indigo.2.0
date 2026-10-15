CREATE TABLE [Common].[DistributionLines] (
    [Id]                                      INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                                    VARCHAR (20)  NOT NULL,
    [Name]                                    VARCHAR (100) NOT NULL,
    [Description]                             VARCHAR (500) CONSTRAINT [DF_DistributionLines_Description] DEFAULT ('-') NULL,
    [IdMainAccount]                           INT           NOT NULL,
    [ExpensesConceptId]                       INT           CONSTRAINT [DF_DistributionLines_ExpensesConceptId] DEFAULT ((1)) NOT NULL,
    [Status]                                  BIT           CONSTRAINT [DF_DistributionLines_Status] DEFAULT ((1)) NOT NULL,
    [CreationUser]                            VARCHAR (20)  CONSTRAINT [DF_DistributionLines_CreationUser] DEFAULT ((999)) NOT NULL,
    [CreationDate]                            DATETIME      CONSTRAINT [DF_DistributionLines_CreationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [ModificationUser]                        VARCHAR (20)  NULL,
    [ModificationDate]                        DATETIME      NULL,
    [TimeStamp]                               ROWVERSION    NOT NULL,
    [AccusationConcept]                       TINYINT       CONSTRAINT [DF_DistributionLines_AccusationConcept] DEFAULT ((6)) NOT NULL,
    [FinancialInstrument]                     TINYINT       CONSTRAINT [DF_DistributionLines_FinancialInstrument] DEFAULT ((6)) NOT NULL,
    [AccountPayableConceptNotesId]            INT           CONSTRAINT [DF__Distribut__Accou__65BC84A7] DEFAULT ((1)) NOT NULL,
    [MainAccountAccountPayableConceptNotesId] INT           NULL,
    [MainAccountCostProvisionId]              INT           NULL,
    CONSTRAINT [PK_DistributionLines__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DistributionLines_AccountPayableConceptNotesId] FOREIGN KEY ([AccountPayableConceptNotesId]) REFERENCES [Payments].[AccountPayableConceptNotes] ([Id]),
    CONSTRAINT [FK_DistributionLines_ExpenseConcepts] FOREIGN KEY ([ExpensesConceptId]) REFERENCES [Treasury].[ExpenseConcepts] ([Id]),
    CONSTRAINT [FK_DistributionLines_MainAccountCostProvision] FOREIGN KEY ([MainAccountCostProvisionId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_DistributionLines_MainAccounts] FOREIGN KEY ([IdMainAccount]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_DistributionLines_MainAccounts_ConceptNote] FOREIGN KEY ([MainAccountAccountPayableConceptNotesId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id])
);


GO
ALTER TABLE [Common].[DistributionLines] NOCHECK CONSTRAINT [FK_DistributionLines_AccountPayableConceptNotesId];


GO
ALTER TABLE [Common].[DistributionLines] NOCHECK CONSTRAINT [FK_DistributionLines_ExpenseConcepts];


GO
ALTER TABLE [Common].[DistributionLines] NOCHECK CONSTRAINT [FK_DistributionLines_MainAccountCostProvision];


GO
ALTER TABLE [Common].[DistributionLines] NOCHECK CONSTRAINT [FK_DistributionLines_MainAccounts];


GO
ALTER TABLE [Common].[DistributionLines] NOCHECK CONSTRAINT [FK_DistributionLines_MainAccounts_ConceptNote];


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_DistributionLines__Code]
    ON [Common].[DistributionLines]([Code] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la cuenta principal de provisión de costos.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DistributionLines', @level2type = N'COLUMN', @level2name = N'MainAccountCostProvisionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuenta contable del concepto de nota CXP', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DistributionLines', @level2type = N'COLUMN', @level2name = N'MainAccountAccountPayableConceptNotesId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Concepto Nota CXP', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DistributionLines', @level2type = N'COLUMN', @level2name = N'AccountPayableConceptNotesId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Medición posterior del instrumento financiero  1 = Precio de la Transacción / Valor Nominal / Costo  2 = Costo Amortizado  3 = Valor Razonable  4 = Valor Razonable con cambios en el ORI  5 = Valor Presente Pagos Futuros  6 = No aplica', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DistributionLines', @level2type = N'COLUMN', @level2name = N'FinancialInstrument';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Concepto de Acreencia  1 = Prestación de servicios de salud  2 = Insumos y medicamentos  3 = Dispositivo médico o equipo biomédico  4 = Administrativo  5 = Restitución de recursos  6 = Otro  ', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DistributionLines', @level2type = N'COLUMN', @level2name = N'AccusationConcept';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DistributionLines', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DistributionLines', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DistributionLines', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DistributionLines', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DistributionLines', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado de la linea', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DistributionLines', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del concepto de egreso', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DistributionLines', @level2type = N'COLUMN', @level2name = N'ExpensesConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuenta contable', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DistributionLines', @level2type = N'COLUMN', @level2name = N'IdMainAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripción de la linea', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DistributionLines', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre de la linea', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DistributionLines', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de la linea', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DistributionLines', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del registro', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DistributionLines', @level2type = N'COLUMN', @level2name = N'Id';

