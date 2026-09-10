CREATE TABLE [Maintenance].[EquipmentReceptionAccesory] (
    [Id]                   INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdAccesory]           INT           NOT NULL,
    [IdEquipmentReception] INT           NOT NULL,
    [Name]                 VARCHAR (50)  NULL,
    [Comment]              VARCHAR (250) NULL,
    [RegisterInvima]       VARCHAR (20)  NULL,
    [LifetimeTime]         CHAR (1)      NULL,
    [MeasurementUnit]      CHAR (1)      NULL,
    [SupplierReference]    VARCHAR (20)  NULL,
    [InstallationDate]     DATETIME      NULL,
    [CalculatedDate]       DATETIME      NULL,
    CONSTRAINT [PK_EquipmentReceptionAccesory] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_EquipmentReceptionAccesory_Accesory] FOREIGN KEY ([IdAccesory]) REFERENCES [Maintenance].[Accessory] ([Id]),
    CONSTRAINT [FK_EquipmentReceptionAccesory_EquipmentRegistration] FOREIGN KEY ([IdEquipmentReception]) REFERENCES [Maintenance].[EquipmentRegistration] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de cálculo de vida útil del accesorio. DATETIME. Fecha en que se calcula o proyecta el vencimiento o renovación del consumible basado en tiempo de instalación y vida útil configurada.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionAccesory', @level2type = N'COLUMN', @level2name = N'CalculatedDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de calculacion.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionAccesory', @level2type = N'COLUMN', @level2name = N'CalculatedDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionAccesory', @level2type = N'COLUMN', @level2name = N'CalculatedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de instalación del accesorio en el equipo médico. DATETIME. Marca el inicio del período de vida útil del consumible o repuesto en la unidad funcional.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionAccesory', @level2type = N'COLUMN', @level2name = N'InstallationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'fecha de instalacion.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionAccesory', @level2type = N'COLUMN', @level2name = N'InstallationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionAccesory', @level2type = N'COLUMN', @level2name = N'InstallationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia o código del proveedor del accesorio. VARCHAR(20). Identificador del fabricante o distribuidor para trazabilidad y reorden de consumibles.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionAccesory', @level2type = N'COLUMN', @level2name = N'SupplierReference';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Referencia del Proveedor.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionAccesory', @level2type = N'COLUMN', @level2name = N'SupplierReference';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionAccesory', @level2type = N'COLUMN', @level2name = N'SupplierReference';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de medida de vida útil: 1=Año, 2=Meses, 3=Días. CHAR(1). Escala temporal en que se expresa el tiempo de vida útil del accesorio o consumible.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionAccesory', @level2type = N'COLUMN', @level2name = N'MeasurementUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad de medida de la vida Util 1-Año 2- Meses 3-Dias', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionAccesory', @level2type = N'COLUMN', @level2name = N'MeasurementUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionAccesory', @level2type = N'COLUMN', @level2name = N'MeasurementUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tiempo de vida útil del accesorio en unidades configuradas. Duración esperada del consumible antes de reemplazo o vencimiento en mantenimiento preventivo de equipos.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionAccesory', @level2type = N'COLUMN', @level2name = N'LifetimeTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tiempo de Vida Util del accesorio.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionAccesory', @level2type = N'COLUMN', @level2name = N'LifetimeTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionAccesory', @level2type = N'COLUMN', @level2name = N'LifetimeTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro INVIMA del accesorio (autorización sanitaria). VARCHAR(20). Número de registro ante el Instituto Nacional de Vigilancia de Medicamentos y Alimentos para dispositivos médicos.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionAccesory', @level2type = N'COLUMN', @level2name = N'RegisterInvima';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Registro Invima ', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionAccesory', @level2type = N'COLUMN', @level2name = N'RegisterInvima';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionAccesory', @level2type = N'COLUMN', @level2name = N'RegisterInvima';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Comentarios o notas sobre el accesorio. VARCHAR(250). Observaciones adicionales, incidencias o especificaciones del consumible en recepción de equipo.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionAccesory', @level2type = N'COLUMN', @level2name = N'Comment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Comentarios', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionAccesory', @level2type = N'COLUMN', @level2name = N'Comment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionAccesory', @level2type = N'COLUMN', @level2name = N'Comment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del accesorio o consumible recibido. VARCHAR(50). Denominación comercial del repuesto, filtro, batería u otro accesorio asociado al equipo médico.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionAccesory', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del consumible', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionAccesory', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionAccesory', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de la recepción del equipo (detalle). INT FK. Identificador que vincula el accesorio al registro de recepción e ingreso del equipo médico a la unidad funcional.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionAccesory', @level2type = N'COLUMN', @level2name = N'IdEquipmentReception';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la recepcion del equipo detalle', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionAccesory', @level2type = N'COLUMN', @level2name = N'IdEquipmentReception';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionAccesory', @level2type = N'COLUMN', @level2name = N'IdEquipmentReception';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID del accesorio maestro. INT FK. Identificador que referencia el catálogo maestro de accesorios relacionados al detalle del registro del equipo.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionAccesory', @level2type = N'COLUMN', @level2name = N'IdAccesory';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del accesorio relacionado a el detalle del registro del equipo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionAccesory', @level2type = N'COLUMN', @level2name = N'IdAccesory';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionAccesory', @level2type = N'COLUMN', @level2name = N'IdAccesory';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de la relación accesorio-recepción. INT IDENTITY. Clave primaria que relaciona cada consumible recibido con el ingreso del equipo médico.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionAccesory', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla que relaciona el detalle  del registro del equipo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionAccesory', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionAccesory', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Accesorios registrados en la recepción de equipos médicos o de mantenimiento. Guarda el detalle de cada accesorio asociado a una recepción de equipo, incluyendo su identificación, referencia del proveedor, registro INVIMA, vida útil y fechas de instalación y vencimiento calculado.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionAccesory';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentReceptionAccesory';
