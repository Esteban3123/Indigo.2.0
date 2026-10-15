CREATE TABLE [dbo].[HCPRESCRI] (
    [IDETIPHIS]                   CHAR (9)                                                                         NOT NULL,
    [NUMEFOLIO]                   CHAR (10)                                                                        NOT NULL,
    [IPCODPACI]                   VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]                   CHAR (10)                                                                        NOT NULL,
    [CODCENATE]                   CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]                   CHAR (10)                                                                        NOT NULL,
    [CODPROSAL]                   CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [CODPRODUC]                   CHAR (20)                                                                        NOT NULL,
    [CODVIAADM]                   VARCHAR (20)                                                                     NOT NULL,
    [CODFORMED]                   VARCHAR (20)                                                                     NOT NULL,
    [DOSISPROD]                   NUMERIC (18, 2)                                                                  NULL,
    [CODUNIMED]                   VARCHAR (20)                                                                     NULL,
    [FRECUENCI]                   INT                                                                              NULL,
    [UNIFRECUE]                   CHAR (1)                                                                         NULL,
    [FECINIDOS]                   DATETIME                                                                         NOT NULL,
    [TIPFORMED]                   CHAR (1)                                                                         NOT NULL,
    [DURACIDOS]                   CHAR (20)                                                                        NOT NULL,
    [VALDURFIJ]                   INT                                                                              NULL,
    [UNIDURFIJ]                   CHAR (1)                                                                         NULL,
    [FECFINDOS]                   DATETIME                                                                         NULL,
    [PREFECANT]                   BIT                                                                              NOT NULL,
    [MOTPREANT]                   CHAR (250)                                                                       NULL,
    [PREESTADO]                   INT                                                                              NOT NULL,
    [CODCONCEC]                   NUMERIC (18)                                                                     NOT NULL,
    [CODDIAGNO]                   CHAR (4) MASKED WITH (FUNCTION = 'partial(0, "DiagnosticCode_Ofuscado", 0)')     NOT NULL,
    [INDAPLMED]                   VARCHAR (MAX)                                                                    NULL,
    [MOTSUSMED]                   VARCHAR (2000)                                                                   NULL,
    [NIVRIEPAC]                   CHAR (1)                                                                         NULL,
    [OBSNIVRIE]                   VARCHAR (1000)                                                                   NULL,
    [MANEXTPRO]                   BIT                                                                              NOT NULL,
    [CANPEDPRO]                   INT                                                                              NOT NULL,
    [NUMFOLSUS]                   CHAR (10)                                                                        NULL,
    [TOTPROUNI]                   NUMERIC (18, 2)                                                                  NULL,
    [MEDPENAGE]                   BIT                                                                              NULL,
    [FORMUMANU]                   BIT                                                                              NULL,
    [DESADMINI]                   VARCHAR (MAX)                                                                    NULL,
    [INDAUDFOR]                   NUMERIC (18)                                                                     NOT NULL,
    [DOSISPRFN]                   NUMERIC (18, 2)                                                                  NULL,
    [CODUNIMFN]                   CHAR (3)                                                                         NULL,
    [OBSJUSMEE]                   VARCHAR (500)                                                                    NULL,
    [CODJUMEES]                   CHAR (3)                                                                         NULL,
    [FORMAPRESCRIBE]              TINYINT                                                                          NULL,
    [NUMERODOSIS]                 TINYINT                                                                          NULL,
    [DOSISPROD1]                  NUMERIC (18, 2)                                                                  NULL,
    [DOSISPROD2]                  NUMERIC (18, 2)                                                                  NULL,
    [DOSISPROD3]                  NUMERIC (18, 2)                                                                  NULL,
    [DOSISPROD4]                  NUMERIC (18, 2)                                                                  NULL,
    [DOSISPROD5]                  NUMERIC (18, 2)                                                                  NULL,
    [DOSISPROD6]                  NUMERIC (18, 2)                                                                  NULL,
    [DOSISPROD7]                  NUMERIC (18, 2)                                                                  NULL,
    [DOSISPROD8]                  NUMERIC (18, 2)                                                                  NULL,
    [DOSISPROD9]                  NUMERIC (18, 2)                                                                  NULL,
    [DOSISPROD10]                 NUMERIC (18, 2)                                                                  NULL,
    [HORA1]                       DATETIME                                                                         NULL,
    [HORA2]                       DATETIME                                                                         NULL,
    [HORA3]                       DATETIME                                                                         NULL,
    [HORA4]                       DATETIME                                                                         NULL,
    [HORA5]                       DATETIME                                                                         NULL,
    [HORA6]                       DATETIME                                                                         NULL,
    [HORA7]                       DATETIME                                                                         NULL,
    [HORA8]                       DATETIME                                                                         NULL,
    [HORA9]                       DATETIME                                                                         NULL,
    [HORA10]                      DATETIME                                                                         NULL,
    [JUSTIFICARB]                 VARCHAR (500)                                                                    NULL,
    [JUSTIFICACIONPBS]            VARCHAR (2000)                                                                   NULL,
    [ReasonDiscontinuationOfDrug] INT                                                                              NULL,
    CONSTRAINT [PK_HCPRESCRI] PRIMARY KEY CLUSTERED ([IDETIPHIS] ASC, [NUMEFOLIO] ASC, [IPCODPACI] ASC, [CODPRODUC] ASC, [MANEXTPRO] ASC),
    CONSTRAINT [FK_HCPRESCRI_ADcenaten] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCPRESCRI_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCPRESCRI_HCVIAADMI] FOREIGN KEY ([CODVIAADM]) REFERENCES [dbo].[HCVIAADMI] ([CODVIAADM]),
    CONSTRAINT [FK_HCPRESCRI_IHFORMEDI] FOREIGN KEY ([CODFORMED]) REFERENCES [dbo].[IHFORMEDI] ([CODFORMED]),
    CONSTRAINT [FK_HCPRESCRI_INDIAGNOS] FOREIGN KEY ([CODDIAGNO]) REFERENCES [dbo].[INDIAGNOS] ([CODDIAGNO]),
    CONSTRAINT [FK_HCPRESCRI_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCPRESCRI_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO]),
    CONSTRAINT [FK_HCPRESCRI_INUNIMEDI] FOREIGN KEY ([CODUNIMED]) REFERENCES [dbo].[INUNIMEDI] ([CODUNIMED])
);


