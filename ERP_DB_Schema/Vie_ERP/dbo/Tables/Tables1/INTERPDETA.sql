CREATE TABLE [dbo].[INTERPDETA] (
    [AUTO]      INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODCONCEC] INT       NOT NULL,
    [CODSERIPS] CHAR (20) NOT NULL,
    [NUMEFOLIO] CHAR (10) NULL,
    [ORDTIP]    CHAR (3)  NULL,
    [AUTOPATP]  INT       NOT NULL,
    CONSTRAINT [PK_INTERPDETA] PRIMARY KEY CLUSTERED ([AUTO] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_INDICE_INTERPDETA]
    ON [dbo].[INTERPDETA]([NUMEFOLIO] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(No se encontró documentación de este campo en la solución de crystal ni información por parte de los desarrolladores.)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERPDETA', @level2type = N'COLUMN', @level2name = N'AUTOPATP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Orden AMB-Ambulatorio    INT-Intrahospitalario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERPDETA', @level2type = N'COLUMN', @level2name = N'ORDTIP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERPDETA', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Servicio IPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERPDETA', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del consecutivo de INTERPCABE', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERPDETA', @level2type = N'COLUMN', @level2name = N'CODCONCEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico Interno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERPDETA', @level2type = N'COLUMN', @level2name = N'AUTO';

