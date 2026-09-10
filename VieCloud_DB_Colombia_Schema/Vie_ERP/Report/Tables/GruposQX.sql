CREATE TABLE [Report].[GruposQX] (
    [Id]          INT           NOT NULL,
    [Code]        VARCHAR (20)  NULL,
    [Description] VARCHAR (300) NULL,
    [Grupoqx]     INT           NULL,
    [PuntosSMLVD] DECIMAL (18)  NULL,
    CONSTRAINT [PK__GruposQX__3214EC0702F6AA2D] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda los puntos SMLVD', @level0type = N'SCHEMA', @level0name = N'Report', @level1type = N'TABLE', @level1name = N'GruposQX', @level2type = N'COLUMN', @level2name = N'PuntosSMLVD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Grupo quirurgico', @level0type = N'SCHEMA', @level0name = N'Report', @level1type = N'TABLE', @level1name = N'GruposQX', @level2type = N'COLUMN', @level2name = N'Grupoqx';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción', @level0type = N'SCHEMA', @level0name = N'Report', @level1type = N'TABLE', @level1name = N'GruposQX', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código', @level0type = N'SCHEMA', @level0name = N'Report', @level1type = N'TABLE', @level1name = N'GruposQX', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'Report', @level1type = N'TABLE', @level1name = N'GruposQX', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla de referencia que almacena los grupos quirúrgicos utilizados en reportes. Cada registro asocia un código y descripción a un grupo quirúrgico específico, junto con los puntos equivalentes en Salario Mínimo Legal Vigente Diario (SMLVD), valor usado típicamente para tarifación o liquidación de procedimientos quirúrgicos en el contexto del sistema de salud colombiano.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'TABLE', @level1name=N'GruposQX';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'TABLE', @level1name=N'GruposQX';
GO
