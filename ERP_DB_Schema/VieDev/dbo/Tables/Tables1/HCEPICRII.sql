CREATE TABLE [dbo].[HCEPICRII] (
    [NUMEPICRI] CHAR (10)                                                                        NOT NULL,
    [IDETIPHIS] CHAR (9)                                                                         NOT NULL,
    [NUMEFOLIO] CHAR (10)                                                                        NOT NULL,
    [IPCODPACI] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES] CHAR (10)                                                                        NOT NULL,
    [CODCENATE] CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO] CHAR (10)                                                                        NOT NULL,
    [CODPROSAL] CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [FECREGEPI] DATETIME                                                                         NOT NULL,
    [COMPLICAC] VARCHAR (4000)                                                                   NOT NULL,
    [PRONOSTIC] VARCHAR (4000) MASKED WITH (FUNCTION = 'partial(0, "Forecast_Ofuscado", 0)')     NOT NULL,
    [RECOMENDA] VARCHAR (4000)                                                                   NOT NULL,
    [INDAUDFOR] NUMERIC (18)                                                                     NOT NULL,
    CONSTRAINT [PK_HCEPICRII] PRIMARY KEY CLUSTERED ([NUMEPICRI] ASC),
    CONSTRAINT [FK_HCEPICRII_ADcenaten] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCEPICRII_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCEPICRII_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCEPICRII_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCEPICRII_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[HCEPICRII] NOCHECK CONSTRAINT [FK_HCEPICRII_ADcenaten];


GO
ALTER TABLE [dbo].[HCEPICRII] NOCHECK CONSTRAINT [FK_HCEPICRII_ADINGRESO];


GO
ALTER TABLE [dbo].[HCEPICRII] NOCHECK CONSTRAINT [FK_HCEPICRII_INPACIENT];


GO
ALTER TABLE [dbo].[HCEPICRII] NOCHECK CONSTRAINT [FK_HCEPICRII_INPROFSAL];


GO
ALTER TABLE [dbo].[HCEPICRII] NOCHECK CONSTRAINT [FK_HCEPICRII_INUNIFUNC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indicacion Paciente:  1: Orden de Hospitalizacion  2: Urgencias  3: Dejar en Observacion  4: Cirugia  5: Remitir  6: Morgue  7: Remitir a Consulta Externa  8: Salida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEPICRII', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';

