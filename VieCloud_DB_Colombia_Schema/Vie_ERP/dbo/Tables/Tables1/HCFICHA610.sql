CREATE TABLE [dbo].[HCFICHA610] (
    [ID]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDFICHANOTIFICACION] INT           NOT NULL,
    [NOMMADREPAC]         VARCHAR (200) NULL,
    [NOMPADREPAC]         VARCHAR (200) NULL,
    [FECHAINVES]          DATE          NULL,
    [DOSISVOP]            NUMERIC (18)  NULL,
    [DOSISCIP]            NUMERIC (18)  NULL,
    [FECHADOSIS]          DATETIME      NULL,
    [CARNET]              INT           NULL,
    [FIEBRE]              INT           NULL,
    [RESPIRATORIO]        INT           NULL,
    [DIGESTIVOS]          INT           NULL,
    [DOLORMUSCULAR]       INT           NULL,
    [SIGNOSMEN]           INT           NULL,
    [FIEBREPARALI]        INT           NULL,
    [INSTALACION]         NUMERIC (18)  NULL,
    [PROGRESION]          INT           NULL,
    [FECHAINIPARA]        DATE          NULL,
    [SUPERIORDERECHO]     BIT           NULL,
    [SUPERIORIZQUIERDO]   BIT           NULL,
    [INFERIORDERECHO]     BIT           NULL,
    [INFERIORIZQUIERDO]   BIT           NULL,
    [MSDPARESIA]          BIT           NULL,
    [MSDPARALISIS]        BIT           NULL,
    [MSDFLACIDA]          BIT           NULL,
    [MSDLOCALIZA]         INT           NULL,
    [MSDSENSIBILIDAD]     INT           NULL,
    [MSDROT]              INT           NULL,
    [MSIPARESIA]          BIT           NULL,
    [MSIPARALISIS]        BIT           NULL,
    [MSIFLACIDA]          BIT           NULL,
    [MSILOCALIZA]         INT           NULL,
    [MSISENSIBILIDAD]     INT           NULL,
    [MSIROT]              INT           NULL,
    [MIDPARECIA]          BIT           NULL,
    [MIDPARALISIS]        BIT           NULL,
    [MIDFLACIDA]          BIT           NULL,
    [MIDLOCALIZA]         INT           NULL,
    [MIDSENSIBILIDAD]     INT           NULL,
    [MIDROT]              INT           NULL,
    [MIZPARESIA]          BIT           NULL,
    [MIZPARALISIS]        BIT           NULL,
    [MIZFLACIDA]          BIT           NULL,
    [MIZLOCALIZA]         INT           NULL,
    [MIZSENSIBILIDAD]     INT           NULL,
    [MIZROT]              INT           NULL,
    [MUSCULOSRESPI]       INT           NULL,
    [SIGNOSMENI2]         INT           NULL,
    [BABINSKY]            INT           NULL,
    [BRUDZINSKY]          INT           NULL,
    [PARESCRANEA]         INT           NULL,
    [LIQUIDOCEFA]         INT           NULL,
    [ELECTROMIO]          INT           NULL,
    [VELOCIDADCOND]       INT           NULL,
    [DIAGNOINICIAL]       VARCHAR (50)  NULL,
    [TOMAMUESTRA]         INT           NULL,
    [FECHATOMA]           DATE          NULL,
    [FECHAENVIO]          DATE          NULL,
    [FECHARECEP]          DATE          NULL,
    [FECHARESULT]         DATE          NULL,
    [RESULTADO]           INT           NULL,
    [FECHAVACUBLO]        DATE          NULL,
    [CASODETEC]           INT           NULL,
    [FECHASEG60]          DATE          NULL,
    [PARALISIS60]         INT           NULL,
    [ARTROFIA60]          INT           NULL,
    [CLASIFINAL]          INT           NULL,
    [FECHACLASI]          DATE          NULL,
    [CRITERIOCLASI]       INT           NULL,
    [DIAGNOFINAL]         VARCHAR (50)  NULL,
    [CODDIAGNO]           CHAR (4)      NULL,
    [VERSION]             VARCHAR (20)  NULL,
    [JSON]                VARCHAR (MAX) NULL,
    CONSTRAINT [PK_HCFICHA610] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [CK_HCFICHA610_JSON] CHECK (isjson([JSON])=(1)),
    CONSTRAINT [FK_HCFICHA610_HCFICHANOTIFICACION] FOREIGN KEY ([IDFICHANOTIFICACION]) REFERENCES [dbo].[HCFICHANOTIFICACION] ([ID])
);


