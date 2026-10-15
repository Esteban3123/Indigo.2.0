CREATE TABLE [dbo].[CHCAMATRAZA] (
    [ID]            INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODICAMAS]     INT                                                                              NOT NULL,
    [CODCENATE]     CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]     CHAR (10)                                                                        NOT NULL,
    [ESTADOCAMA]    INT                                                                              NOT NULL,
    [IPCODPACI]     VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NULL,
    [NUMINGRES]     CHAR (10)                                                                        NULL,
    [IDCHREGEBLO]   INT                                                                              NULL,
    [FECHAINICIAL]  DATETIME                                                                         NOT NULL,
    [FECHAFINAL]    DATETIME                                                                         NULL,
    [FECHACREACION] DATETIME                                                                         NOT NULL,
    [CODUSUARI]     CHAR (20)                                                                        NOT NULL,
    [CODDIAGNO]     CHAR (4) MASKED WITH (FUNCTION = 'partial(0, "DiagnosticCode_Ofuscado", 0)')     NULL,
    CONSTRAINT [PK_CHCAMATRAZA] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_CHCAMATRAZA_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_CHCAMATRAZA_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_CHCAMATRAZA_CHCAMASHO] FOREIGN KEY ([CODICAMAS]) REFERENCES [dbo].[CHCAMASHO] ([CODICAMAS]),
    CONSTRAINT [FK_CHCAMATRAZA_CHREGEBLO] FOREIGN KEY ([IDCHREGEBLO]) REFERENCES [dbo].[CHREGEBLO] ([ID]),
    CONSTRAINT [FK_CHCAMATRAZA_INDIAGNOS] FOREIGN KEY ([CODDIAGNO]) REFERENCES [dbo].[INDIAGNOS] ([CODDIAGNO]),
    CONSTRAINT [FK_CHCAMATRAZA_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_CHCAMATRAZA_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[CHCAMATRAZA] NOCHECK CONSTRAINT [FK_CHCAMATRAZA_ADCENATEN];


GO
ALTER TABLE [dbo].[CHCAMATRAZA] NOCHECK CONSTRAINT [FK_CHCAMATRAZA_ADINGRESO];


GO
ALTER TABLE [dbo].[CHCAMATRAZA] NOCHECK CONSTRAINT [FK_CHCAMATRAZA_CHCAMASHO];


GO
ALTER TABLE [dbo].[CHCAMATRAZA] NOCHECK CONSTRAINT [FK_CHCAMATRAZA_CHREGEBLO];


GO
ALTER TABLE [dbo].[CHCAMATRAZA] NOCHECK CONSTRAINT [FK_CHCAMATRAZA_INDIAGNOS];


GO
ALTER TABLE [dbo].[CHCAMATRAZA] NOCHECK CONSTRAINT [FK_CHCAMATRAZA_INPACIENT];


GO
ALTER TABLE [dbo].[CHCAMATRAZA] NOCHECK CONSTRAINT [FK_CHCAMATRAZA_INUNIFUNC];


GO
CREATE NONCLUSTERED INDEX [IX_CHCAMATRAZA_CODICAMAS]
    ON [dbo].[CHCAMATRAZA]([CODICAMAS] ASC)
    INCLUDE([CODCENATE], [CODDIAGNO], [CODUSUARI], [ESTADOCAMA], [FECHACREACION], [FECHAFINAL], [FECHAINICIAL], [IDCHREGEBLO], [IPCODPACI], [NUMINGRES], [UFUCODIGO]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado de la Cama:    -1 = Libre     -  2 = Asignada   -3 = Inactiva  -4 = Bloqueada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMATRAZA', @level2type = N'COLUMN', @level2name = N'ESTADOCAMA';

