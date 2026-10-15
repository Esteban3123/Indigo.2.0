CREATE TABLE [dbo].[HCREGEGRI] (
    [IDETIPHIS] CHAR (9)                                                                         NOT NULL,
    [NUMEFOLIO] NCHAR (10)                                                                       NOT NULL,
    [IPCODPACI] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES] CHAR (10)                                                                        NOT NULL,
    [CODCENATE] CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO] CHAR (10)                                                                        NOT NULL,
    [FECALTPAC] DATETIME                                                                         NOT NULL,
    [ESTPACEGR] INT                                                                              NOT NULL,
    [FECMUEPAC] DATETIME                                                                         NULL,
    [CODCAUMUE] CHAR (3)                                                                         NULL,
    [NUMCERDEF] CHAR (40) MASKED WITH (FUNCTION = 'partial(0, "DeathCertificate_Ofuscado", 0)')  NULL,
    [DEPMUNCOD] CHAR (5)                                                                         NULL,
    [AIPSREMIS] CHAR (60)                                                                        NULL,
    [REHORASOL] DATETIME                                                                         NULL,
    [REHORCON]  DATETIME                                                                         NULL,
    [REHORLLE]  DATETIME                                                                         NULL,
    [REMINIVEL] INT                                                                              NULL,
    [REESPECIA] CHAR (60)                                                                        NULL,
    [REPERCONF] CHAR (60)                                                                        NULL,
    [RESERVICI] CHAR (60)                                                                        NULL,
    [IOBSERVAC] VARCHAR (MAX)                                                                    NULL,
    [CODPROSAL] CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NULL,
    [CTRSALSEG] BIT                                                                              NULL,
    [INDAUDFOR] NUMERIC (18)                                                                     NOT NULL,
    [NOMSOLREM] VARCHAR (50)                                                                     NULL,
    [MOTIVEMI]  CHAR (1)                                                                         NULL,
    CONSTRAINT [PK_HCREGEGRI] PRIMARY KEY CLUSTERED ([NUMINGRES] ASC),
    CONSTRAINT [FK_HCREGEGRI_ADcenaten] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCREGEGRI_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCREGEGRI_INCAUMUER] FOREIGN KEY ([CODCAUMUE]) REFERENCES [dbo].[INCAUMUER] ([CODCAUMUE]),
    CONSTRAINT [FK_HCREGEGRI_INMunicip] FOREIGN KEY ([DEPMUNCOD]) REFERENCES [dbo].[INMUNICIP] ([DEPMUNCOD]),
    CONSTRAINT [FK_HCREGEGRI_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCREGEGRI_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCREGEGRI_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[HCREGEGRI] NOCHECK CONSTRAINT [FK_HCREGEGRI_ADcenaten];


GO
ALTER TABLE [dbo].[HCREGEGRI] NOCHECK CONSTRAINT [FK_HCREGEGRI_ADINGRESO];


GO
ALTER TABLE [dbo].[HCREGEGRI] NOCHECK CONSTRAINT [FK_HCREGEGRI_INCAUMUER];


GO
ALTER TABLE [dbo].[HCREGEGRI] NOCHECK CONSTRAINT [FK_HCREGEGRI_INMunicip];


GO
ALTER TABLE [dbo].[HCREGEGRI] NOCHECK CONSTRAINT [FK_HCREGEGRI_INPACIENT];


GO
ALTER TABLE [dbo].[HCREGEGRI] NOCHECK CONSTRAINT [FK_HCREGEGRI_INPROFSAL];


GO
ALTER TABLE [dbo].[HCREGEGRI] NOCHECK CONSTRAINT [FK_HCREGEGRI_INUNIFUNC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si el egreso ya tiene el control de Salida y Seguimiento  Null -> No tiene el control por lo tanto se muestra  True -> Ya tiene el control no se muestra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRI', @level2type = N'COLUMN', @level2name = N'CTRSALSEG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del Paciente al Egreso   1: Mejor  2: Igual o Peor  3: Fallecido  4: Remitido  5: Hospitalizacion en Casa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRI', @level2type = N'COLUMN', @level2name = N'ESTPACEGR';

