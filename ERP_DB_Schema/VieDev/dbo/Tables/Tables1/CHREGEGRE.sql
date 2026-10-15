CREATE TABLE [dbo].[CHREGEGRE] (
    [NUMINGRES]    CHAR (10)                                                                        NOT NULL,
    [IPCODPACI]    VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [ESTPACEGR]    INT                                                                              NOT NULL,
    [FECMUEPAC]    DATETIME                                                                         NULL,
    [CODCAUMUE]    CHAR (3)                                                                         NULL,
    [NUMCERDEF]    CHAR (40) MASKED WITH (FUNCTION = 'partial(0, "DeathCertficate_Ofuscado", 0)')   NULL,
    [CODUSUARI]    CHAR (20)                                                                        NULL,
    [DEPMUNCOD]    CHAR (5)                                                                         NULL,
    [AIPSREMIS]    VARCHAR (MAX)                                                                    NULL,
    [IOBSERVAC]    VARCHAR (MAX)                                                                    NULL,
    [FECALTPAC]    DATETIME                                                                         NULL,
    [FECEGRESO]    DATETIME                                                                         NULL,
    [CODPROSAL]    CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NULL,
    [INDAUDFOR]    NUMERIC (18)                                                                     NULL,
    [TRANSPASIST]  TINYINT                                                                          NULL,
    [OTROTRANSP]   NCHAR (30)                                                                       NULL,
    [MEDIOTRANS]   TINYINT                                                                          NULL,
    [EMPRETRANS]   NCHAR (30)                                                                       NULL,
    [OTRAEMPTRA]   NCHAR (30)                                                                       NULL,
    [NOMRECIBE]    NCHAR (30)                                                                       NULL,
    [CARGO]        TINYINT                                                                          NULL,
    [OTROCARGO]    NCHAR (30)                                                                       NULL,
    [FECHATRANS]   DATETIME                                                                         NULL,
    [FECHASISTEMA] DATETIME                                                                         NULL,
    CONSTRAINT [PK_CHREGEGRE] PRIMARY KEY CLUSTERED ([NUMINGRES] ASC),
    CONSTRAINT [FK_CHREGEGRE_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_CHREGEGRE_CHREGEGRE] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[CHREGEGRE] ([NUMINGRES]),
    CONSTRAINT [FK_CHREGEGRE_INCAUMUER1] FOREIGN KEY ([CODCAUMUE]) REFERENCES [dbo].[INCAUMUER] ([CODCAUMUE]),
    CONSTRAINT [FK_CHREGEGRE_INMunicip] FOREIGN KEY ([DEPMUNCOD]) REFERENCES [dbo].[INMUNICIP] ([DEPMUNCOD]),
    CONSTRAINT [FK_CHREGEGRE_INPacient] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[CHREGEGRE] NOCHECK CONSTRAINT [FK_CHREGEGRE_ADINGRESO];


GO
ALTER TABLE [dbo].[CHREGEGRE] NOCHECK CONSTRAINT [FK_CHREGEGRE_CHREGEGRE];


GO
ALTER TABLE [dbo].[CHREGEGRE] NOCHECK CONSTRAINT [FK_CHREGEGRE_INCAUMUER1];


GO
ALTER TABLE [dbo].[CHREGEGRE] NOCHECK CONSTRAINT [FK_CHREGEGRE_INMunicip];


GO
ALTER TABLE [dbo].[CHREGEGRE] NOCHECK CONSTRAINT [FK_CHREGEGRE_INPacient];


GO
CREATE NONCLUSTERED INDEX [_dta_index_CHREGEGRE_6_1670205646__K2_K1_11]
    ON [dbo].[CHREGEGRE]([IPCODPACI] ASC, [NUMINGRES] ASC)
    INCLUDE([FECALTPAC]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cargo (Datos de translado)    Auxiliar 1  Enfermero(a) 2  Médico General 3  Médico Especialista 4  Otro 5', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGEGRE', @level2type = N'COLUMN', @level2name = N'CARGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Medio de transporte (Datos de translado)    Terrestre 1  Maritimo y/o fluial 2  Aéreo 3', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGEGRE', @level2type = N'COLUMN', @level2name = N'MEDIOTRANS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Transporte de Asisitencia (Datos de translado)    Basico 1  Medicalizado 2  Otro 3', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGEGRE', @level2type = N'COLUMN', @level2name = N'TRANSPASIST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del Paciente al Egreso   1: Mejor  2: Igual o Peor  3: Fallecido  4: Remitido  5: Hospitalizacion en Casa  6: Egreso creado por cunas de observacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGEGRE', @level2type = N'COLUMN', @level2name = N'ESTPACEGR';

