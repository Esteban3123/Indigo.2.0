CREATE TABLE [dbo].[PYPPARAHISTPAG] (
    [ID]             INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDPYPPARAHISTO] INT          NOT NULL,
    [CODIGOPAG]      VARCHAR (50) NOT NULL,
    [VISIBLE]        BIT          NOT NULL,
    CONSTRAINT [PK_PYPPARAHISTOPAG] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda si es visible  1 = true  0 = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PYPPARAHISTPAG', @level2type = N'COLUMN', @level2name = N'VISIBLE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1-Clasificación TRIAGE  2- Informacion Médica General  3- Anamnesis  4-Antecedentes  5-Revision Por Sistemas  6-Objetivo - Examen Fisico  7-Paraclinico Ambulatorios  8-Diagnostico y/o Lista de Problemas  9-Paraclinicos  10-Analisis  11-Ordenes Medicas  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PYPPARAHISTPAG', @level2type = N'COLUMN', @level2name = N'CODIGOPAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el Id pagues parametros historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PYPPARAHISTPAG', @level2type = N'COLUMN', @level2name = N'IDPYPPARAHISTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PYPPARAHISTPAG', @level2type = N'COLUMN', @level2name = N'ID';

