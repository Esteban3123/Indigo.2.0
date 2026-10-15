CREATE TABLE [dbo].[AGPARAMET] (
    [CODCENATE]                       CHAR (10)     NOT NULL,
    [GENAGESAB]                       BIT           NULL,
    [GENAGEDOM]                       BIT           NULL,
    [GENAGEFES]                       BIT           NULL,
    [SOLCITCON]                       BIT           NOT NULL,
    [SOLFACTUR]                       BIT           NOT NULL,
    [ACCESOWEB]                       BIT           NOT NULL,
    [HORAINICI]                       DATETIME      NOT NULL,
    [HORAFINAL]                       DATETIME      NOT NULL,
    [MENSAJIMP]                       VARCHAR (MAX) NOT NULL,
    [MENSAJCOR]                       VARCHAR (MAX) NULL,
    [MENSAJSMS]                       VARCHAR (100) NULL,
    [NOTAGRCIT]                       BIT           NOT NULL,
    [NOTCANCIT]                       BIT           NOT NULL,
    [NOTREACIT]                       BIT           NOT NULL,
    [NOTHORCIT]                       TINYINT       NULL,
    [NOTIFISMS]                       BIT           NOT NULL,
    [NOTIFMAIL]                       BIT           NOT NULL,
    [DISPCALENDA]                     BIT           CONSTRAINT [DF__AGPARAMET__DISPC__13C7D8B9] DEFAULT ((0)) NOT NULL,
    [RADCXDOCPEN]                     BIT           NULL,
    [SOLICITUDPAQUETES]               INT           NULL,
    [PERMCREARAGENDAS]                BIT           NULL,
    [ARRANQUEMODULO]                  DATETIME      NULL,
    [HORAPROCESOQX]                   INT           NULL,
    [NOTAGRCITSMS]                    BIT           NULL,
    [NOTCANCITSMS]                    BIT           NULL,
    [NOTREACITSMS]                    BIT           NULL,
    [NOTHORCITSMS]                    TINYINT       NULL,
    [WEBMEDGENERAL]                   BIT           NULL,
    [WEBMEDESPECIALIZADA]             BIT           NULL,
    [WEBODONTOLOGIA]                  BIT           NULL,
    [WEBLABORATORIO]                  BIT           NULL,
    [WEBIMAGENESDX]                   BIT           NULL,
    [WEBPROMOPREVEN]                  BIT           NULL,
    [WEBOTROSPROF]                    BIT           NULL,
    [OBLICUPSAPODX]                   BIT           NULL,
    [IMPREGASITENCIA]                 BIT           NULL,
    [DISPCALENDA_ESP]                 BIT           NULL,
    [PERCREARPACIBASICOS]             BIT           NULL,
    [DISPCALENDA_APODX]               BIT           NULL,
    [DefaultUserWebAppointment]       CHAR (20)     NULL,
    [AllowPromptedQuery]              BIT           NULL,
    [GenerateAgendaOnSaturday]        VARCHAR (MAX) NULL,
    [GenerateAgendaOnSunday]          VARCHAR (MAX) NULL,
    [GenerateAgendaOnHolidays]        VARCHAR (MAX) NULL,
    [AppointmentAssignmentFrequently] INT           NULL,
    CONSTRAINT [PK_AGPARAMET] PRIMARY KEY CLUSTERED ([CODCENATE] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Columna que guarda el valor del campo Asignacion de citas con frecuencia: 1-> Asignacion automática, 2-> Seleccionar disponibilidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'AppointmentAssignmentFrequently';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'JSON donde guardaremos los tipos de agenda que se pueden generar los festivos (Citas médicas, Apoyo Dx, Tratamientos especiales y Cirugia)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'GenerateAgendaOnHolidays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'JSON donde guardaremos los tipos de agenda que se pueden generar los domingos (Citas médicas, Apoyo Dx, Tratamientos especiales y Cirugia)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'GenerateAgendaOnSunday';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'JSON donde guardaremos los tipos de agenda que se pueden generar los sabados (Citas médicas, Apoyo Dx, Tratamientos especiales y Cirugia)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'GenerateAgendaOnSaturday';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario por defecto al crear citas web.    lo que este en este campo es lo que va ir a la tabla AGASISITA cuando se haga la creacion de unas cita web por el portal.  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'DefaultUserWebAppointment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Implementar registro de asistencia en flujo de trabajo de imagenología    True = Si    False = NO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'IMPREGASITENCIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Obligación selección CUPS en citas de apoyo (true or false)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'OBLICUPSAPODX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Consula web Otros Profesionales  True = Si  False = NO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'WEBOTROSPROF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Consula web Promocion y prevencion  True = Si  False = NO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'WEBPROMOPREVEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Consula web Imagenes DX  True = Si  False = NO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'WEBIMAGENESDX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Consula web Laboratorios  True = Si  False = NO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'WEBLABORATORIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Consula web Odontologia  True = Si  False = NO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'WEBODONTOLOGIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Consula web Medicina Especializada  True = Si  False = NO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'WEBMEDESPECIALIZADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Consula web Medicina General  True = Si  False = NO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'WEBMEDGENERAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Pasada la cirugía - Números de horas para cancelar/ re-programar.  (Dashboard Getion QX - Pestaña Programados - estas hora se tendra encuenta cuando una cirugia ya paso y requieren cancelar o reprogramar en una nueva fecha)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'HORAPROCESOQX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Permitir confirmar radicación cirugías con pendientes  True = Si  False = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'RADCXDOCPEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Parametro para Verificar si Genera Agenda Labora los Festivos. -->Campo que queda obsoleto después de PBI 23769-Ajuste al formulario de parametrización del módulo de "Agendamiento" 29-01-2025<--', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'GENAGEFES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Parametro para Verificar si Genera Agenda Labora los Domingos. -->Campo que queda obsoleto después de PBI 23769-Ajuste al formulario de parametrización del módulo de "Agendamiento" 29-01-2025<--', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'GENAGEDOM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Parametro para Verificar si Genera Agenda los Sabados. -->Campo que queda obsoleto después de PBI 23769-Ajuste al formulario de parametrización del módulo de "Agendamiento" 29-01-2025<--', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'GENAGESAB';

