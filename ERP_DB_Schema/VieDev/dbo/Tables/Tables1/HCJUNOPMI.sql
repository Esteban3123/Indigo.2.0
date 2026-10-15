CREATE TABLE [dbo].[HCJUNOPMI] (
    [CODCONCEC] CHAR (10)                                                                        NOT NULL,
    [CODPRONOP] CHAR (20)                                                                        NOT NULL,
    [CODPRODUC] CHAR (20)                                                                        NOT NULL,
    [IDETIPHIS] CHAR (9)                                                                         NOT NULL,
    [NUMEFOLIO] NCHAR (10)                                                                       NOT NULL,
    [IPCODPACI] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES] CHAR (10)                                                                        NOT NULL,
    [CODCENATE] CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO] CHAR (10)                                                                        NOT NULL,
    [CODPROSAL] CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [FECORDMED] DATETIME                                                                         NOT NULL,
    [CODVIAADM] VARCHAR (20)                                                                     NOT NULL,
    [CODFORMED] VARCHAR (20)                                                                     NOT NULL,
    [DOSISPROD] NUMERIC (18, 2)                                                                  NULL,
    [CODUNIMED] CHAR (3)                                                                         NULL,
    [FRECUENCI] INT                                                                              NULL,
    [UNIFRECUE] CHAR (1)                                                                         NULL,
    [TIPFORMED] CHAR (1)                                                                         NOT NULL,
    [DURACIDOS] CHAR (20)                                                                        NOT NULL,
    [VALDURFIJ] INT                                                                              NULL,
    [UNIDURFIJ] CHAR (1)                                                                         NULL,
    [CANPEDPRO] INT                                                                              NOT NULL,
    [CODDIAGNO] CHAR (4) MASKED WITH (FUNCTION = 'partial(0, "DiagnosticCode_Ofuscado", 0)')     NOT NULL,
    [DIASTRATA] INT                                                                              NULL,
    [MEDNOMEJO] BIT                                                                              NULL,
    [MEDREADVE] BIT                                                                              NULL,
    [MEDINTOLE] BIT                                                                              NULL,
    [MEDNOINDI] BIT                                                                              NULL,
    [MEDOBSERV] VARCHAR (500)                                                                    NULL,
    [INDAUDFOR] NUMERIC (18)                                                                     NOT NULL,
    CONSTRAINT [PK_HCJUNOPMI] PRIMARY KEY CLUSTERED ([CODCONCEC] ASC, [CODPRONOP] ASC, [CODPRODUC] ASC),
    CONSTRAINT [FK_HCJUNOPMI_ADcenaten] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCJUNOPMI_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCJUNOPMI_HCVIAADMI] FOREIGN KEY ([CODVIAADM]) REFERENCES [dbo].[HCVIAADMI] ([CODVIAADM]),
    CONSTRAINT [FK_HCJUNOPMI_IHFORMEDI] FOREIGN KEY ([CODFORMED]) REFERENCES [dbo].[IHFORMEDI] ([CODFORMED]),
    CONSTRAINT [FK_HCJUNOPMI_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCJUNOPMI_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[HCJUNOPMI] NOCHECK CONSTRAINT [FK_HCJUNOPMI_ADcenaten];


GO
ALTER TABLE [dbo].[HCJUNOPMI] NOCHECK CONSTRAINT [FK_HCJUNOPMI_ADINGRESO];


GO
ALTER TABLE [dbo].[HCJUNOPMI] NOCHECK CONSTRAINT [FK_HCJUNOPMI_HCVIAADMI];


GO
ALTER TABLE [dbo].[HCJUNOPMI] NOCHECK CONSTRAINT [FK_HCJUNOPMI_IHFORMEDI];


GO
ALTER TABLE [dbo].[HCJUNOPMI] NOCHECK CONSTRAINT [FK_HCJUNOPMI_INPACIENT];


GO
ALTER TABLE [dbo].[HCJUNOPMI] NOCHECK CONSTRAINT [FK_HCJUNOPMI_INUNIFUNC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indicacion Paciente:  1: Orden de Hospitalizacion  2: Urgencias  3: Dejar en Observacion  4: Cirugia  5: Remitir  6: Morgue  7: Remitir a Consulta Externa  8: Salida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMI', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad del Valor de la duracion fija:  1: Minutos  2: Horas  3: Dias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMI', @level2type = N'COLUMN', @level2name = N'UNIDURFIJ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Formulacion del medicamento:  1 Peso  2 Volumen  3 Peso-Volumen  4 Unidad de Administracion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMI', @level2type = N'COLUMN', @level2name = N'TIPFORMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad del Valor de la Frecuencia:  1: Minutos  2: Horas  3: Dias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMI', @level2type = N'COLUMN', @level2name = N'UNIFRECUE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Concecutivo del Formato No POS    Nota: Codigo 00000017', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMI', @level2type = N'COLUMN', @level2name = N'CODCONCEC';

