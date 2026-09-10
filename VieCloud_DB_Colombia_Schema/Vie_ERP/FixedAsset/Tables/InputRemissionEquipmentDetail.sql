CREATE TABLE [FixedAsset].[InputRemissionEquipmentDetail] (
    [Id]                        INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdInputRemissionEquipment] INT          NOT NULL,
    [IdEquipment]               INT          NOT NULL,
    [LicensePlate]              VARCHAR (50) NOT NULL,
    [Serie]                     VARCHAR (50) NOT NULL,
    [IdResponsible]             INT          NOT NULL,
    [IdFunctionalUnit]          INT          NOT NULL,
    [IdLocation]                INT          NOT NULL,
    [AdquisitionDate]           DATE         NOT NULL,
    [Depreciate]                BIT          NOT NULL,
    [ComponentDepreciate]       BIT          NULL,
    CONSTRAINT [PK_InputRemissionEquipmentDetail_1] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_InputRemissionEquipmentDetail_FunctionalUnit] FOREIGN KEY ([IdFunctionalUnit]) REFERENCES [Payroll].[FunctionalUnit] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de depreciación por componente (1=Sí, 0=No). Se completa solo cuando el equipo está en modalidad de préstamo. Tipo: BIT, nullable, PII=No.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipmentDetail', @level2type = N'COLUMN', @level2name = N'ComponentDepreciate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Deprecia x Componente. 1 - SI, 0 - NO. Solo se llena si el campo LOAN es 1. ', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipmentDetail', @level2type = N'COLUMN', @level2name = N'ComponentDepreciate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipmentDetail', @level2type = N'COLUMN', @level2name = N'ComponentDepreciate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de depreciación del activo fijo (1=Sí, 0=No). Determina si el equipo se deprecia contablemente. Tipo: BIT, requerido.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipmentDetail', @level2type = N'COLUMN', @level2name = N'Depreciate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Deprecia (1 - SI, 0 - NO)', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipmentDetail', @level2type = N'COLUMN', @level2name = N'Depreciate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipmentDetail', @level2type = N'COLUMN', @level2name = N'Depreciate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de adquisición o compra del equipo, activo fijo. Tipo: DATE. Base para cálculo de depreciación y antigüedad del bien.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipmentDetail', @level2type = N'COLUMN', @level2name = N'AdquisitionDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Adquisición', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipmentDetail', @level2type = N'COLUMN', @level2name = N'AdquisitionDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipmentDetail', @level2type = N'COLUMN', @level2name = N'AdquisitionDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la localización, sucursal o sede donde se registra físicamente el equipo. Clave foránea a tabla de ubicaciones.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipmentDetail', @level2type = N'COLUMN', @level2name = N'IdLocation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la Localización', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipmentDetail', @level2type = N'COLUMN', @level2name = N'IdLocation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipmentDetail', @level2type = N'COLUMN', @level2name = N'IdLocation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la unidad funcional responsable del equipo (departamento, servicio, centro de atención). FK→[Payroll].[FunctionalUnit].', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipmentDetail', @level2type = N'COLUMN', @level2name = N'IdFunctionalUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipmentDetail', @level2type = N'COLUMN', @level2name = N'IdFunctionalUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipmentDetail', @level2type = N'COLUMN', @level2name = N'IdFunctionalUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del profesional de salud, responsable o custodio del equipo en la unidad funcional. Clave foránea a tabla de personas.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipmentDetail', @level2type = N'COLUMN', @level2name = N'IdResponsible';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Responsable', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipmentDetail', @level2type = N'COLUMN', @level2name = N'IdResponsible';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipmentDetail', @level2type = N'COLUMN', @level2name = N'IdResponsible';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de serie del equipo, identificador único del fabricante. Tipo: VARCHAR(50). Usado para trazabilidad y control de activos fijos.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipmentDetail', @level2type = N'COLUMN', @level2name = N'Serie';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'numero de serie', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipmentDetail', @level2type = N'COLUMN', @level2name = N'Serie';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipmentDetail', @level2type = N'COLUMN', @level2name = N'Serie';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Placa, código o etiqueta de identificación interna del activo en la institución. Tipo: VARCHAR(50). Equivalente a número patrimonial.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipmentDetail', @level2type = N'COLUMN', @level2name = N'LicensePlate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Placa', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipmentDetail', @level2type = N'COLUMN', @level2name = N'LicensePlate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipmentDetail', @level2type = N'COLUMN', @level2name = N'LicensePlate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tipo, modelo o catálogo de equipo (ej: ecógrafo, electrocardiógrafo). Clave foránea a tabla maestra de equipos.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipmentDetail', @level2type = N'COLUMN', @level2name = N'IdEquipment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Equipo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipmentDetail', @level2type = N'COLUMN', @level2name = N'IdEquipment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipmentDetail', @level2type = N'COLUMN', @level2name = N'IdEquipment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la remisión de entrada (ingreso) del equipo al inventario. FK→[FixedAsset].[InputRemissionEquipment].', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipmentDetail', @level2type = N'COLUMN', @level2name = N'IdInputRemissionEquipment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Equipo de remisión de entrada', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipmentDetail', @level2type = N'COLUMN', @level2name = N'IdInputRemissionEquipment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipmentDetail', @level2type = N'COLUMN', @level2name = N'IdInputRemissionEquipment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincremental (IDENTITY) del detalle de equipo en remisión de entrada. Clave primaria, tipo INT.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipmentDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipmentDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipmentDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de equipos incluidos en una remisión de entrada de activos fijos. Registra cada equipo recibido con su placa, serie, responsable, ubicación, unidad funcional y condiciones de depreciación al momento del ingreso al inventario.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipmentDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipmentDetail';
