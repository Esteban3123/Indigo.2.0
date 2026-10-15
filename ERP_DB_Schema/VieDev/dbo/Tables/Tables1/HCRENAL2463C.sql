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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del Documento.  1 - Registrado  2 - Confirmado  3 - Anulado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENAL2463C', @level2type = N'COLUMN', @level2name = N'ESTADO';

