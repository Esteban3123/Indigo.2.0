CREATE TABLE [dbo].[CONPPRCTP] (
    [IDCONPRPRAC] INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDGRUPUSC]   TINYINT       NOT NULL,
    [CODUSUARI]   CHAR (20)     NOT NULL,
    [CONFCHSOL]   DATETIME      NOT NULL,
    [CONTIPPUN]   INT           NOT NULL,
    [CONVALRES]   INT           NULL,
    [CODUSUREG]   CHAR (20)     NULL,
    [CONCOMENT]   VARCHAR (250) NULL,
    [CONVALIDP]   BIT           NULL,
    [CONFCHPRE]   DATETIME      NULL,
    CONSTRAINT [PK_CONPPRCTP] PRIMARY KEY CLUSTERED ([IDCONPRPRAC] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de presentación/entrega de la prueba de conocimientos prácticos u obsequios (DATETIME)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONPPRCTP', @level2type = N'COLUMN', @level2name = N'CONFCHPRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de presentacion prueba', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONPPRCTP', @level2type = N'COLUMN', @level2name = N'CONFCHPRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONPPRCTP', @level2type = N'COLUMN', @level2name = N'CONFCHPRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano que valida/aprueba la pregunta o respuesta presentada en la prueba (BIT, 0=No válido, 1=Válido)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONPPRCTP', @level2type = N'COLUMN', @level2name = N'CONVALIDP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valida la pregunta presentada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONPPRCTP', @level2type = N'COLUMN', @level2name = N'CONVALIDP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONPPRCTP', @level2type = N'COLUMN', @level2name = N'CONVALIDP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Comentario, observación o nota adicional sobre la respuesta, prueba u obsequio (VARCHAR 250 caracteres)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONPPRCTP', @level2type = N'COLUMN', @level2name = N'CONCOMENT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Comentario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONPPRCTP', @level2type = N'COLUMN', @level2name = N'CONCOMENT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONPPRCTP', @level2type = N'COLUMN', @level2name = N'CONCOMENT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del usuario que realiza el registro, evaluación u otorgamiento del obsequio (CHAR 20, FK probable a usuarios)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONPPRCTP', @level2type = N'COLUMN', @level2name = N'CODUSUREG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo usuario que realiza el obsequio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONPPRCTP', @level2type = N'COLUMN', @level2name = N'CODUSUREG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONPPRCTP', @level2type = N'COLUMN', @level2name = N'CODUSUREG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Puntuación, puntaje o valor numérico asignado por la respuesta correcta u obsequio entregado (INT, nullable)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONPPRCTP', @level2type = N'COLUMN', @level2name = N'CONVALRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Puntuación por la respuesta u obsequio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONPPRCTP', @level2type = N'COLUMN', @level2name = N'CONVALRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONPPRCTP', @level2type = N'COLUMN', @level2name = N'CONVALRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de puntuación: 0=Ejercicio Práctico, 1=Obsequio; categoriza el origen del puntaje (INT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONPPRCTP', @level2type = N'COLUMN', @level2name = N'CONTIPPUN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de puntuacion 0- Ejercicio Practico, 1-Obsequio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONPPRCTP', @level2type = N'COLUMN', @level2name = N'CONTIPPUN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONPPRCTP', @level2type = N'COLUMN', @level2name = N'CONTIPPUN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de solicitud o envío de la prueba de conocimientos prácticos al usuario (DATETIME)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONPPRCTP', @level2type = N'COLUMN', @level2name = N'CONFCHSOL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de solicitud de la prueba', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONPPRCTP', @level2type = N'COLUMN', @level2name = N'CONFCHSOL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONPPRCTP', @level2type = N'COLUMN', @level2name = N'CONFCHSOL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del usuario que solicita o presenta/responde la prueba de conocimientos prácticos (CHAR 20, FK probable a usuarios)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONPPRCTP', @level2type = N'COLUMN', @level2name = N'CODUSUARI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del usuario solicita/Presenta prueba', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONPPRCTP', @level2type = N'COLUMN', @level2name = N'CODUSUARI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONPPRCTP', @level2type = N'COLUMN', @level2name = N'CODUSUARI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del grupo de usuario o código de grupo asociado al registro (TINYINT, FK probable a grupos)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONPPRCTP', @level2type = N'COLUMN', @level2name = N'IDGRUPUSC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo código grupo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONPPRCTP', @level2type = N'COLUMN', @level2name = N'IDGRUPUSC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONPPRCTP', @level2type = N'COLUMN', @level2name = N'IDGRUPUSC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único, autonumérico de la respuesta o resultado de prueba de conocimientos prácticos u obsequios (INT IDENTITY, PK)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONPPRCTP', @level2type = N'COLUMN', @level2name = N'IDCONPRPRAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico respuesta de prueba de concocimientos practicos u obsequios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONPPRCTP', @level2type = N'COLUMN', @level2name = N'IDCONPRPRAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONPPRCTP', @level2type = N'COLUMN', @level2name = N'IDCONPRPRAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de configuraciones de puntos de práctica o preguntas de control por usuario, donde se guarda la puntuación asignada, el resultado obtenido, comentarios de evaluación y si la configuración fue validada o aprobada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONPPRCTP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONPPRCTP';
