CREATE TABLE [Report].[TIME_DIMENSION] (
    [Id_Tiempo]         INT           NOT NULL,
    [Tiempo_año]        INT           NULL,
    [Tiempo_semestre]   INT           NULL,
    [Tiempo_trimestre]  INT           NULL,
    [Tiempo_mes]        INT           NULL,
    [Tiempo_semana]     INT           NULL,
    [Tiempo_fecha]      DATE          NULL,
    [Tiempo_dia]        INT           NULL,
    [Tiempo_dia_año]    INT           NULL,
    [Tiempo_dia_semana] INT           NULL,
    [Tiempo_dia_nombre] NVARCHAR (50) NULL,
    [Tiempo_mes_nombre] NVARCHAR (50) NULL,
    CONSTRAINT [PK_Tiempo] PRIMARY KEY CLUSTERED ([Id_Tiempo] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el mes - nombre', @level0type = N'SCHEMA', @level0name = N'Report', @level1type = N'TABLE', @level1name = N'TIME_DIMENSION', @level2type = N'COLUMN', @level2name = N'Tiempo_mes_nombre';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el día - nombre', @level0type = N'SCHEMA', @level0name = N'Report', @level1type = N'TABLE', @level1name = N'TIME_DIMENSION', @level2type = N'COLUMN', @level2name = N'Tiempo_dia_nombre';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el día - semana', @level0type = N'SCHEMA', @level0name = N'Report', @level1type = N'TABLE', @level1name = N'TIME_DIMENSION', @level2type = N'COLUMN', @level2name = N'Tiempo_dia_semana';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el día - año', @level0type = N'SCHEMA', @level0name = N'Report', @level1type = N'TABLE', @level1name = N'TIME_DIMENSION', @level2type = N'COLUMN', @level2name = N'Tiempo_dia_año';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el numero del día', @level0type = N'SCHEMA', @level0name = N'Report', @level1type = N'TABLE', @level1name = N'TIME_DIMENSION', @level2type = N'COLUMN', @level2name = N'Tiempo_dia';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la fecha', @level0type = N'SCHEMA', @level0name = N'Report', @level1type = N'TABLE', @level1name = N'TIME_DIMENSION', @level2type = N'COLUMN', @level2name = N'Tiempo_fecha';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el numero de la semana ', @level0type = N'SCHEMA', @level0name = N'Report', @level1type = N'TABLE', @level1name = N'TIME_DIMENSION', @level2type = N'COLUMN', @level2name = N'Tiempo_semana';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el numero del mes', @level0type = N'SCHEMA', @level0name = N'Report', @level1type = N'TABLE', @level1name = N'TIME_DIMENSION', @level2type = N'COLUMN', @level2name = N'Tiempo_mes';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el numero del trimestre', @level0type = N'SCHEMA', @level0name = N'Report', @level1type = N'TABLE', @level1name = N'TIME_DIMENSION', @level2type = N'COLUMN', @level2name = N'Tiempo_trimestre';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el numero del semestre', @level0type = N'SCHEMA', @level0name = N'Report', @level1type = N'TABLE', @level1name = N'TIME_DIMENSION', @level2type = N'COLUMN', @level2name = N'Tiempo_semestre';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el año', @level0type = N'SCHEMA', @level0name = N'Report', @level1type = N'TABLE', @level1name = N'TIME_DIMENSION', @level2type = N'COLUMN', @level2name = N'Tiempo_año';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'Report', @level1type = N'TABLE', @level1name = N'TIME_DIMENSION', @level2type = N'COLUMN', @level2name = N'Id_Tiempo';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla de dimensión de tiempo perteneciente al esquema de reportería, diseñada para uso en un modelo dimensional (Data Warehouse). Almacena una fila por cada fecha, descompuesta en múltiples granularidades: día, semana, mes, trimestre, semestre y año, incluyendo nombres textuales de día y mes. Sirve como tabla de lookup temporal para relacionar hechos clínicos o administrativos con períodos de tiempo en consultas analíticas y reportes.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'TABLE', @level1name=N'TIME_DIMENSION';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'TABLE', @level1name=N'TIME_DIMENSION';
GO
