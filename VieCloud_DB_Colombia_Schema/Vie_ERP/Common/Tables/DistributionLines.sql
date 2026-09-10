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



GO



GO



GO



GO



GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_DistributionLines__Code]
    ON [Common].[DistributionLines]([Code] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK → GeneralLedger.MainAccounts) de la cuenta contable principal para provisión o acumulación de costos; vinculado a generación de provisiones contables.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DistributionLines', @level2type = N'COLUMN', @level2name = N'MainAccountCostProvisionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta principal de provisión de costos.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DistributionLines', @level2type = N'COLUMN', @level2name = N'MainAccountCostProvisionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DistributionLines', @level2type = N'COLUMN', @level2name = N'MainAccountCostProvisionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK → GeneralLedger.MainAccounts) de la cuenta contable asociada al concepto de nota CXP (Cuentas por Pagar); mapea notas contables de pasivos.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DistributionLines', @level2type = N'COLUMN', @level2name = N'MainAccountAccountPayableConceptNotesId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuenta contable del concepto de nota CXP', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DistributionLines', @level2type = N'COLUMN', @level2name = N'MainAccountAccountPayableConceptNotesId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DistributionLines', @level2type = N'COLUMN', @level2name = N'MainAccountAccountPayableConceptNotesId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK → Payments.AccountPayableConceptNotes) del concepto o nota de CXP (Cuentas por Pagar); categoriza acreencias, obligaciones de pago y pasivos.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DistributionLines', @level2type = N'COLUMN', @level2name = N'AccountPayableConceptNotesId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Concepto Nota CXP', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DistributionLines', @level2type = N'COLUMN', @level2name = N'AccountPayableConceptNotesId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DistributionLines', @level2type = N'COLUMN', @level2name = N'AccountPayableConceptNotesId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación TINYINT (1=Precio Transacción/Nominal/Costo, 2=Costo Amortizado, 3=Valor Razonable, 4=Valor Razonable ORI, 5=Valor Presente Futuros, 6=No aplica) de medición posterior del instrumento financiero según NIIF.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DistributionLines', @level2type = N'COLUMN', @level2name = N'FinancialInstrument';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Medición posterior del instrumento financiero  1 = Precio de la Transacción / Valor Nominal / Costo  2 = Costo Amortizado  3 = Valor Razonable  4 = Valor Razonable con cambios en el ORI  5 = Valor Presente Pagos Futuros  6 = No aplica', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DistributionLines', @level2type = N'COLUMN', @level2name = N'FinancialInstrument';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DistributionLines', @level2type = N'COLUMN', @level2name = N'FinancialInstrument';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación TINYINT (1=Prestación salud, 2=Insumos/medicamentos, 3=Dispositivo médico/biomédico, 4=Administrativo, 5=Restitución recursos, 6=Otro) del concepto de acreencia o imputación.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DistributionLines', @level2type = N'COLUMN', @level2name = N'AccusationConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Concepto de Acreencia  1 = Prestación de servicios de salud  2 = Insumos y medicamentos  3 = Dispositivo médico o equipo biomédico  4 = Administrativo  5 = Restitución de recursos  6 = Otro  ', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DistributionLines', @level2type = N'COLUMN', @level2name = N'AccusationConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DistributionLines', @level2type = N'COLUMN', @level2name = N'AccusationConcept';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Timestamp (ROWVERSION) automático de control de cambios; marca temporal de creación, modificación o evento en el registro de la línea de distribución.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DistributionLines', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DistributionLines', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DistributionLines', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME, nullable) de última modificación del registro; auditoría de cambios en la línea de distribución.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DistributionLines', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DistributionLines', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DistributionLines', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario (VARCHAR 20, nullable) que realizó la última modificación; trazabilidad de cambios en la línea de distribución.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DistributionLines', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DistributionLines', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DistributionLines', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de creación del registro; generada automáticamente por Common.getdate().', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DistributionLines', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DistributionLines', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DistributionLines', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario (VARCHAR 20) que creó el registro; por defecto 999 para auditoría y trazabilidad inicial.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DistributionLines', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DistributionLines', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DistributionLines', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado (BIT: 1=Activo, 0=Inactivo) de la línea de distribución; controla disponibilidad y vigencia.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DistributionLines', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado de la linea', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DistributionLines', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DistributionLines', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK → Treasury.ExpenseConcepts) del concepto de egreso o gasto; categoriza naturaleza del costo o gasto contable.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DistributionLines', @level2type = N'COLUMN', @level2name = N'ExpensesConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del concepto de egreso', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DistributionLines', @level2type = N'COLUMN', @level2name = N'ExpensesConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DistributionLines', @level2type = N'COLUMN', @level2name = N'ExpensesConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK → GeneralLedger.MainAccounts) de la cuenta contable principal o de mayor; vinculación directa a plan de cuentas.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DistributionLines', @level2type = N'COLUMN', @level2name = N'IdMainAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta contable', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DistributionLines', @level2type = N'COLUMN', @level2name = N'IdMainAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DistributionLines', @level2type = N'COLUMN', @level2name = N'IdMainAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción detallada (VARCHAR 500, nullable, default=''''-'''') de la línea de distribución; notas explicativas de propósito o uso.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DistributionLines', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripción de la linea', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DistributionLines', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DistributionLines', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre (VARCHAR 100) de la línea de distribución; identificador amigable para búsqueda y reporte.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DistributionLines', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de la linea', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DistributionLines', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DistributionLines', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único (VARCHAR 20) de la línea de distribución; clave de negocio para identificación rápida.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DistributionLines', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de la linea', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DistributionLines', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DistributionLines', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT, IDENTITY, PK) de cada registro de línea de distribución; clave primaria secuencial.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DistributionLines', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del registro', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DistributionLines', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DistributionLines', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Líneas de distribución contable utilizadas para clasificar y asignar costos o gastos a cuentas contables principales. Define los conceptos de imputación, instrumentos financieros y cuentas asociadas a cada línea de distribución presupuestal o contable.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DistributionLines';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DistributionLines';
