CREATE TABLE [dbo].[Tiempo] (
    [Id]           INT          NOT NULL,
    [Fecha]        DATETIME     NOT NULL,
    [Año]          INT          NOT NULL,
    [Mes]          INT          NOT NULL,
    [Dia]          INT          NOT NULL,
    [MesNombre]    VARCHAR (50) NOT NULL,
    [DiaNombre]    VARCHAR (50) NOT NULL,
    [DiaAño]       INT          NOT NULL,
    [SemanaAño]    INT          NOT NULL,
    [Semestre]     INT          NOT NULL,
    [Trimestre]    INT          NOT NULL,
    [Cuatrimestre] INT          NOT NULL,
    [DiaHabil]     BIT          NOT NULL
);
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla de dimensión de tiempo (calendario) utilizada típicamente en esquemas analíticos o de inteligencia de negocio. Almacena atributos temporales descompuestos de cada fecha, como año, mes, día, semana, semestre, trimestre y cuatrimestre, facilitando agrupaciones y filtros en consultas de reporting. El campo `DiaHabil` indica si la fecha corresponde a un día laborable, dato relevante para cálculos de tiempos de atención o gestión clínica.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Tiempo';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Tiempo';
GO
