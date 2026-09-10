CREATE TABLE [dbo].[HCFICHA607] (
    [ID]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDFICHANOTIFICACION] INT           NOT NULL,
    [CODDIAGNO]           CHAR (4)      NULL,
    [CONTACASO]           INT           NULL,
    [DOSISAPLI]           INT           NULL,
    [FUENTEINFO]          INT           NULL,
    [FIEBRE]              INT           NULL,
    [AMIGDALITIS]         INT           NULL,
    [FARINGITIS]          INT           NULL,
    [LARINGITIS]          INT           NULL,
    [PRESEMEMB]           INT           NULL,
    [COMPLICA]            INT           NULL,
    [TIPOCOMPLI]          INT           NULL,
    [FECHATOMA]           DATE          NULL,
    [FECHARECE]           DATE          NULL,
    [MUESTRA]             VARCHAR (50)  NULL,
    [PRUEBA]              VARCHAR (50)  NULL,
    [AGENTE]              VARCHAR (50)  NULL,
    [RESULTADO]           VARCHAR (50)  NULL,
    [FECHARESUL]          DATE          NULL,
    [VALOR]               VARCHAR (50)  NULL,
    [VERSION]             VARCHAR (20)  NULL,
    [JSON]                VARCHAR (MAX) NULL,
    CONSTRAINT [PK_HCFICHA607] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [CK_HCFICHA607_JSON] CHECK (isjson([JSON])=(1)),
    CONSTRAINT [FK_HCFICHA607_HCFICHANOTIFICACION] FOREIGN KEY ([IDFICHANOTIFICACION]) REFERENCES [dbo].[HCFICHANOTIFICACION] ([ID])
);


GO
ALTER TABLE [dbo].[HCFICHA607] NOCHECK CONSTRAINT [CK_HCFICHA607_JSON];




GO
ALTER TABLE [dbo].[HCFICHA607] NOCHECK CONSTRAINT [CK_HCFICHA607_JSON];


GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Datos adicionales en formato JSON válido. Desde v01_2020-03-06 incluye: CARNET_VACUNACION (bit: 0=No, 1=Sí), TIPO_VACUNA (int: 1=DPT, 2=Pentavalente, 3=TD, 4=Otra), OTRA_VACUNA (string), FECHA_ULTIMA_DOSIS (datetime). VARCHAR(MAX), validado con CHECK isjson().', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607', @level2type = N'COLUMN', @level2name = N'JSON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nuevas columnas en formato JSON    Desde versión "V01_2020-03-06" -->  - CARNET_VACUNACION: bit (0=No, 1=Si)  - TIPO_VACUNA: int ( 1=DPT, 2=Pentavalente, 3=TD, 4=Otra)  - OTRA_VACUNA: string   - FECHA_ULTIMA_DOSIS: datetime    ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607', @level2type = N'COLUMN', @level2name = N'JSON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607', @level2type = N'COLUMN', @level2name = N'JSON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Versión de la ficha de notificación epidemiológica. NULL=primera versión. Formato VARCHAR(20), ej: V01_2020-03-06. Rastreo de cambios en esquema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607', @level2type = N'COLUMN', @level2name = N'VERSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Versión de la ficha de notificación ---> Si es valor nulo = primer versión ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607', @level2type = N'COLUMN', @level2name = N'VERSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607', @level2type = N'COLUMN', @level2name = N'VERSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor cuantitativo o cualitativo del resultado de laboratorio/examen. VARCHAR(50), ej: unidades, rango, interpretación numérica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607', @level2type = N'COLUMN', @level2name = N'VALOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607', @level2type = N'COLUMN', @level2name = N'VALOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607', @level2type = N'COLUMN', @level2name = N'VALOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de generación del resultado de laboratorio o examen diagnóstico (formato dd-mm-aaaa). DATE. Referencia temporal para análisis epidemiológico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607', @level2type = N'COLUMN', @level2name = N'FECHARESUL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'fecha de resultado (dd-mm-aaaa)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607', @level2type = N'COLUMN', @level2name = N'FECHARESUL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607', @level2type = N'COLUMN', @level2name = N'FECHARESUL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado interpretado del examen o prueba diagnóstica. VARCHAR(50), ej: positivo, negativo, reactivo, no reactivo, aislamiento. Hallazgo clínico-laboratorial.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607', @level2type = N'COLUMN', @level2name = N'RESULTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'resultado ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607', @level2type = N'COLUMN', @level2name = N'RESULTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607', @level2type = N'COLUMN', @level2name = N'RESULTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Agente etiológico, patógeno o microorganismo identificado. VARCHAR(50), ej: virus, bacteria, toxina. Componente de vigilancia epidemiológica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607', @level2type = N'COLUMN', @level2name = N'AGENTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'agent', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607', @level2type = N'COLUMN', @level2name = N'AGENTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607', @level2type = N'COLUMN', @level2name = N'AGENTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de prueba o test diagnóstico realizado. VARCHAR(50), ej: cultivo, PCR, antígeno, serología. Método de detección.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607', @level2type = N'COLUMN', @level2name = N'PRUEBA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Prueba', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607', @level2type = N'COLUMN', @level2name = N'PRUEBA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607', @level2type = N'COLUMN', @level2name = N'PRUEBA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de espécimen biológico tomado para análisis. VARCHAR(50), ej: exudado faríngeo, sangre, hisopado. Material de estudio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607', @level2type = N'COLUMN', @level2name = N'MUESTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'muestra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607', @level2type = N'COLUMN', @level2name = N'MUESTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607', @level2type = N'COLUMN', @level2name = N'MUESTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de recepción de la muestra en laboratorio (formato dd-mm-aaaa). DATE. Control de tiempo preanalítico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607', @level2type = N'COLUMN', @level2name = N'FECHARECE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'  Fecha de Recepción (dd-mm-aaaa)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607', @level2type = N'COLUMN', @level2name = N'FECHARECE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607', @level2type = N'COLUMN', @level2name = N'FECHARECE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de toma/recolección de la muestra o examen (formato dd-mm-aaaa). DATE. Momento del evento clínico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607', @level2type = N'COLUMN', @level2name = N'FECHATOMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N' Fecha Toma de Examen (dd-mm-aaaa)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607', @level2type = N'COLUMN', @level2name = N'FECHATOMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607', @level2type = N'COLUMN', @level2name = N'FECHATOMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de complicación en difteria/faringitis. INT: 1=Neurológica, 2=Renal, 3=Cardíaca, 4=Traqueotomía, 5=Otra. Gravedad y secuelas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607', @level2type = N'COLUMN', @level2name = N'TIPOCOMPLI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Complicación  1=Neurológica  2=Renal  3=Cardíaca  4=Traquetomía  5=Otro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607', @level2type = N'COLUMN', @level2name = N'TIPOCOMPLI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607', @level2type = N'COLUMN', @level2name = N'TIPOCOMPLI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Presencia de complicaciones sistémicas. INT: 1=Sí, 2=No, 3=Desconocido. Indicador de severidad clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607', @level2type = N'COLUMN', @level2name = N'COMPLICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'COMPLICA  1=si2=no3=desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607', @level2type = N'COLUMN', @level2name = N'COMPLICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607', @level2type = N'COLUMN', @level2name = N'COMPLICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Presencia de membrana en orofaringe. INT: 1=Sí, 2=No, 3=Desconocido. Hallazgo patognomónico difteria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607', @level2type = N'COLUMN', @level2name = N'PRESEMEMB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'PRESEMEMB  1=si2=no3=desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607', @level2type = N'COLUMN', @level2name = N'PRESEMEMB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607', @level2type = N'COLUMN', @level2name = N'PRESEMEMB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Inflamación de laringe, afectación laríngea. INT: 1=Sí, 2=No, 3=Desconocido. Síntoma respiratorio superior.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607', @level2type = N'COLUMN', @level2name = N'LARINGITIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'LARINGITIS  1=si2=no3=desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607', @level2type = N'COLUMN', @level2name = N'LARINGITIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607', @level2type = N'COLUMN', @level2name = N'LARINGITIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Inflamación de faringe/garganta confirmada. INT: 1=Sí, 2=No, 3=Desconocido. Diagnóstico clínico principal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607', @level2type = N'COLUMN', @level2name = N'FARINGITIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Faringitis  1=si2=no3=desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607', @level2type = N'COLUMN', @level2name = N'FARINGITIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607', @level2type = N'COLUMN', @level2name = N'FARINGITIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Inflamación de amígdalas palatinas. INT: 1=Sí, 2=No, 3=Desconocido. Manifestación orofaríngea.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607', @level2type = N'COLUMN', @level2name = N'AMIGDALITIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Amigdalitis  1=si2=no3=desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607', @level2type = N'COLUMN', @level2name = N'AMIGDALITIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607', @level2type = N'COLUMN', @level2name = N'AMIGDALITIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Presencia de fiebre o temperatura elevada. INT: 1=Sí, 2=No, 3=Desconocido. Síntoma cardinal sistémico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607', @level2type = N'COLUMN', @level2name = N'FIEBRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fiebre  1=si2=no3=desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607', @level2type = N'COLUMN', @level2name = N'FIEBRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607', @level2type = N'COLUMN', @level2name = N'FIEBRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fuente de información de dosis de vacuna antidiftérica (ELIMINADO desde v01_2020-03-06). INT: 1=Carné vacunación, 2=Verbal, 3=PAI Web, 4=Desconocido. Trazabilidad vacunal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607', @level2type = N'COLUMN', @level2name = N'FUENTEINFO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'(campo eliminado desde version V01_2020-03-06)   Fuente de Información de las Dosis :  1=Carné de Vacunación   2=Verbal   3=PAI Web   4=Desconocido ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607', @level2type = N'COLUMN', @level2name = N'FUENTEINFO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607', @level2type = N'COLUMN', @level2name = N'FUENTEINFO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de dosis de vacuna antidiftérica aplicadas. INT: 1=Ninguna, 2=Una, 3=Dos, 4=Tres o más, 5=Primer refuerzo, 6=Segundo refuerzo (desde v01_2020-03-06). Historia de inmunización.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607', @level2type = N'COLUMN', @level2name = N'DOSISAPLI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dosis Aplicadas de Vacuna Antidiftérica:   1 = Ninguna   2 = Una   3 = Dos   4 = Tres o más Dosis   ( 5, 6 --> items nuevos desde version V01_2020-03-06 )  5 = Primer Refuerzo   6 = Segundo Refuerzo   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607', @level2type = N'COLUMN', @level2name = N'DOSISAPLI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607', @level2type = N'COLUMN', @level2name = N'DOSISAPLI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contacto epidemiológico de caso confirmado de difteria. INT: 1=Sí, 2=No, 3=Desconocido. Exposición y rastreo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607', @level2type = N'COLUMN', @level2name = N'CONTACASO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contacto de un Caso Confirmado  1=si  2=no  3=desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607', @level2type = N'COLUMN', @level2name = N'CONTACASO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607', @level2type = N'COLUMN', @level2name = N'CONTACASO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de diagnóstico CIE-10 o nomenclatura. CHAR(4), ej: A36 (difteria). Clasificación diagnóstica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de ficha notificación epidemiológica. INT, FK a HCFICHANOTIFICACION.ID. Relación 1:N con evento notificado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de ficha Notificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico de registro. INT IDENTITY(1,1), PK. Clave primaria de ficha clínica 607.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ficha epidemiológica 607 para notificación de casos de difteria y enfermedades similares. Registra síntomas clínicos (fiebre, amigdalitis, faringitis, laringitis, presencia de membrana), complicaciones, resultados de laboratorio y datos de seguimiento del caso notificado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA607';
