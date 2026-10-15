CREATE TABLE [dbo].[INTERDETA] (
    [AUTO]         INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODCONCEC]    INT       NOT NULL,
    [AUTOLABOR]    INT       NOT NULL,
    [CODSERIPS]    CHAR (20) NOT NULL,
    [NUMEFOLIO]    CHAR (10) NULL,
    [ORDTIP]       CHAR (3)  NULL,
    [TIPOINTERFAZ] TINYINT   NULL,
    CONSTRAINT [PK_INTERDETA] PRIMARY KEY CLUSTERED ([AUTO] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_INTERDETA_ORDTIP]
    ON [dbo].[INTERDETA]([ORDTIP] ASC)
    INCLUDE([CODCONCEC], [AUTOLABOR]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 - Laboratorios  2 - Patologias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERDETA', @level2type = N'COLUMN', @level2name = N'TIPOINTERFAZ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla de laboratorios y patologias.    puede ser el Auto de las sig, tablas:     Laboratorios:    HCORDLABO  AMBORDLAB    Patologias:    HCORDPATO  AMBORDPAT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERDETA', @level2type = N'COLUMN', @level2name = N'AUTOLABOR';

