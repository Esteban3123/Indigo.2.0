CREATE TABLE [dbo].[AGENSALDD] (
    [AUTONUMER] INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODCONCEC] INT       NOT NULL,
    [CODSERIPS] CHAR (20) NOT NULL,
    [TIPOPARAM] NCHAR (1) NOT NULL,
    [PRODAUTON] INT       NULL,
    [CONCECREC] INT       NULL,
    CONSTRAINT [PK_AGENSALDD_1] PRIMARY KEY CLUSTERED ([AUTONUMER] ASC),
    CONSTRAINT [FK_AGENSALDD_AGERECURS] FOREIGN KEY ([CONCECREC]) REFERENCES [dbo].[AGERECURS] ([CODCONCEC]),
    CONSTRAINT [FK_AGENSALDD_SOLPRODUC] FOREIGN KEY ([PRODAUTON]) REFERENCES [dbo].[SOLPRODUC] ([PRODAUTON])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consecutivo del recurso (físico o humano) vinculado a la solicitud; FK a AGERECURS.CODCONCEC; identifica equipamiento, infraestructura o personal asignado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALDD', @level2type = N'COLUMN', @level2name = N'CONCECREC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'consecutivo del recurso, ya sea fisico o humano', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALDD', @level2type = N'COLUMN', @level2name = N'CONCECREC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALDD', @level2type = N'COLUMN', @level2name = N'CONCECREC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consecutivo/identificador del producto solicitado; FK a SOLPRODUC.PRODAUTON; vincula medicamento, insumo o bien de la solicitud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALDD', @level2type = N'COLUMN', @level2name = N'PRODAUTON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'consecutivo del producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALDD', @level2type = N'COLUMN', @level2name = N'PRODAUTON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALDD', @level2type = N'COLUMN', @level2name = N'PRODAUTON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de parámetro (H=Humano/personal, F=Físico/equipo-infraestructura, P=Producto/medicamento-insumo); clasificador de naturaleza del recurso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALDD', @level2type = N'COLUMN', @level2name = N'TIPOPARAM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'TIPO PARAMETRO (H- HUMANO, F- FISICO, P- PRODUCTO)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALDD', @level2type = N'COLUMN', @level2name = N'TIPOPARAM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALDD', @level2type = N'COLUMN', @level2name = N'TIPOPARAM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del servicio IPS (Institución Prestadora de Salud); identificador estándar RIPS del servicio de salud prestado o solicitado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALDD', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'CÓDIGO SERVICIO IPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALDD', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALDD', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código consecutivo/identificador único de la solicitud en tabla AGENSALDD; clave descriptiva del registro de asignación de recursos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALDD', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código Consecutivo AGENSALAD', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALDD', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALDD', @level2type = N'COLUMN', @level2name = N'CODCONCEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave primaria, autonumérico identidad SQL Server; generador secuencial único de registros en tabla AGENSALDD.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALDD', @level2type = N'COLUMN', @level2name = N'AUTONUMER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALDD', @level2type = N'COLUMN', @level2name = N'AUTONUMER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALDD', @level2type = N'COLUMN', @level2name = N'AUTONUMER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Parámetros de configuración que relacionan conceptos de facturación con servicios (CUPS) en el módulo de agendamiento de salud, definiendo el tipo de parámetro aplicable a cada combinación de concepto y servicio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALDD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALDD';
