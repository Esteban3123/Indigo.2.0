CREATE TABLE [dbo].[Grupo] (
    [Codigo]                  VARCHAR (10) NULL,
    [CodigoEmpresa]           VARCHAR (50) NULL,
    [CodigoParametrosNomina]  VARCHAR (10) NULL,
    [Nombre]                  VARCHAR (50) NULL,
    [Liquidacion]             TINYINT      NULL,
    [FechaUltimaLiquidacion]  DATE         NULL,
    [FechaProximaLiquidacion] DATE         NULL,
    [Mes]                     TINYINT      NULL,
    [ManejaProvisiones]       TINYINT      NULL,
    [ClasesContrato]          TINYINT      NULL,
    [Estado]                  BIT          NULL
);

