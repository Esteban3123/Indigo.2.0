CREATE TABLE [dbo].[CALIAASQX] (
    [ID]                     INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDCALREPORTE]           INT           NOT NULL,
    [COMPLEJIDAD]            INT           NOT NULL,
    [SERVICIOPROCEDIMIENTO]  INT           NOT NULL,
    [PROCEDIMIENTOMEDICO]    INT           NOT NULL,
    [DIABETES]               BIT           NULL,
    [INMUNOSUPRESION]        BIT           NULL,
    [OBESIDAD]               BIT           NULL,
    [DESNUTRICION]           BIT           NULL,
    [PRECLAMPSIA]            BIT           NULL,
    [ANEMIA]                 BIT           NULL,
    [CLASIFICACIONASA]       INT           NOT NULL,
    [TIPOHERIDA]             INT           NOT NULL,
    [DURACIONPROCEDIMIENTO]  INT           NOT NULL,
    [SUPERFICIEPRIMARIA]     BIT           NULL,
    [SUPERFICIESECUNDAR]     BIT           NULL,
    [PROFUNDAPRIMARIA]       BIT           NULL,
    [PROFUNDASECUNDARIA]     BIT           NULL,
    [ORGANOESPACIO]          BIT           NULL,
    [PROFIAXISANTIBIOTICA]   BIT           NOT NULL,
    [CUAL]                   VARCHAR (200) NULL,
    [TIEMPOANTIBIOTICO]      INT           NOT NULL,
    [REQUIRIONUEVAINTERVEN]  BIT           NOT NULL,
    [FECHATOMAMUESTRA1]      DATE          NULL,
    [CODIGOMUESTRA1]         INT           NULL,
    [CODIGOPRUEBA1]          VARCHAR (3)   NULL,
    [MICROORGANISMOAISLADO1] VARCHAR (15)  NULL,
    [FECHATOMAMUESTRA2]      DATE          NULL,
    [CODIGOMUESTRA2]         INT           NULL,
    [CODIGOPRUEBA2]          VARCHAR (3)   NULL,
    [MICROORGANISMOAISLADO2] VARCHAR (15)  NULL,
    [FECHATOMAMUESTRA3]      DATE          NULL,
    [CODIGOMUESTRA3]         INT           NULL,
    [CODIGOPRUEBA3]          VARCHAR (3)   NULL,
    [MICROORGANISMOAISLADO3] VARCHAR (15)  NULL,
    CONSTRAINT [PK_CALIAASQX] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_CALIAASQX_CALREPORTE] FOREIGN KEY ([IDCALREPORTE]) REFERENCES [dbo].[CALREPORTE] ([ID])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Microorganismo aislado en muestra 3 (VARCHAR 15), agente infeccioso identificado en cultivo, hemocultivo, biopsia o análisis de laboratorio del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'MICROORGANISMOAISLADO3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Microorganismo aislado 3', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'MICROORGANISMOAISLADO3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'MICROORGANISMOAISLADO3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de tipo de prueba 3 (INT): 55=Cultivo, 92=Hemocultivo, G3=Biopsia, 86=Radiografía, 90=TAC, D4=RNM; método diagnóstico utilizado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'CODIGOPRUEBA3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cód. de la prueba 3  55 = Cultivo92 = HemocultivoG3 = Biopsia86 = Radiografía90 = Tomografía axial computarizadaD4 = Renonancia nuclear magnética', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'CODIGOPRUEBA3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'CODIGOPRUEBA3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de tipo de muestra 3 (INT): 1=Sangre, 4=Tejido, 11=Líquidos estériles, 32=Secreciones; origen del especimen analizado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'CODIGOMUESTRA3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cód. de la muestra 3  1 =  Sangre4 = Tejido11 = Otros Liquidos Esteriles32 = Secreciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'CODIGOMUESTRA3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'CODIGOMUESTRA3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de toma de muestra 3 (DATE), registro de cuándo se recolectó el espécimen para análisis microbiológico o prueba diagnóstica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'FECHATOMAMUESTRA3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N' Fecha de toma de la muestra 3', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'FECHATOMAMUESTRA3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'FECHATOMAMUESTRA3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Microorganismo aislado en muestra 2 (VARCHAR 15), agente infeccioso identificado en cultivo, hemocultivo, biopsia o análisis de laboratorio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'MICROORGANISMOAISLADO2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Microorganismo aislado 2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'MICROORGANISMOAISLADO2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'MICROORGANISMOAISLADO2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de tipo de prueba 2 (INT): 55=Cultivo, 92=Hemocultivo, G3=Biopsia, 86=Radiografía, 90=TAC, D4=RNM; método de diagnóstico empleado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'CODIGOPRUEBA2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cód. de la prueba 2  55 = Cultivo92 = HemocultivoG3 = Biopsia86 = Radiografía90 = Tomografía axial computarizadaD4 = Renonancia nuclear magnética', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'CODIGOPRUEBA2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'CODIGOPRUEBA2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de tipo de muestra 2 (INT): 1=Sangre, 4=Tejido, 11=Líquidos estériles, 32=Secreciones; tipo de espécimen colectado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'CODIGOMUESTRA2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cód. de la muestra 2  1 =  Sangre4 = Tejido11 = Otros Liquidos Esteriles32 = Secreciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'CODIGOMUESTRA2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'CODIGOMUESTRA2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de toma de muestra 2 (DATE), registro temporal de recolección del especimen para análisis microbiológico o pruebas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'FECHATOMAMUESTRA2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N' Fecha de toma de la muestra 2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'FECHATOMAMUESTRA2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'FECHATOMAMUESTRA2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Microorganismo aislado en muestra 1 (VARCHAR 15), agente etiológico identificado en cultivo, hemocultivo, biopsia o laboratorio clínico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'MICROORGANISMOAISLADO1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Microorganismo aislado 1', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'MICROORGANISMOAISLADO1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'MICROORGANISMOAISLADO1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de tipo de prueba 1 (INT): 55=Cultivo, 92=Hemocultivo, G3=Biopsia, 86=Radiografía, 90=TAC, D4=RNM; técnica diagnóstica aplicada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'CODIGOPRUEBA1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cód. de la prueba 1  55 = Cultivo  92 = Hemocultivo  G3 = Biopsia  86 = Radiografía  90 = Tomografía axial computarizada  D4 = Renonancia nuclear magnética', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'CODIGOPRUEBA1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'CODIGOPRUEBA1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de tipo de muestra 1 (INT): 1=Sangre, 4=Tejido, 11=Líquidos estériles, 32=Secreciones; naturaleza del espécimen enviado a laboratorio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'CODIGOMUESTRA1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cód. de la muestra 1  1 =  Sangre  4 = Tejido  11 = Otros Liquidos Esteriles  32 = Secreciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'CODIGOMUESTRA1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'CODIGOMUESTRA1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de toma de muestra 1 (DATE), registro de la fecha de recolección del especimen para análisis microbiológico o exámenes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'FECHATOMAMUESTRA1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N' Fecha de toma de la muestra 1', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'FECHATOMAMUESTRA1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'FECHATOMAMUESTRA1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Requirió nueva intervención quirúrgica o procedimiento adicional (BIT): 1=Sí reoperación/reintervención, 0=No; complicaciones post-operatorias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'REQUIRIONUEVAINTERVEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Requirió una nueva intervención?  True = Si   False = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'REQUIRIONUEVAINTERVEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'REQUIRIONUEVAINTERVEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tiempo de administración del antibiótico (INT): 1=Antes del procedimiento, 2=Durante, 3=Después, 4=Ninguna; timing de profilaxis antibiótica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'TIEMPOANTIBIOTICO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tiempo en que se le administró el antibiótico  1= Antes  2=Durante  3=Despues   4=Ninguna', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'TIEMPOANTIBIOTICO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'TIEMPOANTIBIOTICO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción textual de profilaxis antibiótica (VARCHAR 200), especifica qué antibiótico se administró y motivo de la profilaxis relacionada con Proc.Qx o parto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'CUAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Profilaxis Antibiotica  Cual?', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'CUAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'CUAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Profilaxis antibiótica administrada en relación con procedimiento quirúrgico o parto (BIT): 1=Sí recibió, 0=No; prevención infección sitio quirúrgico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'PROFIAXISANTIBIOTICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Profilaxis antibiótica  Profilaxis antibiótica relacionada con el Proc.Qx o parto    1 = True = Si  0 = False = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'PROFIAXISANTIBIOTICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'PROFIAXISANTIBIOTICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Infección de órgano/espacio quirúrgico (BIT): 1=Presente/Seleccionado, NULL=No; complicación profunda post-operatoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'ORGANOESPACIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de infección sitio quirúrgico  1 = True = Seleccionado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'ORGANOESPACIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'ORGANOESPACIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Infección profunda de sitio quirúrgico secundaria (BIT): 1=Presente/Seleccionado, NULL=No; re-infección de capas profundas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'PROFUNDASECUNDARIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de infección sitio quirúrgico  1 = True = Seleccionado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'PROFUNDASECUNDARIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'PROFUNDASECUNDARIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Infección profunda de sitio quirúrgico primaria (BIT): 1=Presente/Seleccionado, NULL=No; infección en planos musculares/esternón', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'PROFUNDAPRIMARIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de infección sitio quirúrgico  1 = True = Seleccionado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'PROFUNDAPRIMARIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'PROFUNDAPRIMARIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Infección superficial de sitio quirúrgico secundaria (BIT): 1=Presente/Seleccionado, NULL=No; re-infección de piel y tejido subcutáneo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'SUPERFICIESECUNDAR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de infección sitio quirúrgico  1 = True = Seleccionado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'SUPERFICIESECUNDAR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'SUPERFICIESECUNDAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Infección superficial de sitio quirúrgico primaria (BIT): 1=Presente/Seleccionado, NULL=No; infección de incisión, piel o tejido subcutáneo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'SUPERFICIEPRIMARIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de infección sitio quirúrgico  1 = True = Seleccionado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'SUPERFICIEPRIMARIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'SUPERFICIEPRIMARIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Duración total del procedimiento quirúrgico o parto (INT, minutos), factor de riesgo NHSN-NNISS para infección sitio quirúrgico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'DURACIONPROCEDIMIENTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Duración del Procedimiento (Minutos)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'DURACIONPROCEDIMIENTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'DURACIONPROCEDIMIENTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación de herida quirúrgica (INT): 1=Limpia, 2=Limpia-contaminada, 3=Contaminada, 4=Sucia/infectada; índice riesgo infección NHSN-NNISS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'TIPOHERIDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de herida  1=Limpia  2=Limpia contaminada  3=Herida contaminada   4=Herida sucia ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'TIPOHERIDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'TIPOHERIDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación ASA de riesgo anestésico (INT): 1=ASA I, 2=ASA II, 3=ASA III, 4=ASA IV, 5=ASA V; estado físico pre-operatorio del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'CLASIFICACIONASA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indice de riesgo NHSN-NNISS  Clasificación ASA    1=ASA 1  2=ASA 2  3=ASA 3  4=ASA 4  5=ASA 5', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'CLASIFICACIONASA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'CLASIFICACIONASA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Factor de riesgo: anemia presente en paciente (BIT): 1=Sí/Seleccionado, NULL=No; comorbilidad que incrementa riesgo NHSN-NNISS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'ANEMIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Factores de riesgo  1 = True = Seleccionado ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'ANEMIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'ANEMIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Factor de riesgo: preeclampsia presente en gestante (BIT): 1=Sí/Seleccionado, NULL=No; complicación obstétrica relevante', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'PRECLAMPSIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Factores de riesgo  1 = True = Seleccionado ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'PRECLAMPSIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'PRECLAMPSIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Factor de riesgo: desnutrición documentada en paciente (BIT): 1=Sí/Seleccionado, NULL=No; comorbilidad que afecta cicatrización', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'DESNUTRICION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Factores de riesgo  1 = True = Seleccionado ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'DESNUTRICION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'DESNUTRICION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Factor de riesgo: obesidad presente en paciente (BIT): 1=Sí/Seleccionado, NULL=No; comorbilidad que incrementa riesgo quirúrgico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'OBESIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Factores de riesgo  1 = True = Seleccionado ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'OBESIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'OBESIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Factor de riesgo: inmunosupresión documentada (BIT): 1=Sí/Seleccionado, NULL=No; comorbilidad que aumenta riesgo infeccioso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'INMUNOSUPRESION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Factores de riesgo  1 = True = Seleccionado ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'INMUNOSUPRESION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'INMUNOSUPRESION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Factor de riesgo: diabetes mellitus en paciente (BIT): 1=Sí/Seleccionado, NULL=No; comorbilidad mayor para infección sitio quirúrgico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'DIABETES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Factores de riesgo  1 = True = Seleccionado ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'DIABETES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'DIABETES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de procedimiento quirúrgico o obstétrico realizado (INT): 1=Cesárea, 2=Herniorrafia, 3=Parto vaginal, 4=Revascularización miocárdica, 5=Colecistectomía', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'PROCEDIMIENTOMEDICO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Seleccione el procedimiento médico quirúrgico realizado  1=Cesárea  2=Herniorrafia  3=Parto  4=Revascularización miocardica con incisión torácica y del sitio donante  5=Colecistectomía', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'PROCEDIMIENTOMEDICO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'PROCEDIMIENTOMEDICO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Servicio de admisión y modalidad (INT): 1=Programado ambulatorio, 2=Urgencias/emergencia, 3=Programado hospitalizado; contexto del procedimiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'SERVICIOPROCEDIMIENTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Servicio de admisión del  procedimiento qx o parto  1 = Programado ambulatorio  2 = Urgencias  3 = Programado hospitalizado ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'SERVICIOPROCEDIMIENTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'SERVICIOPROCEDIMIENTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nivel de complejidad del servicio médico-quirúrgico (INT): 1=Baja, 2=Media, 3=Alta; clasificación de severidad de la atención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'COMPLEJIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Complejidad del servicio médico quirurgico  1= Baja   2= Media  3= Alta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'COMPLEJIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'COMPLEJIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea a tabla CALREPORTE (INT), enlaza este registro de vigilancia de infección quirúrgica con su reporte de calidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'IDCALREPORTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del reporte que guarda igual que id del tabla CALREPORTE', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'IDCALREPORTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'IDCALREPORTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único auto-incremental (INT IDENTITY), clave primaria de la tabla CALIAASQX', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de vigilancia de infecciones del sitio quirúrgico (ISQ). Almacena los factores de riesgo del paciente, características del procedimiento, clasificación de la herida, profilaxis antibiótica y resultados microbiológicos asociados a cada reporte de calidad quirúrgica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX';
