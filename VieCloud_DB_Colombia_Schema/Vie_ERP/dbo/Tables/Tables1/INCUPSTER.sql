CREATE TABLE [dbo].[INCUPSTER] (
    [CODSERIPS] CHAR (20)  NOT NULL,
    [DESSERIPS] CHAR (300) NOT NULL,
    CONSTRAINT [PK_INCUPSTER] PRIMARY KEY CLUSTERED ([CODSERIPS] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción del procedimiento o servicio RIPS; texto detallado que identifica la prestación sanitaria, intervención quirúrgica, consulta, examen o atención prestada al paciente en el centro de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSTER', @level2type = N'COLUMN', @level2name = N'DESSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion del Procedimiento o Servicio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSTER', @level2type = N'COLUMN', @level2name = N'DESSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSTER', @level2type = N'COLUMN', @level2name = N'DESSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único RIPS de procedimientos y servicios; identificador estándar que normaliza la codificación de prestaciones sanitarias, procedimientos, consultas, exámenes y atenciones en salud para reportes de facturación y gestión clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSTER', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Unico de Procedimientos y Servicios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSTER', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSTER', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de servicios de salud (CUPS) habilitados en la IPS. Contiene el código y la descripción de cada procedimiento, examen o servicio que puede ser prestado y facturado, usado para RIPS, órdenes médicas y facturación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSTER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSTER';
