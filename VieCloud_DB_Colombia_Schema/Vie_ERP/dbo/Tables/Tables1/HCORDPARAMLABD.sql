CREATE TABLE [dbo].[HCORDPARAMLABD] (
    [ID]              INT                                                                        IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [HCORDPARAMLABID] INT                                                                        NOT NULL,
    [HCPARAMBID]      INT                                                                        NOT NULL,
    [RESULTADO]       VARCHAR (4000) MASKED WITH (FUNCTION = 'partial(0, "Result_Ofuscado", 0)') NULL,
    [RESULTADOID]     INT                                                                        NULL,
    CONSTRAINT [PK_HCORDPARAMLABD] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCORDPARAMLABD_HCORDPARAMLAB] FOREIGN KEY ([HCORDPARAMLABID]) REFERENCES [dbo].[HCORDPARAMLAB] ([ID]),
    CONSTRAINT [FK_HCORDPARAMLABD_HCPARAMB] FOREIGN KEY ([HCPARAMBID]) REFERENCES [dbo].[HCPARAMB] ([ID])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCORDPARAMLABD].[RESULTADO]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la relación con valores predefinidos del resultado paraclínico; INT, clave foránea a catálogo de resultados estandarizados para laboratorio y ambulatorio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPARAMLABD', @level2type = N'COLUMN', @level2name = N'RESULTADOID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo para almecenar el ID de la Relacion. Aplica para Valores Predefinidos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPARAMLABD', @level2type = N'COLUMN', @level2name = N'RESULTADOID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPARAMLABD', @level2type = N'COLUMN', @level2name = N'RESULTADOID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado del examen paraclínico (laboratorio, imagen, procedimiento); VARCHAR(4000) con ofuscación parcial (PII - Result_Ofuscado); almacena valor numérico, texto o interpretación clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPARAMLABD', @level2type = N'COLUMN', @level2name = N'RESULTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Resultado Paraclínico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPARAMLABD', @level2type = N'COLUMN', @level2name = N'RESULTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPARAMLABD', @level2type = N'COLUMN', @level2name = N'RESULTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de relación con tabla de configuración de parámetros paraclínicos ambulatorios (HCPARAMB); INT, FK; vincula a definición del tipo de prueba/examen.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPARAMLABD', @level2type = N'COLUMN', @level2name = N'HCPARAMBID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID relacion con la tabla de configuracion de Paraclinicos Ambulatorios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPARAMLABD', @level2type = N'COLUMN', @level2name = N'HCPARAMBID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPARAMLABD', @level2type = N'COLUMN', @level2name = N'HCPARAMBID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de relación con orden paraclínica de laboratorio (HCORDPARAMLAB); INT, FK; vincula a encabezado de solicitud de examen o estudio diagnóstico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPARAMLABD', @level2type = N'COLUMN', @level2name = N'HCORDPARAMLABID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Relacion con Paraclínico Labioratorio HCORDPARAMLAB', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPARAMLABD', @level2type = N'COLUMN', @level2name = N'HCORDPARAMLABID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPARAMLABD', @level2type = N'COLUMN', @level2name = N'HCORDPARAMLABID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consecutivo/identificador único de detalle de resultado paraclínico; INT IDENTITY(1,1); clave primaria clustered de la línea de resultado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPARAMLABD', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo de la Tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPARAMLABD', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPARAMLABD', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultados de parámetros de laboratorio asociados a órdenes médicas de historia clínica. Registra el valor obtenido para cada parámetro analizado en un examen de laboratorio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPARAMLABD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPARAMLABD';
