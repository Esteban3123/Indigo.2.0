CREATE TABLE [dbo].[HCREGMONI] (
    [IDREGISANES] INT           NOT NULL,
    [CODCATESUB]  VARCHAR (102) NOT NULL,
    [HORAREGIST]  CHAR (5)      NOT NULL,
    [VALOR]       CHAR (10)     NOT NULL,
    [CODCATEGO]   CHAR (2)      NOT NULL,
    [CODSUBCATE]  VARCHAR (102) NOT NULL,
    [ORDREGANE]   INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [SURGERYDATE] DATETIME      NULL,
    CONSTRAINT [PK_HCREGMONI] PRIMARY KEY CLUSTERED ([IDREGISANES] ASC, [CODCATESUB] ASC, [HORAREGIST] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de la cirugía/procedimiento quirúrgico. Utilizado para ordenar y filtrar registros de monitoreo anestésico en reportes quirúrgicos. Tipo: DATETIME, Nullable.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGMONI', @level2type = N'COLUMN', @level2name = N'SURGERYDATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de la Cirugia y se usa para listar de manera correcta la informacion en el reporte         ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGMONI', @level2type = N'COLUMN', @level2name = N'SURGERYDATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGMONI', @level2type = N'COLUMN', @level2name = N'SURGERYDATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número secuencial de orden del registro de anestesia. Identificador único auto-incremental para trazabilidad de registros de monitoreo. Tipo: INT Identity.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGMONI', @level2type = N'COLUMN', @level2name = N'ORDREGANE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Orden registro anestesia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGMONI', @level2type = N'COLUMN', @level2name = N'ORDREGANE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGMONI', @level2type = N'COLUMN', @level2name = N'ORDREGANE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la subcategoría de monitoreo anestésico (ej: parámetros vitales, fármacos, eventos). Tipo: VARCHAR(102), Clave primaria compuesta.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGMONI', @level2type = N'COLUMN', @level2name = N'CODSUBCATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Subcategoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGMONI', @level2type = N'COLUMN', @level2name = N'CODSUBCATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGMONI', @level2type = N'COLUMN', @level2name = N'CODSUBCATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la categoría principal de monitoreo (ej: vital, farmacológico, incidentes). Tipo: CHAR(2), clasificador de tipo de registro.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGMONI', @level2type = N'COLUMN', @level2name = N'CODCATEGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Categoria ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGMONI', @level2type = N'COLUMN', @level2name = N'CODCATEGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGMONI', @level2type = N'COLUMN', @level2name = N'CODCATEGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor numérico o alfanumérico del parámetro monitorizado (ej: frecuencia cardíaca, presión, dosis). Tipo: CHAR(10).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGMONI', @level2type = N'COLUMN', @level2name = N'VALOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de la Subcategoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGMONI', @level2type = N'COLUMN', @level2name = N'VALOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGMONI', @level2type = N'COLUMN', @level2name = N'VALOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora y minuto del registro de monitoreo anestésico (formato HH:MM). Tipo: CHAR(5), Clave primaria compuesta, crítico para secuencia temporal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGMONI', @level2type = N'COLUMN', @level2name = N'HORAREGIST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha y Hora del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGMONI', @level2type = N'COLUMN', @level2name = N'HORAREGIST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGMONI', @level2type = N'COLUMN', @level2name = N'HORAREGIST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código combinado de categoría y subcategoría de monitoreo. Identifica el tipo específico de parámetro monitoreado. Tipo: VARCHAR(102), Clave primaria compuesta, FK referencial.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGMONI', @level2type = N'COLUMN', @level2name = N'CODCATESUB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Categoria y Subcategoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGMONI', @level2type = N'COLUMN', @level2name = N'CODCATESUB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGMONI', @level2type = N'COLUMN', @level2name = N'CODCATESUB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del registro de anestesia (cabecera). Vincula registros de monitoreo a la anestesia principal. Tipo: INT, Clave primaria compuesta, FK a tabla HCREGISANES.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGMONI', @level2type = N'COLUMN', @level2name = N'IDREGISANES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero Registrado en la Cabecera de Anestesia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGMONI', @level2type = N'COLUMN', @level2name = N'IDREGISANES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGMONI', @level2type = N'COLUMN', @level2name = N'IDREGISANES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registros de monitoreo y anestesia de Historia Clínica: guarda los valores tomados durante el seguimiento intraoperatorio o de signos vitales del paciente, organizados por categoría, subcategoría y hora de registro, incluyendo la fecha de cirugía asociada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGMONI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGMONI';
