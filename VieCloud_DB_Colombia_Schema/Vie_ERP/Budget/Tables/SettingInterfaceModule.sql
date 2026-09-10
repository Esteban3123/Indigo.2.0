CREATE TABLE [Budget].[SettingInterfaceModule] (
    [Id]       INT        IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ItemType] TINYINT    NOT NULL,
    [Process]  NCHAR (10) NULL,
    CONSTRAINT [PK_SettingInterfaceModule] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del trámite o proceso administrativo que debe ejecutarse (NCHAR 10). Identifica el flujo de trabajo: ingreso de paciente, facturación, glosa, autorización, RIPS u otro procedimiento en el módulo de interfaz.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'SettingInterfaceModule', @level2type = N'COLUMN', @level2name = N'Process';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'tramite que debe hacer', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'SettingInterfaceModule', @level2type = N'COLUMN', @level2name = N'Process';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'SettingInterfaceModule', @level2type = N'COLUMN', @level2name = N'Process';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de registro clasificado como TINYINT: 1=Ingreso (atención del paciente, consulta, hospitalización), 2=Gastos (costos, egresos, facturable). Define si la línea es de ingresos o egresosconsultoria.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'SettingInterfaceModule', @level2type = N'COLUMN', @level2name = N'ItemType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el tipo del registro  1 - Ingreso  2 - Gastos', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'SettingInterfaceModule', @level2type = N'COLUMN', @level2name = N'ItemType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'SettingInterfaceModule', @level2type = N'COLUMN', @level2name = N'ItemType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador numérico único (INT, IDENTITY). Clave primaria autoincremental que genera automáticamente el valor secuencial para cada configuración de módulo de interfaz.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'SettingInterfaceModule', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'SettingInterfaceModule', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'SettingInterfaceModule', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Configuración de módulos de interfaz presupuestal. Define los tipos de ítems y procesos habilitados para la integración entre el módulo de presupuesto y otros módulos del sistema.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'SettingInterfaceModule';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'SettingInterfaceModule';
