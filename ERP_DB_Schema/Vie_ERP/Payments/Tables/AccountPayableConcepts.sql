CREATE TABLE [Payments].[AccountPayableConcepts] (
    [Id]                                INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                              VARCHAR (20)  NOT NULL,
    [Name]                              VARCHAR (100) NULL,
    [ConceptType]                       TINYINT       CONSTRAINT [DF_AccountPayableConcepts_NoteType] DEFAULT ((2)) NOT NULL,
    [IdAccount]                         INT           NULL,
    [HandlesRetention]                  BIT           CONSTRAINT [DF_PaymentsConcept_IvaConcept] DEFAULT ((0)) NOT NULL,
    [EmployeeCategoryRetention]         BIT           CONSTRAINT [DF_AccountPayableConcepts_EmployeeCategoryRetention] DEFAULT ((0)) NOT NULL,
    [ThreeEightThreeRetentionConceptId] INT           NULL,
    [ThreeEightThreeAccountId]          INT           NULL,
    [ThreeEightFourRetentionConceptId]  INT           NULL,
    [ThreeEightFourAccountId]           INT           NULL,
    [RetentionConceptId]                INT           NULL,
    [DeferredCausation]                 BIT           NOT NULL,
    [AccumulatedBudgetCalculation]      BIT           NOT NULL,
    [FreeResourcesUnexecuted]           BIT           NOT NULL,
    [Status]                            BIT           CONSTRAINT [DF_PaymentsConcept_Status] DEFAULT ((1)) NOT NULL,
    [CreationUser]                      VARCHAR (20)  NOT NULL,
    [CreationDate]                      DATETIME      NOT NULL,
    [ModificationUser]                  VARCHAR (20)  NULL,
    [ModificationDate]                  DATETIME      NULL,
    [TimeStamp]                         ROWVERSION    NOT NULL,
    [HandleTaxes]                       BIT           CONSTRAINT [DF__AccountPa__Handl__1067B6E1] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_AccountPayableConcepts__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AccountPayableConcepts_MainAccounts] FOREIGN KEY ([IdAccount]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_AccountPayableConcepts_MainAccounts383] FOREIGN KEY ([ThreeEightThreeAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_AccountPayableConcepts_MainAccounts384] FOREIGN KEY ([ThreeEightFourAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_AccountPayableConcepts_RetentionConcepts] FOREIGN KEY ([RetentionConceptId]) REFERENCES [GeneralLedger].[RetentionConcepts] ([Id]),
    CONSTRAINT [FK_AccountPayableConcepts_RetentionConcepts1] FOREIGN KEY ([ThreeEightThreeRetentionConceptId]) REFERENCES [GeneralLedger].[RetentionConcepts] ([Id]),
    CONSTRAINT [FK_AccountPayableConcepts_RetentionConcepts2] FOREIGN KEY ([ThreeEightFourRetentionConceptId]) REFERENCES [GeneralLedger].[RetentionConcepts] ([Id])
);


GO
ALTER TABLE [Payments].[AccountPayableConcepts] NOCHECK CONSTRAINT [FK_AccountPayableConcepts_MainAccounts];


GO
ALTER TABLE [Payments].[AccountPayableConcepts] NOCHECK CONSTRAINT [FK_AccountPayableConcepts_MainAccounts383];


GO
ALTER TABLE [Payments].[AccountPayableConcepts] NOCHECK CONSTRAINT [FK_AccountPayableConcepts_MainAccounts384];


GO
ALTER TABLE [Payments].[AccountPayableConcepts] NOCHECK CONSTRAINT [FK_AccountPayableConcepts_RetentionConcepts];


GO
ALTER TABLE [Payments].[AccountPayableConcepts] NOCHECK CONSTRAINT [FK_AccountPayableConcepts_RetentionConcepts1];


GO
ALTER TABLE [Payments].[AccountPayableConcepts] NOCHECK CONSTRAINT [FK_AccountPayableConcepts_RetentionConcepts2];


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_AccountPayableConcepts__Code]
    ON [Payments].[AccountPayableConcepts]([Code] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Maneja Impuestos, campo boolean para definir si manjea o no impuestos, por defecto es No', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConcepts', @level2type = N'COLUMN', @level2name = N'HandleTaxes';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConcepts', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConcepts', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConcepts', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConcepts', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConcepts', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del registro: True-Activo, False-Inactivo', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConcepts', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Define si es para liberar recursos no ejecutados', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConcepts', @level2type = N'COLUMN', @level2name = N'FreeResourcesUnexecuted';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Definie si acumula para el calculo presupuestal', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConcepts', @level2type = N'COLUMN', @level2name = N'AccumulatedBudgetCalculation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si el concepto maneja Causacion Diferida', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConcepts', @level2type = N'COLUMN', @level2name = N'DeferredCausation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del concepto de retencion, este se llena solo si el concepto Maneja Retencion (HandlesRetention) y si NO es una retencion de categoria empleado', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConcepts', @level2type = N'COLUMN', @level2name = N'RetentionConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica Id de la cuenta contable que se debe afectar cuuando se cobre el concepto 384', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConcepts', @level2type = N'COLUMN', @level2name = N'ThreeEightFourAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del concepto  de retencion 384, este concepto de retencion debe de ser por Rangos, Solo se llena si es una retencion de categoria a empleados', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConcepts', @level2type = N'COLUMN', @level2name = N'ThreeEightFourRetentionConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica Id de la cuenta contable que se debe afectar cuuando se cobre el concepto 383', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConcepts', @level2type = N'COLUMN', @level2name = N'ThreeEightThreeAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del concepto  de retencion 383, este concepto de retencion debe de ser por Rangos, Solo se llena si es una retencion de categoria a empleados', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConcepts', @level2type = N'COLUMN', @level2name = N'ThreeEightThreeRetentionConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si la retencion es de tipo Categoria a empleados', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConcepts', @level2type = N'COLUMN', @level2name = N'EmployeeCategoryRetention';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si el concepto maneja retencion', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConcepts', @level2type = N'COLUMN', @level2name = N'HandlesRetention';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuenta contable solo se solicita si el tipo del concepto es especifico', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConcepts', @level2type = N'COLUMN', @level2name = N'IdAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el tipo de concepto   1  - General  2 - Especifico    Cuando es general no se solicita la cuenta contable pero si se solicita cuando esten creando la cuenta por pagar    Cuando es especifico si se obliga a que seleccionen una cuenta contable', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConcepts', @level2type = N'COLUMN', @level2name = N'ConceptType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del concepto', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConcepts', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del concepto', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConcepts', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del registro', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConcepts', @level2type = N'COLUMN', @level2name = N'Id';

