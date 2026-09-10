CREATE TABLE [Maintenance].[EquipmentReceptionParte] (
    [Id]                   INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdEquipmentReception] INT           NOT NULL,
    [Idpart]               INT           NOT NULL,
    [NamePart]             VARCHAR (100) NOT NULL,
    [Name]                 VARCHAR (500) NULL,
    [Comment]              VARCHAR (250) NULL,
    [RegisterInvima]       VARCHAR (20)  NULL,
    [LifetimeTime]         CHAR (1)      NULL,
    [MeasurementUnit]      CHAR (1)      NULL,
    [SupplierReference]    VARCHAR (20)  NULL,
    [InstallationDate]     DATETIME      NULL,
    [CalculatedDate]       DATETIME      NULL,
    CONSTRAINT [PK_EquipmentReceptionParte__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_EquipmentReceptionParte_EquipmentRegistration] FOREIGN KEY ([IdEquipmentReception]) REFERENCES [Maintenance].[EquipmentRegistration] ([Id]),
    CONSTRAINT [FK_ParteDetail_Part] FOREIGN KEY ([Idpart]) REFERENCES [Maintenance].[PartDetail] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de cálculo de vida útil de la pieza, tipo DATETIME, determina cuándo se computó el ciclo de vigencia del componente o accesorio médico.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionParte', @level2type = N'COLUMN', @level2name = N'CalculatedDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de calculacion.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionParte', @level2type = N'COLUMN', @level2name = N'CalculatedDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionParte', @level2type = N'COLUMN', @level2name = N'CalculatedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de instalación física de la pieza en el equipo, tipo DATETIME, registra cuándo se montó o colocó el componente en la unidad receptora.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionParte', @level2type = N'COLUMN', @level2name = N'InstallationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'fecha de instalacion.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionParte', @level2type = N'COLUMN', @level2name = N'InstallationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionParte', @level2type = N'COLUMN', @level2name = N'InstallationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia, código o identificador del proveedor (VARCHAR 20), vincula la pieza con el distribuidor o fabricante que la suministró.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionParte', @level2type = N'COLUMN', @level2name = N'SupplierReference';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Referencia del Proveedor.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionParte', @level2type = N'COLUMN', @level2name = N'SupplierReference';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionParte', @level2type = N'COLUMN', @level2name = N'SupplierReference';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de medida de vida útil (CHAR 1): 1=Año, 2=Meses, 3=Días; especifica la escala temporal para el ciclo de vigencia del accesorio.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionParte', @level2type = N'COLUMN', @level2name = N'MeasurementUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad de medida de la vida Util 1-Año 2- Meses 3-Dias', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionParte', @level2type = N'COLUMN', @level2name = N'MeasurementUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionParte', @level2type = N'COLUMN', @level2name = N'MeasurementUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tiempo de vida útil del accesorio (CHAR 1), duración máxima recomendada de funcionamiento o validez técnica del componente antes de reemplazo.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionParte', @level2type = N'COLUMN', @level2name = N'LifetimeTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tiempo de Vida Util del accesorio.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionParte', @level2type = N'COLUMN', @level2name = N'LifetimeTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionParte', @level2type = N'COLUMN', @level2name = N'LifetimeTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro INVIMA (VARCHAR 20), número de autorización sanitaria de la pieza ante la autoridad regulatoria de dispositivos médicos en Colombia.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionParte', @level2type = N'COLUMN', @level2name = N'RegisterInvima';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Registro Invima ', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionParte', @level2type = N'COLUMN', @level2name = N'RegisterInvima';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionParte', @level2type = N'COLUMN', @level2name = N'RegisterInvima';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Comentarios adicionales (VARCHAR 250) sobre la pieza, observaciones técnicas, estados especiales o notas de mantenimiento del componente.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionParte', @level2type = N'COLUMN', @level2name = N'Comment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Comentarios', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionParte', @level2type = N'COLUMN', @level2name = N'Comment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionParte', @level2type = N'COLUMN', @level2name = N'Comment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción detallada de la pieza (VARCHAR 500), especificaciones técnicas o funcionales del componente recibido en el equipo.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionParte', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion del detalle de la pieza', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionParte', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionParte', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre comercial o técnico de la pieza (VARCHAR 100), identificación estándar del componente o accesorio en el catálogo de equipos.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionParte', @level2type = N'COLUMN', @level2name = N'NamePart';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de la pieza relacionada', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionParte', @level2type = N'COLUMN', @level2name = N'NamePart';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionParte', @level2type = N'COLUMN', @level2name = N'NamePart';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de la pieza (INT, FK a PartDetail), referencia interna que vincula al registro maestro del componente en mantenimiento.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionParte', @level2type = N'COLUMN', @level2name = N'Idpart';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la pieza relacionadadel detalle de la pieza', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionParte', @level2type = N'COLUMN', @level2name = N'Idpart';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionParte', @level2type = N'COLUMN', @level2name = N'Idpart';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la recepción del equipo (INT, FK a EquipmentRegistration), vincula la pieza a la orden o ingreso de recepción del equipo completo.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionParte', @level2type = N'COLUMN', @level2name = N'IdEquipmentReception';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la recepcion del equipo relacionado ', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionParte', @level2type = N'COLUMN', @level2name = N'IdEquipmentReception';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionParte', @level2type = N'COLUMN', @level2name = N'IdEquipmentReception';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único auto-incremental (INT PK), clave primaria que identifica unívocamente cada registro de pieza en la recepción de equipos.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionParte', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'autonumerico del detalle de la pieza', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionParte', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionParte', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Partes o componentes registrados durante la recepción de un equipo en mantenimiento. Detalla cada pieza, repuesto o elemento inspeccionado o reemplazado al recibir un equipo, incluyendo su referencia, proveedor y fechas de instalación.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionParte';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionParte';
