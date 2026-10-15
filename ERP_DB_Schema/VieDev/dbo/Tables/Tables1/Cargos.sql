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

