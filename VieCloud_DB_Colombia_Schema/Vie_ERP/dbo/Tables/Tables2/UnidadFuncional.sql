CREATE TABLE [dbo].[UnidadFuncional] (
    [Codigo]            VARCHAR (10)  NULL,
    [Nombre]            VARCHAR (100) NULL,
    [CodigoCentroCosto] VARCHAR (10)  NULL,
    [CodigoSucursal]    VARCHAR (10)  NULL
);
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Catálogo de unidades funcionales de la organización, donde cada registro asocia una unidad con su nombre descriptivo, un centro de costo y una sucursal. Sirve como tabla de referencia para vincular áreas operativas o asistenciales con su estructura financiera y geográfica dentro del sistema.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'UnidadFuncional';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'UnidadFuncional';
GO
