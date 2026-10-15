CREATE TABLE [dbo].[HCEPICRIS] (
    [NUMEPICRI]    CHAR (10)                                                                        NOT NULL,
    [IDETIPHIS]    CHAR (9)                                                                         NOT NULL,
    [NUMEFOLIO]    CHAR (10)                                                                        NOT NULL,
    [IPCODPACI]    VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]    CHAR (10)                                                                        NOT NULL,
    [CODCENATE]    CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]    CHAR (10)                                                                        NOT NULL,
    [CODPROSAL]    CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [FECREGEPI]    DATETIME                                                                         NOT NULL,
    [COMPLICAC]    VARCHAR (4000)                                                                   NOT NULL,
    [PRONOSTIC]    VARCHAR (4000) MASKED WITH (FUNCTION = 'partial(0, "Forecast_Ofuscado", 0)')     NOT NULL,
    [RECOMENDA]    VARCHAR (4000)                                                                   NOT NULL,
    [INDAUDFOR]    NUMERIC (18)                                                                     NOT NULL,
    [EPIRESUMIDA]  TEXT                                                                             NULL,
    [FECREGEPIRES] DATETIME                                                                         NULL,
    [FECMODEPIRES] DATETIME                                                                         NULL,
    [USUARIOCREA]  CHAR (20)                                                                        NULL,
    [USUARIOMOD]   CHAR (20)                                                                        NULL,
    CONSTRAINT [PK_HCEPICRIS] PRIMARY KEY CLUSTERED ([NUMEPICRI] ASC),
    CONSTRAINT [FK_HCEPICRIS_ADcenaten] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCEPICRIS_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCEPICRIS_INPacient] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCEPICRIS_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCEPICRIS_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[HCEPICRIS] NOCHECK CONSTRAINT [FK_HCEPICRIS_ADcenaten];


GO
ALTER TABLE [dbo].[HCEPICRIS] NOCHECK CONSTRAINT [FK_HCEPICRIS_ADINGRESO];


GO
ALTER TABLE [dbo].[HCEPICRIS] NOCHECK CONSTRAINT [FK_HCEPICRIS_INPacient];


GO
ALTER TABLE [dbo].[HCEPICRIS] NOCHECK CONSTRAINT [FK_HCEPICRIS_INPROFSAL];


GO
ALTER TABLE [dbo].[HCEPICRIS] NOCHECK CONSTRAINT [FK_HCEPICRIS_INUNIFUNC];


GO
CREATE NONCLUSTERED INDEX [IX_HCEPICRIS_NUMINGRES>]
    ON [dbo].[HCEPICRIS]([NUMINGRES] ASC)
    INCLUDE([FECREGEPI]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indicacion Paciente:  1: Orden de Hospitalizacion  2: Urgencias  3: Dejar en Observacion  4: Cirugia  5: Remitir  6: Morgue  7: Remitir a Consulta Externa  8: Salida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEPICRIS', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';

