CREATE TABLE [Maintenance].[MaintenancePlanProgramated] (
    [Id]                            INT      IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [MaintenancePlanAndMetrologyId] INT      NOT NULL,
    [FixedAssetPhysicalId]          INT      NOT NULL,
    [DateProgramated]               DATETIME NOT NULL,
    [ProgramType]                   TINYINT  NOT NULL,
    [Notificated]                   BIT      NOT NULL,
    [State]                         TINYINT  NULL,
    CONSTRAINT [PK_MaintenancePlanProgramated__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MaintenancePlanProgramated_FixedAssetPhysicalAsset] FOREIGN KEY ([FixedAssetPhysicalId]) REFERENCES [FixedAsset].[FixedAssetPhysicalAsset] ([Id]),
    CONSTRAINT [FK_MaintenancePlanProgramated_MaintenancePlanAndMetrology] FOREIGN KEY ([MaintenancePlanAndMetrologyId]) REFERENCES [Maintenance].[MaintenancePlanAndMetrology] ([Id]),
    CONSTRAINT [UQ_MaintenancePlanProgramated__DateProgramated__MaintenancePlanAndMetrologyId__ProgramType__FixedAssetPhysicalId] UNIQUE NONCLUSTERED ([DateProgramated] ASC, [MaintenancePlanAndMetrologyId] ASC, [ProgramType] ASC, [FixedAssetPhysicalId] ASC)
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de la programación de mantenimiento: 1=Programado, 2=Con Orden de Trabajo generada, 3=Anulado. Tipo: TINYINT. Indica el ciclo de vida del plan programado.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanProgramated', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado:  1 - Programado  2 - Con Orden de Trabajo  3 - Anulado', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanProgramated', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanProgramated', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera booleana (BIT) que indica si la notificación de mantenimiento o metrología ya fue enviada por correo electrónico al responsable o técnico.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanProgramated', @level2type = N'COLUMN', @level2name = N'Notificated';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indica si ya se ha notificado via mail', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanProgramated', @level2type = N'COLUMN', @level2name = N'Notificated';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanProgramated', @level2type = N'COLUMN', @level2name = N'Notificated';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de programación: 1=Mantenimiento preventivo/correctivo, 2=Metrología/calibración. Tipo: TINYINT. Clasifica el objetivo del plan programado.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanProgramated', @level2type = N'COLUMN', @level2name = N'ProgramType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Programación:  1 - Mantenimiento  2 - Metrología', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanProgramated', @level2type = N'COLUMN', @level2name = N'ProgramType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanProgramated', @level2type = N'COLUMN', @level2name = N'ProgramType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora programada para ejecutar el mantenimiento o metrología del activo fijo. Tipo: DATETIME. Campo clave para búsqueda temporal y alertas.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanProgramated', @level2type = N'COLUMN', @level2name = N'DateProgramated';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha programada', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanProgramated', @level2type = N'COLUMN', @level2name = N'DateProgramated';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanProgramated', @level2type = N'COLUMN', @level2name = N'DateProgramated';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del activo fijo físico (equipo, máquina, instrumento) que requiere mantenimiento. FK a [FixedAsset].[FixedAssetPhysicalAsset]. Tipo: INT.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanProgramated', @level2type = N'COLUMN', @level2name = N'FixedAssetPhysicalId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Activo fijo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanProgramated', @level2type = N'COLUMN', @level2name = N'FixedAssetPhysicalId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanProgramated', @level2type = N'COLUMN', @level2name = N'FixedAssetPhysicalId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del plan de mantenimiento y metrología asociado. FK a [Maintenance].[MaintenancePlanAndMetrology]. Tipo: INT. Vincula la programación al plan maestro.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanProgramated', @level2type = N'COLUMN', @level2name = N'MaintenancePlanAndMetrologyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del plan de mantenimiento y metrología', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanProgramated', @level2type = N'COLUMN', @level2name = N'MaintenancePlanAndMetrologyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanProgramated', @level2type = N'COLUMN', @level2name = N'MaintenancePlanAndMetrologyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincremental (IDENTITY) de cada registro de programación de mantenimiento. Tipo: INT. Clave primaria clustering.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanProgramated', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanProgramated', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanProgramated', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de fechas programadas para la ejecución de planes de mantenimiento y metrología sobre activos fijos físicos. Controla qué mantenimientos están agendados, si ya se notificó al responsable y cuál es el estado actual de cada programación.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanProgramated';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanProgramated';
