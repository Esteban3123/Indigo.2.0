CREATE TABLE [dbo].[AGDISPONSALA] (
    [ID]            INT                                                                           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODCENATE]     CHAR (10)                                                                     NOT NULL,
    [IDSALA]        INT                                                                           NOT NULL,
    [FECHORAIN]     DATETIME                                                                      NOT NULL,
    [FECHORAFI]     DATETIME                                                                      NOT NULL,
    [CODUSUARIO]    CHAR (20)                                                                     NOT NULL,
    [FECHACREACION] DATETIME                                                                      NOT NULL,
    [TURNOBLOQ]     BIT                                                                           NOT NULL,
    [MOSTRARWEB]    BIT                                                                           NULL,
    [CODPROSAL]     CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NULL,
    [CODESPECI]     CHAR (3)                                                                      NULL,
    [IndigoSyncId]  VARCHAR (36)                                                                  NULL,
    CONSTRAINT [PK_AGDISPONSALA] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK__AGDISPONS__CODES__32470242] FOREIGN KEY ([CODESPECI]) REFERENCES [dbo].[INESPECIA] ([CODESPECI]),
    CONSTRAINT [FK__AGDISPONS__CODPR__3152DE09] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_AGDISPONSALA_AGENSALAC] FOREIGN KEY ([IDSALA]) REFERENCES [dbo].[AGENSALAC] ([CODCONCEC])
);


GO
ALTER TABLE [dbo].[AGDISPONSALA] NOCHECK CONSTRAINT [FK__AGDISPONS__CODES__32470242];


GO
ALTER TABLE [dbo].[AGDISPONSALA] NOCHECK CONSTRAINT [FK__AGDISPONS__CODPR__3152DE09];


GO
ALTER TABLE [dbo].[AGDISPONSALA] NOCHECK CONSTRAINT [FK_AGDISPONSALA_AGENSALAC];


GO
CREATE NONCLUSTERED INDEX [IX_AGDISPONSALA_CODCENATE_IDSALA_FECHORAFI]
    ON [dbo].[AGDISPONSALA]([CODCENATE] ASC, [IDSALA] ASC, [FECHORAFI] ASC)
    INCLUDE([FECHORAIN]);


GO
CREATE NONCLUSTERED INDEX [IX_AGDISPONSALA_IDSALA_FECHORAFI_CODESPECI_CODPROSAL_FECHORAIN]
    ON [dbo].[AGDISPONSALA]([IDSALA] ASC, [FECHORAFI] ASC)
    INCLUDE([CODESPECI], [CODPROSAL], [FECHORAIN]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el codigo de le especialidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGDISPONSALA', @level2type = N'COLUMN', @level2name = N'CODESPECI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el codigo profesional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGDISPONSALA', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Mostrar citas En Web  1 = Si   0 = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGDISPONSALA', @level2type = N'COLUMN', @level2name = N'MOSTRARWEB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Bloque de Turno Medico  0 = False   1 = True ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGDISPONSALA', @level2type = N'COLUMN', @level2name = N'TURNOBLOQ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'la Fecha que creo el registro ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGDISPONSALA', @level2type = N'COLUMN', @level2name = N'FECHACREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGDISPONSALA', @level2type = N'COLUMN', @level2name = N'CODUSUARIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Final Seleccionado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGDISPONSALA', @level2type = N'COLUMN', @level2name = N'FECHORAFI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Inicial Seleccionado por Usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGDISPONSALA', @level2type = N'COLUMN', @level2name = N'FECHORAIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la Sala ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGDISPONSALA', @level2type = N'COLUMN', @level2name = N'IDSALA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Centro Atencion ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGDISPONSALA', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la Tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGDISPONSALA', @level2type = N'COLUMN', @level2name = N'ID';

