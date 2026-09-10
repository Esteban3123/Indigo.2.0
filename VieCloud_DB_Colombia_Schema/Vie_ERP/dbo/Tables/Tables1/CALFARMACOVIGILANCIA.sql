CREATE TABLE [dbo].[CALFARMACOVIGILANCIA] (
    [ID]                  INT                                                                            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDCALREPORTE]        INT                                                                            NOT NULL,
    [FECHANACIMIENTO]     DATE MASKED WITH (FUNCTION = 'default()')                                      NOT NULL,
    [ORIGENREPORTE]       CHAR (20)                                                                      NOT NULL,
    [NOMBREINSTITUCION]   VARCHAR (100)                                                                  NOT NULL,
    [CODIGOPNF]           VARCHAR (15)                                                                   NOT NULL,
    [NOMBREREPORTANTE]    VARCHAR (100)                                                                  NOT NULL,
    [PROFESIONREPORTANTE] CHAR (5)                                                                       NOT NULL,
    [CORREOREPORTANTE]    VARCHAR (100)                                                                  NOT NULL,
    [PESO]                NUMERIC (18, 3)                                                                NOT NULL,
    [TALLA]               INT                                                                            NOT NULL,
    [CODDIAGNO]           CHAR (4)                                                                       NOT NULL,
    [TITULARREGISTRO]     VARCHAR (100)                                                                  NOT NULL,
    [NOMBRECOMERCIAL]     VARCHAR (100)                                                                  NOT NULL,
    [REGISTROSANITARIO]   VARCHAR (100)                                                                  NULL,
    [LOTE]                VARCHAR (15)                                                                   NULL,
    [FECHAINICIOEVENTO]   DATE                                                                           NOT NULL,
    [DESCRIPCION]         VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "Description_Ofuscado", 0)') NOT NULL,
    [DESRECUPSINSECUELAS] BIT                                                                            NULL,
    [DESRECUPCONSECUELAS] BIT                                                                            NULL,
    [DESRECUPRESOLVIENDO] BIT                                                                            NULL,
    [DESNORECUPERADO]     BIT                                                                            NULL,
    [DESFATAL]            BIT                                                                            NULL,
    [DESDESCONOCIDO]      BIT                                                                            NULL,
    [SERIPRODUJO]         BIT                                                                            NULL,
    [SERIADNOMALIA]       BIT                                                                            NULL,
    [AMENAZAVIDA]         BIT                                                                            NULL,
    [SERIAMENAZAMUERTE]   BIT                                                                            NULL,
    [SERIPRODUJODISCAPAC] BIT                                                                            NULL,
    [EVENTODESPUES]       INT                                                                            NOT NULL,
    [OTROSFACTORES]       INT                                                                            NOT NULL,
    [EVENTODESAPARE]      INT                                                                            NOT NULL,
    [PACIENTEMISMAREACC]  INT                                                                            NOT NULL,
    [AMPLIARINFORMAC]     INT                                                                            NOT NULL,
    [FECHAMUERTE]         DATE                                                                           NULL,
    CONSTRAINT [PK_CALFARMACOVIGILANCIA] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_CALFARMACOVIGILANCIA_ADACTIVID] FOREIGN KEY ([PROFESIONREPORTANTE]) REFERENCES [dbo].[ADACTIVID] ([codactivi]),
    CONSTRAINT [FK_CALFARMACOVIGILANCIA_INDIAGNOS] FOREIGN KEY ([CODDIAGNO]) REFERENCES [dbo].[INDIAGNOS] ([CODDIAGNO])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[CALFARMACOVIGILANCIA].[FECHANACIMIENTO]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'Date of Birth');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[CALFARMACOVIGILANCIA].[DESCRIPCION]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de defunción del paciente por evento adverso (DATE, PII_Ofuscado). Usado en farmacovigilancia para clasificar desenlaces fatales.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'FECHAMUERTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de la muerte ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'FECHAMUERTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'FECHAMUERTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'¿Se requiere ampliar información del reporte? (INT: 1=Sí, 2=No, 3=No sabe). Indicador de completitud del evento adverso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'AMPLIARINFORMAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1= Si   2= No   3= NO sabe ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'AMPLIARINFORMAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'AMPLIARINFORMAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'¿Paciente presentó la misma reacción adversa previamente? (INT: 1=Sí, 2=No, 3=No sabe). Antecedente de sensibilización.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'PACIENTEMISMAREACC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1= Si   2= No   3= NO sabe ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'PACIENTEMISMAREACC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'PACIENTEMISMAREACC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'¿El evento adverso desapareció al suspender el fármaco? (INT: 1=Sí, 2=No, 3=No sabe). Criterio de causalidad farmacovigilancia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'EVENTODESAPARE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1= Si   2= No   3= NO sabe ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'EVENTODESAPARE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'EVENTODESAPARE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'¿Existieron otros factores que contribuyeron al evento? (INT: 1=Sí, 2=No, 3=No sabe). Indicador de multicausalidad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'OTROSFACTORES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1= Si   2= No   3= NO sabe ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'OTROSFACTORES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'OTROSFACTORES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'¿Reaparece el evento al reiniciar el fármaco? (INT: 1=Sí, 2=No, 3=No sabe). Dechallenge/rechallenge farmacovigilancia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'EVENTODESPUES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1= Si   2= No   3= NO sabe ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'EVENTODESPUES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'EVENTODESPUES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Seriedad: ¿Evento produjo discapacidad o incapacidad permanente? (BIT, 0/1). Clasificación CIM-10 de severidad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'SERIPRODUJODISCAPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Seriedad Produjo discapacidad o incapacidad permanente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'SERIPRODUJODISCAPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'SERIPRODUJODISCAPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Seriedad: ¿Evento causó muerte del paciente? (BIT, 0/1). Reacción adversa fatal, clasificación crítica RIPS.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'SERIAMENAZAMUERTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Seriedad Muerte', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'SERIAMENAZAMUERTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'SERIAMENAZAMUERTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Seriedad: ¿Evento puso en riesgo la vida del paciente? (BIT, 0/1). Evento life-threatening, requiere notificación inmediata.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'AMENAZAVIDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Seriedad amenaza de vida ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'AMENAZAVIDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'AMENAZAVIDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Seriedad: ¿Evento produjo anomalía congénita? (BIT, 0/1). Teratogenicidad detectada en farmacovigilancia pediátrica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'SERIADNOMALIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Seriedad Anomalia congenita', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'SERIADNOMALIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'SERIADNOMALIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Seriedad: ¿Evento produjo o prolongó hospitalización? (BIT, 0/1). Evento adverso que requirió internación o extendió estancia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'SERIPRODUJO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Seriedad Produjo o prolongo hospitalizacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'SERIPRODUJO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'SERIPRODUJO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Desenlace del evento: Estado desconocido o sin seguimiento (BIT, 0/1). Pérdida de contacto con paciente post-evento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'DESDESCONOCIDO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Desenlace del evento Desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'DESDESCONOCIDO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'DESDESCONOCIDO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Desenlace del evento: Muerte relacionada (BIT, 0/1). Evento adverso con resultado letal documentado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'DESFATAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Desenlace del evento Fatal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'DESFATAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'DESFATAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Desenlace del evento: No recuperado / No resuelto (BIT, 0/1). Persistencia de síntomas o secuelas no solucionadas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'DESNORECUPERADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Desenlace del evento No recuperado / No resuelto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'DESNORECUPERADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'DESNORECUPERADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Desenlace del evento: Recuperándose / En resolución (BIT, 0/1). Remisión parcial o gradual de síntomas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'DESRECUPRESOLVIENDO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Desenlace del evento Recuperado / Resolviendo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'DESRECUPRESOLVIENDO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'DESRECUPRESOLVIENDO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Desenlace del evento: Recuperado con secuelas permanentes (BIT, 0/1). Remisión del evento pero con consecuencias residuales.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'DESRECUPCONSECUELAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Desenlace del evento Recuperado / Resuelto Con secuelas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'DESRECUPCONSECUELAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'DESRECUPCONSECUELAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Desenlace del evento: Recuperado completamente sin secuelas (BIT, 0/1). Resolución total y normal del evento adverso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'DESRECUPSINSECUELAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Desenlace del evento Recuperado / Resuelto sin secuelas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'DESRECUPSINSECUELAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'DESRECUPSINSECUELAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción completa y análisis narrativo del evento adverso, incluyendo presentación clínica, intervenciones y evolución (VARCHAR_MAX, PII_Ofuscado_Parcial). Texto libre para búsqueda semántica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'DESCRIPCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion y analisis del evento adverso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'DESCRIPCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'DESCRIPCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de inicio o aparición del evento adverso/reacción no deseada (DATE). Referencia temporal crítica para causalidad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'FECHAINICIOEVENTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de inico del evento adverso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'FECHAINICIOEVENTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'FECHAINICIOEVENTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de lote del medicamento implicado (VARCHAR 15). Trazabilidad de fabricación para alertas de lotes comprometidos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'LOTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Lote ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'LOTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'LOTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de registro sanitario o INVIMA del medicamento (VARCHAR 100). Identificador oficial del producto farmacéutico comercializado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'REGISTROSANITARIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Registro sanitario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'REGISTROSANITARIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'REGISTROSANITARIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre comercial del medicamento que causó el evento (VARCHAR 100). Denominación de venta disponible en farmacias.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'NOMBRECOMERCIAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre comercial', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'NOMBRECOMERCIAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'NOMBRECOMERCIAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Titular del registro sanitario (laboratorio fabricante/distribuidor) (VARCHAR 100). Responsable legal del producto farmacéutico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'TITULARREGISTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Titular del registro sanitario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'TITULARREGISTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'TITULARREGISTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del diagnóstico principal del paciente (CHAR 4, FK→INDIAGNOS). CIE-10 u otra codificación diagnóstica estándar.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo diagnostico principal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Altura del paciente en centímetros (INT). Parámetro antropométrico para dosificación y análisis de severidad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'TALLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Talla en CM', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'TALLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'TALLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Peso del paciente en kilogramos (NUMERIC 18,3). Dato crítico para ajuste de dosis y farmacocinética.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'PESO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Peso en KG', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'PESO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'PESO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Correo electrónico institucional del profesional de salud reportante (VARCHAR 100, PII). Contacto para seguimiento de farmacovigilancia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CORREOREPORTANTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Correo electronico institucional del reportante primario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CORREOREPORTANTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CORREOREPORTANTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código profesión/especialidad del reportante primario (CHAR 5). Identificador de rol: médico, enfermero, farmacéutico, paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'PROFESIONREPORTANTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Profesion del reportante primario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'PROFESIONREPORTANTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'PROFESIONREPORTANTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre completo del profesional de salud que reporta (VARCHAR 100, PII_Ofuscado_Recomendado). Trazabilidad de notificador.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'NOMBREREPORTANTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del reportante primario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'NOMBREREPORTANTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'NOMBREREPORTANTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del Programa Nacional de Farmacovigilancia o entidad notificante (VARCHAR 15). Identificador de origen institucional del reporte RIPS.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CODIGOPNF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo PNF', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CODIGOPNF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CODIGOPNF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de la institución de salud donde ocurrió el evento adverso (VARCHAR 100). Centro de atención, hospital, clínica, farmacia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'NOMBREINSTITUCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de la institucion donde ocurrio el evento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'NOMBREINSTITUCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'NOMBREINSTITUCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Origen o fuente del reporte de farmacovigilancia (CHAR 20). Clasificación: espontáneo, literatura, regulatorio, paciente, profesional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'ORIGENREPORTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Origen del Reporte ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'ORIGENREPORTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'ORIGENREPORTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de nacimiento del paciente afectado (DATE, PII_Ofuscado). Cálculo de edad para estratificación de riesgo farmacovigilancia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'FECHANACIMIENTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de nacimiento  del paciente ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'FECHANACIMIENTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'FECHANACIMIENTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del reporte padre de farmacovigilancia (INT, FK). Referencia a cabecera de notificación de evento adverso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'IDCALREPORTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Reporte ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'IDCALREPORTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'IDCALREPORTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único auto-incremental del registro de farmacovigilancia (INT, PK). Clave primaria de la tabla CALFARMACOVIGILANCIA.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registros de farmacovigilancia: reportes de eventos adversos o reacciones a medicamentos presentados por pacientes, incluyendo datos del reportante, del medicamento involucrado, la gravedad del evento y su desenlace clínico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA';
