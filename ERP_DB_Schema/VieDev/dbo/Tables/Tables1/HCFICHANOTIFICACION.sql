CREATE TABLE [dbo].[HCFICHANOTIFICACION] (
    [ID]                 INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [NUMEFOLIO]          CHAR (10)                                                                        NULL,
    [IDETIPHIS]          CHAR (9)                                                                         NULL,
    [IPCODPACI]          VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]          CHAR (10)                                                                        NOT NULL,
    [CODCENATE]          CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]          CHAR (10)                                                                        NOT NULL,
    [ESTADO]             INT                                                                              NOT NULL,
    [TIPOFICHA]          INT                                                                              NOT NULL,
    [CODUSUARIO]         CHAR (20)                                                                        NOT NULL,
    [FECHACREACION]      DATETIME                                                                         NOT NULL,
    [UPGD]               VARCHAR (50)                                                                     NULL,
    [RAZONSOCIAL]        VARCHAR (100)                                                                    NULL,
    [NOMBEVENTO]         VARCHAR (500)                                                                    NULL,
    [CODEVENTO]          VARCHAR (10)                                                                     NULL,
    [FECHANOTIFI]        DATE                                                                             NULL,
    [TIPODOCUMENTO]      INT                                                                              NULL,
    [TELEFONO]           VARCHAR (15) MASKED WITH (FUNCTION = 'partial(0, "Phone_Ofuscado", 0)')          NULL,
    [EDAD]               INT                                                                              NULL,
    [UNIDADMED]          INT                                                                              NULL,
    [SEXO]               INT                                                                              NULL,
    [CODPAIS]            VARCHAR (5)                                                                      NULL,
    [CODDEPARTAMENTO]    VARCHAR (50)                                                                     NULL,
    [AREA]               INT                                                                              NULL,
    [LOCALIDAD]          VARCHAR (150)                                                                    NULL,
    [BARRIO]             VARCHAR (150)                                                                    NULL,
    [CENTRORURAL]        VARCHAR (150)                                                                    NULL,
    [VEREDA]             VARCHAR (150)                                                                    NULL,
    [OCUPACION]          VARCHAR (5)                                                                      NULL,
    [TIPOREGIMEN]        INT                                                                              NULL,
    [ETNIA]              INT                                                                              NULL,
    [ADMINPLANBENE]      INT                                                                              NULL,
    [CODENTIDA]          VARCHAR (3)                                                                      NULL,
    [RESIDENCIA]         VARCHAR (200)                                                                    NULL,
    [DIRRESIDENCIA]      VARCHAR (150)                                                                    NULL,
    [FECHACONSULTA]      DATE                                                                             NULL,
    [FECHAINICOSINTO]    DATE                                                                             NULL,
    [CLASIFICACIONCASO]  INT                                                                              NULL,
    [HOSPITALIZADO]      BIT                                                                              NULL,
    [FECHAHOSP]          DATE                                                                             NULL,
    [CONDICIONFINAL]     INT                                                                              NULL,
    [FECHADIFUNCION]     DATE                                                                             NULL,
    [NUMCERTIFICADO]     VARCHAR (50)                                                                     NULL,
    [CAUSAMUERTE]        VARCHAR (500)                                                                    NULL,
    [TELNOTIFICA]        VARCHAR (15)                                                                     NULL,
    [NOMPROFESIONAL]     VARCHAR (200)                                                                    NULL,
    [CODDIAGNO]          CHAR (4) MASKED WITH (FUNCTION = 'partial(0, "DiagnosticCode_Ofuscado", 0)')     NULL,
    [MOTIVODESCAR]       INT                                                                              NULL,
    [OBSERVADESCARTE]    VARCHAR (MAX)                                                                    NULL,
    [USUDESCARTA]        CHAR (20)                                                                        NULL,
    [FECHADESCARTE]      DATETIME                                                                         NULL,
    [USUREPORTASIVIGILA] CHAR (20)                                                                        NULL,
    [FECHAREPORSIVIGILA] DATETIME                                                                         NULL,
    [USUARIOVALIDO]      CHAR (20)                                                                        NULL,
    [FECHAVALIDO]        DATETIME                                                                         NULL,
    [ESTRATO]            INT                                                                              NULL,
    [FUENTE]             INT                                                                              NULL,
    [NACIONALIDAD]       VARCHAR (500)                                                                    NULL,
    [SEMANASGESTACION]   NUMERIC (18, 1)                                                                  NULL,
    [VERSION]            VARCHAR (20)                                                                     NULL,
    [JSON]               VARCHAR (MAX)                                                                    NULL,
    CONSTRAINT [PK_HCFICHAGENERALES] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [CK_HCFICHANOTIFICACION_JSON] CHECK (isjson([JSON])=(1)),
    CONSTRAINT [FK_HCFICHANOTIFICACION_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCFICHANOTIFICACION_HCDESCARTEFICHA] FOREIGN KEY ([MOTIVODESCAR]) REFERENCES [dbo].[HCDESCARTEFICHA] ([ID]),
    CONSTRAINT [FK_HCFICHANOTIFICACION_HCFICHANOTIFICACION] FOREIGN KEY ([ID]) REFERENCES [dbo].[HCFICHANOTIFICACION] ([ID]),
    CONSTRAINT [FK_HCFICHANOTIFICACION_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[HCFICHANOTIFICACION] NOCHECK CONSTRAINT [CK_HCFICHANOTIFICACION_JSON];


GO
ALTER TABLE [dbo].[HCFICHANOTIFICACION] NOCHECK CONSTRAINT [FK_HCFICHANOTIFICACION_ADINGRESO];


GO
ALTER TABLE [dbo].[HCFICHANOTIFICACION] NOCHECK CONSTRAINT [FK_HCFICHANOTIFICACION_HCDESCARTEFICHA];


GO
ALTER TABLE [dbo].[HCFICHANOTIFICACION] NOCHECK CONSTRAINT [FK_HCFICHANOTIFICACION_HCFICHANOTIFICACION];


GO
ALTER TABLE [dbo].[HCFICHANOTIFICACION] NOCHECK CONSTRAINT [FK_HCFICHANOTIFICACION_INPACIENT];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda fuente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'FUENTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el estrato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'ESTRATO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Condición final:   1=Vivo   2=Muerto   3=No sabe, no responde', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'CONDICIONFINAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hospitalizado:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'HOSPITALIZADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Clasificación inicial de caso:   1=Sospechoso   2=Probable   3=Conf. por laboratorio   4=Conf. clínica   5=Conf. nexo epidemiológico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'CLASIFICACIONCASO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Pertenencia étnica:  1=Indígena   2=Rom, gitano   3=Raizal   4=Palenquero   5=Negro, mulato afro colombiano   6=Otro     Fichas distritales:  1. Indígena  2. ROM  3. Raizal  4. Caucásico  5. Palenquero  6. Mulato  7. Afrocolombiano  8. Otros', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'ETNIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de régimen en salud:   1=Exepción   2=Especial   3=Contributivo   4=Subsidiado   5=No asegurado   6=Indeterminado/pendiente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'TIPOREGIMEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Área ocurrencia del caso:   1=Cabecera municipal   2=Centro poblado   3=Rural disperso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'AREA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad de medida:   1=Años   2=Días   3=Minutos   4=Meses   5=Horas   6=No aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'UNIDADMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo Documento (para Ficha Notificación):   1= RC - Registro Civil   2= TI - Tarjeta de Identidad   3= CC - Cédula de Ciudadanía   4= CE - Cédula de Extranjería   5= PA - Pasaporte   6= MS - Menor Sin Identificación   7= AS - Adulto Sin Identificación   8= PE - Permiso Especial de Permanencia   9= CN - Certificado de Nacido Vivo   10= CD - Carnet Diplomático   11= SC - Salvoconducto   13= DE - Documento Extranjero    ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'TIPODOCUMENTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Ficha  NoAplica = 0          Ficha100 = 1          Ficha110 = 2          Ficha115 = 3          Ficha155 = 4          Ficha205 = 5          Ficha210 = 6          Ficha348 = 7          Ficha356 = 8          Ficha549 = 9          Ficha560 = 10          Ficha580 = 11          Ficha605 = 12          Ficha610 = 13          Ficha670 = 14          Ficha875 = 15          Ficha217 = 16          Ficha365 = 17          Ficha450 = 18          Ficha760 = 19          Ficha230 = 20          Ficha342 = 21          Ficha850 = 22          Ficha298 = 23          Ficha300 = 24          Ficha813 = 25          Ficha825 = 26          Ficha465 = 27          Ficha113 = 28          Ficha215 = 29          Ficha228 = 30          Ficha305 = 31          Ficha770 = 32          Ficha591 = 33          Ficha750 = 34          Ficha740 = 35          Ficha720 = 36          Ficha310 = 37          Ficha340 = 38          Ficha420 = 39          Ficha430 = 40          Ficha440 = 41          Ficha455 = 42          Ficha200 = 43          Ficha357 = 44          Ficha452 = 45          Ficha453 = 46          Ficha895 = 47          Ficha535 = 48          Ficha800 = 49          Ficha710 = 50          Ficha730 = 51          Ficha550 = 52          Ficha345 = 53          Ficha351 = 54          Ficha352 = 55          Ficha355 = 56          Ficha220 = 57          Ficha831 = 58          Ficha620 = 59          Ficha330 = 60          Ficha346 = 61', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'TIPOFICHA';

