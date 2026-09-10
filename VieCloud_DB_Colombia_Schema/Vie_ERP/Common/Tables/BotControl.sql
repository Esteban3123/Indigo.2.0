CREATE TABLE [Common].[BotControl] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [TableName]   VARCHAR (200) NOT NULL,
    [LastId]      INT           NULL,
    [LastDate]    DATETIME      NOT NULL,
    [TypeControl] TINYINT       NOT NULL,
    [DateRanges]  VARCHAR (200) NOT NULL,
    [Disabled]    BIT           DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_BotControl] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera binaria (BIT, 0/1) para activar (0=activo) o desactivar (1=inactivo) el bot de sincronización de la tabla; controla ejecución automática', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'BotControl', @level2type = N'COLUMN', @level2name = N'Disabled';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Parametro para desactivar o activar el bot, 0 - activado ; 1 - desactivado', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'BotControl', @level2type = N'COLUMN', @level2name = N'Disabled';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'BotControl', @level2type = N'COLUMN', @level2name = N'Disabled';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Configuración de intervalos de fecha para sincronización: estructura #@unidad (dd=día, mm=mes, aa=año). Ejemplo: 1@dd (cada 1 día), 1@mm (cada 1 mes), 1@aa (cada 1 año)', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'BotControl', @level2type = N'COLUMN', @level2name = N'DateRanges';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Rango por intervalos de fecha: estructura del dato(#@(especifica si es Dia: dd, mes: mm, año: aa)                   Ej: 1@dd (intervalo de 1 dia);                    1@mm (intervalos de 1 mes)                    1@aa (intervalos de 1 año)) ', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'BotControl', @level2type = N'COLUMN', @level2name = N'DateRanges';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'BotControl', @level2type = N'COLUMN', @level2name = N'DateRanges';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de control de sincronización: 0=por ID incremental, 1=por rango de fecha; define estrategia de replicación de datos', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'BotControl', @level2type = N'COLUMN', @level2name = N'TypeControl';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'0-Sincronizar por Id; 1-sincronizar por fecha', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'BotControl', @level2type = N'COLUMN', @level2name = N'TypeControl';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'BotControl', @level2type = N'COLUMN', @level2name = N'TypeControl';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Última fecha y hora (DATETIME) del registro insertado o procesado en la tabla; referencia temporal para control de sincronización', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'BotControl', @level2type = N'COLUMN', @level2name = N'LastDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ultima fecha del registro de la tabla insertado', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'BotControl', @level2type = N'COLUMN', @level2name = N'LastDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'BotControl', @level2type = N'COLUMN', @level2name = N'LastDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Último identificador (INT) procesado en la tabla monitoreada; marca el punto de continuidad para sincronización incremental por ID', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'BotControl', @level2type = N'COLUMN', @level2name = N'LastId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ultimo Id', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'BotControl', @level2type = N'COLUMN', @level2name = N'LastId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'BotControl', @level2type = N'COLUMN', @level2name = N'LastId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de la tabla SQL Server a sincronizar o monitorear por el bot de integración de datos', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'BotControl', @level2type = N'COLUMN', @level2name = N'TableName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de la tabla', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'BotControl', @level2type = N'COLUMN', @level2name = N'TableName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'BotControl', @level2type = N'COLUMN', @level2name = N'TableName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincremental (INT IDENTITY) del registro de control del bot de sincronización', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'BotControl', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'BotControl', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'BotControl', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de control y seguimiento de bots o procesos automáticos que sincronizan o recorren tablas del sistema. Guarda el estado de avance de cada proceso indicando hasta qué registro o fecha procesó por última vez, el tipo de control aplicado y los rangos de fechas configurados, permitiendo pausar o deshabilitar cada bot individualmente.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'BotControl';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'BotControl';
