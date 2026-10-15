CREATE TABLE [dbo].[NivelJerarquico] (
    [Codigo] VARCHAR (50) NULL,
    [Nombre] VARCHAR (50) NULL,
    [Estado] BIT          NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el estado   1 = Activo  0 = Inactivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NivelJerarquico', @level2type = N'COLUMN', @level2name = N'Estado';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el Nombre', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NivelJerarquico', @level2type = N'COLUMN', @level2name = N'Nombre';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el codigo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NivelJerarquico', @level2type = N'COLUMN', @level2name = N'Codigo';

