CREATE TABLE [dbo].[HemodynamicMonitoringVariablesD] (
    [Id]                                INT          IDENTITY (1, 1) NOT NULL,
    [IdHemodynamicMonitoringVariablesC] INT          NOT NULL,
    [Code]                              VARCHAR (3)  NOT NULL,
    [Name]                              VARCHAR (50) NOT NULL,
    [Description]                       VARCHAR (50) NULL,
    [Status]                            BIT          NOT NULL,
    CONSTRAINT [PK_HemodynamicMonitoringVariablesD] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_HemodynamicMonitoringVariablesC_Id_IHemodynamicMonitoringVariablesD] FOREIGN KEY ([IdHemodynamicMonitoringVariablesC]) REFERENCES [dbo].[HemodynamicMonitoringVariablesC] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado activo/inactivo (bit booleano): indica si la variable de monitoreo hemodinámico está habilitada o deshabilitada en el sistema', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicMonitoringVariablesD', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado (true or false)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicMonitoringVariablesD', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicMonitoringVariablesD', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción detallada de la variable de monitoreo hemodinámico; texto que amplia la definición técnica del parámetro cardiovascular monitoreado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicMonitoringVariablesD', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripción de la variable de monitoreo hemodinamico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicMonitoringVariablesD', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicMonitoringVariablesD', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre descriptivo de la variable de monitoreo hemodinámico; denominación del parámetro cardiovascular (ej: presión arterial, frecuencia cardíaca, saturación)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicMonitoringVariablesD', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de la variable de monitoreo hemodinamico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicMonitoringVariablesD', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicMonitoringVariablesD', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código alfanumérico de 3 caracteres que identifica únicamente la variable de monitoreo hemodinámico en sistemas de reporte y RIPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicMonitoringVariablesD', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de la variable de monitoreo hemodinamico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicMonitoringVariablesD', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicMonitoringVariablesD', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea (FK) que relaciona con la tabla HemodynamicMonitoringVariablesC; agrupa variables de monitoreo hemodinámico por categoría o protocolo de atención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicMonitoringVariablesD', @level2type = N'COLUMN', @level2name = N'IdHemodynamicMonitoringVariablesC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tiene relación con la tabla HemodynamicMonitoringVariablesC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicMonitoringVariablesD', @level2type = N'COLUMN', @level2name = N'IdHemodynamicMonitoringVariablesC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicMonitoringVariablesD', @level2type = N'COLUMN', @level2name = N'IdHemodynamicMonitoringVariablesC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único secuencial (INT IDENTITY) de la variable de monitoreo hemodinámico en la tabla detalle', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicMonitoringVariablesD', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicMonitoringVariablesD', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicMonitoringVariablesD', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de variables individuales de monitoreo hemodinámico (detalle), como presión arterial, frecuencia cardíaca, gasto cardíaco u otros parámetros vitales hemodinámicos. Cada registro corresponde a una variable específica asociada a un grupo o cabecera de monitoreo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicMonitoringVariablesD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicMonitoringVariablesD';
