CREATE TABLE [dbo].[HCEXFISII] (
    [NUMCONSEC]                               INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDETIPHIS]                               CHAR (9)                                                                         NOT NULL,
    [NUMEFOLIO]                               CHAR (10)                                                                        NULL,
    [IPCODPACI]                               VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]                               CHAR (10)                                                                        NULL,
    [CODCENATE]                               CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]                               CHAR (10)                                                                        NOT NULL,
    [CODPROSAL]                               CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [FECREGITE]                               DATETIME                                                                         NOT NULL,
    [TENARTSIS]                               INT                                                                              NULL,
    [TENARTDIA]                               INT                                                                              NULL,
    [TEMPERPAC]                               CHAR (10) MASKED WITH (FUNCTION = 'partial(0, "Temperature_Ofuscado", 0)')       NULL,
    [FRECARPAC]                               CHAR (10) MASKED WITH (FUNCTION = 'partial(0, "HeartRate_Ofuscado", 0)')         NULL,
    [FRERESPAC]                               CHAR (10) MASKED WITH (FUNCTION = 'partial(0, "HeartRespiratory_Ofuscado", 0)')  NULL,
    [REGSO2PAC]                               CHAR (10)                                                                        NULL,
    [TALLAPACI]                               INT                                                                              NULL,
    [PESOPACIE]                               NUMERIC (18, 3)                                                                  NULL,
    [PRAEJEPAC]                               INT                                                                              NULL,
    [HABDIEPAC]                               INT                                                                              NULL,
    [FISOBSPAC]                               VARCHAR (4000)                                                                   NULL,
    [ANOCABPAC]                               BIT                                                                              NULL,
    [DESCABPAC]                               VARCHAR (2000)                                                                   NULL,
    [ANOOJOPAC]                               BIT                                                                              NULL,
    [DESOJOPAC]                               VARCHAR (2000)                                                                   NULL,
    [ANOORLPAC]                               BIT                                                                              NULL,
    [DESORLPAC]                               VARCHAR (2000)                                                                   NULL,
    [ANOCUEPAC]                               BIT                                                                              NULL,
    [DESCUEPAC]                               VARCHAR (2000)                                                                   NULL,
    [ANOCAPPAC]                               BIT                                                                              NULL,
    [DESCAPPAC]                               VARCHAR (2000)                                                                   NULL,
    [ANOABDPAC]                               BIT                                                                              NULL,
    [DESABDPAC]                               VARCHAR (2000)                                                                   NULL,
    [ANOGEUOAC]                               BIT                                                                              NULL,
    [DESGEUOAC]                               VARCHAR (2000)                                                                   NULL,
    [ANOEXTPAC]                               BIT                                                                              NULL,
    [DESEXTPAC]                               VARCHAR (2000)                                                                   NULL,
    [ANONEUPAC]                               BIT                                                                              NULL,
    [DESNEUPAC]                               VARCHAR (2000)                                                                   NULL,
    [ANOPIELPA]                               BIT                                                                              NULL,
    [DESPEILPA]                               VARCHAR (2000)                                                                   NULL,
    [NEOPERCEF]                               INT                                                                              NULL,
    [NEOPERTOR]                               INT                                                                              NULL,
    [NEOPERABD]                               INT                                                                              NULL,
    [SOPVENPAC]                               BIT                                                                              NULL,
    [SOPVENDES]                               VARCHAR (2000)                                                                   NULL,
    [SOPINOPAC]                               BIT                                                                              NULL,
    [SOPINODES]                               VARCHAR (2000)                                                                   NULL,
    [ACCESOPAC]                               BIT                                                                              NULL,
    [ACCESODES]                               VARCHAR (2000)                                                                   NULL,
    [UCIADUPVC]                               INT                                                                              NULL,
    [UCIADUCUN]                               INT                                                                              NULL,
    [UCIADUPIA]                               INT                                                                              NULL,
    [UCIADUGLU]                               INT                                                                              NULL,
    [UCIADULRG]                               INT                                                                              NULL,
    [UCIADUPIC]                               INT                                                                              NULL,
    [INDAUDFOR]                               NUMERIC (18)                                                                     NOT NULL,
    [FECREGSIS]                               DATETIME                                                                         CONSTRAINT [DF_HCEXFISII_FECREGSIS] DEFAULT ([Common].[getdate]()) NOT NULL,
    [DIURESIS]                                BIT                                                                              NULL,
    [MECONIO]                                 BIT                                                                              NULL,
    [SUCCION]                                 BIT                                                                              NULL,
    [DEGLUCION]                               BIT                                                                              NULL,
    [CONTINUALACTANCIA]                       INT                                                                              NULL,
    [PORQUENOCONTINUALACTANCIA]               VARCHAR (200)                                                                    NULL,
    [GRUPOSANGUINEO]                          INT                                                                              NULL,
    [RHSANGUINEO]                             BIT                                                                              NULL,
    [SENSIBILIZADA]                           INT                                                                              NULL,
    [COOMBS]                                  INT                                                                              NULL,
    [TSH]                                     NUMERIC (4, 2)                                                                   NULL,
    [SIFILISCONGENITA]                        INT                                                                              NULL,
    [PESOPARAEDADGESTACIONAL]                 INT                                                                              NULL,
    [SATURACIONPREDUCTAL]                     INT                                                                              NULL,
    [SATURACIONPOSDUCTAL]                     INT                                                                              NULL,
    [PAMIEMBROSUPERIORDERECHO]                VARCHAR (7)                                                                      NULL,
    [PAMIEMBROSUPERIORIZQUIERDO]              VARCHAR (7)                                                                      NULL,
    [PAMIEMBROINFERIORDERECHO]                VARCHAR (7)                                                                      NULL,
    [PAMIEMBROINFERIORIZQUIERDO]              VARCHAR (7)                                                                      NULL,
    [INTERPRETACIONTAMIZAJECARDIOPATIA]       INT                                                                              NULL,
    [OBSERVACIONESCARDIOPATIA]                VARCHAR (500)                                                                    NULL,
    [INSPECCIONOCULARBILATERAL]               INT                                                                              NULL,
    [ROJORETINIANODERECHO]                    INT                                                                              NULL,
    [ROJORETINIANOIZQUIERDO]                  INT                                                                              NULL,
    [INTERPRETACIONTAMIZAJEOCULAR]            INT                                                                              NULL,
    [OBSERVACIONESOCULAR]                     VARCHAR (500)                                                                    NULL,
    [INTERPRETACIONTAMIZAJEAUDITIVO]          VARCHAR (500)                                                                    NULL,
    [REALIZATAMIZAJEAUDITIVO]                 BIT                                                                              NULL,
    [SELECCIONINTERPRETACIONTAMIZAJEAUDITIVO] INT                                                                              NULL,
    CONSTRAINT [PK_HCEXFISII_1] PRIMARY KEY CLUSTERED ([NUMCONSEC] ASC),
    CONSTRAINT [FK_HCEXFISII_ADcenaten] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCEXFISII_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCEXFISII_INPacient] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCEXFISII_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[HCEXFISII] NOCHECK CONSTRAINT [FK_HCEXFISII_ADcenaten];


