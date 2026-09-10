CREATE TABLE [dbo].[HCFICHA210] (
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
    CONSTRAINT [PK_HCFICHA210] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [CK_HCFICHA210_JSON] CHECK (isjson([JSON])=(1))
);


GO
ALTER TABLE [dbo].[HCFICHA210] NOCHECK CONSTRAINT [CK_HCFICHA210_JSON];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Datos adicionales en formato JSON válido; almacena campos dinámicos o ampliaciones de la ficha de notificación de dengue', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'JSON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nuevas columnas en formato JSON', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'JSON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'JSON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Versión de control de la ficha de notificación epidemiológica; null indica primera versión, incrementa en modificaciones posteriores', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'VERSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Versión de la ficha de notificación ---> Si es valor nulo = primer versión ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'VERSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'VERSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de diagnóstico CIE-10 de 4 caracteres; identifica el diagnóstico final o sospecha de dengue', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor registrado manualmente; dato clínico, laboratorial o de seguimiento capturado en texto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'VALORREGISTRADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'valor registrado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'VALORREGISTRADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'VALORREGISTRADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha en que se reportó el resultado de la prueba diagnóstica; campo DATE para auditoría de laboratorio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'FECHARESULTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha resultado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'FECHARESULTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'FECHARESULTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del resultado: 1=positivo, 2=negativo, 3=no procesado, 4=inadecuado, 5=valor registrado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'RESULTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1 = positivo  2= negativo  3=No procesado  4= inadecuado  5=Valor registrado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'RESULTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'RESULTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Agente etiológico identificado; 3=dengue, usado en vigilancia epidemiológica y notificación RIPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'AGENTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'3-Dengue', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'AGENTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'AGENTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de prueba diagnóstica: 4=PCR, E0=Elisa NS1, 2=IgM, 3=IgG, 5=aislamiento viral', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'PRUEBA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'4-PCR  E0-Elisa NS1    2-IgM    3-IgG    5-Aislamiento viral', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'PRUEBA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'PRUEBA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de muestra biológica: 4=tejido, 13=suero; caracteriza el especimen remitido a laboratorio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'MUESTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Muestra  4-Tejido     13-Suero', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'MUESTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'MUESTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de recepción de la muestra en laboratorio; rastreo de cadena de custodia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'FECHARECEPCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha recepción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'FECHARECEPCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'FECHARECEPCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de toma de la muestra biológica del paciente; inicia línea de tiempo diagnóstica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'FECHATOMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de toma', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'FECHATOMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'FECHATOMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador si riñón fue seleccionado como órgano con daño o afectación; true=sí, null=no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'RINON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Muestras  si seleciona  RINON= True si no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'RINON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'RINON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador si médula ósea fue seleccionada como tejido afectado en dengue grave; true=sí, null=no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'MEDULA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Muestras  si seleciona  MEDULA= True si no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'MEDULA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'MEDULA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador si corazón/miocardio muestra compromiso en dengue grave; true=sí, null=no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'MIOCARDIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Muestras  si seleciona  MIOCARDIO= True si no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'MIOCARDIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'MIOCARDIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador si encéfalo/sistema nervioso central presenta daño en dengue grave; true=sí, null=no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'CEREBRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Muestras  si seleciona  CEREBRO= True si no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'CEREBRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'CEREBRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador si pulmón presenta afectación hemorágica o extravasación en dengue grave; true=sí, null=no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'PULMON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Muestras  si seleciona  PULMON= True si no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'PULMON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'PULMON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador si brazo/extremidad superior presenta manifestación hemorrágica; true=sí, null=no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'BRAZO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Muestras  si seleciona  BRAZO= True si no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'BRAZO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'BRAZO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador si hígado muestra hepatomegalia o necrosis en dengue grave; true=sí, null=no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'HIGADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Muestras  si seleciona  HIGADO= True si no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'HIGADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'HIGADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador general si se seleccionó análisis de tejidos en muestra biológica; true=sí, null=no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'TEJIDOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Muestras  si seleciona  TEJIDOS= True si no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'TEJIDOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'TEJIDOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Conducta clínica recomendada: 0=no aplica, 1=ambulatoria, 2=hospitalización piso, 4=observación, 5=remisión, 6=UCI', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'CONDUCTA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Conducta   0=No aplica   1=Ambulatoria   2=Hospitalización piso   4=Observación   5=Remisión para hospitalización   6=Unidad de cuidados intensivos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'CONDUCTA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'CONDUCTA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación epidemiológica final del caso: 0=no aplica, 1=sin signos alarma, 2=con signos alarma, 3=dengue grave', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'CLASIFICACIONFINAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Clasificación final  0=No aplica  1=Dengue sin signos de alarma  2=Dengue con signos de alarma  3=Dengue grave', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'CLASIFICACIONFINAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'CLASIFICACIONFINAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de daño orgánico detectado en dengue grave; true=sí, null=no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'DAÑOORGANOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dengue grave 3  si seleciona  DAÑOORGANOS= True si no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'DAÑOORGANOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'DAÑOORGANOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de síndrome de shock por dengue en caso grave; true=sí, null=no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'SHOCKDENGUE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dengue grave 3  si seleciona  SHOCKDENGUE= True si no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'SHOCKDENGUE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'SHOCKDENGUE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de compromiso hemodinámico con hemorragia importante en dengue grave; true=sí, null=no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'HEMOCOMPROMISO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dengue grave 3  si seleciona  HEMOCOMPROMISO= True si no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'HEMOCOMPROMISO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'HEMOCOMPROMISO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de extravasación plasmática en cavidades serosas en dengue grave; true=sí, null=no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'EXTRAVASACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dengue grave 3  si seleciona  EXTRAVASACION= True si no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'EXTRAVASACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'EXTRAVASACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de acumulación de líquido (efusión pleural, ascitis) en dengue con signos alarma; true=sí, null=no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'ACOMULACIONLIQUID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dengue con signos de alarma 2  si seleciona  ACOMULACIONLIQUID= True si no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'ACOMULACIONLIQUID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'ACOMULACIONLIQUID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de caída rápida de plaquetas (<100k en 24h) en dengue con signos alarma; true=sí, null=no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'CAIDAPLAQUETAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dengue con signos de alarma 2  si seleciona  CAIDAPLAQUETAS= True si no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'CAIDAPLAQUETAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'CAIDAPLAQUETAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de aumento del hematocrito >20% basal en dengue con signos alarma; true=sí, null=no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'AUMENTOHEMAT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dengue con signos de alarma 2  si seleciona  AUMENTOHEMAT= True si no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'AUMENTOHEMAT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'AUMENTOHEMAT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de temperatura corporal <36°C en dengue con signos alarma; true=sí, null=no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'HIPOTERMIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dengue con signos de alarma 2  si seleciona  HIPOTERMIA= True si no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'HIPOTERMIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'HIPOTERMIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de manifestaciones hemorrágicas (petequias, equimosis, sangrado) en dengue con signos alarma; true=sí, null=no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'HEMORRAGIAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dengue con signos de alarma 2  si seleciona  HEMORRAGIAS= True si no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'HEMORRAGIAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'HEMORRAGIAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de aumento del tamaño hepático palpable en dengue con signos alarma; true=sí, null=no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'HEPATOMEGALIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dengue con signos de alarma 2  si seleciona  HEPATOMEGALIA= True si no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'HEPATOMEGALIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'HEPATOMEGALIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de presión arterial sistólica disminuida en dengue con signos alarma; true=sí, null=no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'HIPOTENSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dengue con signos de alarma 2  si seleciona  HIPOTENSION= True si no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'HIPOTENSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'HIPOTENSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de letargia, confusión o alteración del estado mental en dengue con signos alarma; true=sí, null=no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'SOMNOLENCIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dengue con signos de alarma 2  si seleciona  SOMNOLENCIA= True si no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'SOMNOLENCIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'SOMNOLENCIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de deposiciones líquidas frecuentes en dengue con signos alarma; true=sí, null=no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'DIARREA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dengue con signos de alarma 2  si seleciona  DIARREA= True si no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'DIARREA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'DIARREA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de vómito persistente en dengue con signos alarma; true=sí, null=no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'VOMITO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dengue con signos de alarma 2  si seleciona  VOMITO= True si no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'VOMITO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'VOMITO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de dolor abdominal intenso o persistente en dengue con signos alarma; true=sí, null=no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'DOLORABDOMINAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dengue con signos de alarma 2  si seleciona  DOLORABDOMINAL= True si no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'DOLORABDOMINAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'DOLORABDOMINAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de erupción cutánea o exantema típico en dengue sin signos alarma; true=sí, null=no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'ERUPCIONRASH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dengue sin signos de alarma 1  si seleciona    ERUPCIONRASH= True   si no   Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'ERUPCIONRASH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'ERUPCIONRASH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de dolor en articulaciones (artralgia) en dengue sin signos alarma; true=sí, null=no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'ARTRALGIAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dengue sin signos de alarma 1  si seleciona   ARTRALGIAS= True   si no   Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'ARTRALGIAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'ARTRALGIAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de dolor muscular (mialgia) en dengue sin signos alarma; true=sí, null=no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'MIALGIAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dengue sin signos de alarma 1  si seleciona   MIALGIAS= True   si no   Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'MIALGIAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'MIALGIAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de dolor retrorbitario (detrás de los ojos) en dengue sin signos alarma; true=sí, null=no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'DOLORRECTROO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dengue sin signos de alarma 1  si seleciona   DOLORRECTROO= True   si no   Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'DOLORRECTROO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'DOLORRECTROO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de cefalea (dolor de cabeza) en dengue sin signos alarma; true=sí, null=no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'CAFALEA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dengue sin signos de alarma 1  si seleciona   CAFALEA= True   si no   Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'CAFALEA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'CAFALEA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de fiebre (temp >38°C) como síntoma cardinal en dengue sin signos alarma; true=sí, null=no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'FIEBRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dengue sin signos de alarma 1  si seleciona   Fiebre = True   si no   Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'FIEBRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'FIEBRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del establecimiento educativo o laboral donde reside o trabaja el paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'ESTABLECIMIENTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'  Nombre del establecimiento donde estudia o trabaja', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'ESTABLECIMIENTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'ESTABLECIMIENTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Información de contacto epidemiológico: 1=familiar/conviviente con síntomas dengue últimos 15 días, 2=no, 3=desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'SINTODENGUE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Algún familiar o conviviente ha tenido sintomatología  de dengue en los últimos 15 días  1=si     2=no    3=Desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'SINTODENGUE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'SINTODENGUE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Municipio o departamento de desplazamiento del paciente; campo CHAR(20) para rastreo geográfico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'UBICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Municipio / departamento al que se desplazó', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'UBICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'UBICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de viaje o desplazamiento en últimos 15 días previos a síntomas; true=sí, false=no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'DESPLAZAMIENTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Desplazamiento en los últimos 15 días  True = si     false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'DESPLAZAMIENTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'DESPLAZAMIENTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de ficha de notificación epidemiológica relacionada; clave foránea (FK)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de ficha Notificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico (IDENTITY) de registro en tabla HCFICHA210', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ficha de notificación de dengue (formulario 210 del SIVIGILA). Registra los síntomas clínicos, signos de alarma, clasificación final del caso, conducta médica, órganos afectados y resultados de laboratorio para cada paciente notificado con dengue o dengue grave.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210';
