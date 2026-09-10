CREATE TABLE [dbo].[HCEXAENSITD] (
    [ID]            INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [HCEXAENSITCID] INT       NOT NULL,
    [CODSERIPS]     CHAR (20) NOT NULL,
    [TOMEXAMEN]     TINYINT   NOT NULL,
    CONSTRAINT [PK_HCEXAENSITD] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCEXAENSITD_HCEXAENSITC] FOREIGN KEY ([HCEXAENSITCID]) REFERENCES [dbo].[HCEXAENSITC] ([ID]),
    CONSTRAINT [FK_HCEXAENSITD_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de toma de muestra de examen/laboratorio: 1=En sitio de atención, 2=Pregunta interactiva al usuario en momento de recolección, 3=Sin interfaz/automático. Define flujo de captura de especímenes.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXAENSITD', @level2type = N'COLUMN', @level2name = N'TOMEXAMEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Define como toma muestra de examen   1-> En sitio  2-> Pregunta al usuario al momento de hacer la muestra  3-> No hace interfaz ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXAENSITD', @level2type = N'COLUMN', @level2name = N'TOMEXAMEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXAENSITD', @level2type = N'COLUMN', @level2name = N'TOMEXAMEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del servicio IPS en RIPS; referencia a laboratorio clínico (TIPSERIPS=1). Identifica la unidad funcional de laboratorio donde se procesa el examen/análisis.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXAENSITD', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del servicio IPS  Listar solo los laboratorios es decir donde TIPSERIPS = 1 de la tabla INCUPSIPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXAENSITD', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXAENSITD', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cabecera de examen en sitio (FK a HCEXAENSITC). Agrupa detalles de toma de muestras asociados a una orden de examen.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXAENSITD', @level2type = N'COLUMN', @level2name = N'HCEXAENSITCID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXAENSITD', @level2type = N'COLUMN', @level2name = N'HCEXAENSITCID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXAENSITD', @level2type = N'COLUMN', @level2name = N'HCEXAENSITCID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) de cada registro de detalle de toma de examen en sitio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXAENSITD', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXAENSITD', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXAENSITD', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de exámenes o servicios (CUPS) asociados a una situación o configuración de exámenes en la historia clínica. Permite registrar qué procedimientos o pruebas diagnósticas aplican y si se realiza la toma del examen.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXAENSITD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXAENSITD';
