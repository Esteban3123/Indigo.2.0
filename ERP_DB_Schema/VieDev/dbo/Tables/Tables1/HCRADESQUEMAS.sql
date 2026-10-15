CREATE TABLE [dbo].[HCRADESQUEMAS] (
    [ID]                    INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCRADORDEN]          INT       NOT NULL,
    [DOSTOTAL]              INT       NOT NULL,
    [DOSISPORSESION]        INT       NOT NULL,
    [NUMSESION]             INT       NOT NULL,
    [PORDOSISAUTO]          INT       NOT NULL,
    [UBICACION]             TINYINT   NULL,
    [TIPORADIO]             TINYINT   NULL,
    [TIPOTRATA]             TINYINT   NULL,
    [ESTADO]                TINYINT   NOT NULL,
    [NUMINGRES]             CHAR (10) NOT NULL,
    [FECHACONFIR]           DATETIME  NULL,
    [USUCONFIR]             CHAR (20) NULL,
    [FECHAFINALIZA]         DATETIME  NULL,
    [USUFINALIZA]           CHAR (20) NULL,
    [FECHACREA]             DATETIME  NOT NULL,
    [USUCREA]               CHAR (20) NOT NULL,
    [FECHAMOD]              DATETIME  NULL,
    [USUMOD]                CHAR (20) NULL,
    [TECNICARADIOTEAPIA146] INT       NULL,
    [CODCENATEPLANEACION]   CHAR (10) NULL,
    CONSTRAINT [PK_HCRADESQUEMAS] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCRADESQUEMAS_ADCENATEN] FOREIGN KEY ([CODCENATEPLANEACION]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCRADESQUEMAS_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCRADESQUEMAS_HCRADORDEN] FOREIGN KEY ([IDHCRADORDEN]) REFERENCES [dbo].[HCRADORDEN] ([ID])
);


GO
ALTER TABLE [dbo].[HCRADESQUEMAS] NOCHECK CONSTRAINT [FK_HCRADESQUEMAS_ADCENATEN];


GO
ALTER TABLE [dbo].[HCRADESQUEMAS] NOCHECK CONSTRAINT [FK_HCRADESQUEMAS_ADINGRESO];


GO
ALTER TABLE [dbo].[HCRADESQUEMAS] NOCHECK CONSTRAINT [FK_HCRADESQUEMAS_HCRADORDEN];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del centro de Atención el cual se registra cuando se crea la creación del Esquema.  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADESQUEMAS', @level2type = N'COLUMN', @level2name = N'CODCENATEPLANEACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '0=No recibió radioterapia  1=Convencional 2D  2=IMRT (radioterapia con intensidad moderada)  3=VMAT (modulada voluntariamente)  4=IGRT (radioterapia guiada por imagen)  5=Conformacional 3D  6=Radioterapia estereotaxica  7=Radiocirugia  8=Otra radioterapia interna o radiofármaco (ejemplo braquiterapia, yodoterapia)  9=Radioterapia externa sin mención de la técnica en los soportes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADESQUEMAS', @level2type = N'COLUMN', @level2name = N'TECNICARADIOTEAPIA146';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 - Sin confirmar  2 - Confirmada  3 - Iniciado  4 - Finalizado  5 - Anulado  6 - Completado ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADESQUEMAS', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de tratamiento que está recibinedo el paciente a la fecha (125):    1= Radioterapia    2= Terapia sistémica (incluye quimioterapia, anticuerpos monoclonales, terapia biológica, terapia hormonal)    3= Cirugía    4= 1 y 2    5= 1 y 3    6= 2 y 3    7= Manejo expectante pretratamiento    8= En seguimiento, luego de tratamiento durante el periodo  9=Antecedente de cáncer (no recibió ningún tratamiento, pero tiene como mínimo una consulta de seguimiento relacionada con el cáncer dentro del periodo)    10=1, 2 y 3    11=Manejo de cuidado paliativo o terapia complementaria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADESQUEMAS', @level2type = N'COLUMN', @level2name = N'TIPOTRATA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de radioterapia (90,99):    1=Externa para tratar    2=Interna para tratar (incluye, braquiterapia, yodo radioactivo)  3=Profiláctica (por ejemplo a sistema nervioso central)    4=1 y 2    5=1 y 3    6=2 y 3    7=1,2 y 3', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADESQUEMAS', @level2type = N'COLUMN', @level2name = N'TIPORADIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ubicación temporal de esquema (89,98) :    1=Neoadyuvancia (manejo  inicial prequirúrgico)    2=Tratamiento inicial curativo sin cirugía sugerida    3=Adyuvancia (manejo inicial postquirúrgico)    4=Manejo paliativo inicial    5=Manejo curativo de primera recaída    6=Manejo paliativo de primera recaída    7=Manejo curativo de segunda recaída    8=Manejo paliativo de segunda recaída    9=Manejo curativo de tercera recaída o posterior    10=Manejo paliativo de tercera recaída o posterior', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADESQUEMAS', @level2type = N'COLUMN', @level2name = N'UBICACION';

