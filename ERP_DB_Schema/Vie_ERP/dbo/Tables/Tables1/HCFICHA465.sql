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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Versión de la ficha de notificación ---> Si es valor nulo = primer versión ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'VERSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número de Identificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'NUMIDENT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Responsable del Diagnóstico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'RESPONSDIAGNOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Resultado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'FECHARESUL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Recuento Parasitario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'RECPARASITA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especie  /  Especie Infectante:   1 = P. Vivax   2 = P. Falciparum   3 = P. Malariae   4 = Infección Mixta    ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'ESPECIE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Resultados de Examen    --> campo obsoleto desde version nueva ''V01_2020-03-06''', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'RESULTEXAMEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Examen:   1 = GG    2 = PDR    3 = PCR    4 = OTRO  --> item eliminado desde version nueva ''V01_2020-03-06''  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'TIPOEXAMEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Apellidos del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'APELLPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombres del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'NOMBREPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha del Resultado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'FECHRESU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Resultados de Examen  --> campo obsoleto desde version nueva ''V01_2020-03-06''', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'RESULEXAM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Responsable del Diagnóstico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'RESPONDIAGN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Inicio de Tratamiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'FECHINITRAT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tratamiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'TRATAMIENTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Otras', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'OTRAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hematológica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'HEMATOLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Pulmonar', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'PULMONAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hepática', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'HEPATICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Renal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'RENAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cerebral', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'CEREBRAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Complicaciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'COMPLICACIONES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Gametocitos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'GAMETOCI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = ' Recuento Parasitario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'RECUPARASI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Examen:   1 = GG    2 = PDR    3 = PCR    4 = OTRO  --> item eliminado desde version nueva ''V01_2020-03-06''  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'TIPOEXA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Trimestre de Gestación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'TRIMESGEST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Recrudescencia  /  Recurrencia  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'RECRUDESCEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nuevo    --> campo obsoleto desde version nueva ''V01_2020-03-06''', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'NUEVO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Clasificación Según Origen', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'CLASIORIGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Sintomático', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'SINTOMATICO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Vigilancia Activa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'VIGIACT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Causa Básica de Muerte', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'CAUSABASIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = ' Número de Certificado de Defunción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'NUMECERTIF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Defunción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'FECHADEFUN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Condición Final     --> campo obsoleto desde version nueva ''V01_2020-03-06''  , Se utiliza ahora una columna en campo JSON', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'CONDIFINAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Hospitalización', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'FECHAHOSPIT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hospitalizado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'HOSPITALIZADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Clasificación Inicial del Caso   --> campo obsoleto desde version nueva ''V01_2020-03-06''  , Se utiliza ahora una columna en campo JSON', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'CLASIFCASO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Inicio de Sintomas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'FECHAINICIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Consulta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'FECHACONSULT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dirección de Residencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'DIRERESIDE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'País de Residencia   --> campo obsoleto desde version nueva ''V01_2020-03-06''', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'PAISRESIDE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Departamento y Municipios de Residencia del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'DEPMUNIRESID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fuente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'FUENTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Otros Grupos Poblacionales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'OTROGRUPPOB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Víctimas de Violencia Armada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'VICTVIOLEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Centros Psiquiátricos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'POBPSIQUIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Desmovilizado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'DESMOVIL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Madres Comunitarias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'MADCOMUNIT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Población Infantil a Cargo del ICBF', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'POBLAICBF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indigente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'INDIGENTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Gestante', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'GESTANTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Carcelario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'CARCELARIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Migrante', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'MIGRANTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Desplazado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'DESPLAZADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Discapacitado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'DISCAPACITADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estrato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'ESTRATO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Pertenencia Étnica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'PERTETNICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre de la Administración de Salud y Código  /  Administradora planes de beneficios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'NOMADMISAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Régimen en Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'TIPREGISAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ocupación del Paciente y Código', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'OCUPACIEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Localidad/Barrio/Vereda de Ocurrencia del Caso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'LOCALBARRIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Area de Procedencia / Ocurrencia del Caso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'AREAPROCE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Departamento y Municipio de Procedencia / Ocurrencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'DEPAMUNI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'País de Ocurrencia del Caso y Código', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'PAISOCUR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Sexo:   1 = Masculino   2 = Femenino   3 = Indeterminado   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'SEXO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad de Medida de la Edad:  1 = Años   2 = Meses   3 = Días   4 = Horas   5 = Minutos   6 = No aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'UNIMEDEDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Edad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'EDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Nacimiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'FECHANAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Teléfono', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'TELEFONO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombres y Apellidos del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'NOMAPEPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo documento:   1=RC    2=TI   3=CC    4=CE   5=PA   6=MS   7=AS   8=PE   9= CN   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'TIPDOCU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Caracterización  / Fecha de la notificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'FECHACARACT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especie de Plasmodium  /   Especie Infectante:   1 = P. Vivax   2 = P. Falciparum   3 = P. Malariae   4 = Infección Mixta    ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'ESPEPLASM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de Prestador de Servicios de Salud   /  Código de la UPGD', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'CODPREST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de Diagnóstico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de la tabla cabecera Ficha Notificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumérico de la Tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'ID';

