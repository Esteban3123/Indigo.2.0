CREATE TABLE [dbo].[HCFICHA310] (
    [ID]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDFICHANOTIFICACION] INT           NOT NULL,
    [CODDIAGNO]           CHAR (4)      NULL,
    [CARNEVACU]           BIT           NULL,
    [VACUFIEAMA]          INT           NULL,
    [FECHAAPLI]           DATE          NULL,
    [FIEBRE]              BIT           NULL,
    [MIALGIAS]            BIT           NULL,
    [ARTRALGIAS]          BIT           NULL,
    [CEFALEA]             BIT           NULL,
    [VOMITO]              BIT           NULL,
    [ICTERICIA]           BIT           NULL,
    [SFAGET]              BIT           NULL,
    [OLIGURIA]            BIT           NULL,
    [CHOQUEXSHOCK]        BIT           NULL,
    [BRADICARDIA]         BIT           NULL,
    [FALLARENAL]          BIT           NULL,
    [FALLAHEPA]           BIT           NULL,
    [HEPATOMEGALIA]       BIT           NULL,
    [SIGNOSHEMORRA]       BIT           NULL,
    [SIGHEMORRA]          INT           NULL,
    [CASOFIEAMA]          BIT           NULL,
    [SITIOPROBA]          VARCHAR (50)  NULL,
    [FECHATOMA]           DATE          NULL,
    [FECHARECE]           DATE          NULL,
    [MUESTRA]             INT           NULL,
    [PRUEBA]              INT           NULL,
    [AGENTE]              INT           NULL,
    [RESULTADO]           INT           NULL,
    [FECHARESUL]          DATE          NULL,
    [VALOR]               VARCHAR (50)  NULL,
    [VERSION]             VARCHAR (20)  NULL,
    [JSON]                VARCHAR (MAX) NULL,
    CONSTRAINT [PK_HCFICHA310] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [CK_HCFICHA310_JSON] CHECK (isjson([JSON])=(1)),
    CONSTRAINT [FK_HCFICHA310_HCFICHANOTIFICACION] FOREIGN KEY ([IDFICHANOTIFICACION]) REFERENCES [dbo].[HCFICHANOTIFICACION] ([ID])
);


GO
ALTER TABLE [dbo].[HCFICHA310] NOCHECK CONSTRAINT [CK_HCFICHA310_JSON];




GO
ALTER TABLE [dbo].[HCFICHA310] NOCHECK CONSTRAINT [CK_HCFICHA310_JSON];


GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacenamiento JSON (VARCHAR MAX) con columnas extendidas desde v01_2020-03-06: signos hemorrágicos múltiples (hemoptisis, hiperemia, hematemesis, petequias, metrorragia, melenas, equimosis, epistaxis, hematuria) y código sitio infección; versiones antiguas = nulo. Validado con CHECK isjson().', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'JSON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nuevas columnas en formato JSON  -->  (versión antigua) = nulo   /    (versiones nuevas, desde V01_2020-03-06 ) =    SH_HEMOPTISIS = booleano (S.H. - Hemoptisis)    SH_HIPEREMIA = booleano (S.H. - Hiperemia)    SH_HEMATEMESIS = booleano (S.H. - Hematemesis)    SH_PETEQUIAS = booleano (S.H. - Petequias)    SH_METRORRAGIA = booleano (S.H. - Metrorragia)    SH_MELENAS = booleano (S.H. - Melenas)    SH_EQUIMOSIS = booleano (S.H. - Equimosis)    SH_EPISTAXIS = booleano (S.H. - Epistaxis)    SH_HEMATURIA = booleano (S.H. - Hematuria)    --> en donde S. H. : Signos Hemorrágicos   CODSITIO_INFECCION  = texto ( codigo numerico sitio probable infección)    ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'JSON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'JSON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Versión de la ficha de notificación epidemiológica (VARCHAR 20); nulo = primera versión. Controla esquema JSON y evolución de campos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'VERSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Versión de la ficha de notificación ---> Si es valor nulo = primer versión ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'VERSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'VERSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción o valor del sitio probable de infección (VARCHAR 50); complementa CODSITIO_INFECCION en JSON desde v01_2020-03-06.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'VALOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Sitio Probable de Infección', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'VALOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'VALOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de resultado del examen/laboratorio (DATE); documenta cuándo se obtuvo el resultado de la prueba diagnóstica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'FECHARESUL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de resultado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'FECHARESUL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'FECHARESUL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código resultado (INT): 1=Positivo, 2=Negativo, 3=No Procesado, 4=Inadecuado, 5=Valor Registrado. Interpreta hallazgo diagnóstico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'RESULTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Resultado:   1=Positivo   2=Negativo   3=No Procesado   4=Inadecuado   5=Valor Registrado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'RESULTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'RESULTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código agente etiológico (INT): 1=Fiebre Amarilla. Identifica enfermedad bajo vigilancia epidemiológica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'AGENTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Agente:  1=Fiebre Amarilla ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'AGENTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'AGENTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código tipo prueba (INT): 1=PCR, 2=Aislamiento, 3=TGO, 4=TGP, 5=Bilirrubina Total/Directa/Indirecta, 8=Creatinina, 9=BUN, 10=Parología, 11=Estudio Directo, 12=ELISA, 13=Tiempo Protrombina, 14=Tiempo Tromboplastina. Laboratorio/diagnóstico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'PRUEBA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Prueba:   1=PCR   2=Aislamiento   3=TGO   4=TGP   5=Bilirrubina Total   6=Bilirrubina Directa   7=Bilirrubina Indirecta   8=Creatinina   9=BUN   10=Parología   11=Estudio Directo   12=Elisa   13=Tiempo de Protombina   14=Tiempo Parcial de Tromboplastina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'PRUEBA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'PRUEBA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código tipo muestra (INT): 1=Sangre Total, 2=Tejido, 3=Suero. Especifica material biológico procesado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'MUESTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Muestra:  1=Sangre Total   2=Tejido   3=Suero', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'MUESTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'MUESTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de recepción de la muestra (DATE) en laboratorio; inicia cadena de procesamiento diagnóstico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'FECHARECE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Recepción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'FECHARECE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'FECHARECE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de toma/recolección de examen (DATE); punto inicial de cadena diagnóstica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'FECHATOMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Toma de Examen', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'FECHATOMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'FECHATOMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sitio probable infección (VARCHAR 50) OBSOLETO desde v01_2020-03-06; migrado a CODSITIO_INFECCION en JSON. Mantiene compatibilidad retroactiva.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'SITIOPROBA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Sitio Probable de Infección    --> campo obsoleto desde version ''''V01_2020-03-06'''' , ahora se utiliza la columna CODSITIO_INFECCION en el campo JSON  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'SITIOPROBA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'SITIOPROBA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador caso confirmado Fiebre Amarilla (BIT): True=Sí, False=No. Clasifica paciente como caso positivo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'CASOFIEAMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Casofieama:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'CASOFIEAMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'CASOFIEAMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código signos hemorrágicos OBSOLETO (INT): 1=Hemoptisis, 2=Hiperemia Conjuntival, 3=Hematemesis, 4=Petequias, 5=Metrorragia, 6=Melenas, 7=Equimosis, 8=Epistaxis, 9=Hematuria. Migrado a opción múltiple en JSON desde v01_2020-03-06.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'SIGHEMORRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Signos Hemorrágicos:  1=Hemoptisis   2=Hiperemia Conjuntival   3=Hematemesis   4=Petequias   5=Metrorragia   6=Melenas   7=Equimosis   8=Epistaxis   9=Hematuria    --> campo obsoleto desde version ''''V01_2020-03-06'''' , ahora es opción multiple, por lo que se utiliza cada columna en el campo JSON  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'SIGHEMORRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'SIGHEMORRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador presencia signos hemorrágicos OBSOLETO (BIT): True=Sí, False=No. Reemplazado por campos individuales en JSON (v01_2020-03-06+).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'SIGNOSHEMORRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Signoshemorra (Signos Hemorrágicos) :  True=Si   False=No    --> campo obsoleto desde version ''''V01_2020-03-06''''', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'SIGNOSHEMORRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'SIGNOSHEMORRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador hepatomegalia/aumento hígado (BIT): True=Sí, False=No. Signo clínico observado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'HEPATOMEGALIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hepatomegalia:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'HEPATOMEGALIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'HEPATOMEGALIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador insuficiencia/falla hepática (BIT): True=Sí, False=No. Complicación severa evaluada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'FALLAHEPA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fallahepa:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'FALLAHEPA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'FALLAHEPA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador insuficiencia/falla renal (BIT): True=Sí, False=No. Complicación severa evaluada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'FALLARENAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fallarenal:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'FALLARENAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'FALLARENAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador bradicardia/frecuencia cardíaca baja (BIT): True=Sí, False=No. Manifestación cardiovascular.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'BRADICARDIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Bradicardia:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'BRADICARDIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'BRADICARDIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador choque/shock (BIT): True=Sí, False=No. Complicación crítica evaluada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'CHOQUEXSHOCK';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Choquexshot:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'CHOQUEXSHOCK';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'CHOQUEXSHOCK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador oliguria/disminución orina (BIT): True=Sí, False=No. Manifestación renal evaluada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'OLIGURIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Oliguria:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'OLIGURIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'OLIGURIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador sangrado fiebre amarilla GI (BIT): True=Sí, False=No. Signo hemorrágico gastrointestinal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'SFAGET';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Sfaget:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'SFAGET';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'SFAGET';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador ictericia/coloración amarilla piel (BIT): True=Sí, False=No. Manifestación hepática.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'ICTERICIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ictericia:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'ICTERICIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'ICTERICIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador vómito/emesis (BIT): True=Sí, False=No. Síntoma evaluado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'VOMITO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Vomito:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'VOMITO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'VOMITO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador cefalea/dolor cabeza (BIT): True=Sí, False=No. Síntoma inicial común.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'CEFALEA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cefalea:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'CEFALEA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'CEFALEA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador artralgias/dolor articular (BIT): True=Sí, False=No. Síntoma evaluado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'ARTRALGIAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Artralgias:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'ARTRALGIAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'ARTRALGIAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador mialgias/dolor muscular (BIT): True=Sí, False=No. Síntoma evaluado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'MIALGIAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mialgias:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'MIALGIAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'MIALGIAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador fiebre/temperatura elevada (BIT): True=Sí, False=No. Síntoma cardinal evaluado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'FIEBRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fiebre:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'FIEBRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'FIEBRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha aplicación/administración intervención (DATE); puede referir vacunación o procedimiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'FECHAAPLI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de aplicación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'FECHAAPLI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'FECHAAPLI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código antecedente vacunación Fiebre Amarilla (INT): 1=Sí, 2=No, 3=Desconocido. Estado inmunización.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'VACUFIEAMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Vacuna fiebre amarilla:   1= Si   2= No   3= Desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'VACUFIEAMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'VACUFIEAMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador carnet/registro vacunación disponible (BIT): True=Sí, False=No. Disponibilidad documentación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'CARNEVACU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Carnet de vacunacion:  True=Si   false=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'CARNEVACU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'CARNEVACU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código diagnóstico (CHAR 4); referencia patología principal o diagnóstico clínico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID ficha de notificación epidemiológica (INT, FK); enlaza con HCFICHANOTIFICACION padre.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de ficha Notificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador primario autoincremental (INT IDENTITY 1,1); clave única registro clínico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ficha de notificación epidemiológica de fiebre amarilla (código 310). Registra los síntomas clínicos, antecedentes de vacunación, datos de laboratorio y resultado del caso para cada notificación de un paciente sospechoso o confirmado de fiebre amarilla.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA310';
