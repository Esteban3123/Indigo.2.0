CREATE TABLE [dbo].[ADNIVELED] (
    [NIVECODIGO] CHAR (3)    NOT NULL,
    [NIVEDESCRI] CHAR (100)  NULL,
    [TIPONIVEL]  VARCHAR (3) NULL,
    [ESTADO]     TINYINT     NULL,
    CONSTRAINT [PK_ADNIVELED] PRIMARY KEY CLUSTERED ([NIVECODIGO] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Activo: 1->Si 2->No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADNIVELED', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo Nivel', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADNIVELED', @level2type = N'COLUMN', @level2name = N'TIPONIVEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion nivel educativo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADNIVELED', @level2type = N'COLUMN', @level2name = N'NIVEDESCRI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo nivel educativo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADNIVELED', @level2type = N'COLUMN', @level2name = N'NIVECODIGO';

