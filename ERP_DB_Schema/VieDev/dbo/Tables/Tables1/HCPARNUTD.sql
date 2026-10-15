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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda un JSON con las respuestas a los campos de adulto en la seccion de alertas de nutrientes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTD', @level2type = N'COLUMN', @level2name = N'AdultNutrientAlert';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda un JSON con las respuestas a los campos de pediatrico en la seccion de alertas de nutrientes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTD', @level2type = N'COLUMN', @level2name = N'PediatricNutrientAlert';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda un JSON con las respuestas a los campos de lactante en la seccion de alertas de nutrientes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTD', @level2type = N'COLUMN', @level2name = N'BreastfeedingNutrientAlert';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda un JSON con las respuestas a los campos de neonato en la seccion de alertas de nutrientes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTD', @level2type = N'COLUMN', @level2name = N'NewbornNutrientAlert';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el tipo unidad dosis  1 = (mg/kg/min)    2 = (g/Kg/día)    3 = (mEq/Kg/día)    4 = (mg/Kg/día)    5 = (mL/día)  6 = (UI/mLdía)  7 = (mg/día)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTD', @level2type = N'COLUMN', @level2name = N'TIPOVOLUMEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guardao la referencia nutriente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTD', @level2type = N'COLUMN', @level2name = N'ReferenceVariable';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tiene gravedad específica (gr)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTD', @level2type = N'COLUMN', @level2name = N'SpecifiedGravity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Formula del nutriente para calcular la correcion de purga durante la prescripción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTD', @level2type = N'COLUMN', @level2name = N'CORRPURGA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Formula del nutriente para calcular el volumen administrar durante la prescripción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTD', @level2type = N'COLUMN', @level2name = N'VOLUADM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indicaciones del nutriente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTD', @level2type = N'COLUMN', @level2name = N'INDICATIONS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad de medida del medicamento asociado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTD', @level2type = N'COLUMN', @level2name = N'CODUNIMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del medicamento al que esta asociado el nutriente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTD', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el Estado del nutriente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTD', @level2type = N'COLUMN', @level2name = N'STATUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la concentracion del medicamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTD', @level2type = N'COLUMN', @level2name = N'CONCENMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del nutriente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTD', @level2type = N'COLUMN', @level2name = N'CODE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de la cabecera', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTD', @level2type = N'COLUMN', @level2name = N'HCPARNUTCID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID del detalle de los nutrientes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTD', @level2type = N'COLUMN', @level2name = N'ID';

