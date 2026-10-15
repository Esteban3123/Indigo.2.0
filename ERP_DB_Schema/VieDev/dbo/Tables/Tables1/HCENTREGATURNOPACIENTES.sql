CREATE TABLE [dbo].[HCENTREGATURNOPACIENTES] (
    [ID]          INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCTURNO]   INT           NOT NULL,
    [CODICAMAS]   CHAR (50)     NULL,
    [IPCOPDACI]   VARCHAR (25)  NULL,
    [IPNOMCOMP]   CHAR (250)    NULL,
    [IPFECNACI]   DATETIME      NULL,
    [NOMENTIDA]   CHAR (150)    NULL,
    [OBSERVACION] VARCHAR (100) NULL,
    [ESTANCIA]    CHAR (4)      NOT NULL,
    [ALERTA]      CHAR (150)    NOT NULL,
    CONSTRAINT [PK_ENTREGATURNOPACIENTES] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOPACIENTES', @level2type = N'COLUMN', @level2name = N'ALERTA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOPACIENTES', @level2type = N'COLUMN', @level2name = N'ESTANCIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOPACIENTES', @level2type = N'COLUMN', @level2name = N'OBSERVACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOPACIENTES', @level2type = N'COLUMN', @level2name = N'NOMENTIDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOPACIENTES', @level2type = N'COLUMN', @level2name = N'IPFECNACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOPACIENTES', @level2type = N'COLUMN', @level2name = N'IPNOMCOMP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOPACIENTES', @level2type = N'COLUMN', @level2name = N'IPCOPDACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la cama del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOPACIENTES', @level2type = N'COLUMN', @level2name = N'CODICAMAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOPACIENTES', @level2type = N'COLUMN', @level2name = N'IDHCTURNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOPACIENTES', @level2type = N'COLUMN', @level2name = N'ID';

