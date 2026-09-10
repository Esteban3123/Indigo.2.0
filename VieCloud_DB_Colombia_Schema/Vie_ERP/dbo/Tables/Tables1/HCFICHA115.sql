CREATE TABLE [dbo].[HCFICHA115] (
    [ID]                     INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDFICHANOTIFICACION]    INT           NOT NULL,
    [TIPOTUMOR]              INT           NULL,
    [CONSULTANEO]            BIT           NULL,
    [CONSULTARECA]           BIT           NULL,
    [FECHADIAGINI]           DATE          NULL,
    [CRITERIODIAG]           INT           NULL,
    [FECHATOMA1]             DATE          NULL,
    [FECHARESUL1]            DATE          NULL,
    [CRITERIOCONF]           INT           NULL,
    [FECHATOMA2]             DATE          NULL,
    [FECHARESUL2]            DATE          NULL,
    [CODDIAGNO]              CHAR (4)      NULL,
    [FECHAINICIOTRATAMIENTO] DATE          NULL,
    [VERSION]                VARCHAR (20)  NULL,
    [JSON]                   VARCHAR (MAX) NULL,
    CONSTRAINT [PK_HCFICHA115_1] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [CK_HCFICHA115_JSON] CHECK (isjson([JSON])=(1)),
    CONSTRAINT [FK_HCFICHA115_HCFICHANOTIFICACION1] FOREIGN KEY ([IDFICHANOTIFICACION]) REFERENCES [dbo].[HCFICHANOTIFICACION] ([ID])
);


GO
ALTER TABLE [dbo].[HCFICHA115] NOCHECK CONSTRAINT [CK_HCFICHA115_JSON];




GO
ALTER TABLE [dbo].[HCFICHA115] NOCHECK CONSTRAINT [CK_HCFICHA115_JSON];


GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Payload JSON (VARCHAR MAX) con columnas ampliadas de la ficha oncológica; datos no estructurados adicionales para notificación de cáncer/tumor infantil.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA115', @level2type = N'COLUMN', @level2name = N'JSON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nuevas columnas en formato JSON ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA115', @level2type = N'COLUMN', @level2name = N'JSON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA115', @level2type = N'COLUMN', @level2name = N'JSON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Versión de ficha notificación oncológica (VARCHAR 20); NULL = primera versión, control de cambios históricos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA115', @level2type = N'COLUMN', @level2name = N'VERSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Versión de la ficha de notificación ---> Si es valor nulo = primer versión ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA115', @level2type = N'COLUMN', @level2name = N'VERSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA115', @level2type = N'COLUMN', @level2name = N'VERSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha inicio tratamiento oncológico (DATE); marca inicio de quimioterapia, radioterapia o intervención quirúrgica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA115', @level2type = N'COLUMN', @level2name = N'FECHAINICIOTRATAMIENTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha inicio de tratamiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA115', @level2type = N'COLUMN', @level2name = N'FECHAINICIOTRATAMIENTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA115', @level2type = N'COLUMN', @level2name = N'FECHAINICIOTRATAMIENTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código diagnóstico ICD-O o CIE-10 (CHAR 4); clasificación topográfica/histológica de neoplasia maligna.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA115', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA115', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA115', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha resultado segundo examen confirmatorio (DATE); mielograma, histopatología, citología, inmunotipificación, citogenética o radiología.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA115', @level2type = N'COLUMN', @level2name = N'FECHARESUL2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de resultado 2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA115', @level2type = N'COLUMN', @level2name = N'FECHARESUL2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA115', @level2type = N'COLUMN', @level2name = N'FECHARESUL2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha toma segunda muestra confirmación (DATE); para análisis citológico, histopatológico o citogenético del tumor.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA115', @level2type = N'COLUMN', @level2name = N'FECHATOMA2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de toma 2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA115', @level2type = N'COLUMN', @level2name = N'FECHATOMA2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA115', @level2type = N'COLUMN', @level2name = N'FECHATOMA2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Criterio confirmación diagnóstico (INT); 1=Mielograma, 2=Histopatología/citología fluido, 3=Inmunotipificación, 4=Criterio médico especializado, 5=Certificado defunción, 6=Citogenética, 7=Radiología diagnóstica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA115', @level2type = N'COLUMN', @level2name = N'CRITERIOCONF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Criterio confirmación del diagnóstico  1=Mielograma  2=Histopatología o citología de fluido corporal  3=Inmunotipificación  4=Criterio médico especializado  5=Certificado de defunción  6=Citogenética  7=Radiología diagnóstica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA115', @level2type = N'COLUMN', @level2name = N'CRITERIOCONF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA115', @level2type = N'COLUMN', @level2name = N'CRITERIOCONF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha resultado primer examen diagnóstico (DATE); hemograma, radiología, gammagrafía, marcadores tumorales o valoración clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA115', @level2type = N'COLUMN', @level2name = N'FECHARESUL1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de resultado 1', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA115', @level2type = N'COLUMN', @level2name = N'FECHARESUL1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA115', @level2type = N'COLUMN', @level2name = N'FECHARESUL1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha toma primera muestra diagnóstica (DATE); sangre, biopsia o estudio radiológico inicial del paciente oncológico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA115', @level2type = N'COLUMN', @level2name = N'FECHATOMA1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de toma 1', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA115', @level2type = N'COLUMN', @level2name = N'FECHATOMA1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA115', @level2type = N'COLUMN', @level2name = N'FECHATOMA1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Criterio diagnóstico probable (INT); 1=Hemograma/extendido sangre periférica, 2=Radiología diagnóstica, 3=Gammagrafía, 4=Marcadores tumorales, 5=Clínica sin otra ayuda diagnóstica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA115', @level2type = N'COLUMN', @level2name = N'CRITERIODIAG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Criterio diagnóstico probable  1=Hemograma o extendido de sangre periférica  2=Radiología diagnóstica  3=Gammagrafía  4=Marcadores tumorales  5=Clínica sin otra ayuda diagnóstica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA115', @level2type = N'COLUMN', @level2name = N'CRITERIODIAG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA115', @level2type = N'COLUMN', @level2name = N'CRITERIODIAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha diagnóstico inicial oncológico (DATE); primer registro/confirmación de neoplasia maligna o cáncer.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA115', @level2type = N'COLUMN', @level2name = N'FECHADIAGINI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha diagnóstico inicial', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA115', @level2type = N'COLUMN', @level2name = N'FECHADIAGINI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA115', @level2type = N'COLUMN', @level2name = N'FECHADIAGINI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta actual por recaída tumoral (BIT); True=Sí, False=No; paciente con antecedente oncológico que reactiva enfermedad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA115', @level2type = N'COLUMN', @level2name = N'CONSULTARECA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consulta actual por recaída  True = SiFalse = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA115', @level2type = N'COLUMN', @level2name = N'CONSULTARECA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA115', @level2type = N'COLUMN', @level2name = N'CONSULTARECA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta actual por segunda neoplasia (BIT); True=Sí, False=No; paciente con historia previa de cáncer diagnostica nueva malignidad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA115', @level2type = N'COLUMN', @level2name = N'CONSULTANEO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consulta actual por segunda neoplasia  True = Si  False = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA115', @level2type = N'COLUMN', @level2name = N'CONSULTANEO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA115', @level2type = N'COLUMN', @level2name = N'CONSULTANEO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo cáncer/tumor infantil (INT); 1=Leucemia linfoide aguda, 2=Leucemia mieloide aguda, 3=Otras leucemias, 4=Linfomas/neoplasias, 5=Tumores SNC, 6=Neuroblastoma, 7=Retinoblastoma, 8=Tumores renales, 9=Hepatocarcinoma, 10=Tumores óseos malignos, 11=Sarcomas tejidos blandos, 12=Tumores germinales, 13=Tumores epiteliales/melanoma, 14=Otras neoplasias.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA115', @level2type = N'COLUMN', @level2name = N'TIPOTUMOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Cancer or Tumor  1:Leucemia linfoide aguda    2:Leucemia mieloide aguda  3:Otras leucemias  4:Linfomas y neoplasias  5:Tumores del sistema nervioso central  6:Neuroblastoma y otros tumores de células nerviosas periféricas  7:Retinoblastoma  8:Tumores renales  9:Tumores hepáticos  10:Tumores óseos malignos  11:Sarcomas de tejidos blandos y extra óseos  12:Tumores germinales trofoblásticos y otros gonadales  13:Tumores epiteliales malignos y melanoma  14:Otras neoplasias malignas no especificadas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA115', @level2type = N'COLUMN', @level2name = N'TIPOTUMOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA115', @level2type = N'COLUMN', @level2name = N'TIPOTUMOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador ficha notificación oncológica (INT FK); vincula a tabla HCFICHANOTIFICACION; notificación RIPS cáncer/tumor infantil.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA115', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de ficha Notificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA115', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA115', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autonumérico registro (INT PK); clave primaria tabla HCFICHA115, notificación oncológica pediátrica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA115', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA115', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA115', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ficha 115 de notificación obligatoria de casos de cáncer (tumores). Registra los datos clínicos y diagnósticos de la notificación de neoplasias: tipo de tumor, criterios diagnósticos, fechas de diagnóstico, toma de muestras, resultados y tratamiento, conforme al sistema de vigilancia epidemiológica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA115';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA115';
