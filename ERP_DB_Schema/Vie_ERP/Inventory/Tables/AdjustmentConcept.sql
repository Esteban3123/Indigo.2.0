CREATE TABLE [Inventory].[AdjustmentConcept] (
    [Id]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                VARCHAR (20)  NOT NULL,
    [Name]                VARCHAR (100) NOT NULL,
    [MovementClass]       TINYINT       NOT NULL,
    [ConceptType]         TINYINT       NOT NULL,
    [AffectsAverageCost]  BIT           NOT NULL,
    [ShowExpiredProduct]  BIT           NOT NULL,
    [AdjustmentAccountId] INT           NOT NULL,
    [CostCenterId]        INT           NULL,
    [IvaAffects]          BIT           NOT NULL,
    [IvaAccountId]        INT           NULL,
    [Status]              BIT           NOT NULL,
    [CreationUser]        VARCHAR (20)  CONSTRAINT [DF_AdjustmentConcept_CreationUser] DEFAULT ((999)) NOT NULL,
    [CreationDate]        DATETIME      CONSTRAINT [DF_AdjustmentConcept_CreationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [ModificationUser]    VARCHAR (20)  NULL,
    [ModificationDate]    DATETIME      NULL,
    CONSTRAINT [PK_AdjustmentConcept__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AdjustmentConcept_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_AdjustmentConcept_MainAccounts] FOREIGN KEY ([AdjustmentAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_AdjustmentConcept_MainAccounts1] FOREIGN KEY ([IvaAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id])
);


GO
ALTER TABLE [Inventory].[AdjustmentConcept] NOCHECK CONSTRAINT [FK_AdjustmentConcept_CostCenter];


GO
ALTER TABLE [Inventory].[AdjustmentConcept] NOCHECK CONSTRAINT [FK_AdjustmentConcept_MainAccounts];


GO
ALTER TABLE [Inventory].[AdjustmentConcept] NOCHECK CONSTRAINT [FK_AdjustmentConcept_MainAccounts1];


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_AdjustmentConcept__Code]
    ON [Inventory].[AdjustmentConcept]([Code] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustmentConcept', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustmentConcept', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustmentConcept', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustmentConcept', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del Documento  1 - Activado  0 - Inactivo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustmentConcept', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Si afecta iva este campo se llena, este especifica la cuenta contable que afecta Iva', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustmentConcept', @level2type = N'COLUMN', @level2name = N'IvaAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si afecta Iva, solo se llena si el tipo de concepto es de salida', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustmentConcept', @level2type = N'COLUMN', @level2name = N'IvaAffects';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Si la cuenta de ajuste maneja centro de costo el frontal se lo debe solicitar', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustmentConcept', @level2type = N'COLUMN', @level2name = N'CostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuenta contable de ajuste ', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustmentConcept', @level2type = N'COLUMN', @level2name = N'AdjustmentAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Mostrar productos vencidos', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustmentConcept', @level2type = N'COLUMN', @level2name = N'ShowExpiredProduct';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Afecta costo promedio', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustmentConcept', @level2type = N'COLUMN', @level2name = N'AffectsAverageCost';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de concepto 1 - Entrada 2 - Salida    Nota : Cuando la clase del movimiento es Traslado el concepto de ajuste siempre es de salida, asiq ue no hay que mostrarlo en el formulario', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustmentConcept', @level2type = N'COLUMN', @level2name = N'ConceptType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la clase del movimiento  1 - Ajuste de inventario   2 - Traslado por consumo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustmentConcept', @level2type = N'COLUMN', @level2name = N'MovementClass';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del concepto de ajuste', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustmentConcept', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del concepto de ajuste', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustmentConcept', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del concepto de ajuste', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustmentConcept', @level2type = N'COLUMN', @level2name = N'Id';

