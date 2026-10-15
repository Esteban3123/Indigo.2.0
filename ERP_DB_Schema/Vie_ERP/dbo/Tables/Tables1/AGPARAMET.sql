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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Parametro permite que a un profesional se le pueda crear la disponibilidad de agenda en un mismo momento en varios consultorios diferentes.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'AllowPromptedQuery';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario por defecto al crear citas web.    lo que este en este campo es lo que va ir a la tabla AGASISITA cuando se haga la creacion de unas cita web por el portal.  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'DefaultUserWebAppointment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Determina si se habilita o no calendario de calculo de disponibilidades en formulario de citas de apoyo diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'DISPCALENDA_APODX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Permitir en agendamiento crear el paciente con datos basicos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'PERCREARPACIBASICOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Determina si se habilita o no calendario de calculo de disponibilidades en formulario de citas de tratamiento especial', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'DISPCALENDA_ESP';


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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Notifica al Paciente a las X Horas Antes de la Cita, con SMS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'NOTHORCITSMS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Saber si se Notifica al Paciente al Reasignar una Cita medica, con SMS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'NOTREACITSMS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Saber si se Notifica al Paciente al Cancelar una Cita medica, con SMS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'NOTCANCITSMS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Saber si se Notifica al Paciente al Agregar una Cita medica, con SMS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'NOTAGRCITSMS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Pasada la cirugía - Números de horas para cancelar/ re-programar.  (Dashboard Getion QX - Pestaña Programados - estas hora se tendra encuenta cuando una cirugia ya paso y requieren cancelar o reprogramar en una nueva fecha)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'HORAPROCESOQX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Año de arranque del módulo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'ARRANQUEMODULO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Permitir crear agendas para sala de apoyo diagnóstico (laboratorio, imágenes Dx)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'PERMCREARAGENDAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Inicio cirugía - Números de horas previas para la solicitud de paquetes:', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'SOLICITUDPAQUETES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Permitir confirmar radicación cirugías con pendientes  True = Si  False = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'RADCXDOCPEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Disponibilidad calendario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'DISPCALENDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Saber si se Notifica al Paciente via Correo Electronico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'NOTIFMAIL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Saber si se Notifica al Paciente via Mensaje de Texto.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'NOTIFISMS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Notifica al Paciente a las X Horas Antes de la Cita.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'NOTHORCIT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Saber si se Notifica al Paciente al Reasignar una Cita medica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'NOTREACIT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Saber si se Notifica al Paciente al Cancelar una Cita medica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'NOTCANCIT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Saber si se Notifica al Paciente al Agregar una Cita medica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'NOTAGRCIT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Mensaje inferior para la cita medica cuando se va a mandar un mensaje de texto.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'MENSAJSMS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Mensaje inferior para la cita medica cuando se va a mandar un correo electronico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'MENSAJCOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Mensaje inferior para la cita medica cuando se va imprimir.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'MENSAJIMP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Parametro de la Hora Final del Horario Laboral', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'HORAFINAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Parametro de la Hora Inicial del Horario Laboral', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'HORAINICI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Parametro para Permitir Acceso desde la Web', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'ACCESOWEB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Parametro Para Verificar si Permite Facturar ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'SOLFACTUR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Parametro Para Verificar si Realiza Citas Continuas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'SOLCITCON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Parametro para Verificar si Genera Agenda Labora los Festivos. -->Campo que queda obsoleto después de PBI 23769-Ajuste al formulario de parametrización del módulo de "Agendamiento" 29-01-2025<--', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'GENAGEFES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Parametro para Verificar si Genera Agenda Labora los Domingos. -->Campo que queda obsoleto después de PBI 23769-Ajuste al formulario de parametrización del módulo de "Agendamiento" 29-01-2025<--', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'GENAGEDOM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Parametro para Verificar si Genera Agenda los Sabados. -->Campo que queda obsoleto después de PBI 23769-Ajuste al formulario de parametrización del módulo de "Agendamiento" 29-01-2025<--', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'GENAGESAB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Centro de Atención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'CODCENATE';

