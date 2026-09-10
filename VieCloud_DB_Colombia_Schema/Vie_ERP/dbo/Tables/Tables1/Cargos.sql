CREATE TABLE [dbo].[Cargos] (
    [Codigo]                   VARCHAR (10)  NULL,
    [Nombre]                   VARCHAR (100) NULL,
    [CodigoNivelJerarquico]    VARCHAR (5)   NULL,
    [ResolucionAnterior]       VARCHAR (100) NULL,
    [ResolucionActual]         VARCHAR (100) NULL,
    [MinimoHorasLaborales]     INT           NULL,
    [MaximoHorasLaborales]     INT           NULL,
    [SalarioMinimoBasico]      NUMERIC (18)  NULL,
    [SalarioMaximo]            NUMERIC (18)  NULL,
    [CodigoNivelRiesgos]       VARCHAR (5)   NULL,
    [SueldoPorSimilacion]      TINYINT       NULL,
    [ManejaCuadroTurnos]       TINYINT       NULL,
    [AutorizaRecargoNocturnos] TINYINT       NULL,
    [Estado]                   TINYINT       NULL
);
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla de catálogo que define los cargos o puestos de trabajo de la organización de salud. Almacena atributos laborales como rango salarial (mínimo y máximo), horas laborales permitidas, nivel jerárquico y nivel de riesgo asociado al cargo. Incluye indicadores de comportamiento como manejo de cuadros de turnos, autorización de recargos nocturnos y sueldo por similación, junto con las resoluciones normativas que respaldan cada cargo.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Cargos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Cargos';
GO