GO
ALTER TABLE [dbo].[HCFICHA610] NOCHECK CONSTRAINT [CK_HCFICHA610_JSON];




GO
ALTER TABLE [dbo].[HCFICHA610] NOCHECK CONSTRAINT [CK_HCFICHA610_JSON];


GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Columnas adicionales en formato JSON VARCHAR(MAX), validado con constraint ISJSON; permite extensibilidad sin ALTER TABLE.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'JSON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nuevas columnas en formato JSON ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'JSON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'JSON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Versión de la ficha epidemiológica de notificación; NULL indica primera versión (v1.0), valores posteriores marcan revisiones del registro.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'VERSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Versión de la ficha de notificación ---> Si es valor nulo = primer versión ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'VERSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'VERSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código CIE-10 de diagnóstico final (CHAR 4); identifica patología confirmada tras investigación epidemiológica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del diagnóstico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Diagnóstico final VARCHAR(50) registrado tras cierre de investigación; confirma o descarta poliovirus, enterovirus, u otra condición.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'DIAGNOFINAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda el diagnostico final', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'DIAGNOFINAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'DIAGNOFINAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Criterio de clasificación epidemiológica (1=laboratorio, 2=perdido seguimiento, 3=defunción, 4=parálisis residual, 5=sin parálisis, 6=otro diagnóstico).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'CRITERIOCLASI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Criterio para la clasificación (SEGUIMIENTO Y CLASIFICACIÓN FINAL, (1. laboratorio, 2. perdido para seguimiento, 3. defunción, 4. con paralisis residual, 5. sin paralisis residual, 6. otro diagnóstico clínico))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'CRITERIOCLASI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'CRITERIOCLASI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha DATE de clasificación epidemiológica final; marca cierre de investigación y seguimiento poliomielitis.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'FECHACLASI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha clasificación (seguimiento y clasificación final)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'FECHACLASI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'FECHACLASI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación final poliovirus (1=salvaje tipo1/2/3, 2=derivado, 3=asociado vacuna, 4=compatible, 5=descartado); categoría epidemiológica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'CLASIFINAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Clasificación final (SEGUIMIENTO Y CLASIFICACIÓN FINAL, (1. polio salvaje, 2. polio derivado, 3. polio asociado, 4. polio compatible, 5. descartado))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'CLASIFINAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'CLASIFINAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Presencia de atrofia muscular a los 60 días post-parálisis (1=sí, 2=no, 3=desconocido); indica secuela neurológica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'ARTROFIA60';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Atrofia a los 60 días (1. si, 2. no, 3. desconocido)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'ARTROFIA60';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'ARTROFIA60';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Parálisis residual a los 60 días post-inicio (1=sí, 2=no, 3=desconocido); determina discapacidad permanente en poliomielitis.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'PARALISIS60';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Parálisis residual a los 60 días (SEGUIMIENTO Y CLASIFICACIÓN FINAL, (1. si, 2. no, 3. desconocido))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'PARALISIS60';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'PARALISIS60';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha DATE de seguimiento clínico a 60 días; evalúa evolución parálisis y recuperación funcional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'FECHASEG60';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha seguimiento a 60 días (SEGUIMIENTO Y CLASIFICACIÓN FINAL)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'FECHASEG60';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'FECHASEG60';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Origen detección caso (1=consulta, 2=laboratorio, 3=búsqueda activa institucional, 4=comunitaria, 5=investigación contactos, 6=comunidad, 7=otros, 8=desconocido).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'CASODETEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Caso detectado por (SEGUIMIENTO Y CLASIFICACIÓN FINAL, (1. consulta, 2. laboratorio, 3. busqueda activa institucional, 4. busqueda activa comunitaria, 5. investigación de contactos, 6. comunidad, 7. otros, 8. desconocido))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'CASODETEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'CASODETEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha DATE de vacunación de bloqueo VOP/VIP; intervención epidemiológica para contener transmisión.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'FECHAVACUBLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de vacunación de bloqueo (Laboratorio)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'FECHAVACUBLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'FECHAVACUBLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado laboratorio poliovirus (1=negativo, 2=enterovirus no-polio, 3=PVS tipo1, 4=PVS tipo2, 5=PVS tipo3, 6=PVV tipo1, 7=PVV tipo2, 8=PVV tipo3, 9=PVDV tipo1).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'RESULTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Resultado (Laboratorio, (1. negativo, 2. enterovirus no polio, 3. poliovirus salvaje tipo 1, 4. poliovirus salvaje tipo 2, 5. poliovirus salvaje tipo 3, 6. poliovirus vacunal tipo 1, 7. poliovirus vacunal tipo 2, 8. poliovirus vacunal tipo 3, 9. poliovirus derivado de vacuna tipo 1))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'RESULTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'RESULTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha DATE resultado laboratorio; confirma o descarta poliovirus en muestra fecal/suero.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'FECHARESULT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de resultado (Laboratorio)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'FECHARESULT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'FECHARESULT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha DATE recepción laboratorio de la muestra; traza cadena custodia epidemiológica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'FECHARECEP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de recepción (Laboratorio)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'FECHARECEP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'FECHARECEP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha DATE envío muestra a laboratorio de referencia; inicia proceso confirmación diagnóstica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'FECHAENVIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de envío (laboratorio)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'FECHAENVIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'FECHAENVIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha DATE de toma de muestra (heces/suero); marcador temporal para análisis epidemiológico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'FECHATOMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de toma  (Laboratorio)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'FECHATOMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'FECHATOMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Realización toma muestra para laboratorio (1=sí, 2=no, 3=desconocido); documenta adherencia protocolo RIPS/notificación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'TOMAMUESTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Toma de muestra (Laboratorio, (1. si, 2. no, 3. desconocido))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'TOMAMUESTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'TOMAMUESTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Impresión diagnóstica inicial VARCHAR(50) del profesional sanitario; hipótesis diagnóstica al primer contacto.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'DIAGNOINICIAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda la impresión diagnostica inicial', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'DIAGNOINICIAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'DIAGNOINICIAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Electromiografía-velocidad conducción realizada (1=sí, 2=no, 3=desconocido); estudio neuroelectrofisiológico para neuropatía.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'VELOCIDADCOND';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Velocidad de conducción (Laboratorio, (1. si, 2. no , 3. desconocido))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'VELOCIDADCOND';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'VELOCIDADCOND';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Electromiografía realizada (1=sí, 2=no, 3=desconocido); examen neurofisiológico confirma lesión neuromotora.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'ELECTROMIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Electromiografia (laboratorio, (1. si, 2. no, 3. desconocido))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'ELECTROMIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'ELECTROMIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Análisis líquido cefalorraquideo realizado (1=sí, 2=no, 3=desconocido); investigación meningitis/encefalitis.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'LIQUIDOCEFA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Líquido cefalorraquideo (Laboratorio, (1. si, 2. no, 3. desconocido))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'LIQUIDOCEFA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'LIQUIDOCEFA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Compromiso de pares craneanos presente (1=sí, 2=no, 3=desconocido); hallazgo clínico neurológico adicional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'PARESCRANEA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Pares craneanos (otros compromisos, (1. si, 2. no, 3. desconocido))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'PARESCRANEA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'PARESCRANEA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Signo de Brudzinsky presente (1=sí, 2=no, 3=desconocido); reflejo meníngeo indicativo de irritación meníngea.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'BRUDZINSKY';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'BRUDZINSKY (6.6 otros compromisos, (1. si, 2. no, 3. desconocido))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'BRUDZINSKY';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'BRUDZINSKY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Signo de Babinsky presente (1=sí, 2=no, 3=desconocido); reflejo patológico de vía piramidal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'BABINSKY';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'BABINSKY (6.6 otros compromisos, (1. si, 2. no, 3. desconocido))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'BABINSKY';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'BABINSKY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Signos meníngeos presentes (1=sí, 2=no, 3=desconocido); rigidez nuca, fotofobia, otros signos de meningismo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'SIGNOSMENI2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Signos meníngeos (Otros compromisos, (1. si, 2. no, 3. desconocido))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'SIGNOSMENI2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'SIGNOSMENI2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Compromiso músculos respiratorios (1=sí, 2=no, 3=desconocido); requiere soporte ventilatorio en poliomielitis bulbar.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'MUSCULOSRESPI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Músculos respiratorios (OTROS COMPROMISOS, (1. si, 2. no, 3. desconocido))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'MUSCULOSRESPI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'MUSCULOSRESPI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'M.inferior izquierdo, reflejo osteotendíneo (1=Normal, 2=Abolido, 3=Disminuido); evaluación neurológica segmentaria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'MIZROT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'M. inferior izquierdo, ROT (cuadro clinico, (1. N, 2. A, 3. D))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'MIZROT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'MIZROT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'M.inferior izquierdo, sensibilidad (1=Normal, 2=Abolida, 3=Disminuida); patrón distribución déficit sensorial.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'MIZSENSIBILIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'M. inferior izquierdo, Sensibiliad (cuadro clinico, (1. N, 2. A, 3. D))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'MIZSENSIBILIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'MIZSENSIBILIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'M.inferior izquierdo, localización parálisis (1=proximal muslo, 2=distal pantorrilla); topografía lesión nerviosa.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'MIZLOCALIZA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'M. inferior izquierdo, Localización (cuadro clinico, (1. proximal, 2. distal))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'MIZLOCALIZA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'MIZLOCALIZA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'M.inferior izquierdo, parálisis flácida presente (1=sí, 2=no); característica típica poliomielitis.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'MIZFLACIDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'M. inferior izquierdo, Flácida (cuadro clinico, (1. si, 2. no))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'MIZFLACIDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'MIZFLACIDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'M.inferior izquierdo, parálisis motora presente (1=sí, 2=no); pérdida movimiento volontario.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'MIZPARALISIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'M. inferior izquierdo, Parálisis (cuadro clinico, (1. si, 2. no))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'MIZPARALISIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'MIZPARALISIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'M.inferior izquierdo, debilidad motora/paresia (1=sí, 2=no); grado incompleto pérdida fuerza.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'MIZPARESIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'M. inferior izquierdo, Paresia Extremidad (cuadro clinico, (1. si, 2. no))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'MIZPARESIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'MIZPARESIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'M.inferior derecho, reflejo osteotendíneo (1=Normal, 2=Abolido, 3=Disminuido); evaluación neurológica segmentaria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'MIDROT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'M. inferior derecho, ROT (cuadro clinico, (1. N, 2. A, 3. D))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'MIDROT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'MIDROT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'M.inferior derecho, sensibilidad (1=Normal, 2=Abolida, 3=Disminuida); patrón distribución déficit sensorial.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'MIDSENSIBILIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'M. inferior derecho, Sensibiliad (cuadro clinico, (1. N, 2. A, 3. D))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'MIDSENSIBILIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'MIDSENSIBILIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'M.inferior derecho, localización parálisis (1=proximal, 2=distal); topografía lesión nerviosa.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'MIDLOCALIZA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'M. inferior derecho, Localización (cuadro clinico, (1. proximal, 2. distal))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'MIDLOCALIZA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'MIDLOCALIZA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'M.inferior derecho, parálisis flácida presente (1=sí, 2=no); característica poliomielitis anterior.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'MIDFLACIDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'M. inferior derecho, Flácida (cuadro clinico, (1. si, 2. no))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'MIDFLACIDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'MIDFLACIDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'M.inferior derecho, parálisis motora presente (1=sí, 2=no); pérdida movimiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'MIDPARALISIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'M. inferior derecho, Parálisis (cuadro clinico, (1. si, 2. no))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'MIDPARALISIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'MIDPARALISIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'M.inferior derecho, debilidad/paresia (1=sí, 2=no); grado incompleto pérdida fuerza muscular.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'MIDPARECIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'M. inferior derecho, Paresia Extremidad (cuadro clinico, (1. si, 2. no))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'MIDPARECIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'MIDPARECIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'M.superior izquierdo, reflejo osteotendíneo (1=Normal, 2=Abolido, 3=Disminuido); evaluación neurológica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'MSIROT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'M. superior izquierdo, ROT (cuadro clinico, (1. N, 2. A, 3. D))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'MSIROT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'MSIROT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'M.superior izquierdo, sensibilidad (1=Normal, 2=Abolida, 3=Disminuida); patrón distribución.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'MSISENSIBILIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'M. superior izquierdo, Sensibiliad (cuadro clinico, (1. N, 2. A, 3. D))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'MSISENSIBILIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'MSISENSIBILIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'M.superior izquierdo, localización parálisis (1=proximal brazo, 2=distal antebrazo/mano); topografía.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'MSILOCALIZA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'M. superior izquierdo, Localización (cuadro clinico, (1. proximal, 2. distal))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'MSILOCALIZA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'MSILOCALIZA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'M.superior izquierdo, parálisis flácida (1=sí, 2=no); característica polioviral.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'MSIFLACIDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'M. superior izquierdo, Flácida (cuadro clinico, (1. si, 2. no))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'MSIFLACIDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'MSIFLACIDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'M.superior izquierdo, parálisis motora (1=sí, 2=no); pérdida movimiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'MSIPARALISIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'M. superior izquierdo, Parálisis (cuadro clinico, (1. si, 2. no))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'MSIPARALISIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'MSIPARALISIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'M.superior izquierdo, debilidad/paresia (1=sí, 2=no); debilitamiento muscular.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'MSIPARESIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'M. superior izquierdo, Paresia Extremidad (cuadro clinico, (1. si, 2. no))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'MSIPARESIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'MSIPARESIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'M.superior derecho, reflejo osteotendíneo (1=Normal, 2=Abolido, 3=Disminuido); evaluación neurológica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'MSDROT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'M. superior derecho, ROT (cuadro clinico, (1. N, 2. A, 3. D))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'MSDROT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'MSDROT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'M.superior derecho, sensibilidad (1=Normal, 2=Abolida, 3=Disminuida); patrón déficit.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'MSDSENSIBILIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'M. superior derecho, Sensibiliad (cuadro clinico, (1. N, 2. A, 3. D))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'MSDSENSIBILIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'MSDSENSIBILIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'M.superior derecho, localización parálisis (1=proximal, 2=distal); topografía lesión.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'MSDLOCALIZA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'M. superior derecho, Localización (cuadro clinico, (1. proximal, 2. distal))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'MSDLOCALIZA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'MSDLOCALIZA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'M.superior derecho, parálisis flácida (1=sí, 2=no); característica poliomielitis.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'MSDFLACIDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'M. superior derecho, Flácida (cuadro clinico, (1. si, 2. no))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'MSDFLACIDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'MSDFLACIDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'M.superior derecho, parálisis motora (1=sí, 2=no); pérdida movimiento voluntario.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'MSDPARALISIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'M. superior derecho, Parálisis (cuadro clinico, (1. si, 2. no))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'MSDPARALISIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'MSDPARALISIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'M.superior derecho, debilidad/paresia (1=sí, 2=no); grado incompleto pérdida fuerza.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'MSDPARESIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'M. superior derecho, Paresia Extremidad (cuadro clinico, (1. si, 2. no))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'MSDPARESIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'MSDPARESIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Extremidad inferior izquierda afectada (BIT, checkbox); marca involucro miembro inferior izquierdo en cuadro clínico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'INFERIORIZQUIERDO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'M. inferior izquierdo (cuadro clinico, (checked or unchecked))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'INFERIORIZQUIERDO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'INFERIORIZQUIERDO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Extremidad inferior derecha afectada (BIT, checkbox); marca involucro miembro inferior derecho.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'INFERIORDERECHO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'M. inferior derecho (cuadro clinico, (checked or unchecked))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'INFERIORDERECHO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'INFERIORDERECHO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Extremidad superior izquierda afectada (BIT, checkbox); marca involucro miembro superior izquierdo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'SUPERIORIZQUIERDO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'M. superior izquierdo (cuadro clinico, (Checked or unchecked))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'SUPERIORIZQUIERDO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'SUPERIORIZQUIERDO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Extremidad superior derecha afectada (BIT, checkbox); marca involucro miembro superior derecho.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'SUPERIORDERECHO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'M. superior derecho (cuadro clinico, (Checked or unchecked))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'SUPERIORDERECHO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'SUPERIORDERECHO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha DATE inicio parálisis; marcador temporal crucial para cálculo instalación y progresión.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'FECHAINIPARA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de inicio de parálisis (cuadro clinico)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'FECHAINIPARA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'FECHAINIPARA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Patrón progresión parálisis (1=ascendente proximal→distal, 2=descendente, 3=indeterminada); caracteriza evolución clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'PROGRESION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Progresión (Cuadro clinico, (1. ascendente, 2. descendente, 3. indeterminada))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'PROGRESION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'PROGRESION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número NUMERIC(18) días desde inicio parálisis hasta máxima intensidad; velocidad progresión clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'INSTALACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Instalación (cuadro clinico, (días comprendidos desde el inicio de la paralisis hasta la maxima intensidad de la misma))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'INSTALACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'INSTALACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fiebre presente al inicio parálisis (1=sí, 2=no, 3=desconocido); síntoma prodrómico poliomielitis.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'FIEBREPARALI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fiebre inicio parálisis (Cuadro clinico, (1. si, 2. no, 3. desconocido))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'FIEBREPARALI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'FIEBREPARALI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Signos meníngeos presentes (1=sí, 2=no, 3=desconocido); rigidez nuca, Kernig, Brudzinsky positivos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'SIGNOSMEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Signos meníngeos (cuadro clinicio, (1. si, 2. no, 3. desconocido))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'SIGNOSMEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'SIGNOSMEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Mialgias/dolor muscular registrado (1=sí, 2=no, 3=desconocido); síntoma fase prodrómica viral.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'DOLORMUSCULAR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda dolor muscular (cuadro clinico, (1. si, 2. no, 3. desconocido))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'DOLORMUSCULAR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'DOLORMUSCULAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Síntomas gastrointestinales presentes (1=sí, 2=no, 3=desconocido); diarrea, vómito en fase viral.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'DIGESTIVOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda los digestivos (cuadro clinico, (1. si, 2. no, 3. desconocido))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'DIGESTIVOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'DIGESTIVOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Síntomas respiratorios presentes (1=sí, 2=no, 3=desconocido); tos, congestión nasal, faringitis.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'RESPIRATORIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Respiratorios (cuadro clinicio, (1. si, 2. no, 3. desconocido))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'RESPIRATORIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'RESPIRATORIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fiebre documentada (1=sí, 2=no, 3=desconocido); temperatura elevada en cuadro clínico inicial.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'FIEBRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'FIEBRE (Cuadro clinico, (1.si, 2. no, 3. desconocido))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'FIEBRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'FIEBRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Posesión carnet vacunación (1=sí, 2=no, 3=desconocido); documenta estado inmunización poliomielitis del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'CARNET';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'¿Tiene carné? (1. si, 2. no, 3. desconocido)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'CARNET';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'CARNET';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha DATETIME última dosis VOP/VIP recibida; antecedente inmunización para cálculo susceptibilidad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'FECHADOSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha última dósis (información general y antecedentes)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'FECHADOSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'FECHADOSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número NUMERIC(18) dosis VIP (inactivada) recibidas; historia inmunización incompleta o completa.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'DOSISCIP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de dosis recibidas VIP (INFORMACIÓN GENERAL Y ANTECEDENTES)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'DOSISCIP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'DOSISCIP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número NUMERIC(18) dosis VOP (oral) recibidas; trazabilidad vacunación oral poliomielitis.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'DOSISVOP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de dosis recibidas VOP (INFORMACIÓN GENERAL Y ANTECEDENTES)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'DOSISVOP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'DOSISVOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha DATE investigación epidemiológica; inicio del proceso investigación caso notificado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'FECHAINVES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de la investigación (INFORMACIÓN GENERAL Y ANTECEDENTES)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'FECHAINVES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'FECHAINVES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre padre del paciente VARCHAR(200); datos filiación PII; permite trazabilidad contactos familiar.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'NOMPADREPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del padre del paciente (INFORMACIÓN GENERAL Y ANTECEDENTES)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'NOMPADREPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'NOMPADREPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre madre del paciente VARCHAR(200); datos filiación PII; contacto familiar para investigación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'NOMMADREPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de la madre del paciente (INFORMACIÓN GENERAL Y ANTECEDENTES)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'NOMMADREPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'NOMMADREPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK INT referencia tabla HCFICHANOTIFICACION; vincula datos específicos poliovirus a notificación epidemiológica general.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tiene relación con la tabla HCFICHANOTIFICACION', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador consecutivo INT IDENTITY(1,1); PK único para registro ficha investigación poliomielitis/PFA.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ficha de notificación epidemiológica 610 para casos de Parálisis Flácida Aguda (PFA) / Poliomielitis. Registra datos de investigación de campo, antecedentes de vacunación (VOP/CIP), síntomas, localización y tipo de parálisis por extremidad, hallazgos de laboratorio (líquido cefalorraquídeo, electromiografía), seguimiento a los 60 días y clasificación final del caso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA610';
