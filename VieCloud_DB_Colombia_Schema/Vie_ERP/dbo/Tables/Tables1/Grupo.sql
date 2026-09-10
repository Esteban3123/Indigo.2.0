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
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla que almacena los grupos de nómina de una empresa, identificados por un código y asociados a una empresa y a parámetros de nómina. Registra información del ciclo de liquidación: fechas de última y próxima liquidación, mes de pago y si el grupo maneja provisiones. Incluye configuración sobre clases de contrato y un estado activo/inactivo.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Grupo';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Grupo';
GO
