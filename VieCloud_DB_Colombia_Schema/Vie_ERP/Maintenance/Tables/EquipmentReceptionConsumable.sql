CREATE TABLE [Maintenance].[EquipmentReceptionConsumable] (
    [Id]                   INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdConsumable]         INT           NOT NULL,
    [IdEquipmentReception] INT           NOT NULL,
    [Name]                 VARCHAR (50)  NULL,
    [Comment]              VARCHAR (250) NULL,
    [RegisterInvima]       VARCHAR (20)  NULL,
    [LifetimeTime]         CHAR (1)      NULL,
    [MeasurementUnit]      CHAR (1)      NULL,
    [SupplierReference]    VARCHAR (20)  NULL,
    [InstallationDate]     DATETIME      NULL,
    [CalculatedDate]       DATETIME      NULL,
    CONSTRAINT [PK_EquipmentReceptionDConsumable] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_EquipmentReceptionConsumable_EquipmentRegistration] FOREIGN KEY ([IdEquipmentReception]) REFERENCES [Maintenance].[EquipmentRegistration] ([Id]),
    CONSTRAINT [FK_EquipmentReceptionDConsumable_ConsumableDetail] FOREIGN KEY ([IdConsumable]) REFERENCES [Maintenance].[Consumable] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de cálculo automático de vencimiento o vida útil del consumible, basada en la fecha de instalación y el tiempo de vida útil configurado (DATETIME).', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionConsumable', @level2type = N'COLUMN', @level2name = N'CalculatedDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de calculacion.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionConsumable', @level2type = N'COLUMN', @level2name = N'CalculatedDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionConsumable', @level2type = N'COLUMN', @level2name = N'CalculatedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de instalación o montaje del consumible en el equipo médico (DATETIME).', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionConsumable', @level2type = N'COLUMN', @level2name = N'InstallationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'fecha de instalacion.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionConsumable', @level2type = N'COLUMN', @level2name = N'InstallationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionConsumable', @level2type = N'COLUMN', @level2name = N'InstallationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia, código o número de serie del proveedor del consumible, para trazabilidad de compra (VARCHAR 20).', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionConsumable', @level2type = N'COLUMN', @level2name = N'SupplierReference';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Referencia del Proveedor.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionConsumable', @level2type = N'COLUMN', @level2name = N'SupplierReference';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionConsumable', @level2type = N'COLUMN', @level2name = N'SupplierReference';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de medida de la vida útil del consumible: 1=Año, 2=Meses, 3=Días (CHAR 1, dominio cerrado).', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionConsumable', @level2type = N'COLUMN', @level2name = N'MeasurementUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad de medida de la vida Util 1-Año 2- Meses 3-Dias', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionConsumable', @level2type = N'COLUMN', @level2name = N'MeasurementUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionConsumable', @level2type = N'COLUMN', @level2name = N'MeasurementUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tiempo de vida útil o vigencia del consumible (accesorios, insumos médicos), expresado en la unidad indicada en MeasurementUnit (CHAR 1).', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionConsumable', @level2type = N'COLUMN', @level2name = N'LifetimeTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tiempo de Vida Util del accesorio.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionConsumable', @level2type = N'COLUMN', @level2name = N'LifetimeTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionConsumable', @level2type = N'COLUMN', @level2name = N'LifetimeTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de registro o certificación INVIMA (Instituto Nacional de Vigilancia de Medicamentos y Alimentos) del consumible (VARCHAR 20, PII sensible).', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionConsumable', @level2type = N'COLUMN', @level2name = N'RegisterInvima';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Registro Invima ', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionConsumable', @level2type = N'COLUMN', @level2name = N'RegisterInvima';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionConsumable', @level2type = N'COLUMN', @level2name = N'RegisterInvima';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Notas, observaciones o comentarios adicionales sobre el consumible, su instalación o estado (VARCHAR 250).', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionConsumable', @level2type = N'COLUMN', @level2name = N'Comment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Comentarios', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionConsumable', @level2type = N'COLUMN', @level2name = N'Comment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionConsumable', @level2type = N'COLUMN', @level2name = N'Comment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o denominación del consumible, accesorio o insumo asociado al equipo médico (VARCHAR 50).', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionConsumable', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del consumible', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionConsumable', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionConsumable', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de la recepción/registro de equipo relacionada (FK a EquipmentRegistration, INT).', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionConsumable', @level2type = N'COLUMN', @level2name = N'IdEquipmentReception';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del registro detalle de equipo relacionado', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionConsumable', @level2type = N'COLUMN', @level2name = N'IdEquipmentReception';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionConsumable', @level2type = N'COLUMN', @level2name = N'IdEquipmentReception';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del consumible maestro o catálogo relacionado (FK a Consumable, INT).', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionConsumable', @level2type = N'COLUMN', @level2name = N'IdConsumable';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del consumible relacionado', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionConsumable', @level2type = N'COLUMN', @level2name = N'IdConsumable';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionConsumable', @level2type = N'COLUMN', @level2name = N'IdConsumable';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (clave primaria) de la relación entre el registro de recepción del equipo y sus consumibles asociados (INT IDENTITY, autonumérico).', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionConsumable', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla de relacion entre  el registro del detalle del equipo  y los consumibles', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionConsumable', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionConsumable', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de consumibles asociados a la recepción de equipos de mantenimiento. Guarda el detalle de cada insumo o material fungible instalado o recibido junto con un equipo, incluyendo datos de registro sanitario (INVIMA), vida útil, unidad de medida, referencia del proveedor y fechas de instalación y vencimiento calculado.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionConsumable';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionConsumable';
