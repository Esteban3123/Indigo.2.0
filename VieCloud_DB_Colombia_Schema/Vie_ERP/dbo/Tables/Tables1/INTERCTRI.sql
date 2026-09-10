CREATE TABLE [dbo].[INTERCTRI] (
    [AUTO]         INT                                                                           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ORDEN_INDIGO] VARCHAR (20)                                                                  NOT NULL,
    [NUMUESTRA]    TINYINT                                                                       NOT NULL,
    [ESTADOINT]    BIT                                                                           NOT NULL,
    [INTERPRET]    VARCHAR (MAX)                                                                 NULL,
    [AUTOLABOR]    INT                                                                           NOT NULL,
    [FECGENERA]    VARCHAR (20)                                                                  NULL,
    [FECREGIST]    DATETIME                                                                      NULL,
    [FECSERIPS]    VARCHAR (20)                                                                  NULL,
    [CODPROSAL]    CHAR (70) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NULL,
    CONSTRAINT [PK_INTERCTRI] PRIMARY KEY CLUSTERED ([AUTO] ASC)
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[INTERCTRI].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Interpretaciones de resultados de laboratorio por orden clínica. Registra el texto interpretativo, el estado y las fechas clave asociadas a cada muestra procesada en el laboratorio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERCTRI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERCTRI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador interno único y autoincremental del registro de interpretación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERCTRI', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERCTRI', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de orden clínica o de laboratorio en el sistema Indigo, identifica la solicitud de examen.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERCTRI', @level2type = N'COLUMN', @level2name = N'ORDEN_INDIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERCTRI', @level2type = N'COLUMN', @level2name = N'ORDEN_INDIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de muestra dentro de la orden; permite distinguir varias muestras asociadas a una misma orden.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERCTRI', @level2type = N'COLUMN', @level2name = N'NUMUESTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERCTRI', @level2type = N'COLUMN', @level2name = N'NUMUESTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de la interpretación: indica si la interpretación está activa o inactiva (sí/no, activo/inactivo).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERCTRI', @level2type = N'COLUMN', @level2name = N'ESTADOINT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERCTRI', @level2type = N'COLUMN', @level2name = N'ESTADOINT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Texto libre con la interpretación clínica o comentario del resultado de laboratorio emitido por el profesional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERCTRI', @level2type = N'COLUMN', @level2name = N'INTERPRET';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERCTRI', @level2type = N'COLUMN', @level2name = N'INTERPRET';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia interna al registro del laboratorio o resultado al que pertenece esta interpretación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERCTRI', @level2type = N'COLUMN', @level2name = N'AUTOLABOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERCTRI', @level2type = N'COLUMN', @level2name = N'AUTOLABOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha en que se generó la interpretación del resultado de laboratorio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERCTRI', @level2type = N'COLUMN', @level2name = N'FECGENERA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERCTRI', @level2type = N'COLUMN', @level2name = N'FECGENERA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora exacta en que se registró la interpretación en el sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERCTRI', @level2type = N'COLUMN', @level2name = N'FECREGIST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERCTRI', @level2type = N'COLUMN', @level2name = N'FECREGIST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del servicio reportada para RIPS, corresponde a la fecha de prestación del servicio de laboratorio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERCTRI', @level2type = N'COLUMN', @level2name = N'FECSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERCTRI', @level2type = N'COLUMN', @level2name = N'FECSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cédula o identificación del profesional de la salud que emitió la interpretación (dato enmascarado, documento del médico o profesional responsable).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERCTRI', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERCTRI', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
