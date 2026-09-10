CREATE TABLE [dbo].[HCEXFISIC] (
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
    [TALLAPACI]                               NUMERIC (4, 1)                                                                   NULL,
    [PESOPACIE]                               NUMERIC (18, 3)                                                                  NULL,
    [PRAEJEPAC]                               INT                                                                              NULL,
    [HABDIEPAC]                               INT                                                                              NULL,
    [FISOBSPAC]                               VARCHAR (4000)                                                                   NULL,
    [ANOCABPAC]                               TINYINT                                                                          NULL,
    [DESCABPAC]                               VARCHAR (2000)                                                                   NULL,
    [ANOOJOPAC]                               TINYINT                                                                          NULL,
    [DESOJOPAC]                               VARCHAR (2000)                                                                   NULL,
    [ANOORLPAC]                               TINYINT                                                                          NULL,
    [DESORLPAC]                               VARCHAR (2000)                                                                   NULL,
    [ANOCUEPAC]                               TINYINT                                                                          NULL,
    [DESCUEPAC]                               VARCHAR (2000)                                                                   NULL,
    [ANOCAPPAC]                               TINYINT                                                                          NULL,
    [DESCAPPAC]                               VARCHAR (2000)                                                                   NULL,
    [ANOABDPAC]                               TINYINT                                                                          NULL,
    [DESABDPAC]                               VARCHAR (2000)                                                                   NULL,
    [ANOGEUOAC]                               TINYINT                                                                          NULL,
    [DESGEUOAC]                               VARCHAR (2000)                                                                   NULL,
    [ANOEXTPAC]                               TINYINT                                                                          NULL,
    [DESEXTPAC]                               VARCHAR (2000)                                                                   NULL,
    [ANONEUPAC]                               TINYINT                                                                          NULL,
    [DESNEUPAC]                               VARCHAR (2000)                                                                   NULL,
    [ANOPIELPA]                               TINYINT                                                                          NULL,
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
    [FECREGSIS]                               DATETIME                                                                         CONSTRAINT [DF_HCEXFISIC_FECREGSIS] DEFAULT ([Common].[getdate]()) NOT NULL,
    [PESOSEC]                                 NUMERIC (18, 3)                                                                  NULL,
    [TFG]                                     VARCHAR (50)                                                                     NULL,
    [ESTADIO]                                 VARCHAR (50)                                                                     NULL,
    [ACCVASCULAR]                             VARCHAR (50)                                                                     NULL,
    [ULTRAFILTRA]                             BIT                                                                              NULL,
    [ULTRAFILTRAV]                            NUMERIC (18, 1)                                                                  NULL,
    [TIEMPOSESIO]                             INT                                                                              NULL,
    [KTV]                                     VARCHAR (50)                                                                     NULL,
    [DIURESIS]                                BIT                                                                              NULL,
    [MECONIO]                                 BIT                                                                              NULL,
    [SUCCION]                                 BIT                                                                              NULL,
    [DEGLUCION]                               BIT                                                                              NULL,
    [IDHCPAREXFISC]                           INT                                                                              NULL,
    [VALORTAM]                                VARCHAR (50)                                                                     NULL,
    [SIDROMETABO]                             TINYINT                                                                          NULL,
    [PB]                                      NUMERIC (4, 1)                                                                   NULL,
    [DOLOR]                                   INT                                                                              NULL,
    [INTERPESOPARATALLA]                      VARCHAR (50) MASKED WITH (FUNCTION = 'partial(0, "Interpretation_Ofuscado", 0)') NULL,
    [INTERINDICEMASACO]                       VARCHAR (50)                                                                     NULL,
    [INTERPESOPARAEDAD]                       VARCHAR (50) MASKED WITH (FUNCTION = 'partial(0, "Interpretation_Ofuscado", 0)') NULL,
    [INTERPERIMETROCEFA]                      VARCHAR (50)                                                                     NULL,
    [INTERTALLAPARAEDAD]                      VARCHAR (50) MASKED WITH (FUNCTION = 'partial(0, "Interpretation_Ofuscado", 0)') NULL,
    [INTERALTURAUTERINA]                      VARCHAR (50)                                                                     NULL,
    [INTERIMCPARALAEDAD]                      VARCHAR (50) MASKED WITH (FUNCTION = 'partial(0, "Interpretation_Ofuscado", 0)') NULL,
    [ANOOSTEO]                                TINYINT                                                                          NULL,
    [DESOSTEO]                                VARCHAR (2000)                                                                   NULL,
    [ANOVASCU]                                TINYINT                                                                          NULL,
    [DESVASCU]                                VARCHAR (2000)                                                                   NULL,
    [AR]                                      NUMERIC (4, 1)                                                                   NULL,
    [FETOCARDIA]                              VARCHAR (300)                                                                    NULL,
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
    CONSTRAINT [PK_HCEXFISIC_1] PRIMARY KEY CLUSTERED ([NUMCONSEC] ASC)
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCEXFISIC].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCEXFISIC].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCEXFISIC].[TEMPERPAC]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCEXFISIC].[FRECARPAC]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCEXFISIC].[FRERESPAC]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCEXFISIC].[INTERPESOPARATALLA]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCEXFISIC].[INTERPESOPARAEDAD]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCEXFISIC].[INTERTALLAPARAEDAD]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCEXFISIC].[INTERIMCPARALAEDAD]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');




