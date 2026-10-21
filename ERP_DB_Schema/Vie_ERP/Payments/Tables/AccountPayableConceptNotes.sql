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
    [CatalogOfPropertyandServicesId] INT           NULL,
    CONSTRAINT [PK_AccountPayableConceptNotes__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AccountPayableConceptNotes_RetentionConcept383Id] FOREIGN KEY ([RetentionConcept383Id]) REFERENCES [GeneralLedger].[RetentionConcepts] ([Id]),
    CONSTRAINT [FK_AccountPayableConceptNotes_RetentionConceptId] FOREIGN KEY ([RetentionConceptId]) REFERENCES [GeneralLedger].[RetentionConcepts] ([Id]),
    CONSTRAINT [FK_AccountPayableConceptNotes_RetentionMainAccount383Id] FOREIGN KEY ([RetentionMainAccount383Id]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_PaymentsNoteConcept_PUC] FOREIGN KEY ([IdAccount]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_AccountPayableConceptNotes_FixedAssetCatalogOfPropertyandServices] FOREIGN KEY ([CatalogOfPropertyandServicesId]) REFERENCES [FixedAsset].[FixedAssetCatalogOfPropertyandServices] ([Id])
);


GO
ALTER TABLE [Payments].[AccountPayableConceptNotes] NOCHECK CONSTRAINT [FK_AccountPayableConceptNotes_RetentionConcept383Id];


GO
ALTER TABLE [Payments].[AccountPayableConceptNotes] NOCHECK CONSTRAINT [FK_AccountPayableConceptNotes_RetentionConceptId];


GO
ALTER TABLE [Payments].[AccountPayableConceptNotes] NOCHECK CONSTRAINT [FK_AccountPayableConceptNotes_RetentionMainAccount383Id];


GO
ALTER TABLE [Payments].[AccountPayableConceptNotes] NOCHECK CONSTRAINT [FK_PaymentsNoteConcept_PUC];


GO
ALTER TABLE [Payments].[AccountPayableConceptNotes] NOCHECK CONSTRAINT [FK_AccountPayableConceptNotes_FixedAssetCatalogOfPropertyandServices];


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_AccountPayableConceptNotes__Code]
    ON [Payments].[AccountPayableConceptNotes]([Code] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del catalogo de bienes y servicios. Aplica cuando el tenant tiene habilitado Aplica Catalogo de Bienes y Servicios y el tipo de concepto es Especifico.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConceptNotes', @level2type = N'COLUMN', @level2name = N'CatalogOfPropertyandServicesId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Concepto de Retención', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConceptNotes', @level2type = N'COLUMN', @level2name = N'RetentionConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuenta contable Retención 383', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConceptNotes', @level2type = N'COLUMN', @level2name = N'RetentionMainAccount383Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Concepto Retención 383', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConceptNotes', @level2type = N'COLUMN', @level2name = N'RetentionConcept383Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo categoria trabajadores independientes', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConceptNotes', @level2type = N'COLUMN', @level2name = N'TypeCategoryIndependentWorkers';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Gestionar impuestos', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConceptNotes', @level2type = N'COLUMN', @level2name = N'ManageTax';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Gestionar retención', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConceptNotes', @level2type = N'COLUMN', @level2name = N'ManageRetention';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConceptNotes', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConceptNotes', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConceptNotes', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConceptNotes', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConceptNotes', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del registro: True-Activo, False-Inactivo', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConceptNotes', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Comportamiento(0 = No Aplica, 1 = Liberar Recursos)', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConceptNotes', @level2type = N'COLUMN', @level2name = N'Behavior';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Afecta Presupuesto', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConceptNotes', @level2type = N'COLUMN', @level2name = N'AffectBudget';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuenta contable', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConceptNotes', @level2type = N'COLUMN', @level2name = N'IdAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el tipo de concepto  1 - General  2 - Especifico    Cuando es general no se pide la cuenta contable cuando se esten creando el concepto de nota, pero si se pide cuando se este creando la nota    Cuando es especifico se solicita la cuenta contable cuando se crea el concepto y solo se muestra como ReadOnly cuando se esta creando la nota', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConceptNotes', @level2type = N'COLUMN', @level2name = N'ConceptType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del concepto', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConceptNotes', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del concepto', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConceptNotes', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del registro', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConceptNotes', @level2type = N'COLUMN', @level2name = N'Id';

