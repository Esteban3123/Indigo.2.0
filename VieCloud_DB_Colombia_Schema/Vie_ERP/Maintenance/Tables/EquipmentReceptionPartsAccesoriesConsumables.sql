CREATE TABLE [Maintenance].[EquipmentReceptionPartsAccesoriesConsumables] (
    [Id]                           INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdPartsAccesoriesConsumables] INT           NOT NULL,
    [IdEquipmentReception]         INT           NOT NULL,
    [Name]                         VARCHAR (50)  NULL,
    [Comment]                      VARCHAR (250) NULL,
    [RegisterInvima]               VARCHAR (20)  NULL,
    [LifeTime]                     CHAR (1)      NULL,
    [MeasurementUnit]              CHAR (1)      NULL,
    [SupplierReference]            VARCHAR (20)  NULL,
    [InstallationDate]             DATETIME      NULL,
    [CalculatedDate]               DATETIME      NULL,
    CONSTRAINT [PK_EquipmentReceptionPartsAccesoriesConsumables] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_EquipmentReceptionPartsAccesoriesConsumables_EquipmentRegistration] FOREIGN KEY ([IdEquipmentReception]) REFERENCES [Maintenance].[EquipmentRegistration] ([Id]),
    CONSTRAINT [FK_EquipmentReceptionPartsAccesoriesConsumables_FixedAssetPartsAccesoriesConsumables] FOREIGN KEY ([IdPartsAccesoriesConsumables]) REFERENCES [FixedAsset].[FixedAssetPartsAccesoriesConsumables] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha estimada o calculada de vencimiento/reemplazo de la pieza, accesorio o consumible. DATETIME.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionPartsAccesoriesConsumables', @level2type = N'COLUMN', @level2name = N'CalculatedDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha estimada', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionPartsAccesoriesConsumables', @level2type = N'COLUMN', @level2name = N'CalculatedDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionPartsAccesoriesConsumables', @level2type = N'COLUMN', @level2name = N'CalculatedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de instalación o montaje de la pieza, accesorio o consumible en el equipo médico. DATETIME.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionPartsAccesoriesConsumables', @level2type = N'COLUMN', @level2name = N'InstallationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de instalación', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionPartsAccesoriesConsumables', @level2type = N'COLUMN', @level2name = N'InstallationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionPartsAccesoriesConsumables', @level2type = N'COLUMN', @level2name = N'InstallationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia, código o identificador del proveedor para la pieza, accesorio o consumible. VARCHAR(20).', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionPartsAccesoriesConsumables', @level2type = N'COLUMN', @level2name = N'SupplierReference';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Referencia del proveedor', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionPartsAccesoriesConsumables', @level2type = N'COLUMN', @level2name = N'SupplierReference';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionPartsAccesoriesConsumables', @level2type = N'COLUMN', @level2name = N'SupplierReference';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de medida del consumible o accesorio (unidad, caja, frasco, etc.). CHAR(1).', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionPartsAccesoriesConsumables', @level2type = N'COLUMN', @level2name = N'MeasurementUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad de medida', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionPartsAccesoriesConsumables', @level2type = N'COLUMN', @level2name = N'MeasurementUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionPartsAccesoriesConsumables', @level2type = N'COLUMN', @level2name = N'MeasurementUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vida útil o vigencia de la pieza, accesorio o consumible. Período de funcionamiento o validez. CHAR(1).', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionPartsAccesoriesConsumables', @level2type = N'COLUMN', @level2name = N'LifeTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'vida util', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionPartsAccesoriesConsumables', @level2type = N'COLUMN', @level2name = N'LifeTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionPartsAccesoriesConsumables', @level2type = N'COLUMN', @level2name = N'LifeTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro INVIMA o número de autorización sanitaria de la pieza, accesorio o consumible. VARCHAR(20).', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionPartsAccesoriesConsumables', @level2type = N'COLUMN', @level2name = N'RegisterInvima';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Registro invima', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionPartsAccesoriesConsumables', @level2type = N'COLUMN', @level2name = N'RegisterInvima';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionPartsAccesoriesConsumables', @level2type = N'COLUMN', @level2name = N'RegisterInvima';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Comentario o nota adicional sobre la recepción, instalación o estado de la pieza, accesorio o consumible. VARCHAR(250).', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionPartsAccesoriesConsumables', @level2type = N'COLUMN', @level2name = N'Comment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Comentario', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionPartsAccesoriesConsumables', @level2type = N'COLUMN', @level2name = N'Comment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionPartsAccesoriesConsumables', @level2type = N'COLUMN', @level2name = N'Comment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o denominación de la pieza, accesorio o consumible recibido. VARCHAR(50).', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionPartsAccesoriesConsumables', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionPartsAccesoriesConsumables', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionPartsAccesoriesConsumables', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la recepción de equipos médicos. FK a [Maintenance].[EquipmentRegistration]. INT.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionPartsAccesoriesConsumables', @level2type = N'COLUMN', @level2name = N'IdEquipmentReception';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID recepción de equipos', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionPartsAccesoriesConsumables', @level2type = N'COLUMN', @level2name = N'IdEquipmentReception';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionPartsAccesoriesConsumables', @level2type = N'COLUMN', @level2name = N'IdEquipmentReception';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la pieza, accesorio o consumible del activo fijo. FK a [FixedAsset].[FixedAssetPartsAccesoriesConsumables]. INT.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionPartsAccesoriesConsumables', @level2type = N'COLUMN', @level2name = N'IdPartsAccesoriesConsumables';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Piezas Accesorios Consumibles', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionPartsAccesoriesConsumables', @level2type = N'COLUMN', @level2name = N'IdPartsAccesoriesConsumables';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionPartsAccesoriesConsumables', @level2type = N'COLUMN', @level2name = N'IdPartsAccesoriesConsumables';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincremental de la relación entre recepción de equipo y pieza/accesorio/consumible. INT IDENTITY.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionPartsAccesoriesConsumables', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionPartsAccesoriesConsumables', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionPartsAccesoriesConsumables', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de partes, accesorios y consumibles asociados a la recepción de equipos en mantenimiento. Permite llevar el control de los componentes entregados junto con cada equipo, incluyendo su vigencia, unidad de medida, referencia del proveedor y fechas de instalación o vencimiento calculado.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionPartsAccesoriesConsumables';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionPartsAccesoriesConsumables';
