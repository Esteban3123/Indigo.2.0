CREATE TABLE [dbo].[HCRENAL2463C] (
    [ID]          INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CONSEC]      VARCHAR (10)  NOT NULL,
    [CODCENATE]   CHAR (10)     NOT NULL,
    [DESCRIPCI]   VARCHAR (100) NOT NULL,
    [EAPB]        CHAR (9)      NULL,
    [FECHINICIAL] DATE          NOT NULL,
    [FECHFINAL]   DATE          NOT NULL,
    [ESTADO]      TINYINT       NULL,
    [FECREGCRE]   DATETIME      NOT NULL,
    [CODUSUCRE]   CHAR (20)     NOT NULL,
    [FECREGMOD]   DATETIME      NULL,
    [CODUSUMOD]   CHAR (20)     NULL,
    [FECREGANU]   DATETIME      NULL,
    [CODUSUANU]   CHAR (20)     NULL,
    [FECCONFREG]  DATETIME      NULL,
    [CODUSUCONF]  CHAR (20)     NULL,
    [IDEAPBVIE]   INT           NULL,
    CONSTRAINT [PK_HCRENAL2463C] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la entidad administradora de VIE', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463C', @level2type = N'COLUMN', @level2name = N'IDEAPBVIE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código Confirmación del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463C', @level2type = N'COLUMN', @level2name = N'CODUSUCONF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Confirmación del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463C', @level2type = N'COLUMN', @level2name = N'FECCONFREG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'CODUSUANU', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463C', @level2type = N'COLUMN', @level2name = N'CODUSUANU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Anulacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463C', @level2type = N'COLUMN', @level2name = N'FECREGANU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario de Modificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463C', @level2type = N'COLUMN', @level2name = N'CODUSUMOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'FECREGMOD', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463C', @level2type = N'COLUMN', @level2name = N'FECREGMOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de Usuario de creacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463C', @level2type = N'COLUMN', @level2name = N'CODUSUCRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de  Creacion de registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463C', @level2type = N'COLUMN', @level2name = N'FECREGCRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del Documento.  1 - Registrado  2 - Confirmado  3 - Anulado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463C', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha final Reporte', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463C', @level2type = N'COLUMN', @level2name = N'FECHFINAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Inicial de reporte', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463C', @level2type = N'COLUMN', @level2name = N'FECHINICIAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Entidad Administradoras', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463C', @level2type = N'COLUMN', @level2name = N'EAPB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion de Documento Res. 2463', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463C', @level2type = N'COLUMN', @level2name = N'DESCRIPCI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Centro de  Atención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463C', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463C', @level2type = N'COLUMN', @level2name = N'CONSEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463C', @level2type = N'COLUMN', @level2name = N'ID';

