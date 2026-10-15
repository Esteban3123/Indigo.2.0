CREATE TABLE [dbo].[HCONCOFICH] (
    [ID]            INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IPCODPACI]     VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMFICHA]      CHAR (4)                                                                         NOT NULL,
    [ESTFICHA]      BIT                                                                              NOT NULL,
    [NUMANTRA]      TINYINT                                                                          NULL,
    [TIPVIVIENDA]   TINYINT                                                                          NULL,
    [REGBENIGNO]    VARCHAR (200)                                                                    NULL,
    [REGMALIGNO]    VARCHAR (200)                                                                    NULL,
    [TIPOTUMOR]     TINYINT                                                                          NULL,
    [FECHINGRES]    DATETIME                                                                         NULL,
    [FECHPRIDIAG]   DATETIME                                                                         NULL,
    [CODENTIDA]     CHAR (9)                                                                         NULL,
    [CODIGOIPS]     CHAR (100)                                                                       NULL,
    [CODHABILI]     CHAR (15)                                                                        NULL,
    [DEPMUNCOD]     CHAR (5)                                                                         NULL,
    [IDTOPOGRAF]    INT                                                                              NULL,
    [CODTOPOGRAF]   VARCHAR (5)                                                                      NULL,
    [IDMORFOLO]     INT                                                                              NULL,
    [CODMORFOL]     VARCHAR (10)                                                                     NULL,
    [HISTOLOGICO]   VARCHAR (200)                                                                    NULL,
    [DIFERENCIACI]  TINYINT                                                                          NULL,
    [CLINICO]       BIT                                                                              NULL,
    [EXPC]          BIT                                                                              NULL,
    [PESP]          BIT                                                                              NULL,
    [CITOL]         BIT                                                                              NULL,
    [HMTT]          BIT                                                                              NULL,
    [HIRIO]         BIT                                                                              NULL,
    [DES]           BIT                                                                              NULL,
    [PATOLOGIA]     VARCHAR (100)                                                                    NULL,
    [T]             TINYINT                                                                          NULL,
    [N]             TINYINT                                                                          NULL,
    [M]             TINYINT                                                                          NULL,
    [ESTCLINICO1]   VARCHAR (5)                                                                      NULL,
    [ESTCLINICO2]   VARCHAR (1)                                                                      NULL,
    [METASTASIS1]   VARCHAR (100)                                                                    NULL,
    [METASTASIS2]   VARCHAR (100)                                                                    NULL,
    [METASTASIS3]   VARCHAR (100)                                                                    NULL,
    [CARCINOMA]     BIT                                                                              NULL,
    [VIH]           BIT                                                                              NULL,
    [EMBARAZO]      BIT                                                                              NULL,
    [LATERALIDAD]   TINYINT                                                                          NULL,
    [SINTRATAMIE]   BIT                                                                              NULL,
    [FECHATRAMITE]  DATETIME                                                                         NULL,
    [CIRUGIA]       BIT                                                                              NULL,
    [RADIOTERAP]    BIT                                                                              NULL,
    [QUMIOTERAP]    BIT                                                                              NULL,
    [PUVATERAPIA]   BIT                                                                              NULL,
    [YODOTERAPIA]   BIT                                                                              NULL,
    [INMUNOTERA]    BIT                                                                              NULL,
    [HORMONOTERA]   BIT                                                                              NULL,
    [TBIOLOGICA]    BIT                                                                              NULL,
    [PALIATIVO]     BIT                                                                              NULL,
    [SINDATO]       BIT                                                                              NULL,
    [OTRO]          BIT                                                                              NULL,
    [OTRODESCRI]    VARCHAR (200)                                                                    NULL,
    [FECHINIPRITRA] DATETIME                                                                         NULL,
    [FECHFINPRITRA] DATETIME                                                                         NULL,
    [RECURRENCIA]   BIT                                                                              NULL,
    [FECHARECURR]   DATETIME                                                                         NULL,
    [UBICARECURRE]  TINYINT                                                                          NULL,
    [FECHULTCMED]   DATETIME                                                                         NULL,
    [ESTVITAL]      TINYINT                                                                          NULL,
    [FECHMUERTE]    DATETIME                                                                         NULL,
    [SITIODEFUNCI]  TINYINT                                                                          NULL,
    [NUMCERDEF]     VARCHAR (40)                                                                     NULL,
    [ESTREGISTRO]   BIT                                                                              NULL,
    [FECHDILIGEN]   DATETIME                                                                         NULL,
    [UFUCODIGO]     CHAR (10)                                                                        NOT NULL,
    [CODCENATE]     CHAR (10)                                                                        NOT NULL,
    CONSTRAINT [PK_HCONCOFICH] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCONCOFICH_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCONCOFICH_ADCONTIPS] FOREIGN KEY ([CODIGOIPS]) REFERENCES [dbo].[ADCONTIPS] ([CODIGOIPS]),
    CONSTRAINT [FK_HCONCOFICH_HCONCOMORFO] FOREIGN KEY ([IDMORFOLO]) REFERENCES [dbo].[HCONCOMORFO] ([ID]),
    CONSTRAINT [FK_HCONCOFICH_HCONCOTOPO] FOREIGN KEY ([IDTOPOGRAF]) REFERENCES [dbo].[HCONCOTOPO] ([ID]),
    CONSTRAINT [FK_HCONCOFICH_INMUNICIP] FOREIGN KEY ([DEPMUNCOD]) REFERENCES [dbo].[INMUNICIP] ([DEPMUNCOD]),
    CONSTRAINT [FK_HCONCOFICH_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCONCOFICH_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[HCONCOFICH] NOCHECK CONSTRAINT [FK_HCONCOFICH_ADCENATEN];


GO
ALTER TABLE [dbo].[HCONCOFICH] NOCHECK CONSTRAINT [FK_HCONCOFICH_ADCONTIPS];


GO
ALTER TABLE [dbo].[HCONCOFICH] NOCHECK CONSTRAINT [FK_HCONCOFICH_HCONCOMORFO];


GO
ALTER TABLE [dbo].[HCONCOFICH] NOCHECK CONSTRAINT [FK_HCONCOFICH_HCONCOTOPO];


GO
ALTER TABLE [dbo].[HCONCOFICH] NOCHECK CONSTRAINT [FK_HCONCOFICH_INMUNICIP];


GO
ALTER TABLE [dbo].[HCONCOFICH] NOCHECK CONSTRAINT [FK_HCONCOFICH_INPACIENT];


GO
ALTER TABLE [dbo].[HCONCOFICH] NOCHECK CONSTRAINT [FK_HCONCOFICH_INUNIFUNC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del registro  0->Pendiente  1->Confirmado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'ESTREGISTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Sitio de Defunción:  1-> Institucion Propia  2-> Otra Institucion  3-> Casa  4-> Otro  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'SITIODEFUNCI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado Vital del Paciente  1-> Vivo  2-> Muerto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'ESTVITAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ubicación de la recurrencia:  1->Local  2->Regional  3->a Distancia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'UBICARECURRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si el paciente esta sin tratamiento  1-> Si  0-> No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'SINTRATAMIE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Lateralidad  1-> Izquierda  2-> Derecha  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'LATERALIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Embarazo  1-> Si  0-> No  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'EMBARAZO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'VIH  1-> Si  0-> No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'VIH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Carcinomatosis  1-> Si  0-> No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'CARCINOMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado Clínico 2  Opciones:  O  A  B  C  8  9', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'ESTCLINICO2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado Clínico 1  opciones:  O  I  II  III  IV  8  9', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'ESTCLINICO1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de tumor  1-> Maligno  2-> Benigno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'TIPOTUMOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Vivienda:  1-> Propia  2-> Familiar  3-> Amigo  4-> Albergue', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'TIPVIVIENDA';

