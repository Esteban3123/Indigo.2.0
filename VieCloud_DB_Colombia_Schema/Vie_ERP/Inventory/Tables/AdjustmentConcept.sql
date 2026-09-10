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



GO



GO



GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_AdjustmentConcept__Code]
    ON [Inventory].[AdjustmentConcept]([Code] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de última modificación del concepto de ajuste; DATETIME, auditoria de cambios', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustmentConcept', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustmentConcept', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustmentConcept', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que realizó la última modificación; VARCHAR(20), trazabilidad de cambios en el concepto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustmentConcept', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustmentConcept', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustmentConcept', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del concepto de ajuste; DATETIME, registro de auditoría', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustmentConcept', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustmentConcept', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustmentConcept', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que creó el concepto de ajuste; VARCHAR(20), trazabilidad de origen', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustmentConcept', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustmentConcept', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustmentConcept', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado activo/inactivo del concepto (1=Activado, 0=Inactivo); BIT, controla disponibilidad en ajustes de inventario', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustmentConcept', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del Documento  1 - Activado  0 - Inactivo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustmentConcept', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustmentConcept', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de cuenta contable para IVA; FK a MainAccounts, se completa solo si IvaAffects=1 y ConceptType=2 (salida)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustmentConcept', @level2type = N'COLUMN', @level2name = N'IvaAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Si afecta iva este campo se llena, este especifica la cuenta contable que afecta Iva', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustmentConcept', @level2type = N'COLUMN', @level2name = N'IvaAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustmentConcept', @level2type = N'COLUMN', @level2name = N'IvaAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si el concepto de ajuste afecta IVA; BIT, solo relevante en movimientos de salida (ConceptType=2)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustmentConcept', @level2type = N'COLUMN', @level2name = N'IvaAffects';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si afecta Iva, solo se llena si el tipo de concepto es de salida', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustmentConcept', @level2type = N'COLUMN', @level2name = N'IvaAffects';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustmentConcept', @level2type = N'COLUMN', @level2name = N'IvaAffects';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del centro de costo asociado; FK a CostCenter, opcional si la cuenta de ajuste requiere asignación de centro', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustmentConcept', @level2type = N'COLUMN', @level2name = N'CostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Si la cuenta de ajuste maneja centro de costo el frontal se lo debe solicitar', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustmentConcept', @level2type = N'COLUMN', @level2name = N'CostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustmentConcept', @level2type = N'COLUMN', @level2name = N'CostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de cuenta contable principal para el ajuste; FK a MainAccounts, obligatorio, registra la contabilización', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustmentConcept', @level2type = N'COLUMN', @level2name = N'AdjustmentAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta contable de ajuste ', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustmentConcept', @level2type = N'COLUMN', @level2name = N'AdjustmentAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustmentConcept', @level2type = N'COLUMN', @level2name = N'AdjustmentAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Permite visualizar y ajustar productos con fecha de vencimiento expirada; BIT, control de inventario obsoleto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustmentConcept', @level2type = N'COLUMN', @level2name = N'ShowExpiredProduct';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mostrar productos vencidos', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustmentConcept', @level2type = N'COLUMN', @level2name = N'ShowExpiredProduct';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustmentConcept', @level2type = N'COLUMN', @level2name = N'ShowExpiredProduct';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si el concepto de ajuste impacta el costo promedio ponderado del inventario; BIT, afecta valuación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustmentConcept', @level2type = N'COLUMN', @level2name = N'AffectsAverageCost';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Afecta costo promedio', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustmentConcept', @level2type = N'COLUMN', @level2name = N'AffectsAverageCost';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustmentConcept', @level2type = N'COLUMN', @level2name = N'AffectsAverageCost';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de movimiento (1=Entrada/Ingreso, 2=Salida/Egreso); TINYINT, nota: en Traslado (MovementClass=2) siempre es salida', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustmentConcept', @level2type = N'COLUMN', @level2name = N'ConceptType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de concepto 1 - Entrada 2 - Salida    Nota : Cuando la clase del movimiento es Traslado el concepto de ajuste siempre es de salida, asiq ue no hay que mostrarlo en el formulario', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustmentConcept', @level2type = N'COLUMN', @level2name = N'ConceptType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustmentConcept', @level2type = N'COLUMN', @level2name = N'ConceptType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clase del movimiento (1=Ajuste de inventario, 2=Traslado por consumo); TINYINT, clasifica operación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustmentConcept', @level2type = N'COLUMN', @level2name = N'MovementClass';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la clase del movimiento  1 - Ajuste de inventario   2 - Traslado por consumo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustmentConcept', @level2type = N'COLUMN', @level2name = N'MovementClass';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustmentConcept', @level2type = N'COLUMN', @level2name = N'MovementClass';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción del concepto de ajuste; VARCHAR(100), identificador legible para usuarios y reportes', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustmentConcept', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del concepto de ajuste', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustmentConcept', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustmentConcept', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del concepto de ajuste; VARCHAR(20), identificador corto para búsqueda y referencias', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustmentConcept', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del concepto de ajuste', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustmentConcept', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustmentConcept', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del concepto de ajuste; INT IDENTITY, clave primaria del registro', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustmentConcept', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del concepto de ajuste', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustmentConcept', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustmentConcept', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Conceptos de ajuste de inventario: define los motivos o causas por los cuales se puede registrar un ajuste (entrada o salida) de medicamentos e insumos en el inventario, incluyendo su comportamiento contable, afectación al costo promedio y manejo del IVA.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustmentConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustmentConcept';
