CREATE TABLE [dbo].[AGERECURS] (
    [CODCONCEC]  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODIGOREC]  CHAR (6)      NOT NULL,
    [TIPORECUR]  CHAR (1)      NOT NULL,
    [DESCRIPREC] VARCHAR (100) NOT NULL,
    [ESTADOREC]  BIT           NOT NULL,
    CONSTRAINT [PK_AGERECURS_1] PRIMARY KEY CLUSTERED ([CODCONCEC] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del recurso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGERECURS', @level2type = N'COLUMN', @level2name = N'ESTADOREC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripción del recurso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGERECURS', @level2type = N'COLUMN', @level2name = N'DESCRIPREC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Define el Tipo de recurso F - Fisico, H - Humano', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGERECURS', @level2type = N'COLUMN', @level2name = N'TIPORECUR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de recurso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGERECURS', @level2type = N'COLUMN', @level2name = N'CODIGOREC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'codigo consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGERECURS', @level2type = N'COLUMN', @level2name = N'CODCONCEC';

