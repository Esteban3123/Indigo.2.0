CREATE TABLE [dbo].[HCORHEMRASTREO] (
    [ID]                 INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [HCORHEMCOID]        INT           NOT NULL,
    [CODIGOBACTERIOLOGO] CHAR (20)     NOT NULL,
    [FECHARASTREO]       DATETIME      NOT NULL,
    [FECHAREGISTRO]      DATETIME      NOT NULL,
    [CODIGOPARAMETRO]    VARCHAR (20)  NULL,
    [NOMBREPARAMETRO]    VARCHAR (100) NULL,
    [RESULTADO]          VARCHAR (100) NULL,
    [VALORREFERENCIA]    VARCHAR (100) NULL,
    [NOMBREBACTERIOLOGO] VARCHAR (150) NULL,
    CONSTRAINT [PK_HCORHEMRASTREO] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCORHEMRASTREO_HCORHEMCO] FOREIGN KEY ([HCORHEMCOID]) REFERENCES [dbo].[HCORHEMCO] ([ID]),
    CONSTRAINT [FK_HCORHEMRASTREO_INPROFSAL] FOREIGN KEY ([CODIGOBACTERIOLOGO]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre completo del profesional bacteriólogo o técnico de laboratorio que realizó el rastreo de anticuerpos; FK a INPROFSAL (profesional de salud)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMRASTREO', @level2type = N'COLUMN', @level2name = N'NOMBREBACTERIOLOGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda el nombre del bacteriologo del rastreo de anticuerpos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMRASTREO', @level2type = N'COLUMN', @level2name = N'NOMBREBACTERIOLOGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMRASTREO', @level2type = N'COLUMN', @level2name = N'NOMBREBACTERIOLOGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de referencia o rango normal esperado para el parámetro en el rastreo de anticuerpos; utilizado para interpretar resultados de serología o inmunología', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMRASTREO', @level2type = N'COLUMN', @level2name = N'VALORREFERENCIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda el valor de referencia del rastreo de anticuerpos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMRASTREO', @level2type = N'COLUMN', @level2name = N'VALORREFERENCIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMRASTREO', @level2type = N'COLUMN', @level2name = N'VALORREFERENCIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado obtenido del rastreo de anticuerpos (ej: positivo, negativo, cuantitativo); hallazgo principal del examen de laboratorio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMRASTREO', @level2type = N'COLUMN', @level2name = N'RESULTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda el resultado del rastreo de anticuerpos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMRASTREO', @level2type = N'COLUMN', @level2name = N'RESULTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMRASTREO', @level2type = N'COLUMN', @level2name = N'RESULTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre descriptivo del parámetro analizado en rastreo de anticuerpos (ej: anticuerpo IgG, IgM, antígeno); VARCHAR(100)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMRASTREO', @level2type = N'COLUMN', @level2name = N'NOMBREPARAMETRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda el nombre del parametro del rastreo de anticuerpos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMRASTREO', @level2type = N'COLUMN', @level2name = N'NOMBREPARAMETRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMRASTREO', @level2type = N'COLUMN', @level2name = N'NOMBREPARAMETRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador del parámetro del rastreo de anticuerpos; referencia a catálogo de pruebas inmunológicas o serológicas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMRASTREO', @level2type = N'COLUMN', @level2name = N'CODIGOPARAMETRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda el código del parametro del rastreo de anticuerpos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMRASTREO', @level2type = N'COLUMN', @level2name = N'CODIGOPARAMETRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMRASTREO', @level2type = N'COLUMN', @level2name = N'CODIGOPARAMETRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación/ingreso del registro en el sistema; timestamp de auditoría de rastreo de anticuerpos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMRASTREO', @level2type = N'COLUMN', @level2name = N'FECHAREGISTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creacion del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMRASTREO', @level2type = N'COLUMN', @level2name = N'FECHAREGISTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMRASTREO', @level2type = N'COLUMN', @level2name = N'FECHAREGISTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que se realizó efectivamente el rastreo de anticuerpos en el laboratorio; fecha de ejecución del examen', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMRASTREO', @level2type = N'COLUMN', @level2name = N'FECHARASTREO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de realizacion del rastreo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMRASTREO', @level2type = N'COLUMN', @level2name = N'FECHARASTREO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMRASTREO', @level2type = N'COLUMN', @level2name = N'FECHARASTREO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código profesional/documento del bacteriólogo o técnico que ejecutó el rastreo de anticuerpos; FK a INPROFSAL.CODPROSAL', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMRASTREO', @level2type = N'COLUMN', @level2name = N'CODIGOBACTERIOLOGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'codigo profesional que hace el rastreo de anticuerpos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMRASTREO', @level2type = N'COLUMN', @level2name = N'CODIGOBACTERIOLOGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMRASTREO', @level2type = N'COLUMN', @level2name = N'CODIGOBACTERIOLOGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (ID) de la orden de laboratorio hemato-serológica asociada; FK a HCORHEMCO; agrupa rastreos de anticuerpos por solicitud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMRASTREO', @level2type = N'COLUMN', @level2name = N'HCORHEMCOID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la Orden  (HCRODHEMCO)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMRASTREO', @level2type = N'COLUMN', @level2name = N'HCORHEMCOID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMRASTREO', @level2type = N'COLUMN', @level2name = N'HCORHEMCOID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementable (IDENTITY) del registro de rastreo de anticuerpos; clave primaria de auditoría', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMRASTREO', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMRASTREO', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMRASTREO', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Rastreo de resultados de hemocultivos: registra los parámetros microbiológicos analizados por el bacteriólogo para cada hemocultivo, incluyendo el resultado obtenido y los valores de referencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMRASTREO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMRASTREO';