GO
ALTER TABLE [dbo].[HCEXFISII] NOCHECK CONSTRAINT [FK_HCEXFISII_ADINGRESO];


GO
ALTER TABLE [dbo].[HCEXFISII] NOCHECK CONSTRAINT [FK_HCEXFISII_INPacient];


GO
ALTER TABLE [dbo].[HCEXFISII] NOCHECK CONSTRAINT [FK_HCEXFISII_INUNIFUNC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacena el campo Seleccion Interpretacion Tamizaje Audtivo del agrupador Tamizaje Auditivo 1 - Normal  2 - Anormal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISII', @level2type = N'COLUMN', @level2name = N'SELECCIONINTERPRETACIONTAMIZAJEAUDITIVO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Realiza Tamizaje Auditivo campo para historia de recien nacido True-> Si, False-> No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISII', @level2type = N'COLUMN', @level2name = N'REALIZATAMIZAJEAUDITIVO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Interpretación del tamizaje auditivo del agrupador Tamizaje auditivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISII', @level2type = N'COLUMN', @level2name = N'INTERPRETACIONTAMIZAJEAUDITIVO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Observaciones del agrupador Tamizaje ocular', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISII', @level2type = N'COLUMN', @level2name = N'OBSERVACIONESOCULAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Interpretación del tamizaje ocular del agrupador Tamizaje ocular  1 - Normal  2 - Anormal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISII', @level2type = N'COLUMN', @level2name = N'INTERPRETACIONTAMIZAJEOCULAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Rojo retiniano ojo izquierdo del agrupador Tamizaje ocular  1 - Presente  2 - Ausente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISII', @level2type = N'COLUMN', @level2name = N'ROJORETINIANOIZQUIERDO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Rojo retiniano ojo derecho del agrupador Tamizaje ocular  1 - Presente  2 - Ausente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISII', @level2type = N'COLUMN', @level2name = N'ROJORETINIANODERECHO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Inspección ocular bilateral del agrupador Tamizaje ocular  1 - Normal  2 - Anormal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISII', @level2type = N'COLUMN', @level2name = N'INSPECCIONOCULARBILATERAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena las observaciones del agrupador Tamizaje de cardiopatía congénita', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISII', @level2type = N'COLUMN', @level2name = N'OBSERVACIONESCARDIOPATIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Interpretación del tamizaje para cardiopatía congénita del agrupador Tamizaje de cardiopatía congénita  1 - Normal  2 - Anormal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISII', @level2type = N'COLUMN', @level2name = N'INTERPRETACIONTAMIZAJECARDIOPATIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Presión arterial del Miembro inferior izquierdo del agrupador Tamizaje de cardiopatía congénita', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISII', @level2type = N'COLUMN', @level2name = N'PAMIEMBROINFERIORIZQUIERDO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Presión arterial del Miembro inferior derecho del agrupador Tamizaje de cardiopatía congénita', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISII', @level2type = N'COLUMN', @level2name = N'PAMIEMBROINFERIORDERECHO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Presión arterial del Miembro superiror izquierdo del agrupador Tamizaje de cardiopatía congénita', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISII', @level2type = N'COLUMN', @level2name = N'PAMIEMBROSUPERIORIZQUIERDO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Presión arterial del Miembro superiror derecho del agrupador Tamizaje de cardiopatía congénita', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISII', @level2type = N'COLUMN', @level2name = N'PAMIEMBROSUPERIORDERECHO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Saturación posductal del agrupador Tamizaje de cardiopatía congénita', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISII', @level2type = N'COLUMN', @level2name = N'SATURACIONPOSDUCTAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Saturación preductal del agrupador Tamizaje de cardiopatía congénita', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISII', @level2type = N'COLUMN', @level2name = N'SATURACIONPREDUCTAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo peso para edad gestacional.  1 = Pequeño para la edad gestacional   2 = Adecuado para la edad gestacional  3 = Grande para la edad gestacional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISII', @level2type = N'COLUMN', @level2name = N'PESOPARAEDADGESTACIONAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Sífilis congénita  True = Si  False = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISII', @level2type = N'COLUMN', @level2name = N'SIFILISCONGENITA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo TSH(mUI/L)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISII', @level2type = N'COLUMN', @level2name = N'TSH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo coombs  1 = Sin dato  2 = Positivo  3 = Negativo  4 = Riesgo no evaluado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISII', @level2type = N'COLUMN', @level2name = N'COOMBS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el dato Sensibilizada  1 = Sin dato  2 = Si  3 = No  4 = Riesgo no evaluado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISII', @level2type = N'COLUMN', @level2name = N'SENSIBILIZADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo RH  True = +  Flase = -', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISII', @level2type = N'COLUMN', @level2name = N'RHSANGUINEO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Grupo  1 = A  2 = B  3 = AB  4 = O', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISII', @level2type = N'COLUMN', @level2name = N'GRUPOSANGUINEO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo ¿Por qué no continua la lactancia materna?', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISII', @level2type = N'COLUMN', @level2name = N'PORQUENOCONTINUALACTANCIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Se almacena el campo ¿Continua lactancia materna?  True = Sí  False = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISII', @level2type = N'COLUMN', @level2name = N'CONTINUALACTANCIA';

