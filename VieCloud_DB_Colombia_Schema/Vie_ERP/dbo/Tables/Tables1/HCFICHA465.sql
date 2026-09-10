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
ALTER TABLE [dbo].[HCFICHA465] NOCHECK CONSTRAINT [CK_HCFICHA465_JSON];


GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Payload JSON con campos adicionales por versión (nulo=v0, desde V01_2020-03-06): nacionalidad, desplazamiento últimos 15 días, código/lugar desplazamiento, razón social, nombre/código evento, barrio, centro rural, vereda, semanas gestación, clasificación caso (1=sospechoso, 2=probable, 3=laboratorio, 4=clínica, 5=epidemiólogo), condición final (1=vivo, 2=muerto, 3=no sabe), profesional notificador, teléfono notificador. Tipo: VARCHAR(MAX), validado JSON.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'JSON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nuevas columnas en formato JSON  -->  (versión antigua) = nulo   /    (versiones nuevas, desde ''''V01_2020-03-06'''' ) =     NACIONALIDAD = texto (Nacionalidad) ,    DESPLAZAMIENTO15 = booleano (Desplazamiento ultimos 15 días --> true = Si, False = No ) ,  CODLUGAR_DESPLAZAMIENTO = texto (Código Lugar Desplazamiento) ,   RAZON_SOCIAL = texto (Razon Social) ,  NOMBRE_EVENTO = texto (Nombre Evento) ,   CODIGO_EVENTO = texto (Código Evento) ,   BARRIO  = texto (Barrio) ,  CENTRORURAL = texto (Centro Rural) ,  VEREDA = texto (Vereda) ,  SEMANAS_GESTACION = numero (Numero Semanas de Gestacion) ,  CLASIFICACION_CASO = numero (Clasificacion Caso --> 1= sospechoso, 2= probable, 3= laboratorio, 4= clinica, 5= epimediologo ) ,  CONDICION_FINAL = numero (Condicion Final --> 1= vivo, 2= muerto, 3= no sabe) ,  NOMBRE_PROFESIONAL = texto (Nombre profesional Notifica) ,  TELEFONO_NOTIFICA = texto (Telefono Profesional Notifica)    ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'JSON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'JSON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Versión de la ficha de notificación epidemiológica. Nulo indica primera versión; desde V01_2020-03-06 hay cambios en estructura. Tipo: VARCHAR(20).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'VERSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Versión de la ficha de notificación ---> Si es valor nulo = primer versión ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'VERSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'VERSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de identificación, cédula o documento del paciente (PII sensible). Tipo: VARCHAR(50).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'NUMIDENT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de Identificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'NUMIDENT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'NUMIDENT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del profesional de salud responsable del diagnóstico clínico/epidemiológico. Tipo: VARCHAR(50).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'RESPONSDIAGNOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Responsable del Diagnóstico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'RESPONSDIAGNOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'RESPONSDIAGNOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha en que se reportó/emitió el resultado del examen diagnóstico. Tipo: DATE.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'FECHARESUL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Resultado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'FECHARESUL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'FECHARESUL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Recuento parasitario, densidad de parásitos en sangre (examen malaria). Tipo: VARCHAR(50).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'RECPARASITA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Recuento Parasitario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'RECPARASITA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'RECPARASITA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especie de Plasmodium infectante: 1=P. vivax, 2=P. falciparum, 3=P. malariae, 4=infección mixta. Tipo: VARCHAR(50).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'ESPECIE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especie  /  Especie Infectante:   1 = P. Vivax   2 = P. Falciparum   3 = P. Malariae   4 = Infección Mixta    ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'ESPECIE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'ESPECIE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado de examen (booleano). Campo obsoleto desde V01_2020-03-06. Tipo: BIT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'RESULTEXAMEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Resultados de Examen    --> campo obsoleto desde version nueva ''''V01_2020-03-06''''', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'RESULTEXAMEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'RESULTEXAMEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de examen diagnóstico: 1=GG (gota gruesa), 2=PDR (prueba rápida), 3=PCR (molecular), 4=otro. Eliminado desde V01_2020-03-06. Tipo: INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'TIPOEXAMEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Examen:   1 = GG    2 = PDR    3 = PCR    4 = OTRO  --> item eliminado desde version nueva ''''V01_2020-03-06''''  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'TIPOEXAMEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'TIPOEXAMEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Apellido(s) del paciente notificado. Tipo: VARCHAR(50).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'APELLPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Apellidos del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'APELLPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'APELLPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre(s) del paciente notificado. Tipo: VARCHAR(50).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'NOMBREPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombres del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'NOMBREPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'NOMBREPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del resultado del examen de laboratorio/diagnóstico. Tipo: DATE.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'FECHRESU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha del Resultado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'FECHRESU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'FECHRESU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado de examen, campo booleano obsoleto desde V01_2020-03-06. Tipo: BIT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'RESULEXAM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Resultados de Examen  --> campo obsoleto desde version nueva ''''V01_2020-03-06''''', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'RESULEXAM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'RESULEXAM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Responsable del diagnóstico epidemiológico, profesional notificador. Tipo: VARCHAR(50).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'RESPONDIAGN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Responsable del Diagnóstico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'RESPONDIAGN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'RESPONDIAGN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de inicio de tratamiento farmacológico (antimalarial, antiparasitario). Tipo: DATE.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'FECHINITRAT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Inicio de Tratamiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'FECHINITRAT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'FECHINITRAT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo/código de tratamiento iniciado para la enfermedad notificada. Tipo: INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'TRATAMIENTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tratamiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'TRATAMIENTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'TRATAMIENTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Otras complicaciones o aspectos relevantes del caso. Tipo: BIT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'OTRAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Otras', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'OTRAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'OTRAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Complicación hematológica (anemia, hemoglobinuria, trombocitopenia). Tipo: BIT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'HEMATOLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hematológica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'HEMATOLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'HEMATOLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Complicación pulmonar, síndrome de dificultad respiratoria. Tipo: BIT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'PULMONAR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Pulmonar', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'PULMONAR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'PULMONAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Complicación hepática, daño hepatocelular, insuficiencia hepática. Tipo: BIT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'HEPATICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hepática', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'HEPATICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'HEPATICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Complicación renal, insuficiencia renal aguda, necrosis tubular. Tipo: BIT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'RENAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Renal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'RENAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'RENAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Complicación cerebral, malaria cerebral, coma, convulsiones. Tipo: BIT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'CEREBRAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cerebral', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'CEREBRAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'CEREBRAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de presencia de complicaciones en el curso de la enfermedad. Tipo: BIT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'COMPLICACIONES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Complicaciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'COMPLICACIONES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'COMPLICACIONES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Presencia de gametocitos (formas sexuales del parásito en sangre). Tipo: BIT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'GAMETOCI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Gametocitos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'GAMETOCI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'GAMETOCI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Recuento parasitario, densidad parasitaria en examen de laboratorio. Tipo: VARCHAR(50).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'RECUPARASI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N' Recuento Parasitario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'RECUPARASI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'RECUPARASI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de examen: 1=GG (gota gruesa), 2=PDR (prueba rápida), 3=PCR, 4=otro. Eliminado V01_2020-03-06. Tipo: INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'TIPOEXA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Examen:   1 = GG    2 = PDR    3 = PCR    4 = OTRO  --> item eliminado desde version nueva ''''V01_2020-03-06''''  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'TIPOEXA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'TIPOEXA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Trimestre de gestación (1, 2, 3) al momento de diagnóstico en gestante. Tipo: INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'TRIMESGEST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Trimestre de Gestación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'TRIMESGEST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'TRIMESGEST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Recrudescencia o recurrencia, reaparición de síntomas tras curación aparente. Tipo: BIT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'RECRUDESCEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Recrudescencia  /  Recurrencia  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'RECRUDESCEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'RECRUDESCEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Caso nuevo (no recaída/recrudescencia). Campo obsoleto desde V01_2020-03-06. Tipo: BIT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'NUEVO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nuevo    --> campo obsoleto desde version nueva ''''V01_2020-03-06''''', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'NUEVO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'NUEVO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación según origen: importado, autóctono, inducido. Tipo: BIT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'CLASIORIGE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Clasificación Según Origen', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'CLASIORIGE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'CLASIORIGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador: paciente presenta síntomas clínicos de la enfermedad. Tipo: BIT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'SINTOMATICO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Sintomático', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'SINTOMATICO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'SINTOMATICO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vigilancia activa, búsqueda activa de casos en comunidad. Tipo: BIT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'VIGIACT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Vigilancia Activa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'VIGIACT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'VIGIACT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Causa básica de muerte registrada en certificado de defunción. Tipo: VARCHAR(50).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'CAUSABASIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Causa Básica de Muerte', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'CAUSABASIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'CAUSABASIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de certificado de defunción (cuando aplica). Tipo: VARCHAR(50).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'NUMECERTIF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N' Número de Certificado de Defunción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'NUMECERTIF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'NUMECERTIF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de fallecimiento del paciente. Tipo: DATE.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'FECHADEFUN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Defunción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'FECHADEFUN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'FECHADEFUN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Condición final del caso: 1=vivo, 2=muerto, 3=no sabe. Obsoleto V01_2020-03-06, se usa JSON. Tipo: BIT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'CONDIFINAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Condición Final     --> campo obsoleto desde version nueva ''''V01_2020-03-06''''  , Se utiliza ahora una columna en campo JSON', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'CONDIFINAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'CONDIFINAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de hospitalización o ingreso a institución de salud. Tipo: DATE.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'FECHAHOSPIT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Hospitalización', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'FECHAHOSPIT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'FECHAHOSPIT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador: paciente requirió hospitalización. Tipo: BIT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'HOSPITALIZADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hospitalizado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'HOSPITALIZADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'HOSPITALIZADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación inicial del caso: sospechoso, probable, confirmado. Obsoleto V01_2020-03-06, migrado a JSON. Tipo: BIT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'CLASIFCASO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Clasificación Inicial del Caso   --> campo obsoleto desde version nueva ''''V01_2020-03-06''''  , Se utiliza ahora una columna en campo JSON', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'CLASIFCASO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'CLASIFCASO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de inicio de síntomas, comienzo de enfermedad. Tipo: DATE.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'FECHAINICIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Inicio de Sintomas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'FECHAINICIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'FECHAINICIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de la consulta, atención o notificación del caso. Tipo: DATE.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'FECHACONSULT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Consulta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'FECHACONSULT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'FECHACONSULT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dirección de residencia habitual del paciente (PII). Tipo: VARCHAR(50).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'DIRERESIDE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dirección de Residencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'DIRERESIDE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'DIRERESIDE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'País de residencia actual del paciente. Obsoleto V01_2020-03-06. Tipo: VARCHAR(50).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'PAISRESIDE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'País de Residencia   --> campo obsoleto desde version nueva ''''V01_2020-03-06''''', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'PAISRESIDE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'PAISRESIDE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Departamento y municipio de residencia habitual del paciente. Tipo: VARCHAR(50).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'DEPMUNIRESID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Departamento y Municipios de Residencia del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'DEPMUNIRESID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'DEPMUNIRESID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fuente de información, origen del reporte (paciente, contacto, establecimiento). Tipo: INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'FUENTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fuente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'FUENTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'FUENTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Pertenencia a otros grupos poblacionales especiales. Tipo: BIT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'OTROGRUPPOB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Otros Grupos Poblacionales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'OTROGRUPPOB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'OTROGRUPPOB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador: víctima de violencia armada, conflicto. Tipo: BIT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'VICTVIOLEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Víctimas de Violencia Armada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'VICTVIOLEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'VICTVIOLEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Residente en centros psiquiátricos o instituciones de salud mental. Tipo: BIT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'POBPSIQUIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Centros Psiquiátricos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'POBPSIQUIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'POBPSIQUIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador: persona desmovilizada de grupos armados. Tipo: BIT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'DESMOVIL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Desmovilizado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'DESMOVIL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'DESMOVIL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador: madre comunitaria, cuidadora de ICBF. Tipo: BIT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'MADCOMUNIT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Madres Comunitarias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'MADCOMUNIT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'MADCOMUNIT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador: población infantil a cargo del ICBF (Instituto Colombiano de Bienestar Familiar). Tipo: BIT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'POBLAICBF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Población Infantil a Cargo del ICBF', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'POBLAICBF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'POBLAICBF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador: sin vivienda, persona en situación de indigencia. Tipo: BIT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'INDIGENTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indigente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'INDIGENTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'INDIGENTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador: paciente embarazada o gestante. Tipo: BIT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'GESTANTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Gestante', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'GESTANTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'GESTANTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador: privado de libertad, recluido en establecimiento penitenciario. Tipo: BIT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'CARCELARIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Carcelario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'CARCELARIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'CARCELARIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador: migrante internacional o desplazamiento reciente. Tipo: BIT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'MIGRANTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Migrante', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'MIGRANTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'MIGRANTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador: desplazado interno por violencia o desastre. Tipo: BIT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'DESPLAZADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Desplazado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'DESPLAZADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'DESPLAZADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador: persona con discapacidad certificada. Tipo: BIT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'DISCAPACITADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Discapacitado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'DISCAPACITADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'DISCAPACITADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estrato socioeconómico (1-6 en Colombia). Tipo: VARCHAR(50).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'ESTRATO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estrato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'ESTRATO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'ESTRATO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Pertenencia étnica: indígena, afrodescendiente, palenquero, raizal, ROM. Tipo: INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'PERTETNICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Pertenencia Étnica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'PERTETNICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'PERTETNICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de la administradora de planes de beneficios (EPS, aseguradora). Tipo: VARCHAR(50).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'NOMADMISAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de la Administración de Salud y Código  /  Administradora planes de beneficios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'NOMADMISAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'NOMADMISAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de régimen de salud: contributivo, subsidiado, especial. Tipo: INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'TIPREGISAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Régimen en Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'TIPREGISAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'TIPREGISAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ocupación o profesión del paciente, código ISCO. Tipo: VARCHAR(50).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'OCUPACIEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ocupación del Paciente y Código', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'OCUPACIEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'OCUPACIEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Localidad, barrio o vereda de ocurrencia del caso. Tipo: VARCHAR(50).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'LOCALBARRIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Localidad/Barrio/Vereda de Ocurrencia del Caso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'LOCALBARRIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'LOCALBARRIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Área de procedencia/ocurrencia: urbana, rural, corregimiento. Tipo: INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'AREAPROCE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Area de Procedencia / Ocurrencia del Caso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'AREAPROCE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'AREAPROCE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Departamento y municipio de procedencia u ocurrencia del evento. Tipo: VARCHAR(50).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'DEPAMUNI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Departamento y Municipio de Procedencia / Ocurrencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'DEPAMUNI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'DEPAMUNI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'País de ocurrencia del caso, si fue internacional. Tipo: VARCHAR(50).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'PAISOCUR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'País de Ocurrencia del Caso y Código', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'PAISOCUR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'PAISOCUR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sexo biológico: 1=masculino, 2=femenino, 3=indeterminado. Tipo: INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'SEXO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Sexo:   1 = Masculino   2 = Femenino   3 = Indeterminado   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'SEXO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'SEXO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de medida de edad: 1=años, 2=meses, 3=días, 4=horas, 5=minutos, 6=no aplica. Tipo: INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'UNIMEDEDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad de Medida de la Edad:  1 = Años   2 = Meses   3 = Días   4 = Horas   5 = Minutos   6 = No aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'UNIMEDEDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'UNIMEDEDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Edad del paciente en la unidad especificada. Tipo: VARCHAR(50).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'EDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Edad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'EDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'EDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de nacimiento del paciente (PII). Tipo: DATE.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'FECHANAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Nacimiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'FECHANAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'FECHANAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Teléfono de contacto del paciente o notificador (PII). Tipo: VARCHAR(50).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'TELEFONO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Teléfono', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'TELEFONO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'TELEFONO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre completo y apellidos del paciente (PII). Tipo: VARCHAR(50).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'NOMAPEPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombres y Apellidos del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'NOMAPEPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'NOMAPEPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de documento: 1=RC (registro civil), 2=TI (tarjeta identidad), 3=CC (cédula), 4=CE (cédula extranjería), 5=PA (pasaporte), 6=MS/AS/PE/CN. Tipo: INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'TIPDOCU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo documento:   1=RC    2=TI   3=CC    4=CE   5=PA   6=MS   7=AS   8=PE   9= CN   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'TIPDOCU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'TIPDOCU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de caracterización o notificación del caso epidemiológico. Tipo: DATE.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'FECHACARACT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Caracterización  / Fecha de la notificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'FECHACARACT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'FECHACARACT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especie de Plasmodium infectante: 1=P. vivax, 2=P. falciparum, 3=P. malariae, 4=infección mixta. Tipo: INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'ESPEPLASM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especie de Plasmodium  /   Especie Infectante:   1 = P. Vivax   2 = P. Falciparum   3 = P. Malariae   4 = Infección Mixta    ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'ESPEPLASM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'ESPEPLASM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de prestador de servicios de salud, Unidad Primaria de Generación de Datos (UPGD). Tipo: VARCHAR(50).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'CODPREST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de Prestador de Servicios de Salud   /  Código de la UPGD', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'CODPREST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'CODPREST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de diagnóstico CIE-10 (código enfermedad notificable). Tipo: CHAR(4).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de Diagnóstico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de registro padre en tabla HCFICHANOTIFICACION. FK a HCFICHANOTIFICACION.ID. Tipo: INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de la tabla cabecera Ficha Notificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autonumérico (IDENTITY 1,1) de la tabla HCFICHA465. Tipo: INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumérico de la Tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ficha epidemiológica 465 del SIVIGILA para notificación de casos de malaria. Registra los datos del paciente, características clínicas, clasificación del caso, complicaciones, tratamiento y resultados de exámenes de laboratorio reportados al sistema de vigilancia en salud pública.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA465';
