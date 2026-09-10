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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT. Modo de asignación de citas: 1=automática, 2=seleccionar disponibilidad. Frecuencia de asignación de citas médicas, consultas, agendamiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'AppointmentAssignmentFrequently';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Columna que guarda el valor del campo Asignacion de citas con frecuencia: 1-> Asignacion automática, 2-> Seleccionar disponibilidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'AppointmentAssignmentFrequently';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'AppointmentAssignmentFrequently';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(MAX) JSON. Tipos de agenda permitidos en días festivos: citas médicas, apoyo diagnóstico (laboratorio/imágenes), tratamientos especiales, cirugía. Parametrización agendamiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'GenerateAgendaOnHolidays';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'JSON donde guardaremos los tipos de agenda que se pueden generar los festivos (Citas médicas, Apoyo Dx, Tratamientos especiales y Cirugia)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'GenerateAgendaOnHolidays';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'GenerateAgendaOnHolidays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(MAX) JSON. Tipos de agenda permitidos domingos: citas médicas, apoyo diagnóstico, tratamientos especiales, cirugía. Control generación agenda semanal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'GenerateAgendaOnSunday';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'JSON donde guardaremos los tipos de agenda que se pueden generar los domingos (Citas médicas, Apoyo Dx, Tratamientos especiales y Cirugia)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'GenerateAgendaOnSunday';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'GenerateAgendaOnSunday';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(MAX) JSON. Tipos de agenda permitidos sábados: citas médicas, apoyo diagnóstico, tratamientos especiales, cirugía. Control generación agenda semanal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'GenerateAgendaOnSaturday';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'JSON donde guardaremos los tipos de agenda que se pueden generar los sabados (Citas médicas, Apoyo Dx, Tratamientos especiales y Cirugia)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'GenerateAgendaOnSaturday';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'GenerateAgendaOnSaturday';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT. Permite crear disponibilidad agenda profesional simultáneamente en múltiples consultorios. Funcionalidad agendamiento multi-consultorio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'AllowPromptedQuery';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Parametro permite que a un profesional se le pueda crear la disponibilidad de agenda en un mismo momento en varios consultorios diferentes.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'AllowPromptedQuery';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'AllowPromptedQuery';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'CHAR(20). Usuario por defecto asignado citas web (portal paciente). Se registra en AGASISITA. PII_Usuario, agendamiento web.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'DefaultUserWebAppointment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario por defecto al crear citas web.    lo que este en este campo es lo que va ir a la tabla AGASISITA cuando se haga la creacion de unas cita web por el portal.  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'DefaultUserWebAppointment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'DefaultUserWebAppointment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT. Habilita/deshabilita calendario cálculo disponibilidades en citas de apoyo diagnóstico (laboratorio/imágenes). Control UI agendamiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'DISPCALENDA_APODX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Determina si se habilita o no calendario de calculo de disponibilidades en formulario de citas de apoyo diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'DISPCALENDA_APODX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'DISPCALENDA_APODX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT. Permite crear paciente durante agendamiento con datos básicos. Control registro paciente, citas médicas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'PERCREARPACIBASICOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Permitir en agendamiento crear el paciente con datos basicos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'PERCREARPACIBASICOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'PERCREARPACIBASICOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT. Habilita/deshabilita calendario cálculo disponibilidades en citas tratamiento especial. Control UI agendamiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'DISPCALENDA_ESP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Determina si se habilita o no calendario de calculo de disponibilidades en formulario de citas de tratamiento especial', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'DISPCALENDA_ESP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'DISPCALENDA_ESP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT. Implementar registro asistencia en imagenología: 1=Sí, 0=No. Flujo trabajo imagen diagnóstica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'IMPREGASITENCIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Implementar registro de asistencia en flujo de trabajo de imagenología    True = Si    False = NO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'IMPREGASITENCIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'IMPREGASITENCIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT. Obliga seleccionar CUPS (procedimiento) en citas apoyo diagnóstico. Validación obligatoria agendamiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'OBLICUPSAPODX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Obligación selección CUPS en citas de apoyo (true or false)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'OBLICUPSAPODX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'OBLICUPSAPODX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT. Habilita consultas web Otros Profesionales: 1=Sí, 0=No. Tipo consulta telemedicina portal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'WEBOTROSPROF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Consula web Otros Profesionales  True = Si  False = NO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'WEBOTROSPROF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'WEBOTROSPROF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT. Habilita consultas web Promoción y Prevención: 1=Sí, 0=No. Tipo consulta telemedicina portal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'WEBPROMOPREVEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Consula web Promocion y prevencion  True = Si  False = NO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'WEBPROMOPREVEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'WEBPROMOPREVEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT. Habilita solicitud web Imágenes Diagnósticas: 1=Sí, 0=No. Tipo consulta apoyo diagnóstico portal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'WEBIMAGENESDX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Consula web Imagenes DX  True = Si  False = NO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'WEBIMAGENESDX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'WEBIMAGENESDX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT. Habilita solicitud web Laboratorio: 1=Sí, 0=No. Tipo consulta apoyo diagnóstico portal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'WEBLABORATORIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Consula web Laboratorios  True = Si  False = NO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'WEBLABORATORIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'WEBLABORATORIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT. Habilita consultas web Odontología: 1=Sí, 0=No. Tipo consulta especializada portal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'WEBODONTOLOGIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Consula web Odontologia  True = Si  False = NO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'WEBODONTOLOGIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'WEBODONTOLOGIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT. Habilita consultas web Medicina Especializada: 1=Sí, 0=No. Tipo consulta especialista portal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'WEBMEDESPECIALIZADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Consula web Medicina Especializada  True = Si  False = NO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'WEBMEDESPECIALIZADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'WEBMEDESPECIALIZADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT. Habilita consultas web Medicina General: 1=Sí, 0=No. Tipo consulta atención primaria portal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'WEBMEDGENERAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Consula web Medicina General  True = Si  False = NO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'WEBMEDGENERAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'WEBMEDGENERAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TINYINT. Notificación SMS X horas antes cita. Recordatorio de cita médica vía mensaje texto.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'NOTHORCITSMS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Notifica al Paciente a las X Horas Antes de la Cita, con SMS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'NOTHORCITSMS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'NOTHORCITSMS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT. Notifica reasignación cita vía SMS: 1=Sí, 0=No. Cambio cita comunicación paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'NOTREACITSMS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Saber si se Notifica al Paciente al Reasignar una Cita medica, con SMS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'NOTREACITSMS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'NOTREACITSMS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT. Notifica cancelación cita vía SMS: 1=Sí, 0=No. Cancelación cita comunicación paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'NOTCANCITSMS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Saber si se Notifica al Paciente al Cancelar una Cita medica, con SMS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'NOTCANCITSMS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'NOTCANCITSMS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT. Notifica creación cita vía SMS: 1=Sí, 0=No. Confirmación nueva cita comunicación paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'NOTAGRCITSMS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Saber si se Notifica al Paciente al Agregar una Cita medica, con SMS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'NOTAGRCITSMS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'NOTAGRCITSMS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT. Horas post-cirugía para cancelar/reprogramar. Gestión QX: duración máxima para reasignar procedimiento quirúrgico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'HORAPROCESOQX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Pasada la cirugía - Números de horas para cancelar/ re-programar.  (Dashboard Getion QX - Pestaña Programados - estas hora se tendra encuenta cuando una cirugia ya paso y requieren cancelar o reprogramar en una nueva fecha)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'HORAPROCESOQX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'HORAPROCESOQX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DATETIME. Fecha/hora arranque módulo agendamiento. Auditoría implementación funcionalidad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'ARRANQUEMODULO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Año de arranque del módulo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'ARRANQUEMODULO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'ARRANQUEMODULO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT. Permite crear agendas sala apoyo diagnóstico (laboratorio, imágenes). Control creación agenda especializada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'PERMCREARAGENDAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Permitir crear agendas para sala de apoyo diagnóstico (laboratorio, imágenes Dx)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'PERMCREARAGENDAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'PERMCREARAGENDAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT. Horas previas cirugía para solicitar paquetes. Preoperatorio: tiempo anticipado preparación quirúrgica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'SOLICITUDPAQUETES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Inicio cirugía - Números de horas previas para la solicitud de paquetes:', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'SOLICITUDPAQUETES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'SOLICITUDPAQUETES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT. Permite confirmar radicación cirugía con documentos pendientes: 1=Sí, 0=No. Control flujo cirugía.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'RADCXDOCPEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Permitir confirmar radicación cirugías con pendientes  True = Si  False = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'RADCXDOCPEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'RADCXDOCPEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT. Habilita cálculo disponibilidades calendario. Control UI agendamiento: 0=deshabilitado (default).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'DISPCALENDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Disponibilidad calendario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'DISPCALENDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'DISPCALENDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT. Notifica paciente vía correo electrónico: 1=Sí, 0=No. Canal comunicación cita médica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'NOTIFMAIL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Saber si se Notifica al Paciente via Correo Electronico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'NOTIFMAIL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'NOTIFMAIL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT. Notifica paciente vía SMS/mensaje texto: 1=Sí, 0=No. Canal comunicación cita médica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'NOTIFISMS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Saber si se Notifica al Paciente via Mensaje de Texto.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'NOTIFISMS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'NOTIFISMS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TINYINT. Notificación X horas antes cita (correo/SMS). Recordatorio previo cita médica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'NOTHORCIT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Notifica al Paciente a las X Horas Antes de la Cita.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'NOTHORCIT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'NOTHORCIT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT. Notifica reasignación cita: 1=Sí, 0=No. Cambio cita comunicación paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'NOTREACIT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Saber si se Notifica al Paciente al Reasignar una Cita medica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'NOTREACIT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'NOTREACIT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT. Notifica cancelación cita: 1=Sí, 0=No. Cancelación cita comunicación paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'NOTCANCIT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Saber si se Notifica al Paciente al Cancelar una Cita medica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'NOTCANCIT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'NOTCANCIT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT. Notifica creación/asignación cita: 1=Sí, 0=No. Confirmación cita comunicación paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'NOTAGRCIT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Saber si se Notifica al Paciente al Agregar una Cita medica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'NOTAGRCIT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'NOTAGRCIT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(100). Plantilla mensaje SMS footer cita médica. Texto personalizado notificación texto.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'MENSAJSMS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mensaje inferior para la cita medica cuando se va a mandar un mensaje de texto.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'MENSAJSMS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'MENSAJSMS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(MAX). Plantilla mensaje email footer cita médica. Texto personalizado notificación correo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'MENSAJCOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mensaje inferior para la cita medica cuando se va a mandar un correo electronico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'MENSAJCOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'MENSAJCOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(MAX). Plantilla mensaje impresión footer cita médica. Texto personalizado comprobante impreso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'MENSAJIMP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mensaje inferior para la cita medica cuando se va imprimir.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'MENSAJIMP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'MENSAJIMP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DATETIME. Hora final jornada laboral centro atención. Horario funcionamiento cierre agenda.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'HORAFINAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Parametro de la Hora Final del Horario Laboral', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'HORAFINAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'HORAFINAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DATETIME. Hora inicial jornada laboral centro atención. Horario funcionamiento apertura agenda.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'HORAINICI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Parametro de la Hora Inicial del Horario Laboral', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'HORAINICI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'HORAINICI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT. Permite acceso plataforma web: 1=Sí, 0=No. Portal paciente, citas online.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'ACCESOWEB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Parametro para Permitir Acceso desde la Web', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'ACCESOWEB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'ACCESOWEB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT. Permite facturación: 1=Sí, 0=No. Control emisión facturas, facturación servicios.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'SOLFACTUR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Parametro Para Verificar si Permite Facturar ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'SOLFACTUR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'SOLFACTUR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT. Permite citas continuas/consecutivas: 1=Sí, 0=No. Control agendamiento doble cita.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'SOLCITCON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Parametro Para Verificar si Realiza Citas Continuas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'SOLCITCON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'SOLCITCON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT. [OBSOLETO PBI 23769] Genera agenda laboral festivos: 1=Sí, 0=No. Reemplazado GenerateAgendaOnHolidays JSON.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'GENAGEFES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Parametro para Verificar si Genera Agenda Labora los Festivos. -->Campo que queda obsoleto después de PBI 23769-Ajuste al formulario de parametrización del módulo de "Agendamiento" 29-01-2025<--', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'GENAGEFES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'GENAGEFES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT. [OBSOLETO PBI 23769] Genera agenda laboral domingos: 1=Sí, 0=No. Reemplazado GenerateAgendaOnSunday JSON.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'GENAGEDOM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Parametro para Verificar si Genera Agenda Labora los Domingos. -->Campo que queda obsoleto después de PBI 23769-Ajuste al formulario de parametrización del módulo de "Agendamiento" 29-01-2025<--', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'GENAGEDOM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'GENAGEDOM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT. [OBSOLETO PBI 23769] Genera agenda laboral sábados: 1=Sí, 0=No. Reemplazado GenerateAgendaOnSaturday JSON.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'GENAGESAB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Parametro para Verificar si Genera Agenda los Sabados. -->Campo que queda obsoleto después de PBI 23769-Ajuste al formulario de parametrización del módulo de "Agendamiento" 29-01-2025<--', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'GENAGESAB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'GENAGESAB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'CHAR(10) PK. Código centro atención, sede, unidad funcional. FK referencia configuración agendamiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Parámetros de configuración del módulo de agendamiento por centro de atención. Define reglas operativas, horarios, notificaciones, accesos web y comportamientos del sistema de citas médicas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPARAMET';
