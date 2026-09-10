CREATE TABLE [Billing].[BotServicesProceduresLog] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [Row]            INT            NOT NULL,
    [DataSourceType] INT            NOT NULL,
    [State]          BIT            NOT NULL,
    [Message]        NVARCHAR (MAX) NULL,
    [CreationDate]   DATETIME       NOT NULL,
    [UpdateDate]     DATETIME       NULL,
    [InitialDate]    DATETIME       NULL,
    [EndDate]        DATETIME       NULL,
    [Data]           NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_BotServicesProceduresLog] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_BotServicesProceduresLog_DataSourceType_EndDate_InitialDate_CreationDate_Data_Message_Row_State_UpdateDate]
    ON [Billing].[BotServicesProceduresLog]([DataSourceType] ASC, [EndDate] ASC, [InitialDate] ASC)
    INCLUDE([CreationDate], [Data], [Message], [Row], [State], [UpdateDate]);


GO
CREATE NONCLUSTERED INDEX [IX_BotServicesProceduresLog_DataSourceType_EndDate_InitialDate_Row]
    ON [Billing].[BotServicesProceduresLog]([DataSourceType] ASC, [EndDate] ASC, [InitialDate] ASC, [Row] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacena JSON con información completa y mensaje de resultado del proceso de sincronización de servicios y procedimientos (Laboratorios, Imágenes, Patologías, Procedimientos, Interconsultas, Terapias, Enfermería, Oxígeno, Valoraciones, Central de Mezclas).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BotServicesProceduresLog', @level2type = N'COLUMN', @level2name = N'Data';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el JSON con la información y el mensaje del proceso.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BotServicesProceduresLog', @level2type = N'COLUMN', @level2name = N'Data';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BotServicesProceduresLog', @level2type = N'COLUMN', @level2name = N'Data';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de finalización de la ejecución del proceso del bot de servicios y procedimientos (DATETIME).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BotServicesProceduresLog', @level2type = N'COLUMN', @level2name = N'EndDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha final del registro.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BotServicesProceduresLog', @level2type = N'COLUMN', @level2name = N'EndDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BotServicesProceduresLog', @level2type = N'COLUMN', @level2name = N'EndDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de inicio de la ejecución del proceso del bot de servicios y procedimientos (DATETIME).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BotServicesProceduresLog', @level2type = N'COLUMN', @level2name = N'InitialDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha inicial del registro.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BotServicesProceduresLog', @level2type = N'COLUMN', @level2name = N'InitialDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BotServicesProceduresLog', @level2type = N'COLUMN', @level2name = N'InitialDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de última modificación o actualización del registro de ejecución del proceso (DATETIME).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BotServicesProceduresLog', @level2type = N'COLUMN', @level2name = N'UpdateDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de modificación del registro.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BotServicesProceduresLog', @level2type = N'COLUMN', @level2name = N'UpdateDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BotServicesProceduresLog', @level2type = N'COLUMN', @level2name = N'UpdateDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación/registro del log de ejecución del bot de servicios (DATETIME, auditoría).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BotServicesProceduresLog', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha actual del registro.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BotServicesProceduresLog', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BotServicesProceduresLog', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción textual del resultado, error o estado del proceso ejecutado por el bot de servicios y procedimientos.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BotServicesProceduresLog', @level2type = N'COLUMN', @level2name = N'Message';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripción del registro.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BotServicesProceduresLog', @level2type = N'COLUMN', @level2name = N'Message';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BotServicesProceduresLog', @level2type = N'COLUMN', @level2name = N'Message';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de ejecución del proceso: 0=Fallo/Error en ejecución, 1=Éxito/Ejecución sin problemas (BIT, booleano).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BotServicesProceduresLog', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Determina el estado del registro:  0 - Cuando falle  1 - Cuando se haya ejecutado sin problema', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BotServicesProceduresLog', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BotServicesProceduresLog', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de fuente de datos procesada: 1=Laboratorios, 2=Imágenes Dx, 3=Patologías, 4=Procedimientos NoQx, 5=Interconsultas, 6=Terapias, 7=Procedimientos Enfermería, 8=Consumo Oxígeno, 9=Valoraciones, 10=Central Mezclas (INT).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BotServicesProceduresLog', @level2type = N'COLUMN', @level2name = N'DataSourceType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1 - Laboratorios  2 - ImagenesDx  3 - Patologías  4 - ProcedimientosNoQx  5 - Interconsultas  6 - Terapias  7 - Procedimientos de Enfermeria  8 - Consumo de Oxigeno  9 - Valoraciones  10- central de mezclas', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BotServicesProceduresLog', @level2type = N'COLUMN', @level2name = N'DataSourceType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BotServicesProceduresLog', @level2type = N'COLUMN', @level2name = N'DataSourceType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador secuencial auto-generado que vincula el registro a la tabla HCORDLABO (referencia de auditoría de laboratorios, INT).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BotServicesProceduresLog', @level2type = N'COLUMN', @level2name = N'Row';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Auto que determina el registro de la tabla HCORDLABO', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BotServicesProceduresLog', @level2type = N'COLUMN', @level2name = N'Row';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BotServicesProceduresLog', @level2type = N'COLUMN', @level2name = N'Row';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (PK) del registro de log del bot de servicios y procedimientos, clave primaria de identidad (INT IDENTITY).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BotServicesProceduresLog', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Llave primaria de la tabla.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BotServicesProceduresLog', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BotServicesProceduresLog', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de ejecución del bot de procesamiento de servicios y procedimientos de facturación. Guarda el historial de cada intento de procesamiento automatizado, incluyendo su estado, mensajes de resultado y fechas de ciclo de vida.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BotServicesProceduresLog';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BotServicesProceduresLog';