GO



GO



GO



GO
CREATE NONCLUSTERED INDEX [nci_wi_HCEXFISIC_A7CCE9B9FD29BFC4FD0C2895666BF41C]
    ON [dbo].[HCEXFISIC]([IPCODPACI] ASC, [NUMINGRES] ASC, [NUMEFOLIO] ASC)
    INCLUDE([ACCESODES], [ACCESOPAC], [ACCVASCULAR], [ANOABDPAC], [ANOCABPAC], [ANOCAPPAC], [ANOCUEPAC], [ANOEXTPAC], [ANOGEUOAC], [ANONEUPAC], [ANOOJOPAC], [ANOORLPAC], [ANOOSTEO], [ANOPIELPA], [ANOVASCU], [DESABDPAC], [DESCABPAC], [DESCAPPAC], [DESCUEPAC], [DESEXTPAC], [DESGEUOAC], [DESNEUPAC], [DESOJOPAC], [DESORLPAC], [DESOSTEO], [DESPEILPA], [DESVASCU], [DOLOR], [ESTADIO], [FECREGITE], [FISOBSPAC], [FRECARPAC], [FRERESPAC], [IDHCPAREXFISC], [INTERINDICEMASACO], [INTERPERIMETROCEFA], [INTERPESOPARAEDAD], [INTERPESOPARATALLA], [INTERTALLAPARAEDAD], [KTV], [NEOPERABD], [NEOPERCEF], [NEOPERTOR], [PB], [PESOPACIE], [PESOSEC], [REGSO2PAC], [SOPINODES], [SOPINOPAC], [SOPVENDES], [SOPVENPAC], [TALLAPACI], [TEMPERPAC], [TENARTDIA], [TENARTSIS], [TFG], [TIEMPOSESIO], [UCIADUCUN], [UCIADUGLU], [UCIADULRG], [UCIADUPIA], [UCIADUPIC], [UCIADUPVC], [ULTRAFILTRA], [ULTRAFILTRAV]);


GO
CREATE NONCLUSTERED INDEX [IX_HCEXFISIC__NUMINGRES]
    ON [dbo].[HCEXFISIC]([NUMINGRES] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_HCEXFISIC_NUMINGRES]
    ON [dbo].[HCEXFISIC]([NUMEFOLIO] ASC, [IPCODPACI] ASC, [NUMINGRES] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_HCEXFISIC]
    ON [dbo].[HCEXFISIC]([IPCODPACI] ASC, [NUMINGRES] ASC, [NUMEFOLIO] ASC, [CODCENATE] ASC, [UFUCODIGO] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_HCEXFISIC__IPCODPACI__FECREGITE__NUMCONSEC__INC__FRECARPAC__FRERESPAC__PESOPACIE__REGSO2PAC__TALLAPACI__TEMPERPAC__TENARTDIA_]
    ON [dbo].[HCEXFISIC]([IPCODPACI] ASC, [FECREGITE] ASC, [NUMCONSEC] ASC)
    INCLUDE([FRECARPAC], [FRERESPAC], [PESOPACIE], [REGSO2PAC], [TALLAPACI], [TEMPERPAC], [TENARTDIA], [TENARTSIS], [UCIADUCUN], [UCIADUGLU], [UCIADULRG], [UCIADUPIA], [UCIADUPIC], [UCIADUPVC]);


