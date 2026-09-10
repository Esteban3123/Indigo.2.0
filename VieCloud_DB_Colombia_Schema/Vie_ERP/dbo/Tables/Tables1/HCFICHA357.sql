CREATE TABLE [dbo].[HCFICHA357] (
    [ID]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDFICHANOTIFICACION] INT           NOT NULL,
    [CODDIAGNO]           CHAR (4)      NULL,
    [TIPOUCI]             INT           NULL,
    [FECHAINGRES]         DATE          NULL,
    [REINGRESO]           BIT           NULL,
    [PACIREMITI]          BIT           NULL,
    [CASOIAD]             BIT           NULL,
    [NOMINSTIT]           VARCHAR (50)  NULL,
    [PARAUCIN]            VARCHAR (50)  NULL,
    [TIPOIAD]             INT           NULL,
    [CRITCLASIFNAV]       INT           NULL,
    [CRITCLASIFITSAC]     INT           NULL,
    [CRITCLASIFISTUAC]    INT           NULL,
    [FECHADIAGN]          DATE          NULL,
    [IADPOLIMICRO]        BIT           NULL,
    [VENTIMECANI]         BIT           NULL,
    [FECHAINSER1]         DATE          NULL,
    [FECHAINSER2]         DATE          NULL,
    [FECHAINSER3]         DATE          NULL,
    [FECHARETIRO1]        DATE          NULL,
    [FECHARETIRO2]        DATE          NULL,
    [FECHARETIRO3]        DATE          NULL,
    [CATETECENTR]         BIT           NULL,
    [CATETEURINA]         BIT           NULL,
    [CANCER]              BIT           NULL,
    [CORTICOTERA]         BIT           NULL,
    [DESNUTRICION]        BIT           NULL,
    [DIABETES]            BIT           NULL,
    [DIALISIS]            BIT           NULL,
    [ENFERENAL]           BIT           NULL,
    [EPOC]                BIT           NULL,
    [INMUNOSUPRE]         BIT           NULL,
    [PARALISIS]           BIT           NULL,
    [VIHSIDA]             BIT           NULL,
    [INFECCIPREV]         BIT           NULL,
    [QUIMIOTERA]          BIT           NULL,
    [TRAUMATIS]           BIT           NULL,
    [OBESIDAD]            BIT           NULL,
    [NINGUNO]             BIT           NULL,
    [OTRO]                BIT           NULL,
    [OTRO2]               VARCHAR (50)  NULL,
    [FECHATOMAMU1]        DATE          NULL,
    [FECHATOMAMU2]        DATE          NULL,
    [FECHATOMAMU3]        DATE          NULL,
    [CODMUESTRA1]         INT           NULL,
    [CODMUESTRA2]         INT           NULL,
    [CODMUESTRA3]         INT           NULL,
    [CODPRUEBA1]          INT           NULL,
    [CODPRUEBA2]          INT           NULL,
    [CODPRUEBA3]          INT           NULL,
    [MICROORGA1]          VARCHAR (50)  NULL,
    [MICROORGA2]          VARCHAR (50)  NULL,
    [MICROORGA3]          VARCHAR (50)  NULL,
    [VERSION]             VARCHAR (20)  NULL,
    [JSON]                VARCHAR (MAX) NULL,
    CONSTRAINT [PK_HCFICHA357] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [CK_HCFICHA357_JSON] CHECK (isjson([JSON])=(1)),
    CONSTRAINT [FK_HCFICHA357_HCFICHANOTIFICACION] FOREIGN KEY ([IDFICHANOTIFICACION]) REFERENCES [dbo].[HCFICHANOTIFICACION] ([ID])
);


GO
ALTER TABLE [dbo].[HCFICHA357] NOCHECK CONSTRAINT [CK_HCFICHA357_JSON];




GO
ALTER TABLE [dbo].[HCFICHA357] NOCHECK CONSTRAINT [CK_HCFICHA357_JSON];


GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Objeto JSON (VARCHAR MAX) con campos adicionales desde V01_2020-03-06: DISPOSITIVO_UCI (bit: dispositivo insertado en UCI, 0=No/1=Sí), EVENTO_48H (bit: evento ocurrido 48h post-retiro de catéter o ventilador, 0=No/1=Sí). Validado con CHECK isjson().', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'JSON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nuevas columnas en formato JSON    Desde versión "V01_2020-03-06" -->  - DIPOSITIVO_UCI : bit (Dispositivo insertado en UCI : 0 - No , 1 - Si)  - EVENTO_48H: bit (El evento se desarrolló 48 horas después de retirado el catéter o ventilador : 0 - No , 1 - Si)   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'JSON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'JSON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(20) - Versión de la ficha de notificación de infección asociada a dispositivo (IAD). NULL = primera versión; ej: V01_2020-03-06 indica cambios de estructura.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'VERSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Versión de la ficha de notificación ---> Si es valor nulo = primer versión ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'VERSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'VERSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(50) - Nombre del tercer microorganismo aislado en cultivo/laboratorio. Bacteria, hongo o virus identificado en muestra de diagnóstico de IAD.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'MICROORGA3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N' Microorganismo aislado 3', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'MICROORGA3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'MICROORGA3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(50) - Nombre del segundo microorganismo aislado en cultivo/laboratorio. Bacteria, hongo o virus identificado en muestra de diagnóstico de IAD.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'MICROORGA2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N' Microorganismo aislado 2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'MICROORGA2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'MICROORGA2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(50) - Nombre del primer microorganismo aislado en cultivo/laboratorio. Bacteria, hongo o virus identificado en muestra de diagnóstico de IAD.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'MICROORGA1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N' Microorganismo aislado 1', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'MICROORGA1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'MICROORGA1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT - Código de la tercera prueba de diagnóstico microbiológico: 1=IgG, 2=Cultivo, 3=Elisa, 4=Hemocultivo, 5=RT/PCR, 6=Urocultivo, 7=Cultivo líquido pleural, 8=Lavado broncoalveolar (LBA), 9=Biopsia, 10=Cultivo parénquima pulmonar, 11=Cultivo secreciones respiratorias, 12=Micro inmunofluorescencia Chlamydia, 13=Microinmunofluorescencia, 14=Radioinmunoanalisis, 15=IFA indirecta, 16=Hisopado nasofaríngeo, 17=LBA protegido, 18=Cepillado protegido.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'CODPRUEBA3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N' Cód. de la prueba 3       1= IgG    2= Cultivo  3= Elisa   4= Hemocultivo  5= RT/PCR  6= Urocultivo  7=Cultivo de Liquido Pleural  8= Lavado broncoalveolar  9= Biopsia  10= Cultivo del parenquima pulmonar 11=Cultivo de secresiones respiratorias  12= Test de micro inmunofluorescencia para chalmydia  13=  Microinmunofluorescencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'CODPRUEBA3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'CODPRUEBA3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT - Código de la segunda prueba de diagnóstico microbiológico: 1=IgG, 2=Cultivo, 3=Elisa, 4=Hemocultivo, 5=RT/PCR, 6=Urocultivo, 7=Cultivo líquido pleural, 8=Lavado broncoalveolar (LBA), 9=Biopsia, 10=Cultivo parénquima pulmonar, 11=Cultivo secreciones respiratorias, 12=Micro inmunofluorescencia Chlamydia, 13=Microinmunofluorescencia, 14=Radioinmunoanalisis, 15=IFA indirecta, 16=Hisopado nasofaríngeo, 17=LBA protegido, 18=Cepillado protegido.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'CODPRUEBA2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N' Cód. de la prueba 2     1= IgG    2= Cultivo  3= Elisa   4= Hemocultivo  5= RT/PCR  6= Urocultivo  7=Cultivo de Liquido Pleural  8= Lavado broncoalveolar  9= Biopsia  10= Cultivo del parenquima pulmonar 11=Cultivo de secresiones respiratorias  12= Test de micro inmunofluorescencia para chalmydia  13=  Microinmunofluorescencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'CODPRUEBA2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'CODPRUEBA2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT - Código de la primera prueba de diagnóstico microbiológico: 1=IgG, 2=Cultivo, 3=Elisa, 4=Hemocultivo, 5=RT/PCR, 6=Urocultivo, 7=Cultivo líquido pleural, 8=Lavado broncoalveolar (LBA), 9=Biopsia, 10=Cultivo parénquima pulmonar, 11=Cultivo secreciones respiratorias, 12=Micro inmunofluorescencia Chlamydia, 13=Microinmunofluorescencia, 14=Radioinmunoanalisis, 15=IFA indirecta, 16=Hisopado nasofaríngeo, 17=LBA protegido, 18=Cepillado protegido.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'CODPRUEBA1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N' Cód. de la prueba1      1= IgG    2= Cultivo  3= Elisa   4= Hemocultivo  5= RT/PCR  6= Urocultivo  7=Cultivo de Liquido Pleural  8= Lavado broncoalveolar  9= Biopsia  10= Cultivo del parenquima pulmonar 11=Cultivo de secresiones respiratorias  12= Test de micro inmunofluorescencia para chalmydia  13=  Microinmunofluorescencia
14=  Radioinmunoanalisis  15= Inmunofluorescencia (IFA) indirecta
16= Hisopado - Aspirado/ nasofaríngeo 17 = Lavado broncoalveolar (LBA) protegido  18 = Cepillado protegido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'CODPRUEBA1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'CODPRUEBA1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT - Código de la tercera muestra recogida para cultivo/análisis: 1=Sangre total, 2=Orina, 3=Tejido, 4=Espúto, 5=Otros líquidos estériles, 6=Moco, 7=Secreciones respiratorias.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'CODMUESTRA3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N' Cod. de la muestra 3            1 = sangre total  2 = Orina   3 = tejido 4 = Espúto  5= Otros Líquidos estériles  6 = Moco  7 = Secreciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'CODMUESTRA3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'CODMUESTRA3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT - Código de la segunda muestra recogida para cultivo/análisis: 1=Sangre total, 2=Orina, 3=Tejido, 4=Espúto, 5=Otros líquidos estériles, 6=Moco, 7=Secreciones respiratorias.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'CODMUESTRA2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N' Cod. de la muestra2    1 = sangre total  2 = Orina   3 = tejido 4 = Espúto  5= Otros Líquidos estériles  6 = Moco  7 = Secreciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'CODMUESTRA2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'CODMUESTRA2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT - Código de la primera muestra recogida para cultivo/análisis: 1=Sangre total, 2=Orina, 3=Tejido, 4=Espúto, 5=Otros líquidos estériles, 6=Moco, 7=Secreciones respiratorias.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'CODMUESTRA1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N' Cod. de la muestra1      1 = sangre total  2 = Orina   3 = tejido 4 = Espúto  5= Otros Líquidos estériles  6 = Moco  7 = Secreciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'CODMUESTRA1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'CODMUESTRA1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DATE - Fecha de recolección de la tercera muestra para estudio microbiológico de IAD.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'FECHATOMAMU3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N' Fecha de toma de la muestra 3', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'FECHATOMAMU3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'FECHATOMAMU3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DATE - Fecha de recolección de la segunda muestra para estudio microbiológico de IAD.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'FECHATOMAMU2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N' Fecha de toma de la muestra 2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'FECHATOMAMU2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'FECHATOMAMU2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DATE - Fecha de recolección de la primera muestra para estudio microbiológico de IAD.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'FECHATOMAMU1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N' Fecha de toma de la muestra 1', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'FECHATOMAMU1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'FECHATOMAMU1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(50) - Descripción de otro factor de riesgo no catalogado, campo libre.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'OTRO2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'texto otro 2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'OTRO2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'OTRO2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT - Indicador de otro factor de riesgo adicional (0=No, 1=Sí). Complemento a comorbilidades predefinidas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'OTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Otro : 0 - No , 1 - Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'OTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'OTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT - [DEPRECADO desde V01_2020-03-06] Indicador si paciente no presenta comorbilidades (0=No, 1=Sí).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'NINGUNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'(campo eliminado desde version V01_2020-03-06 ) Ninguno : 0 - No , 1 - Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'NINGUNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'NINGUNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT - Comorbilidad: Obesidad presente (0=No, 1=Sí). Factor de riesgo para infecciones asociadas a dispositivos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'OBESIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Obesidad : 0 - No , 1 - Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'OBESIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'OBESIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT - Comorbilidad: Traumatismo/lesión traumática presente (0=No, 1=Sí). Factor de riesgo para IAD.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'TRAUMATIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Traumatismo : 0 - No , 1 - Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'TRAUMATIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'TRAUMATIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT - [DEPRECADO desde V01_2020-03-06] Comorbilidad: Quimioterapia activa (0=No, 1=Sí).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'QUIMIOTERA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'(campo eliminado desde version V01_2020-03-06 ) Quimioterapia : 0 - No , 1 - Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'QUIMIOTERA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'QUIMIOTERA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT - Comorbilidad: Infección previa/antecedente de infección (0=No, 1=Sí). Factor de riesgo para IAD.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'INFECCIPREV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Infección Previa : 0 - No , 1 - Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'INFECCIPREV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'INFECCIPREV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT - Comorbilidad: VIH/SIDA presente (0=No, 1=Sí). Inmunosupresión grave.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'VIHSIDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  VIH-SIDA : 0 - No , 1 - Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'VIHSIDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'VIHSIDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT - [DEPRECADO desde V01_2020-03-06] Comorbilidad: Parálisis/inmovilidad (0=No, 1=Sí).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'PARALISIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'(campo eliminado desde version V01_2020-03-06 ) Parálisis : 0 - No , 1 - Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'PARALISIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'PARALISIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT - Comorbilidad: Inmunosupresión/inmunocompromiso (0=No, 1=Sí). Factor de riesgo para IAD.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'INMUNOSUPRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Inmunosupresión : 0 - No , 1 - Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'INMUNOSUPRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'INMUNOSUPRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT - Comorbilidad: EPOC/Enfermedad pulmonar obstructiva crónica (0=No, 1=Sí).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'EPOC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'EPOC : 0 - No , 1 - Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'EPOC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'EPOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT - Comorbilidad: Enfermedad renal crónica/insuficiencia renal (0=No, 1=Sí).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'ENFERENAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Enfermedad Renal : 0 - No , 1 - Si ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'ENFERENAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'ENFERENAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT - [DEPRECADO desde V01_2020-03-06] Comorbilidad: Diálisis/terapia de reemplazo renal (0=No, 1=Sí).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'DIALISIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'(campo eliminado desde version V01_2020-03-06 ) Diálisis : 0 - No , 1 - Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'DIALISIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'DIALISIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT - Comorbilidad: Diabetes mellitus (0=No, 1=Sí). Factor de riesgo para infecciones.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'DIABETES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Diabetes : 0 - No , 1 - Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'DIABETES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'DIABETES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT - Comorbilidad: Desnutrición/estado nutricional deficiente (0=No, 1=Sí).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'DESNUTRICION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Desnutrición : 0 - No , 1 - Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'DESNUTRICION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'DESNUTRICION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT - [DEPRECADO desde V01_2020-03-06] Comorbilidad: Corticoterapia sistémica (0=No, 1=Sí).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'CORTICOTERA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'(campo eliminado desde version V01_2020-03-06 ) Corticoterapia : 0 - No , 1 - Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'CORTICOTERA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'CORTICOTERA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT - Comorbilidad: Cáncer/neoplasia activa (0=No, 1=Sí). Factor de riesgo para IAD.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'CANCER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cáncer : 0 - No , 1 - Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'CANCER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'CANCER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT - Dispositivo: Catéter urinario presente en el paciente (0=No/false, 1=Sí/true).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'CATETEURINA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cateter urinario  1 = true  0 = false ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'CATETEURINA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'CATETEURINA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT - Dispositivo: Catéter central presente en el paciente (0=No/false, 1=Sí/true).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'CATETECENTR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cateter central 1 = true  0 = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'CATETECENTR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'CATETECENTR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DATE - Fecha de retiro del tercer dispositivo invasivo (catéter central, urinario o ventilador).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'FECHARETIRO3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda fecha de retiro 3', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'FECHARETIRO3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'FECHARETIRO3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DATE - Fecha de retiro del segundo dispositivo invasivo (catéter central, urinario o ventilador).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'FECHARETIRO2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda fecha de retiro 2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'FECHARETIRO2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'FECHARETIRO2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DATE - Fecha de retiro del primer dispositivo invasivo (catéter central, urinario o ventilador).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'FECHARETIRO1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda fecha de retiro 1', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'FECHARETIRO1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'FECHARETIRO1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DATE - Fecha de inserción del tercer dispositivo invasivo (catéter central, urinario o ventilador).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'FECHAINSER3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda fecha de insercion 3', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'FECHAINSER3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'FECHAINSER3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DATE - Fecha de inserción del segundo dispositivo invasivo (catéter central, urinario o ventilador).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'FECHAINSER2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda fecha de insercion 2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'FECHAINSER2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'FECHAINSER2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DATE - Fecha de inserción del primer dispositivo invasivo (catéter central, urinario o ventilador).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'FECHAINSER1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda fecha de insercion 1', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'FECHAINSER1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'FECHAINSER1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT - Dispositivo: Ventilador mecánico/asistencia ventilatoria presente (0=No/false, 1=Sí/true).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'VENTIMECANI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ventilador mecánico 1 = true     0= false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'VENTIMECANI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'VENTIMECANI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT - Indicador de IAD polimicrobiana: infección por más de un microorganismo (0=No/false, 1=Sí/true).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'IADPOLIMICRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N' IAD polimicrobiana (asociada a más de un microorganismo)  1 = true  2 = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'IADPOLIMICRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'IADPOLIMICRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DATE - Fecha de diagnóstico de la infección asociada a dispositivo (IAD).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'FECHADIAGN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda fecha diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'FECHADIAGN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'FECHADIAGN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT - Criterio de clasificación para ISTU-AC (Infección de sitio quirúrgico relacionada UCI-Adultos): 1=Criterio 1a, 2=Criterio 2a, 3=Criterio 3, 4=Criterio 4.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'CRITCLASIFISTUAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N' Criterio de clasificación para ISTU-AC   1 = Criterio 1a   2 = Criterio 2a   3 = Criterio 3     4 = Criterio 4', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'CRITCLASIFISTUAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'CRITCLASIFISTUAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT - Criterio de clasificación para ITS-AC (Infección transvaginal/sitio quirúrgico UCI-Adultos): 1=Criterio 1, 2=Criterio 2, 3=Criterio 3.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'CRITCLASIFITSAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Criterio de clasificación para ITS-AC  1 = Criterio 1  2 = Criterio 2    3 = Criterio 3 ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'CRITCLASIFITSAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'CRITCLASIFITSAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT - Criterio de clasificación para NAV (Neumonía asociada a ventilador): 1=NEU 1, 2=NEU 2, 3=NEU 3.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'CRITCLASIFNAV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N' Criterio de clasificación NAV   1= NEU 1    2= NEU 2     3 = NEU 3', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'CRITCLASIFNAV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'CRITCLASIFNAV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT - Tipo de infección asociada a dispositivo notificada: 1=NAV (Neumonía Asociada a Ventilador), 2=ISTU-AC (Infección de Sitio Quirúrgico UCI-Adultos), 3=ITS-AC (Infección de Tracto Sanguíneo UCI-Adultos).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'TIPOIAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de IAD   1=NAV  2=ISTU-AC  3=ITS-AC ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'TIPOIAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'TIPOIAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(50) - Para UCI-N (Neonatal): Peso al nacimiento en gramos del recién nacido.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'PARAUCIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N' Para UCI-N, Peso al nacer (gramos)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'PARAUCIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'PARAUCIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(50) - Nombre de la institución/centro de atención a la cual se atribuye el caso de infección asociada a dispositivo (IAD).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'NOMINSTIT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N' Nombre de la institución a la que se atribuye el caso de IAD', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'NOMINSTIT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'NOMINSTIT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT - Indicador de caso de IAD extrahospitalario/comunitario (0=No/intrahospitalario, 1=Sí/extrahospitalario).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'CASOIAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'caso IAD extrahospitalario 1 = true   0 = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'CASOIAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'CASOIAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT - Indicador de paciente remitido de otra institución (0=No, 1=Sí/paciente referido).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'PACIREMITI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'paciente remitdo 1 = true  0 = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'PACIREMITI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'PACIREMITI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT - [DEPRECADO desde V01_2020-03-06] Indicador de reingreso del paciente a UCI (0=No, 1=Sí).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'REINGRESO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'(campo eliminado desde version V01_2020-03-06)  Reingreso:   0 - No    1 - Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'REINGRESO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'REINGRESO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DATE - Fecha de ingreso del paciente a la unidad de cuidados intensivos (UCI).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'FECHAINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda fecha ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'FECHAINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'FECHAINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT - Tipo de unidad de cuidados intensivos relacionada con el evento: 1=UCI-A (Adultos), 2=UCI-P (Pediátrica), 3=UCI-N (Neonatal).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'TIPOUCI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de UCI relacionado con ocurrencia del evento  1 =UCI-A  2 =UCI-P  3 =UCI-N', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'TIPOUCI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'TIPOUCI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'CHAR(4) - Código de diagnóstico de la infección (CIE-10 u otro clasificador clínico).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el Codigo del diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT - Identificador único de la ficha de notificación de IAD (FK hacia HCFICHANOTIFICACION). Vincula el evento infeccioso al registro maestro.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el ID de ficha Notificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT IDENTITY - Identificador único secuencial de la tabla HCFICHA357. Clave primaria, auto-incremento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ficha de notificación de Infecciones Asociadas a la Atención en Salud (IAAS), específicamente para vigilancia epidemiológica en Unidades de Cuidados Intensivos (UCI). Registra datos clínicos, factores de riesgo, dispositivos invasivos, comorbilidades y resultados microbiológicos de pacientes con eventos de infección intrahospitalaria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357';
