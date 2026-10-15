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
CREATE NONCLUSTERED INDEX [_dta_index_INTERDETA_7_1099919040__K3_K1_K4_K2_K5_6]
    ON [dbo].[INTERDETA]([AUTOLABOR] ASC, [AUTO] ASC, [CODSERIPS] ASC, [CODCONCEC] ASC, [NUMEFOLIO] ASC)
    INCLUDE([ORDTIP]);


GO
CREATE NONCLUSTERED INDEX [IX_INTERDETA_ORDTIP]
    ON [dbo].[INTERDETA]([ORDTIP] ASC)
    INCLUDE([CODCONCEC], [AUTOLABOR]);


GO
CREATE NONCLUSTERED INDEX [_dta_index_INTERDETA_7_1099919040__K4_K3_K2_K5_K1_6]
    ON [dbo].[INTERDETA]([CODSERIPS] ASC, [AUTOLABOR] ASC, [CODCONCEC] ASC, [NUMEFOLIO] ASC, [AUTO] ASC)
    INCLUDE([ORDTIP]);


GO
CREATE NONCLUSTERED INDEX [_dta_index_INTERDETA_6_1099919040__K2_K4_K3]
    ON [dbo].[INTERDETA]([CODCONCEC] ASC, [CODSERIPS] ASC, [AUTOLABOR] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 - Laboratorios  2 - Patologias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERDETA', @level2type = N'COLUMN', @level2name = N'TIPOINTERFAZ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Orden AMB-Ambulatorio    INT-Intrahospitalario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERDETA', @level2type = N'COLUMN', @level2name = N'ORDTIP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERDETA', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Servicio IPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERDETA', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla de laboratorios y patologias.    puede ser el Auto de las sig, tablas:     Laboratorios:    HCORDLABO  AMBORDLAB    Patologias:    HCORDPATO  AMBORDPAT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERDETA', @level2type = N'COLUMN', @level2name = N'AUTOLABOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del consecutivo de INTERCABE', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERDETA', @level2type = N'COLUMN', @level2name = N'CODCONCEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico Interno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERDETA', @level2type = N'COLUMN', @level2name = N'AUTO';