GO
ALTER TABLE [dbo].[HCPRESCRI] NOCHECK CONSTRAINT [FK_HCPRESCRI_ADcenaten];


GO
ALTER TABLE [dbo].[HCPRESCRI] NOCHECK CONSTRAINT [FK_HCPRESCRI_ADINGRESO];


GO
ALTER TABLE [dbo].[HCPRESCRI] NOCHECK CONSTRAINT [FK_HCPRESCRI_HCVIAADMI];


GO
ALTER TABLE [dbo].[HCPRESCRI] NOCHECK CONSTRAINT [FK_HCPRESCRI_IHFORMEDI];


GO
ALTER TABLE [dbo].[HCPRESCRI] NOCHECK CONSTRAINT [FK_HCPRESCRI_INDIAGNOS];


GO
ALTER TABLE [dbo].[HCPRESCRI] NOCHECK CONSTRAINT [FK_HCPRESCRI_INPACIENT];


GO
ALTER TABLE [dbo].[HCPRESCRI] NOCHECK CONSTRAINT [FK_HCPRESCRI_INUNIFUNC];


GO
ALTER TABLE [dbo].[HCPRESCRI] NOCHECK CONSTRAINT [FK_HCPRESCRI_INUNIMEDI];


GO
CREATE NONCLUSTERED INDEX [IX_HCPRESCRI_IPCODPACI_NUMEFOLIO_NUMINGRES]
    ON [dbo].[HCPRESCRI]([IPCODPACI] ASC, [NUMEFOLIO] ASC, [NUMINGRES] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Medicamento Pendiente de Agendar en la Hoja de Medicamentos:  True => pendiente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'MEDPENAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del Medicamento  1: Iniciado: Cuando el Medicamento se Solicita por Primera Vez  2: Ciclo Completado  3: Tratamiento descontinuado: Cuando existe una modificacion en la Dosificacion, Duracion o Frecuencia  4: Tratamiento Suspendido: Cuando el Medicamento es Suspendido  5: Plan de Manejo Externo: Cuando los Medicamentos son entregados por un Tercero  6: Medicamentos Solicitados sin Existencia Actual en el Kardex.  7: Tratamiento Terminado por Salida del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'PREESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad del Valor de la duracion fija:  1: Minutos  2: Horas  3: Dias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'UNIDURFIJ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Formulacion del medicamento:  1 Peso  2 Volumen  3 Peso-Volumen  4 Unidad de Administracion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'TIPFORMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad del Valor de la Frecuencia:  1: Minutos  2: Horas  3: Dias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'UNIFRECUE';

