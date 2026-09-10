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
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCEXFISII].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCEXFISII].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCEXFISII].[TEMPERPAC]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCEXFISII].[FRECARPAC]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCEXFISII].[FRERESPAC]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');



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


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Deglucion campo para historia de recien nacido True-> Si, False-> No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISII', @level2type = N'COLUMN', @level2name = N'DEGLUCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Succion campo para historia de recien nacido True-> Si, False-> No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISII', @level2type = N'COLUMN', @level2name = N'SUCCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Meconio campo para historia de recien nacido True-> Si, False-> No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISII', @level2type = N'COLUMN', @level2name = N'MECONIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Diuresis campo para historia de recien nacido True-> Si, False-> No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISII', @level2type = N'COLUMN', @level2name = N'DIURESIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISII', @level2type = N'COLUMN', @level2name = N'FECREGSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Control Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISII', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Solo Uci Adultos - PIC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISII', @level2type = N'COLUMN', @level2name = N'UCIADUPIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Solo Uci Adultos - RG', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISII', @level2type = N'COLUMN', @level2name = N'UCIADULRG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Solo Uci Adultos - Glucometria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISII', @level2type = N'COLUMN', @level2name = N'UCIADUGLU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Solo Uci Adultos - PIA', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISII', @level2type = N'COLUMN', @level2name = N'UCIADUPIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Solo Uci Adultos - Cuña', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISII', @level2type = N'COLUMN', @level2name = N'UCIADUCUN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Solo Uci Adultos - PVC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISII', @level2type = N'COLUMN', @level2name = N'UCIADUPVC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Acceso Descripcion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISII', @level2type = N'COLUMN', @level2name = N'ACCESODES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Acceso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISII', @level2type = N'COLUMN', @level2name = N'ACCESOPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Soporte Descripcion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISII', @level2type = N'COLUMN', @level2name = N'SOPINODES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Soporte Inotropico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISII', @level2type = N'COLUMN', @level2name = N'SOPINOPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Soporte Ventilatorio Descripcion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISII', @level2type = N'COLUMN', @level2name = N'SOPVENDES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Soporte Ventilatorio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISII', @level2type = N'COLUMN', @level2name = N'SOPVENPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Solo para Neonatos - Perimetro Abdominal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISII', @level2type = N'COLUMN', @level2name = N'NEOPERABD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Solo para Neonatos - Perimetro Toraxico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISII', @level2type = N'COLUMN', @level2name = N'NEOPERTOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Solo para Neonatos - Perimetro Cefalico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISII', @level2type = N'COLUMN', @level2name = N'NEOPERCEF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion Anomalia Piel', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISII', @level2type = N'COLUMN', @level2name = N'DESPEILPA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Anomalia Piel', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISII', @level2type = N'COLUMN', @level2name = N'ANOPIELPA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion Anomalia Neurologica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISII', @level2type = N'COLUMN', @level2name = N'DESNEUPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Anomalia Neurologica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISII', @level2type = N'COLUMN', @level2name = N'ANONEUPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion Anomalia Extremidades', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISII', @level2type = N'COLUMN', @level2name = N'DESEXTPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Anomalia Extremidades', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISII', @level2type = N'COLUMN', @level2name = N'ANOEXTPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion Anomalila Genitourinario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISII', @level2type = N'COLUMN', @level2name = N'DESGEUOAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Anomalila Genitourinario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISII', @level2type = N'COLUMN', @level2name = N'ANOGEUOAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion Anomalia Abdomen', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISII', @level2type = N'COLUMN', @level2name = N'DESABDPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Anomalia Abdomen', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISII', @level2type = N'COLUMN', @level2name = N'ANOABDPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion Anomalia Cardiopulmonar', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISII', @level2type = N'COLUMN', @level2name = N'DESCAPPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Anomalia Cardiopulmonar', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISII', @level2type = N'COLUMN', @level2name = N'ANOCAPPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion Anomalia Cuello', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISII', @level2type = N'COLUMN', @level2name = N'DESCUEPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Anomalia Cuello', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISII', @level2type = N'COLUMN', @level2name = N'ANOCUEPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion Anomalia ORL (Otorrinolaringologico)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISII', @level2type = N'COLUMN', @level2name = N'DESORLPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Anomalia ORL (Otorrinolaringologico)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISII', @level2type = N'COLUMN', @level2name = N'ANOORLPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'descripcion Anomalia Ojos Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISII', @level2type = N'COLUMN', @level2name = N'DESOJOPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Anomalia Ojos Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISII', @level2type = N'COLUMN', @level2name = N'ANOOJOPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'descripcion Anomalia Cabeza Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISII', @level2type = N'COLUMN', @level2name = N'DESCABPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Anomalia Cabeza Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISII', @level2type = N'COLUMN', @level2name = N'ANOCABPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISII', @level2type = N'COLUMN', @level2name = N'FISOBSPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Habito de Dieta del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISII', @level2type = N'COLUMN', @level2name = N'HABDIEPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Practica ejercicio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISII', @level2type = N'COLUMN', @level2name = N'PRAEJEPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Peso en Gramos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISII', @level2type = N'COLUMN', @level2name = N'PESOPACIE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Talla Paciente en cm', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISII', @level2type = N'COLUMN', @level2name = N'TALLAPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Saturacion de Oxigeno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISII', @level2type = N'COLUMN', @level2name = N'REGSO2PAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Frecuencia Respiratoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISII', @level2type = N'COLUMN', @level2name = N'FRERESPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Frecuencia Cardiaca', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISII', @level2type = N'COLUMN', @level2name = N'FRECARPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Temperatura', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISII', @level2type = N'COLUMN', @level2name = N'TEMPERPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tension Arterial Diastolica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISII', @level2type = N'COLUMN', @level2name = N'TENARTDIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tension Sistolica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISII', @level2type = N'COLUMN', @level2name = N'TENARTSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISII', @level2type = N'COLUMN', @level2name = N'FECREGITE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISII', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISII', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISII', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISII', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISII', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISII', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre Interno Tipo Historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISII', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISII', @level2type = N'COLUMN', @level2name = N'NUMCONSEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registros del examen físico integral del paciente durante una atención clínica. Incluye signos vitales, hallazgos por sistemas corporales (cabeza, ojos, oídos, cuello, abdomen, genitourinario, extremidades, neurológico, piel), parámetros de UCI, y tamizajes neonatales (cardiopatía, ocular, auditivo, grupo sanguíneo, lactancia), asociados al ingreso y al profesional de salud tratante.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISII';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISII';
