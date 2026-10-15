CREATE TABLE [dbo].[ADCATTRIU] (
    [TRIACATEG] CHAR (5)     NOT NULL,
    [TRIANOMCA] NCHAR (250)  NOT NULL,
    [TRIANIVEL] CHAR (1)     NOT NULL,
    [TRIACRITE] NCHAR (100)  NOT NULL,
    [INDAUDFOR] NUMERIC (18) NOT NULL,
    [ESTADO]    INT          NULL,
    CONSTRAINT [PK_ADCATTRIU] PRIMARY KEY CLUSTERED ([TRIACATEG] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado 1->Activo 2->Inactivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCATTRIU', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCATTRIU', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipos de Dx Sindromatico:  1: Abdominales  2: Cardiovasculares  3: GenitoUrinarios  4: MuscoloEsqueleticos  5: Neurologicos  6: Oculares  7: Otorrino  8: Patologia Dolor  9: Patologia Ocular  10: Patologia Psiquiatria  11: Patologia Piel  12: Patologia Trauma  13: Respiratorios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCATTRIU', @level2type = N'COLUMN', @level2name = N'TRIACRITE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nivel de la Clasificacion del Triage', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCATTRIU', @level2type = N'COLUMN', @level2name = N'TRIANIVEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion del Dx Sindromatico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCATTRIU', @level2type = N'COLUMN', @level2name = N'TRIANOMCA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Dx Sindromatico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCATTRIU', @level2type = N'COLUMN', @level2name = N'TRIACATEG';

