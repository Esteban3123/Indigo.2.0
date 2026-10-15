CREATE TABLE [dbo].[AGENTURSAL] (
    [ID]          INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDSALA]      INT       NOT NULL,
    [IDEQUIPOTRA] INT       NOT NULL,
    [NUMTURNO]    INT       NOT NULL,
    [FECHAINI]    DATETIME  NULL,
    [FECHAFIN]    DATETIME  NULL,
    [FECCREREG]   DATETIME  NULL,
    [USUCREREG]   CHAR (20) NULL,
    CONSTRAINT [PK_AGENTURSAL] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario de creación del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENTURSAL', @level2type = N'COLUMN', @level2name = N'USUCREREG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de creación del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENTURSAL', @level2type = N'COLUMN', @level2name = N'FECCREREG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de fin de la agenda', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENTURSAL', @level2type = N'COLUMN', @level2name = N'FECHAFIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de inicio de la agenda', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENTURSAL', @level2type = N'COLUMN', @level2name = N'FECHAINI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número del Turno de sala', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENTURSAL', @level2type = N'COLUMN', @level2name = N'NUMTURNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del equipo de tratamiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENTURSAL', @level2type = N'COLUMN', @level2name = N'IDEQUIPOTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la Sala', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENTURSAL', @level2type = N'COLUMN', @level2name = N'IDSALA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la agenda del turno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENTURSAL', @level2type = N'COLUMN', @level2name = N'ID';

