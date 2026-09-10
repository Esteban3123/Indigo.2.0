CREATE TABLE [dbo].[HCFICHA230] (
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
    CONSTRAINT [PK_HCFICHA230] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [CK_HCFICHA230_JSON] CHECK (isjson([JSON])=(1)),
    CONSTRAINT [FK_HCFICHA230_HCFICHANOTIFICACION] FOREIGN KEY ([IDFICHANOTIFICACION]) REFERENCES [dbo].[HCFICHANOTIFICACION] ([ID])
);


GO
ALTER TABLE [dbo].[HCFICHA230] NOCHECK CONSTRAINT [CK_HCFICHA230_JSON];




GO
ALTER TABLE [dbo].[HCFICHA230] NOCHECK CONSTRAINT [CK_HCFICHA230_JSON];


GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Datos adicionales en formato JSON (VARCHAR MAX). Desde v01_2020-03-06: carnet_vacunacion (bit: 0=No, 1=Sí), tipo_vacuna (int: 1=DPT, 2=Pentavalente, 3=TD, 4=Otra), otra_vacuna (string), fecha_ultima_dosis (datetime). Validado con CHECK ISJSON.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA230', @level2type = N'COLUMN', @level2name = N'JSON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nuevas columnas en formato JSON    Desde versión "V01_2020-03-06" -->  - CARNET_VACUNACION: bit (0=No, 1=Si)  - TIPO_VACUNA: int ( 1=DPT, 2=Pentavalente, 3=TD, 4=Otra)  - OTRA_VACUNA: string   - FECHA_ULTIMA_DOSIS: datetime    ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA230', @level2type = N'COLUMN', @level2name = N'JSON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA230', @level2type = N'COLUMN', @level2name = N'JSON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Versión de la ficha de notificación epidemiológica (VARCHAR 20). NULL=primera versión. Controla evolución y cambios en el registro de notificación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA230', @level2type = N'COLUMN', @level2name = N'VERSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Versión de la ficha de notificación ---> Si es valor nulo = primer versión ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA230', @level2type = N'COLUMN', @level2name = N'VERSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA230', @level2type = N'COLUMN', @level2name = N'VERSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor numérico o cuantitativo de resultado de laboratorio (VARCHAR 50). Datos de examen, prueba diagnóstica o análisis clínico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA230', @level2type = N'COLUMN', @level2name = N'VALOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor (Datos de laboratorio)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA230', @level2type = N'COLUMN', @level2name = N'VALOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA230', @level2type = N'COLUMN', @level2name = N'VALOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de resultado del examen/laboratorio (DATE, formato dd-mm-aaaa). Fecha en que se entrega diagnóstico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA230', @level2type = N'COLUMN', @level2name = N'FECHARESUL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'fecha de resultado (dd-mm-aaaa)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA230', @level2type = N'COLUMN', @level2name = N'FECHARESUL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA230', @level2type = N'COLUMN', @level2name = N'FECHARESUL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado cualitativo de laboratorio o prueba diagnóstica (VARCHAR 50). Positivo, negativo, inconcluso u otro hallazgo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA230', @level2type = N'COLUMN', @level2name = N'RESULTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'resultado ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA230', @level2type = N'COLUMN', @level2name = N'RESULTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA230', @level2type = N'COLUMN', @level2name = N'RESULTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Agente etiológico identificado en muestra (VARCHAR 50). Microorganismo, virus, bacteria o patógeno causal detectado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA230', @level2type = N'COLUMN', @level2name = N'AGENTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'agent', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA230', @level2type = N'COLUMN', @level2name = N'AGENTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA230', @level2type = N'COLUMN', @level2name = N'AGENTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de prueba diagnóstica realizada (VARCHAR 50). Cultivo, PCR, serología, antígeno, moleculares u otros métodos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA230', @level2type = N'COLUMN', @level2name = N'PRUEBA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Prueba', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA230', @level2type = N'COLUMN', @level2name = N'PRUEBA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA230', @level2type = N'COLUMN', @level2name = N'PRUEBA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de muestra biológica analizada (VARCHAR 50). Sangre, orina, saliva, hisopo, esputo, exudado u otro especimen.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA230', @level2type = N'COLUMN', @level2name = N'MUESTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'muestra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA230', @level2type = N'COLUMN', @level2name = N'MUESTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA230', @level2type = N'COLUMN', @level2name = N'MUESTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de recepción de muestra en laboratorio (DATE, formato dd-mm-aaaa). Ingreso de especimen para procesamiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA230', @level2type = N'COLUMN', @level2name = N'FECHARECE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'  Fecha de Recepción (dd-mm-aaaa)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA230', @level2type = N'COLUMN', @level2name = N'FECHARECE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA230', @level2type = N'COLUMN', @level2name = N'FECHARECE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de toma de muestra/examen al paciente (DATE, formato dd-mm-aaaa). Momento de recolección de especimen.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA230', @level2type = N'COLUMN', @level2name = N'FECHATOMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N' Fecha Toma de Examen (dd-mm-aaaa)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA230', @level2type = N'COLUMN', @level2name = N'FECHATOMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA230', @level2type = N'COLUMN', @level2name = N'FECHATOMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de complicación clínica (INT). 1=Neurológica, 2=Renal, 3=Cardíaca, 4=Traqueotomía, 5=Otra.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA230', @level2type = N'COLUMN', @level2name = N'TIPOCOMPLI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Complicación  1=Neurológica  2=Renal  3=Cardíaca  4=Traquetomía  5=Otro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA230', @level2type = N'COLUMN', @level2name = N'TIPOCOMPLI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA230', @level2type = N'COLUMN', @level2name = N'TIPOCOMPLI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Presencia de complicaciones (INT). 1=Sí, 2=No, 3=Desconocido. Evento adverso durante enfermedad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA230', @level2type = N'COLUMN', @level2name = N'COMPLICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'COMPLICA  1=si2=no3=desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA230', @level2type = N'COLUMN', @level2name = N'COMPLICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA230', @level2type = N'COLUMN', @level2name = N'COMPLICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Presencia de exantema/membrana (INT). 1=Sí, 2=No, 3=Desconocido. Manifestación cutánea o de mucosa.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA230', @level2type = N'COLUMN', @level2name = N'PRESEMEMB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'PRESEMEMB  1=si2=no3=desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA230', @level2type = N'COLUMN', @level2name = N'PRESEMEMB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA230', @level2type = N'COLUMN', @level2name = N'PRESEMEMB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Inflamación de laringe (INT). 1=Sí, 2=No, 3=Desconocido. Síntoma/signo respiratorio superior.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA230', @level2type = N'COLUMN', @level2name = N'LARINGITIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'LARINGITIS  1=si2=no3=desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA230', @level2type = N'COLUMN', @level2name = N'LARINGITIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA230', @level2type = N'COLUMN', @level2name = N'LARINGITIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Inflamación de faringe (INT). 1=Sí, 2=No, 3=Desconocido. Dolor/enrojecimiento de garganta.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA230', @level2type = N'COLUMN', @level2name = N'FARINGITIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Faringitis  1=si2=no3=desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA230', @level2type = N'COLUMN', @level2name = N'FARINGITIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA230', @level2type = N'COLUMN', @level2name = N'FARINGITIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Inflamación de amígdalas (INT). 1=Sí, 2=No, 3=Desconocido. Síntoma orofaríngeo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA230', @level2type = N'COLUMN', @level2name = N'AMIGDALITIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Amigdalitis  1=si2=no3=desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA230', @level2type = N'COLUMN', @level2name = N'AMIGDALITIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA230', @level2type = N'COLUMN', @level2name = N'AMIGDALITIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Presencia de fiebre (INT). 1=Sí, 2=No, 3=Desconocido. Temperatura elevada durante enfermedad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA230', @level2type = N'COLUMN', @level2name = N'FIEBRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fiebre  1=si2=no3=desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA230', @level2type = N'COLUMN', @level2name = N'FIEBRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA230', @level2type = N'COLUMN', @level2name = N'FIEBRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fuente de información de dosis de vacuna (INT, DEPRECATED desde v01_2020-03-06). 1=Carné vacunación, 2=Verbal, 3=PAI Web, 4=Desconocido.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA230', @level2type = N'COLUMN', @level2name = N'FUENTEINFO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'(campo eliminado desde version V01_2020-03-06)   Fuente de Información de las Dosis :  1=Carné de Vacunación   2=Verbal   3=PAI Web   4=Desconocido ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA230', @level2type = N'COLUMN', @level2name = N'FUENTEINFO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA230', @level2type = N'COLUMN', @level2name = N'FUENTEINFO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de dosis de vacuna antidiftérica aplicadas (INT). 1=Ninguna, 2=Una, 3=Dos, 4=Tres+, 5=Primer refuerzo, 6=Segundo refuerzo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA230', @level2type = N'COLUMN', @level2name = N'DOSISAPLI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dosis Aplicadas de Vacuna Antidiftérica:   1 = Ninguna   2 = Una   3 = Dos   4 = Tres o más Dosis   ( 5, 6 --> items nuevos desde version V01_2020-03-06 )  5 = Primer Refuerzo   6 = Segundo Refuerzo   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA230', @level2type = N'COLUMN', @level2name = N'DOSISAPLI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA230', @level2type = N'COLUMN', @level2name = N'DOSISAPLI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contacto epidemiológico con caso confirmado (INT). 1=Sí, 2=No, 3=Desconocido. Exposición a enfermo notificado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA230', @level2type = N'COLUMN', @level2name = N'CONTACASO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contacto de un Caso Confirmado  1=si  2=no  3=desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA230', @level2type = N'COLUMN', @level2name = N'CONTACASO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA230', @level2type = N'COLUMN', @level2name = N'CONTACASO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código CIE/diagnóstico principal (CHAR 4). Clasificación de la enfermedad notificada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA230', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA230', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA230', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de ficha de notificación epidemiológica (INT, FK a HCFICHANOTIFICACION). Referencia al evento de salud pública.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA230', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de ficha Notificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA230', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA230', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincremental (INT, PK). Autonumérico de 1 en 1.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA230', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA230', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA230', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro clínico-epidemiológico de la ficha de notificación 230 (Difteria y enfermedades relacionadas con síntomas faríngeos/laríngeos). Guarda síntomas, diagnóstico CIE-10, información de laboratorio y resultados de muestras tomadas para cada caso notificado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA230';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA230';
