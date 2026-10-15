CREATE TABLE [dbo].[ADGRUETNI] (
    [CODGRUPOE] CHAR (3)     NOT NULL,
    [DESGRUPET] VARCHAR (70) NOT NULL,
    [TIPOET]    VARCHAR (3)  NULL,
    [ESTADO]    TINYINT      NULL,
    CONSTRAINT [PK_ADGRUETNI] PRIMARY KEY CLUSTERED ([CODGRUPOE] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Activo: 1->Si 2->No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADGRUETNI', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo etinico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADGRUETNI', @level2type = N'COLUMN', @level2name = N'TIPOET';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion del grupo etnico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADGRUETNI', @level2type = N'COLUMN', @level2name = N'DESGRUPET';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del grupo etnico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADGRUETNI', @level2type = N'COLUMN', @level2name = N'CODGRUPOE';

