CREATE TABLE [dbo].[HCFICHA580] (
    [ID]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDFICHANOTIFICACION] INT           NOT NULL,
    [DESPLAZAMIENTO]      BIT           NULL,
    [UBICACION]           CHAR (20)     NULL,
    [SINTODENGUE]         INT           NULL,
    [ESTABLECIMIENTO]     VARCHAR (200) NULL,
    [FIEBRE]              BIT           NULL,
    [CAFALEA]             BIT           NULL,
    [DOLORRECTROO]        BIT           NULL,
    [MIALGIAS]            BIT           NULL,
    [ARTRALGIAS]          BIT           NULL,
    [ERUPCIONRASH]        BIT           NULL,
    [DOLORABDOMINAL]      BIT           NULL,
    [VOMITO]              BIT           NULL,
    [DIARREA]             BIT           NULL,
    [SOMNOLENCIA]         BIT           NULL,
    [HIPOTENSION]         BIT           NULL,
    [HEPATOMEGALIA]       BIT           NULL,
    [HEMORRAGIAS]         BIT           NULL,
    [HIPOTERMIA]          BIT           NULL,
    [AUMENTOHEMAT]        BIT           NULL,
    [CAIDAPLAQUETAS]      BIT           NULL,
    [ACOMULACIONLIQUID]   BIT           NULL,
    [EXTRAVASACION]       BIT           NULL,
    [HEMOCOMPROMISO]      BIT           NULL,
    [SHOCKDENGUE]         BIT           NULL,
    [DAÑOORGANOS]         BIT           NULL,
    [CLASIFICACIONFINAL]  INT           NULL,
    [CONDUCTA]            INT           NULL,
    [TEJIDOS]             BIT           NULL,
    [HIGADO]              BIT           NULL,
    [BRAZO]               BIT           NULL,
    [PULMON]              BIT           NULL,
    [CEREBRO]             BIT           NULL,
    [MIOCARDIO]           BIT           NULL,
    [MEDULA]              BIT           NULL,
    [RINON]               BIT           NULL,
    [FECHATOMA]           DATE          NULL,
    [FECHARECEPCION]      DATE          NULL,
    [MUESTRA]             INT           NULL,
    [PRUEBA]              VARCHAR (2)   NULL,
    [AGENTE]              INT           NULL,
    [RESULTADO]           INT           NULL,
    [FECHARESULTADO]      DATE          NULL,
    [VALORREGISTRADO]     VARCHAR (200) NULL,
    [CODDIAGNO]           CHAR (4)      NULL,
    [VERSION]             VARCHAR (20)  NULL,
    [JSON]                VARCHAR (MAX) NULL,
    CONSTRAINT [PK_HCFICHA580] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [CK_HCFICHA580_JSON] CHECK (isjson([JSON])=(1)),
    CONSTRAINT [FK_HCFICHA580_HCFICHANOTIFICACION] FOREIGN KEY ([IDFICHANOTIFICACION]) REFERENCES [dbo].[HCFICHANOTIFICACION] ([ID])
);


GO
ALTER TABLE [dbo].[HCFICHA580] NOCHECK CONSTRAINT [CK_HCFICHA580_JSON];




GO
ALTER TABLE [dbo].[HCFICHA580] NOCHECK CONSTRAINT [CK_HCFICHA580_JSON];


GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Datos adicionales en formato JSON validado (VARCHAR MAX, constraint ISJSON). Almacena campos dinámicos de la ficha de notificación de dengue.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'JSON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nuevas columnas en formato JSON ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'JSON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'JSON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Versión de la ficha de notificación epidemiológica. NULL indica primera versión; valores posteriores registran modificaciones y auditoría de cambios.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'VERSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Versión de la ficha de notificación ---> Si es valor nulo = primer versión ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'VERSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'VERSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código diagnóstico CIE-10 (4 caracteres). Identifica dengue sin alarma, con alarma o grave. CHAR(4), searchable por diagnóstico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del diagnóstico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor cuantitativo o cualitativo registrado del examen de laboratorio (carga viral, antígenos, títulos de anticuerpos). VARCHAR(200).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'VALORREGISTRADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda el valor registrado del examen (datos de laboratorio)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'VALORREGISTRADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'VALORREGISTRADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de emisión/reporte del resultado del examen de laboratorio. DATE, rastrea temporalidad diagnóstica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'FECHARESULTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda la fecha de resultado del examen (datos de laboratorio)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'FECHARESULTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'FECHARESULTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado del examen de laboratorio (1=Positivo, 2=Negativo, 3=No procesado, 4=Inadecuado, 6=Valor registrado). INT, clasificación de laboratorio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'RESULTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda el resultado del examen ((1- Positivo, 2- Negativo, 3- No procesado, 4- Inadecuado, 6. Valor registrado), datos de laboratorio)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'RESULTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'RESULTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Agente infeccioso identificado. Código 3 denota dengue. INT, vinculado a catálogo de virus/patógenos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'AGENTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda el agente (3. dengue, datos de laboratorio)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'AGENTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'AGENTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de prueba de laboratorio (4=PCR, E0=ELISA NS1, 2=IgM, 3=IgG, 5=Aislamiento viral). VARCHAR(2), metodología diagnóstica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'PRUEBA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda la prueba ((4. PCR,  E0 Elisa NS1, 2. IgM, 3. IgG, 5. Aislamiento viral), datos de laboratorio)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'PRUEBA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'PRUEBA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de muestra biológica (4=Tejido, 13=Suero). INT, especímenes para detección de dengue.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'MUESTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda la muestra tomada del examen ((4. tejido, 13. suero), datos de laboratorio)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'MUESTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'MUESTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de recepción de la muestra en el laboratorio. DATE, trazabilidad pre-analítica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'FECHARECEPCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda la fecha de recepción del examen (datos de laboratorio)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'FECHARECEPCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'FECHARECEPCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de toma/recolección de la muestra biológica. DATE, calcula ventana diagnóstica (PCR, NS1, anticuerpos).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'FECHATOMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda la fecha de toma del examen (datos de laboratorio)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'FECHATOMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'FECHATOMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera (BIT) indicadora de toma de muestra de riñón en caso de mortalidad por dengue. Auditoría de necropsia/autopsia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'RINON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda la muestra tomada del riñon (Checked or Unchecked, en caso de mortalidad por dengue)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'RINON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'RINON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera (BIT) indicadora de toma de muestra de médula ósea en caso de mortalidad por dengue. Análisis post-mortem.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'MEDULA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda la muestra tomada de la medula (Checked or Unchecked, en caso de mortalidad por dengue)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'MEDULA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'MEDULA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera (BIT) indicadora de toma de muestra de miocardio en caso de mortalidad por dengue grave. Causa de muerte.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'MIOCARDIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda la muestra tomada del miocardio (Checked or Unchecked, en caso de mortalidad por dengue)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'MIOCARDIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'MIOCARDIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera (BIT) indicadora de toma de muestra de tejido cerebral en caso de mortalidad por dengue grave. Encefalopatía.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'CEREBRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda la muestra tomada cerebro (Checked or Unchecked, en caso de mortalidad por dengue)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'CEREBRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'CEREBRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera (BIT) indicadora de toma de muestra de pulmón en caso de mortalidad por dengue. Hemorragia pulmonar.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'PULMON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda la muestra tomada del pulmon (Checked or Unchecked, en caso de mortalidad por dengue)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'PULMON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'PULMON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera (BIT) indicadora de toma de muestra de bazo en caso de mortalidad por dengue grave. [Nota: Campo nombrado ''''BRAZO'''' pero ref. bazo en descripción].', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'BRAZO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda la muestra tomada del bazo (Checked or Unchecked, en caso de mortalidad por dengue)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'BRAZO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'BRAZO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera (BIT) indicadora de toma de muestra de hígado en caso de mortalidad por dengue. Hepatomegalia/necrosis.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'HIGADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda la muestra tomada hígado (Checked or Unchecked, en caso de mortalidad por dengue)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'HIGADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'HIGADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera (BIT) indicadora de toma de muestras de tejidos múltiples en caso de mortalidad por dengue. Protocolo de necropsia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'TEJIDOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda la muestra tomada de tejidos (Checked or Unchecked, en caso de mortalidad por dengue)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'TEJIDOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'TEJIDOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Conducta terapéutica recomendada (0=No aplica, 1=Ambulatoria, 2=Hospitalización piso, 3=UCI, 4=Observación, 5=Remisión). INT, manejo clínico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'CONDUCTA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda la 7.2 conducta ((0. no aplica, 1. ambulatoria, 2. hospitalización piso, 3. unidades de cuidados intensivos, 4. observación, 5. remisión para hospitalización), CLASIFICACIÓN FINAL Y ATENCION DEL CASO)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'CONDUCTA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'CONDUCTA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación epidemiológica final del caso (0=No aplica, 1=Dengue sin signos de alarma, 2=Dengue con signos de alarma, 3=Dengue grave). INT, severidad RIPS.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'CLASIFICACIONFINAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda la clasificación final ((0. no aplica, 1. dengue sin signos de alarma, 2. dengue con signos de alarma, 3. dengue grave), CLASIFICACIÓN FINAL Y ATENCION DEL CASO)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'CLASIFICACIONFINAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'CLASIFICACIONFINAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) de daño grave multiorgánico documentado. Marca presencia de dengue grave con falla renal, hepática, neurológica o miocárdica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'DAÑOORGANOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda el daño grave de órganos (dengue grave)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'DAÑOORGANOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'DAÑOORGANOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) de shock hipovolémico por dengue grave. Extravasación masiva de plasma con hipotensión refractaria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'SHOCKDENGUE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda el shock por dengue (Checked or Unchecked, dengue grave)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'SHOCKDENGUE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'SHOCKDENGUE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) de hemorragia con compromiso hemodinámico (sangrado en mucosas, tubo digestivo). Dengue grave.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'HEMOCOMPROMISO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda la hemorragía con compromiso hemodinámico (Checked or Unchecked, dengue grave)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'HEMOCOMPROMISO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'HEMOCOMPROMISO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) de extravasación severa de plasma (ascitis, derrame pleural, pericárdico). Signo de alarma/dengue grave.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'EXTRAVASACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda la extravasación severa de plasma (Checked or Unchecked, dengue grave)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'EXTRAVASACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'EXTRAVASACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) de acumulación de líquidos (ascitis, derrame pleural). Dengue con signo de alarma.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'ACOMULACIONLIQUID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda la acomulación de liquidos "Checked or Unchecked" (dengue con signo de alarma)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'ACOMULACIONLIQUID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'ACOMULACIONLIQUID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) de trombocitopenia progresiva (caída de plaquetas <100.000/µL). Signo de alarma.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'CAIDAPLAQUETAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda la caida de plaquetas (Checked or Unchecked, dengue con signos de alarma)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'CAIDAPLAQUETAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'CAIDAPLAQUETAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) de hemoconcentración (aumento hematocrito >20% respecto basal). Dengue con signo de alarma.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'AUMENTOHEMAT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda el aumento de hematocrito  (Checked or Unchecked, dengue con signos de alarma)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'AUMENTOHEMAT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'AUMENTOHEMAT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) de temperatura corporal <36°C. Signo de alarma en dengue.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'HIPOTERMIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda la hipotermia (Checked or Unchecked, dengue con signos de alarma)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'HIPOTERMIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'HIPOTERMIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) de hemorragias importantes en mucosas (epistaxis, sangrado gingival, hemorroides). Signo de alarma.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'HEMORRAGIAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guara las hemorragias importantes en mucosas (Checked or Unchecked, dengue con signos de alarma)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'HEMORRAGIAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'HEMORRAGIAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) de aumento de tamaño hepático palpable. Signo de alarma en dengue.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'HEPATOMEGALIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'se guarda la hepatomegalia (Checked or Unchecked, dengue con signos de alarma)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'HEPATOMEGALIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'HEPATOMEGALIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) de presión arterial sistólica <90 mmHg. Signo de alarma/dengue grave.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'HIPOTENSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'se guarda la hipotensión (Checked or Unchecked, dengue con signos de alarma)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'HIPOTENSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'HIPOTENSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) de somnolencia o irritabilidad (letargo, cambio mental). Signo de alarma en dengue.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'SOMNOLENCIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda la somnolencia o irritabilidad (Checked or Unchecked, dengue con signos de alarma)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'SOMNOLENCIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'SOMNOLENCIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) de diarrea acuosa. Signo de alarma en dengue.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'DIARREA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda la diarrea (Checked or Unchecked, dengue con signos de alarma)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'DIARREA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'DIARREA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) de vómito persistente (>2 episodios/2 horas). Signo de alarma.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'VOMITO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda el vomito persistente (Checked or Unchecked, dengue con signos de alarma)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'VOMITO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'VOMITO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) de dolor abdominal intenso y continuo persistente. Signo de alarma en dengue.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'DOLORABDOMINAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda el dolor abdominal intenso y continuo, (Checked or Unchecked, dengue con signos de alarma)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'DOLORABDOMINAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'DOLORABDOMINAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) de erupción maculopapular o rash confluente. Síntoma típico dengue sin alarma.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'ERUPCIONRASH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda erupción o rash (Checked or Unchecked, dengue sin signos de alarma)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'ERUPCIONRASH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'ERUPCIONRASH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) de artralgias (dolor articular). Síntoma clásico dengue sin signos de alarma.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'ARTRALGIAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda ARTRALGIAS (Checked or Unchecked, dengue sin signos de alarma)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'ARTRALGIAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'ARTRALGIAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) de mialgias (dolor muscular intenso). Síntoma cardinal dengue sin alarma.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'MIALGIAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda la mialgias (Checked or Unchecked, dengue sin signos de alarma)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'MIALGIAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'MIALGIAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) de dolor retroocular (detrás de los ojos). Síntoma patognomónico dengue sin alarma.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'DOLORRECTROO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda el dolor retroocular (Checked or Unchecked, dengue sin signos de alarma)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'DOLORRECTROO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'DOLORRECTROO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) de cefalea (dolor de cabeza). Síntoma común dengue sin signos de alarma.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'CAFALEA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda cefalea (Checked or Unchecked, dengue sin signo de alarma)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'CAFALEA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'CAFALEA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) de fiebre (temperatura ≥38°C). Síntoma cardinal dengue sin signos de alarma.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'FIEBRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda la fiebre (Checked or Unchecked, dengue sin signos de alarma)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'FIEBRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'FIEBRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del establecimiento educativo o laboral donde se expuso el paciente. VARCHAR(200), vinculado a brote/conglomerado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'ESTABLECIMIENTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda el nombre del establecimiento donde estudia o trabaja  (Checked or Uncheked, datos especificos)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'ESTABLECIMIENTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'ESTABLECIMIENTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedente familiar o conviviente con sintomatología dengue últimos 15 días (1=Sí, 2=No, 3=Desconocido). INT, epidemiología de contactos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'SINTODENGUE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'¿Algún familiar o conviviente ha tenido sintomatología de dengue en los últimos 15 días? (1. si, 2. no, 3. desconocido)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'SINTODENGUE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'SINTODENGUE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código geográfico de desplazamiento (país, departamento, municipio, localidad). CHAR(20), trazabilidad de exposición.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'UBICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda la ubicación "código" al que se desplazo (País, Departamento o municipio)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'UBICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'UBICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) de desplazamiento a zona endémica últimos 15 días antes de síntomas. BIT, factor de riesgo epidemiológico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'DESPLAZAMIENTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda el desplazamiento de los ultimos 15 días (1. si, 2. no)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'DESPLAZAMIENTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'DESPLAZAMIENTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea (FK) a tabla HCFICHANOTIFICACION. INT, vincula notificación epidemiológica padre con detalles clínicos dengue.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relación con la tabla HCFICHANOTIFICACION', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único secuencial (IDENTITY 1,1). INT PK CLUSTERED, llave primaria tabla HCFICHA580.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ficha de notificación obligatoria para casos de dengue (SIVIGILA). Registra los síntomas, signos de alarma, clasificación clínica final, conducta médica, órganos afectados y resultados de laboratorio de cada caso notificado de dengue, dengue grave o dengue con señales de alarma.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580';
