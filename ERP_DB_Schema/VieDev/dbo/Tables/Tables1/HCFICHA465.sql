CREATE TABLE [dbo].[HCFICHA465] (
    [ID]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDFICHANOTIFICACION] INT           NOT NULL,
    [CODDIAGNO]           CHAR (4)      NULL,
    [CODPREST]            VARCHAR (50)  NULL,
    [ESPEPLASM]           INT           NULL,
    [FECHACARACT]         DATE          NULL,
    [TIPDOCU]             INT           NULL,
    [NOMAPEPAC]           VARCHAR (50)  NULL,
    [TELEFONO]            VARCHAR (50)  NULL,
    [FECHANAC]            DATE          NULL,
    [EDAD]                VARCHAR (50)  NULL,
    [UNIMEDEDAD]          INT           NULL,
    [SEXO]                INT           NULL,
    [PAISOCUR]            VARCHAR (50)  NULL,
    [DEPAMUNI]            VARCHAR (50)  NULL,
    [AREAPROCE]           INT           NULL,
    [LOCALBARRIO]         VARCHAR (50)  NULL,
    [OCUPACIEN]           VARCHAR (50)  NULL,
    [TIPREGISAL]          INT           NULL,
    [NOMADMISAL]          VARCHAR (50)  NULL,
    [PERTETNICA]          INT           NULL,
    [ESTRATO]             VARCHAR (50)  NULL,
    [DISCAPACITADO]       BIT           NULL,
    [DESPLAZADO]          BIT           NULL,
    [MIGRANTE]            BIT           NULL,
    [CARCELARIO]          BIT           NULL,
    [GESTANTE]            BIT           NULL,
    [INDIGENTE]           BIT           NULL,
    [POBLAICBF]           BIT           NULL,
    [MADCOMUNIT]          BIT           NULL,
    [DESMOVIL]            BIT           NULL,
    [POBPSIQUIA]          BIT           NULL,
    [VICTVIOLEN]          BIT           NULL,
    [OTROGRUPPOB]         BIT           NULL,
    [FUENTE]              INT           NULL,
    [DEPMUNIRESID]        VARCHAR (50)  NULL,
    [PAISRESIDE]          VARCHAR (50)  NULL,
    [DIRERESIDE]          VARCHAR (50)  NULL,
    [FECHACONSULT]        DATE          NULL,
    [FECHAINICIO]         DATE          NULL,
    [CLASIFCASO]          BIT           NULL,
    [HOSPITALIZADO]       BIT           NULL,
    [FECHAHOSPIT]         DATE          NULL,
    [CONDIFINAL]          BIT           NULL,
    [FECHADEFUN]          DATE          NULL,
    [NUMECERTIF]          VARCHAR (50)  NULL,
    [CAUSABASIC]          VARCHAR (50)  NULL,
    [VIGIACT]             BIT           NULL,
    [SINTOMATICO]         BIT           NULL,
    [CLASIORIGE]          BIT           NULL,
    [NUEVO]               BIT           NULL,
    [RECRUDESCEN]         BIT           NULL,
    [TRIMESGEST]          INT           NULL,
    [TIPOEXA]             INT           NULL,
    [RECUPARASI]          VARCHAR (50)  NULL,
    [GAMETOCI]            BIT           NULL,
    [COMPLICACIONES]      BIT           NULL,
    [CEREBRAL]            BIT           NULL,
    [RENAL]               BIT           NULL,
    [HEPATICA]            BIT           NULL,
    [PULMONAR]            BIT           NULL,
    [HEMATOLO]            BIT           NULL,
    [OTRAS]               BIT           NULL,
    [TRATAMIENTO]         INT           NULL,
    [FECHINITRAT]         DATE          NULL,
    [RESPONDIAGN]         VARCHAR (50)  NULL,
    [RESULEXAM]           BIT           NULL,
    [FECHRESU]            DATE          NULL,
    [NOMBREPACI]          VARCHAR (50)  NULL,
    [APELLPACI]           VARCHAR (50)  NULL,
    [TIPOEXAMEN]          INT           NULL,
    [RESULTEXAMEN]        BIT           NULL,
    [ESPECIE]             VARCHAR (50)  NULL,
    [RECPARASITA]         VARCHAR (50)  NULL,
    [FECHARESUL]          DATE          NULL,
    [RESPONSDIAGNOS]      VARCHAR (50)  NULL,
    [NUMIDENT]            VARCHAR (50)  NULL,
    [VERSION]             VARCHAR (20)  NULL,
    [JSON]                VARCHAR (MAX) NULL,
    CONSTRAINT [PK_HCFICHA465] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [CK_HCFICHA465_JSON] CHECK (isjson([JSON])=(1)),
    CONSTRAINT [FK_HCFICHA465_HCFICHANOTIFICACION] FOREIGN KEY ([IDFICHANOTIFICACION]) REFERENCES [dbo].[HCFICHANOTIFICACION] ([ID])
);


GO
ALTER TABLE [dbo].[HCFICHA465] NOCHECK CONSTRAINT [CK_HCFICHA465_JSON];


GO
ALTER TABLE [dbo].[HCFICHA465] NOCHECK CONSTRAINT [FK_HCFICHA465_HCFICHANOTIFICACION];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nuevas columnas en formato JSON  -->  (versión antigua) = nulo   /    (versiones nuevas, desde ''V01_2020-03-06'' ) =     NACIONALIDAD = texto (Nacionalidad) ,    DESPLAZAMIENTO15 = booleano (Desplazamiento ultimos 15 días --> true = Si, False = No ) ,  CODLUGAR_DESPLAZAMIENTO = texto (Código Lugar Desplazamiento) ,   RAZON_SOCIAL = texto (Razon Social) ,  NOMBRE_EVENTO = texto (Nombre Evento) ,   CODIGO_EVENTO = texto (Código Evento) ,   BARRIO  = texto (Barrio) ,  CENTRORURAL = texto (Centro Rural) ,  VEREDA = texto (Vereda) ,  SEMANAS_GESTACION = numero (Numero Semanas de Gestacion) ,  CLASIFICACION_CASO = numero (Clasificacion Caso --> 1= sospechoso, 2= probable, 3= laboratorio, 4= clinica, 5= epimediologo ) ,  CONDICION_FINAL = numero (Condicion Final --> 1= vivo, 2= muerto, 3= no sabe) ,  NOMBRE_PROFESIONAL = texto (Nombre profesional Notifica) ,  TELEFONO_NOTIFICA = texto (Telefono Profesional Notifica)    ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'JSON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especie  /  Especie Infectante:   1 = P. Vivax   2 = P. Falciparum   3 = P. Malariae   4 = Infección Mixta    ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'ESPECIE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Examen:   1 = GG    2 = PDR    3 = PCR    4 = OTRO  --> item eliminado desde version nueva ''V01_2020-03-06''  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'TIPOEXAMEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Examen:   1 = GG    2 = PDR    3 = PCR    4 = OTRO  --> item eliminado desde version nueva ''V01_2020-03-06''  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'TIPOEXA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Sexo:   1 = Masculino   2 = Femenino   3 = Indeterminado   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'SEXO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad de Medida de la Edad:  1 = Años   2 = Meses   3 = Días   4 = Horas   5 = Minutos   6 = No aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'UNIMEDEDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo documento:   1=RC    2=TI   3=CC    4=CE   5=PA   6=MS   7=AS   8=PE   9= CN   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'TIPDOCU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especie de Plasmodium  /   Especie Infectante:   1 = P. Vivax   2 = P. Falciparum   3 = P. Malariae   4 = Infección Mixta    ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'ESPEPLASM';

