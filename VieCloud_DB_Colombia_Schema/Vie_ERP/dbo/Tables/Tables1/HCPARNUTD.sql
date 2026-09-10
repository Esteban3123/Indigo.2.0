CREATE TABLE [dbo].[HCPARNUTD] (
    [ID]                         INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [HCPARNUTCID]                INT            NULL,
    [CODE]                       CHAR (3)       NOT NULL,
    [CONCENMED]                  CHAR (50)      NULL,
    [STATUS]                     BIT            NOT NULL,
    [CODPRODUC]                  CHAR (20)      NULL,
    [CODUNIMED]                  VARCHAR (20)   NULL,
    [INDICATIONS]                VARCHAR (MAX)  NULL,
    [VOLUADM]                    VARCHAR (MAX)  NULL,
    [CORRPURGA]                  VARCHAR (MAX)  NULL,
    [SpecifiedGravity]           NUMERIC (7, 3) NULL,
    [ReferenceVariable]          VARCHAR (300)  NULL,
    [TIPOVOLUMEN]                INT            NULL,
    [NewbornNutrientAlert]       VARCHAR (2000) NULL,
    [BreastfeedingNutrientAlert] VARCHAR (2000) NULL,
    [PediatricNutrientAlert]     VARCHAR (2000) NULL,
    [AdultNutrientAlert]         VARCHAR (2000) NULL,
    CONSTRAINT [PK__HCPARNUT__3214EC276A3325D7] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'JSON con respuestas de alertas nutricionales para pacientes adultos; contiene validaciones y advertencias específicas del régimen de nutrición en adultos (VARCHAR 2000)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTD', @level2type = N'COLUMN', @level2name = N'AdultNutrientAlert';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda un JSON con las respuestas a los campos de adulto en la seccion de alertas de nutrientes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTD', @level2type = N'COLUMN', @level2name = N'AdultNutrientAlert';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTD', @level2type = N'COLUMN', @level2name = N'AdultNutrientAlert';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'JSON con respuestas de alertas nutricionales para pacientes pediátricos; contiene validaciones y advertencias específicas del régimen de nutrición en niños (VARCHAR 2000)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTD', @level2type = N'COLUMN', @level2name = N'PediatricNutrientAlert';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda un JSON con las respuestas a los campos de pediatrico en la seccion de alertas de nutrientes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTD', @level2type = N'COLUMN', @level2name = N'PediatricNutrientAlert';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTD', @level2type = N'COLUMN', @level2name = N'PediatricNutrientAlert';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'JSON con respuestas de alertas nutricionales para lactantes; contiene validaciones y advertencias específicas del régimen de nutrición en bebés lactantes (VARCHAR 2000)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTD', @level2type = N'COLUMN', @level2name = N'BreastfeedingNutrientAlert';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda un JSON con las respuestas a los campos de lactante en la seccion de alertas de nutrientes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTD', @level2type = N'COLUMN', @level2name = N'BreastfeedingNutrientAlert';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTD', @level2type = N'COLUMN', @level2name = N'BreastfeedingNutrientAlert';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'JSON con respuestas de alertas nutricionales para neonatos; contiene validaciones y advertencias específicas del régimen de nutrición en recién nacidos (VARCHAR 2000)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTD', @level2type = N'COLUMN', @level2name = N'NewbornNutrientAlert';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda un JSON con las respuestas a los campos de neonato en la seccion de alertas de nutrientes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTD', @level2type = N'COLUMN', @level2name = N'NewbornNutrientAlert';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTD', @level2type = N'COLUMN', @level2name = N'NewbornNutrientAlert';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de unidad de dosis nutricional: 1=mg/kg/min, 2=g/Kg/día, 3=mEq/Kg/día, 4=mg/Kg/día, 5=mL/día, 6=UI/mL/día, 7=mg/día (INT, clave para cálculo de volumen)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTD', @level2type = N'COLUMN', @level2name = N'TIPOVOLUMEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el tipo unidad dosis  1 = (mg/kg/min)    2 = (g/Kg/día)    3 = (mEq/Kg/día)    4 = (mg/Kg/día)    5 = (mL/día)  6 = (UI/mLdía)  7 = (mg/día)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTD', @level2type = N'COLUMN', @level2name = N'TIPOVOLUMEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTD', @level2type = N'COLUMN', @level2name = N'TIPOVOLUMEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia o variable de nutriente utilizada como parámetro en cálculos de prescripción nutricional (VARCHAR 300)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTD', @level2type = N'COLUMN', @level2name = N'ReferenceVariable';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guardao la referencia nutriente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTD', @level2type = N'COLUMN', @level2name = N'ReferenceVariable';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTD', @level2type = N'COLUMN', @level2name = N'ReferenceVariable';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Gravedad específica del nutriente en gramos; propiedad fisicoquímica para preparación (NUMERIC 7,3)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTD', @level2type = N'COLUMN', @level2name = N'SpecifiedGravity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tiene gravedad específica (gr)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTD', @level2type = N'COLUMN', @level2name = N'SpecifiedGravity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTD', @level2type = N'COLUMN', @level2name = N'SpecifiedGravity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fórmula del nutriente para calcular la corrección de purga durante prescripción nutricional (VARCHAR MAX)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTD', @level2type = N'COLUMN', @level2name = N'CORRPURGA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Formula del nutriente para calcular la correcion de purga durante la prescripción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTD', @level2type = N'COLUMN', @level2name = N'CORRPURGA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTD', @level2type = N'COLUMN', @level2name = N'CORRPURGA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fórmula del nutriente para calcular el volumen a administrar durante prescripción nutricional (VARCHAR MAX)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTD', @level2type = N'COLUMN', @level2name = N'VOLUADM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Formula del nutriente para calcular el volumen administrar durante la prescripción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTD', @level2type = N'COLUMN', @level2name = N'VOLUADM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTD', @level2type = N'COLUMN', @level2name = N'VOLUADM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicaciones clínicas y terapéuticas del nutriente; condiciones médicas para su uso (VARCHAR MAX)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTD', @level2type = N'COLUMN', @level2name = N'INDICATIONS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indicaciones del nutriente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTD', @level2type = N'COLUMN', @level2name = N'INDICATIONS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTD', @level2type = N'COLUMN', @level2name = N'INDICATIONS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de unidad de medida del medicamento/nutriente asociado; equivale a presentación farmacéutica (VARCHAR 20)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTD', @level2type = N'COLUMN', @level2name = N'CODUNIMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad de medida del medicamento asociado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTD', @level2type = N'COLUMN', @level2name = N'CODUNIMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTD', @level2type = N'COLUMN', @level2name = N'CODUNIMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del medicamento o producto farmacéutico al que está asociado el nutriente (CHAR 20, FK implícita)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTD', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del medicamento al que esta asociado el nutriente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTD', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTD', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado activo/inactivo del registro de nutriente; BIT (1=activo, 0=inactivo)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTD', @level2type = N'COLUMN', @level2name = N'STATUS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el Estado del nutriente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTD', @level2type = N'COLUMN', @level2name = N'STATUS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTD', @level2type = N'COLUMN', @level2name = N'STATUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Concentración del medicamento/nutriente en la presentación (CHAR 50)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTD', @level2type = N'COLUMN', @level2name = N'CONCENMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la concentracion del medicamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTD', @level2type = N'COLUMN', @level2name = N'CONCENMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTD', @level2type = N'COLUMN', @level2name = N'CONCENMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador del nutriente; clave única de 3 caracteres (CHAR 3)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTD', @level2type = N'COLUMN', @level2name = N'CODE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del nutriente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTD', @level2type = N'COLUMN', @level2name = N'CODE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTD', @level2type = N'COLUMN', @level2name = N'CODE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de la cabecera/encabezado de nutrientes; referencia a registro padre (INT, FK)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTD', @level2type = N'COLUMN', @level2name = N'HCPARNUTCID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de la cabecera', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTD', @level2type = N'COLUMN', @level2name = N'HCPARNUTCID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTD', @level2type = N'COLUMN', @level2name = N'HCPARNUTCID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del detalle de nutrientes; clave primaria (INT IDENTITY)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTD', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID del detalle de los nutrientes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTD', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTD', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de nutrientes o componentes utilizados en la preparación de nutrición parenteral u otras fórmulas clínicas. Registra cada insumo o producto con su concentración, unidad de medida, indicaciones, volumen de administración y alertas nutricionales por tipo de paciente (recién nacido, lactancia, pediátrico, adulto).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTD';