GO
CREATE NONCLUSTERED INDEX [IX_HCEXFISIC_NUMINGRES_TALLAPACI]
    ON [dbo].[HCEXFISIC]([NUMINGRES] ASC)
    INCLUDE([TALLAPACI]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacena el campo Seleccion Interpretacion Tamizaje Audtivo del agrupador Tamizaje Auditivo 1 - Normal  2 - Anormal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'SELECCIONINTERPRETACIONTAMIZAJEAUDITIVO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Realiza Tamizaje Auditivo campo para historia de recien nacido True-> Si, False-> No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'REALIZATAMIZAJEAUDITIVO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Interpretación del tamizaje auditivo del agrupador Tamizaje auditivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'INTERPRETACIONTAMIZAJEAUDITIVO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Observaciones del agrupador Tamizaje ocular', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'OBSERVACIONESOCULAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Interpretación del tamizaje ocular del agrupador Tamizaje ocular  1 - Normal  2 - Anormal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'INTERPRETACIONTAMIZAJEOCULAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Rojo retiniano ojo izquierdo del agrupador Tamizaje ocular  1 - Presente  2 - Ausente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'ROJORETINIANOIZQUIERDO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Rojo retiniano ojo derecho del agrupador Tamizaje ocular  1 - Presente  2 - Ausente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'ROJORETINIANODERECHO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Inspección ocular bilateral del agrupador Tamizaje ocular  1 - Normal  2 - Anormal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'INSPECCIONOCULARBILATERAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena las observaciones del agrupador Tamizaje de cardiopatía congénita', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'OBSERVACIONESCARDIOPATIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Interpretación del tamizaje para cardiopatía congénita del agrupador Tamizaje de cardiopatía congénita  1 - Normal  2 - Anormal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'INTERPRETACIONTAMIZAJECARDIOPATIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Presión arterial del Miembro inferior izquierdo del agrupador Tamizaje de cardiopatía congénita', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'PAMIEMBROINFERIORIZQUIERDO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Presión arterial del Miembro inferior derecho del agrupador Tamizaje de cardiopatía congénita', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'PAMIEMBROINFERIORDERECHO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Presión arterial del Miembro superiror izquierdo del agrupador Tamizaje de cardiopatía congénita', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'PAMIEMBROSUPERIORIZQUIERDO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Presión arterial del Miembro superiror derecho del agrupador Tamizaje de cardiopatía congénita', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'PAMIEMBROSUPERIORDERECHO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Saturación posductal del agrupador Tamizaje de cardiopatía congénita', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'SATURACIONPOSDUCTAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Saturación preductal del agrupador Tamizaje de cardiopatía congénita', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'SATURACIONPREDUCTAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo peso para edad gestacional.  1 = Pequeño para la edad gestacional   2 = Adecuado para la edad gestacional  3 = Grande para la edad gestacional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'PESOPARAEDADGESTACIONAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Sífilis congénita  True = Si  False = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'SIFILISCONGENITA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo TSH(mUI/L)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'TSH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo coombs  1 = Sin dato  2 = Positivo  3 = Negativo  4 = Riesgo no evaluado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'COOMBS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacena el dato Sensibilizada  1 = Sin dato  2 = Si  3 = No  4 = Riesgo no evaluado | Obsoleto desde 09/09/20205 por PBI-27426', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'SENSIBILIZADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo RH  True = +  Flase = -', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'RHSANGUINEO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo Grupo  1 = A  2 = B  3 = AB  4 = O', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'GRUPOSANGUINEO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el campo ¿Por qué no continua la lactancia materna?', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'PORQUENOCONTINUALACTANCIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Se almacena el campo ¿Continua lactancia materna?  True = Sí  False = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'CONTINUALACTANCIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacena un Json con las fetocardias del bebe en el viente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'FETOCARDIA';




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda la  altura rodilla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'AR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la DESCRIPCION ANOMALIA VASCULAR', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'DESVASCU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la  anomalia vascular', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'ANOVASCU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la descripción Anomalia Osteoporosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'DESOSTEO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la  anomalia osteoporosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'ANOOSTEO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'interpretacion de la grafica IMC para la edad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'INTERIMCPARALAEDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'interpretacion de la grafica altura uterina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'INTERALTURAUTERINA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'interpretacion de la grafica talla para la edad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'INTERTALLAPARAEDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'interpretacion de la grafica perimetro cefalico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'INTERPERIMETROCEFA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'interpretacion de la grafica peso para la edad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'INTERPESOPARAEDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'interpretacion de la grafica indice masa corporal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'INTERINDICEMASACO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Interpretacion de la grafica peso para la talla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'INTERPESOPARATALLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'DOLOR   (0-1-2-3-4-5-6-7-8-9-10)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'DOLOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'perímetro braquial', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'PB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'sindrome metabolico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'SIDROMETABO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor del Tensiòn Arterial Media', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'VALORTAM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID relacionado de tabala HCPAREXFISC   (Tabla cabecera de parametros de examen fisico)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'IDHCPAREXFISC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Deglución campo para historia de recien nacido: 1->Normal 2->Anormal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'DEGLUCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Succión campo para historia de recien nacido: 1->Normal 2-> Anormal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'SUCCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Meconio campo para historia de recien nacido: 1->Si, 0->No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'MECONIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Diuresis campo para historia de recien nacido True-> Si, False-> No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'DIURESIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'KTV  Habilitar si el tipo de Tratamiento es Hemodiálisis o Peritoneal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'KTV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tiempo Sesión.  habilitar solo si el tipo de tratamiento es hemodiálisis (1 hasta 6). Obligatorio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'TIEMPOSESIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'UltraFiltracion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'ULTRAFILTRAV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'UltraFiltracion. habilitar solo si el tipo de tratamiento es hemodiálisis.  1 - SI ; 0 - NO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'ULTRAFILTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'UNIDAD RENAL  Si el tipo de Tratamiento es Hemodiálisis se habilitan las siguiente opciones  1= Fístula  2= Catéter  De lo contrario se inhabilita el control mostrando la siguiente opción  98= No aplica, no recibe hemodiálisis.   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'ACCVASCULAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'UNIDAD RENAL    1 - TFG igual o mayor a 90 ml/min  2 - TFG entre 60 y menor de 90 ml/min  3 - TFG entre 30 y menor de 60 ml/min  4 - TFG entre 15 y menor de 30 ml/min  5 - TFG menos de 15 ml/min  99 - Desconocido  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'ESTADIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tasa de filtración glomerular. UNIDAD RENAL.  i. Campo calculado con funcionalidad de calculadora. Permite decimal. Debe permitir hasta 5 caracteres desde 1.0 hasta 250.0. Lo deben hacer con la calculadora    988  -No Aplica. Tiene ERC Estadio 5 y ya no se mide la Creatinina.  999 - No hay reporte de Creatinina  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'TFG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Peso Seco del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'PESOSEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'FECREGSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Control Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Solo Uci Adultos - PIC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'UCIADUPIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Solo Uci Adultos - RG', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'UCIADULRG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Solo Uci Adultos - Glucometria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'UCIADUGLU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Solo Uci Adultos - PIA', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'UCIADUPIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Solo Uci Adultos - Cuña', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'UCIADUCUN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Solo Uci Adultos - PVC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'UCIADUPVC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Acceso Descripcion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'ACCESODES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Acceso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'ACCESOPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Soporte Descripcion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'SOPINODES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Soporte Inotropico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'SOPINOPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Soporte Ventilatorio Descripcion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'SOPVENDES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Soporte Ventilatorio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'SOPVENPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Solo para Neonatos - Perimetro Abdominal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'NEOPERABD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Solo para Neonatos - Perimetro Toraxico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'NEOPERTOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Solo para Neonatos - Perimetro Cefalico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'NEOPERCEF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion Anomalia Piel', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'DESPEILPA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Anomalia Piel', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'ANOPIELPA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion Anomalia Neurologica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'DESNEUPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Anomalia Neurologica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'ANONEUPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion Anomalia Extremidades', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'DESEXTPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Anomalia Extremidades', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'ANOEXTPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion Anomalila Genitourinario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'DESGEUOAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Anomalila Genitourinario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'ANOGEUOAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion Anomalia Abdomen', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'DESABDPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Anomalia Abdomen', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'ANOABDPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion Anomalia Cardiopulmonar', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'DESCAPPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Anomalia Cardiopulmonar', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'ANOCAPPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion Anomalia Cuello', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'DESCUEPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Anomalia Cuello', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'ANOCUEPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion Anomalia ORL (Otorrinolaringologico)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'DESORLPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Anomalia ORL (Otorrinolaringologico)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'ANOORLPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'descripcion Anomalia Ojos Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'DESOJOPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Anomalia Ojos Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'ANOOJOPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'descripcion Anomalia Cabeza Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'DESCABPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Anomalia Cabeza Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'ANOCABPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'FISOBSPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Habito de Dieta del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'HABDIEPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Practica ejercicio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'PRAEJEPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Peso en Gramos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'PESOPACIE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Talla Paciente en cm', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'TALLAPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Saturacion de Oxigeno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'REGSO2PAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Frecuencia Respiratoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'FRERESPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Frecuencia Cardiaca', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'FRECARPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Temperatura', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'TEMPERPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tension Arterial Diastolica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'TENARTDIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tension Sistolica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'TENARTSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'FECREGITE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre Interno Tipo Historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISIC', @level2type = N'COLUMN', @level2name = N'NUMCONSEC';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla que registra el examen físico del paciente en la historia clínica, almacenando signos vitales (presión arterial sistólica/diastólica, temperatura, frecuencia cardíaca y respiratoria, saturación de O₂), antropometría (talla, peso, perímetro braquial) y hallazgos por segmento corporal (cabeza, ojos, oídos, abdomen, genitourinario, extremidades, neurológico, piel, entre otros). Incluye datos especializados para UCI (PVC, glucemia, diuresis), hemodiálisis (TFG, KTV, ultrafiltración), obstetricia (altura uterina, fetocardia, grupo sanguíneo) y neonatología (tamizaje auditivo, ocular y de cardiopatía congénita). Los campos de identificación del paciente y profesional están enmascarados con Dynamic Data Masking.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'HCEXFISIC';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'HCEXFISIC';
GO
