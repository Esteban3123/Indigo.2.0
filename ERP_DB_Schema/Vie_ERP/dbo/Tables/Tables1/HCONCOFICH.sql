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
CREATE UNIQUE NONCLUSTERED INDEX [IX_HCONCOFICH_1]
    ON [dbo].[HCONCOFICH]([NUMFICHA] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_HCONCOFICH]
    ON [dbo].[HCONCOFICH]([IPCODPACI] ASC, [NUMFICHA] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Relacion con centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Relacioncon Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de diligenciamiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'FECHDILIGEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del registro  0->Pendiente  1->Confirmado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'ESTREGISTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número de certificado de defunción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'NUMCERDEF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Sitio de Defunción:  1-> Institucion Propia  2-> Otra Institucion  3-> Casa  4-> Otro  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'SITIODEFUNCI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Muerte Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'FECHMUERTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado Vital del Paciente  1-> Vivo  2-> Muerto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'ESTVITAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Última Cita Médica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'FECHULTCMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ubicación de la recurrencia:  1->Local  2->Regional  3->a Distancia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'UBICARECURRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de la recurrencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'FECHARECURR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si hay recurrencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'RECURRENCIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha fin Tratamiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'FECHFINPRITRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Inicio del primer tratamiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'FECHINIPRITRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Se llena el campo cuando se seleccionó otro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'OTRODESCRI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si es Otro, 1-> si, 0-> No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'OTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si no hay dato, 1-> si, 0-> No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'SINDATO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si es Paliativo, 1-> si, 0-> No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'PALIATIVO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si es T Biologica, 1-> si, 0-> No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'TBIOLOGICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si tiene Hormonoterapias , 1-> si, 0-> No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'HORMONOTERA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si es Inmunoterapia, 1-> si, 0-> No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'INMUNOTERA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si es Yodoterapia, 1-> si, 0-> No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'YODOTERAPIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si es Puvaterapia, 1-> si, 0-> No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'PUVATERAPIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si es QuimioTerapia, 1-> si, 0-> No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'QUMIOTERAP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si es Radioterapia, 1-> si, 0-> No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'RADIOTERAP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si es Cirugía, 1-> si, 0-> No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'CIRUGIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la fecha del trámite', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'FECHATRAMITE';


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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo 3 de metastasis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'METASTASIS3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo 2 de metastasis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'METASTASIS2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo 1 de metastasis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'METASTASIS1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado Clínico 2  Opciones:  O  A  B  C  8  9', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'ESTCLINICO2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado Clínico 1  opciones:  O  I  II  III  IV  8  9', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'ESTCLINICO1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'hace parte del campo TNM = N es un numero de 0 a 3', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'M';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'hace parte del campo TNM = N es un numero de 0 a 4', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'N';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'hace parte del campo TNM = T es un numero de 1 a 10', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'T';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Registro de Patologia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'PATOLOGIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Base Válida DX Opcion DESC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'DES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Base Válida DX Opcion H. Irio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'HIRIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Base Válida DX Opcion H.Mtt', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'HMTT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Base Válida DX Opcion Citol', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'CITOL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Base Válida DX Opcion P. Esp', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'PESP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Base Válida DX Opcion Exp C', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'EXPC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Base Válida DX Opcion Clínico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'CLINICO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Diferenciación, Número de 1 dígito', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'DIFERENCIACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Histológico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'HISTOLOGICO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de Morfologico Padre a la cual pertenece', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'CODMORFOL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Relacion con la tabla de morfologicos (HCONCOMORFO)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'IDMORFOLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del registro Topografico Padre', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'CODTOPOGRAF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID relacion con la tabla de Topograficos (HCONCOTOPO)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'IDTOPOGRAF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de departamento y municipio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'DEPMUNCOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de Habilitación de IPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'CODHABILI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de la IPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'CODIGOIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de la EPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'CODENTIDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha del Primer Diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'FECHPRIDIAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'FECHINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de tumor  1-> Maligno  2-> Benigno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'TIPOTUMOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Registro Maligno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'REGMALIGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Registro Benigno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'REGBENIGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Vivienda:  1-> Propia  2-> Familiar  3-> Amigo  4-> Albergue', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'TIPVIVIENDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Años Trabajados', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'NUMANTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado de la ficha Oncológica  1-> ACTIVO   0 ->INACTIVO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'ESTFICHA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número de ficha Oncológica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'NUMFICHA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificación Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la Tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'ID';

