CREATE TABLE [Inventory].[RequestParamDetailPeriodicity] (
    [Id]              INT          IDENTITY (1, 1) NOT NULL,
    [IdRequestParam]  INT          NOT NULL,
    [Month]           TINYINT      NULL,
    [SequenceOfMonth] VARCHAR (50) NULL,
    [Days]            VARCHAR (50) NULL,
    CONSTRAINT [PK_RequestParamDetailPeriodicity] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RequestParamDetailPeriodicity_RequestParam] FOREIGN KEY ([IdRequestParam]) REFERENCES [Inventory].[RequestParam] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Día o días seleccionados del mes para la periodicidad de solicitud de inventario (VARCHAR 50, ej: ''''1,15,28'''')', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParamDetailPeriodicity', @level2type = N'COLUMN', @level2name = N'Days';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Día seleccionado', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParamDetailPeriodicity', @level2type = N'COLUMN', @level2name = N'Days';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParamDetailPeriodicity', @level2type = N'COLUMN', @level2name = N'Days';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Secuencia u orden del día dentro del mes para ejecución de solicitud (1era, 2da, 3era semana; VARCHAR 50)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParamDetailPeriodicity', @level2type = N'COLUMN', @level2name = N'SequenceOfMonth';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Secuencia del día en el mes', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParamDetailPeriodicity', @level2type = N'COLUMN', @level2name = N'SequenceOfMonth';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParamDetailPeriodicity', @level2type = N'COLUMN', @level2name = N'SequenceOfMonth';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Mes del año seleccionado para la periodicidad (TINYINT 1-12, enero a diciembre)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParamDetailPeriodicity', @level2type = N'COLUMN', @level2name = N'Month';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mes del año seleccionado', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParamDetailPeriodicity', @level2type = N'COLUMN', @level2name = N'Month';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParamDetailPeriodicity', @level2type = N'COLUMN', @level2name = N'Month';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la solicitud del parámetro de inventario (FK a Inventory.RequestParam.Id, INT)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParamDetailPeriodicity', @level2type = N'COLUMN', @level2name = N'IdRequestParam';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la solictud del parametro', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParamDetailPeriodicity', @level2type = N'COLUMN', @level2name = N'IdRequestParam';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParamDetailPeriodicity', @level2type = N'COLUMN', @level2name = N'IdRequestParam';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincremental de la periodicidad de solicitud (INT IDENTITY, PK)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParamDetailPeriodicity', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParamDetailPeriodicity', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParamDetailPeriodicity', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Periodicidad de los parámetros de solicitud de inventario: define en qué meses, secuencia dentro del mes y días específicos se deben ejecutar o renovar las solicitudes de reabastecimiento o pedidos de inventario.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParamDetailPeriodicity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParamDetailPeriodicity';
