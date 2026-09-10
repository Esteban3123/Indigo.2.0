CREATE TABLE [Payments].[AccountPayableConceptNotes] (
    [Id]                             INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                           VARCHAR (20)  NOT NULL,
    [Name]                           VARCHAR (100) NOT NULL,
    [ConceptType]                    TINYINT       NOT NULL,
    [IdAccount]                      INT           NULL,
    [AffectBudget]                   BIT           NOT NULL,
    [Behavior]                       TINYINT       NULL,
    [Status]                         BIT           CONSTRAINT [DF_PaymentsNoteConcept_Status] DEFAULT ((1)) NOT NULL,
    [CreationUser]                   VARCHAR (20)  NOT NULL,
    [CreationDate]                   DATETIME      NOT NULL,
    [ModificationUser]               VARCHAR (20)  NULL,
    [ModificationDate]               DATETIME      NULL,
    [TimeStamp]                      ROWVERSION    NOT NULL,
    [ManageRetention]                BIT           CONSTRAINT [DF__AccountPa__Manag__4897016B] DEFAULT ((0)) NOT NULL,
    [ManageTax]                      BIT           CONSTRAINT [DF__AccountPa__Manag__498B25A4] DEFAULT ((0)) NOT NULL,
    [TypeCategoryIndependentWorkers] BIT           CONSTRAINT [DF_AccountPayableConceptNotes_TypeCategoryIndependentWorkers] DEFAULT ((0)) NOT NULL,
    [RetentionConcept383Id]          INT           NULL,
    [RetentionMainAccount383Id]      INT           NULL,
    [RetentionConceptId]             INT           NULL,
    CONSTRAINT [PK_AccountPayableConceptNotes__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AccountPayableConceptNotes_RetentionConcept383Id] FOREIGN KEY ([RetentionConcept383Id]) REFERENCES [GeneralLedger].[RetentionConcepts] ([Id]),
    CONSTRAINT [FK_AccountPayableConceptNotes_RetentionConceptId] FOREIGN KEY ([RetentionConceptId]) REFERENCES [GeneralLedger].[RetentionConcepts] ([Id]),
    CONSTRAINT [FK_AccountPayableConceptNotes_RetentionMainAccount383Id] FOREIGN KEY ([RetentionMainAccount383Id]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_PaymentsNoteConcept_PUC] FOREIGN KEY ([IdAccount]) REFERENCES [GeneralLedger].[MainAccounts] ([Id])
);




GO



GO



GO



GO



GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_AccountPayableConceptNotes__Code]
    ON [Payments].[AccountPayableConceptNotes]([Code] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del concepto de retención vinculado (FK a RetentionConcepts). Usado para gestionar retenciones fiscales en notas contables. INT, nullable.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConceptNotes', @level2type = N'COLUMN', @level2name = N'RetentionConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Concepto de Retención', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConceptNotes', @level2type = N'COLUMN', @level2name = N'RetentionConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConceptNotes', @level2type = N'COLUMN', @level2name = N'RetentionConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cuenta contable principal para retención categoría 383 (FK a MainAccounts). Referencia contable para impuestos retenidos. INT, nullable.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConceptNotes', @level2type = N'COLUMN', @level2name = N'RetentionMainAccount383Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuenta contable Retención 383', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConceptNotes', @level2type = N'COLUMN', @level2name = N'RetentionMainAccount383Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConceptNotes', @level2type = N'COLUMN', @level2name = N'RetentionMainAccount383Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del concepto de retención categoría 383 (FK a RetentionConcepts). Especifica la retención fiscal aplicable según norma DIAN. INT, nullable.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConceptNotes', @level2type = N'COLUMN', @level2name = N'RetentionConcept383Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Concepto Retención 383', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConceptNotes', @level2type = N'COLUMN', @level2name = N'RetentionConcept383Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConceptNotes', @level2type = N'COLUMN', @level2name = N'RetentionConcept383Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (0/1) que señala si el concepto aplica a trabajadores independientes, autónomos o contratistas. BIT, default 0.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConceptNotes', @level2type = N'COLUMN', @level2name = N'TypeCategoryIndependentWorkers';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo categoria trabajadores independientes', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConceptNotes', @level2type = N'COLUMN', @level2name = N'TypeCategoryIndependentWorkers';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConceptNotes', @level2type = N'COLUMN', @level2name = N'TypeCategoryIndependentWorkers';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera booleano (0/1) que controla si este concepto de nota gestiona impuestos, retenciones o tributos en cuentas por pagar. BIT, default 0.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConceptNotes', @level2type = N'COLUMN', @level2name = N'ManageTax';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Gestionar impuestos', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConceptNotes', @level2type = N'COLUMN', @level2name = N'ManageTax';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConceptNotes', @level2type = N'COLUMN', @level2name = N'ManageTax';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera booleano (0/1) que indica si el concepto maneja retención en la fuente u otras retenciones fiscales. BIT, default 0.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConceptNotes', @level2type = N'COLUMN', @level2name = N'ManageRetention';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Gestionar retención', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConceptNotes', @level2type = N'COLUMN', @level2name = N'ManageRetention';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConceptNotes', @level2type = N'COLUMN', @level2name = N'ManageRetention';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal automática (TIMESTAMP) que captura el instante exacto de creación, modificación o evento del registro en cuentas por pagar. Sincroniza cambios en BD.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConceptNotes', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConceptNotes', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConceptNotes', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) del último cambio registrado en el concepto de nota contable. NULL si no hay modificaciones posteriores a creación.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConceptNotes', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConceptNotes', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConceptNotes', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (VARCHAR 20) del usuario que realizó la última modificación del concepto. NULL si el registro no ha sido editado.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConceptNotes', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConceptNotes', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConceptNotes', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) en que se creó el concepto de nota en cuentas por pagar. Requerido, no nulo.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConceptNotes', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConceptNotes', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConceptNotes', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (VARCHAR 20) del usuario que creó el concepto de nota contable. Requerido para auditoría. No nulo.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConceptNotes', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConceptNotes', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConceptNotes', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del concepto: 1=Activo, 0=Inactivo. BIT, default 1. Controla disponibilidad en creación de notas de débito/crédito.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConceptNotes', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del registro: True-Activo, False-Inactivo', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConceptNotes', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConceptNotes', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Comportamiento del concepto (TINYINT nullable): 0=No aplica, 1=Liberar Recursos. Define acción contable al aplicar la nota.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConceptNotes', @level2type = N'COLUMN', @level2name = N'Behavior';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Comportamiento(0 = No Aplica, 1 = Liberar Recursos)', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConceptNotes', @level2type = N'COLUMN', @level2name = N'Behavior';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConceptNotes', @level2type = N'COLUMN', @level2name = N'Behavior';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera booleano (0/1) que determina si el concepto de nota afecta presupuesto, asignaciones o compromisos presupuestales. BIT, requerido.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConceptNotes', @level2type = N'COLUMN', @level2name = N'AffectBudget';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Afecta Presupuesto', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConceptNotes', @level2type = N'COLUMN', @level2name = N'AffectBudget';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConceptNotes', @level2type = N'COLUMN', @level2name = N'AffectBudget';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) de la cuenta contable del plan de cuentas asociada (FK a MainAccounts). Requerido si ConceptType=Especifico. Nullable para ConceptType=General.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConceptNotes', @level2type = N'COLUMN', @level2name = N'IdAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta contable', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConceptNotes', @level2type = N'COLUMN', @level2name = N'IdAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConceptNotes', @level2type = N'COLUMN', @level2name = N'IdAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de concepto (TINYINT): 1=General (sin cuenta obligatoria al crear concepto), 2=Especifico (exige cuenta contable). Define flujo de captura en notas.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConceptNotes', @level2type = N'COLUMN', @level2name = N'ConceptType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el tipo de concepto  1 - General  2 - Especifico    Cuando es general no se pide la cuenta contable cuando se esten creando el concepto de nota, pero si se pide cuando se este creando la nota    Cuando es especifico se solicita la cuenta contable cuando se crea el concepto y solo se muestra como ReadOnly cuando se esta creando la nota', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConceptNotes', @level2type = N'COLUMN', @level2name = N'ConceptType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConceptNotes', @level2type = N'COLUMN', @level2name = N'ConceptType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre descriptivo (VARCHAR 100) del concepto de nota contable. Ej: ''''Descuento comercial'''', ''''Ajuste por glosa''''. Visible en cuentas por pagar.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConceptNotes', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del concepto', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConceptNotes', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConceptNotes', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único (VARCHAR 20) que identifica el concepto de nota. Usado para referencia rápida en procesos de débito/crédito. Requerido, no nulo.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConceptNotes', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del concepto', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConceptNotes', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConceptNotes', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincremental (INT IDENTITY) del registro de concepto de nota contable. Clave primaria clustered. No replicable.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConceptNotes', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del registro', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConceptNotes', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConceptNotes', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Conceptos de notas de cuentas por pagar: registra los tipos de conceptos (débitos, créditos, ajustes) que se pueden aplicar como notas a las cuentas por pagar de proveedores, incluyendo su comportamiento contable, manejo de retenciones e impuestos.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConceptNotes';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConceptNotes';
