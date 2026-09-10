CREATE TABLE [dbo].[PYPPARAHISTPAG] (
    [ID]             INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDPYPPARAHISTO] INT          NOT NULL,
    [CODIGOPAG]      VARCHAR (50) NOT NULL,
    [VISIBLE]        BIT          NOT NULL,
    CONSTRAINT [PK_PYPPARAHISTOPAG] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT: 1=visible/activo, 0=oculto/inactivo) que controla si la sección de historia clínica se muestra en la interfaz de usuario.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PYPPARAHISTPAG', @level2type = N'COLUMN', @level2name = N'VISIBLE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda si es visible  1 = true  0 = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PYPPARAHISTPAG', @level2type = N'COLUMN', @level2name = N'VISIBLE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PYPPARAHISTPAG', @level2type = N'COLUMN', @level2name = N'VISIBLE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código numérico (1-11) que clasifica secciones/pestañas de historia clínica: TRIAGE, Información Médica, Anamnesis, Antecedentes, Revisión por Sistemas, Examen Físico, Paraclínicos, Diagnóstico/Problemas, Análisis, Órdenes Médicas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PYPPARAHISTPAG', @level2type = N'COLUMN', @level2name = N'CODIGOPAG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1-Clasificación TRIAGE  2- Informacion Médica General  3- Anamnesis  4-Antecedentes  5-Revision Por Sistemas  6-Objetivo - Examen Fisico  7-Paraclinico Ambulatorios  8-Diagnostico y/o Lista de Problemas  9-Paraclinicos  10-Analisis  11-Ordenes Medicas  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PYPPARAHISTPAG', @level2type = N'COLUMN', @level2name = N'CODIGOPAG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PYPPARAHISTPAG', @level2type = N'COLUMN', @level2name = N'CODIGOPAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de referencia a parámetros de historia clínica (FK), vincula registro a configuración de plantilla/sección de historia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PYPPARAHISTPAG', @level2type = N'COLUMN', @level2name = N'IDPYPPARAHISTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el Id pagues parametros historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PYPPARAHISTPAG', @level2type = N'COLUMN', @level2name = N'IDPYPPARAHISTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PYPPARAHISTPAG', @level2type = N'COLUMN', @level2name = N'IDPYPPARAHISTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (IDENTITY INT), consecutivo/número secuencial de la tabla PYPPARAHISTPAG.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PYPPARAHISTPAG', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PYPPARAHISTPAG', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PYPPARAHISTPAG', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Histórico de parámetros de pago: registra los códigos de pago asociados a cada parámetro histórico de nómina o liquidación, indicando si cada código es visible o está activo en el sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PYPPARAHISTPAG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PYPPARAHISTPAG';
