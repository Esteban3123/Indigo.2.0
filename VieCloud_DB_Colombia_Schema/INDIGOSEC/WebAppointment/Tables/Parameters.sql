CREATE TABLE [WebAppointment].[Parameters] (
    [Id]                                        INT            IDENTITY (1, 1) NOT NULL,
    [ContainerId]                               INT            NOT NULL,
    [PrimaryColorHex]                           VARCHAR (7)    CONSTRAINT [DF_Parameters_PrimaryColorHex] DEFAULT ('#555195') NOT NULL,
    [BrandImgUrl]                               VARCHAR (MAX)  CONSTRAINT [DF_Parameters_BrandImgUrl] DEFAULT ('/default/brand.png') NOT NULL,
    [FaviconUrl]                                VARCHAR (MAX)  CONSTRAINT [DF_Parameters_FaviconUrl] DEFAULT ('/default/favicon.ico') NOT NULL,
    [ManifestUrl]                               VARCHAR (MAX)  CONSTRAINT [DF_Parameters_ManifestUrl] DEFAULT ('/default/site.webmanifest') NOT NULL,
    [ValidateNewPatientIdentity]                BIT            CONSTRAINT [DF_Parameters_ValidateNewPatientIdentity] DEFAULT ((0)) NOT NULL,
    [AllowCreatePatient]                        BIT            CONSTRAINT [DF_Parameters_AllowCreatePatient] DEFAULT ((1)) NOT NULL,
    [CreatedAt]                                 DATETIME       CONSTRAINT [DF__Parameter__Creat__06A2E7C5] DEFAULT (getdate()) NULL,
    [UpdatedAt]                                 DATETIME       CONSTRAINT [DF__Parameter__Updat__07970BFE] DEFAULT (getdate()) NULL,
    [NewAppointmentsStatus]                     INT            CONSTRAINT [DF_Parameters_NewAppointmentsStatus] DEFAULT ((3)) NOT NULL,
    [ReservationExpiryMinutes]                  INT            CONSTRAINT [DF_Parameters_ReservationExpiryMinutes] DEFAULT ((5)) NOT NULL,
    [WhatsAppNumbers]                           VARCHAR (MAX)  NULL,
    [WebAppointmentDomains]                     VARCHAR (MAX)  NULL,
    [StylesheetUrl]                             VARCHAR (MAX)  CONSTRAINT [DF_Parameters_StylesheetUrl] DEFAULT ('/default/styles.css') NOT NULL,
    [HeaderLinks]                               NVARCHAR (MAX) NULL,
    [TutorialLinkUrl]                           NVARCHAR (MAX) NULL,
    [HomeMainFormMessage]                       VARCHAR (MAX)  NULL,
    [AuthFormMessage]                           VARCHAR (MAX)  NULL,
    [MaxActiveAppointmentsPerActivitySpecialty] SMALLINT       CONSTRAINT [DF_Parameters_MaxActiveAppointmentsPerActivitySpecialty] DEFAULT ((0)) NOT NULL,
    [AgentPhoneNumbers]                         VARCHAR (MAX)  NULL,
    [PrivacyPolicy]                             NVARCHAR (MAX) NULL,
    [MaxAgeForOutpatientBooking]                SMALLINT       CONSTRAINT [DF_Parameters_MaxAgeForOutpatientBooking] DEFAULT ((-1)) NOT NULL,
    [MaxAgeForOutpatientBookingMessage]         VARCHAR (500)  NULL,
    [MaxActiveAppointmentsPerActivity]          SMALLINT       CONSTRAINT [DF_Parameters_MaxActiveAppointmentsPerActivity] DEFAULT ((0)) NOT NULL,
    [MaxActiveAppointmentsPerSpecialty]         SMALLINT       CONSTRAINT [DF_Parameters_MaxActiveAppointmentsPerSpecialty] DEFAULT ((0)) NOT NULL,
    [NotificationPatientWhitelistQa]            VARCHAR (MAX)  NULL,
    CONSTRAINT [PK__Paramete__3214EC078FBDFD00] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Parameters_Containers] FOREIGN KEY ([ContainerId]) REFERENCES [Security].[Containers] ([Id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'Parameters';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'Parameters', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relacion con la tabla Security.Containers', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'Parameters', @level2type = N'COLUMN', @level2name = N'ContainerId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código hexadecimal del color principal', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'Parameters', @level2type = N'COLUMN', @level2name = N'PrimaryColorHex';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'URL de la imagen de la marca o logo.', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'Parameters', @level2type = N'COLUMN', @level2name = N'BrandImgUrl';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'URL del favicon', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'Parameters', @level2type = N'COLUMN', @level2name = N'FaviconUrl';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'URL al archivo manifest.json', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'Parameters', @level2type = N'COLUMN', @level2name = N'ManifestUrl';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Parametro para activar la validacion de identidad cuando se registra un usuario que no está registrado como paciente.', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'Parameters', @level2type = N'COLUMN', @level2name = N'ValidateNewPatientIdentity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Parametro para permitir la creación de un paciente nuevo.', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'Parameters', @level2type = N'COLUMN', @level2name = N'AllowCreatePatient';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de la creacion del registro', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'Parameters', @level2type = N'COLUMN', @level2name = N'CreatedAt';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de la actualizacion del registro', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'Parameters', @level2type = N'COLUMN', @level2name = N'UpdatedAt';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de la nueva cita 0. Asignada 3. PreAsignada', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'Parameters', @level2type = N'COLUMN', @level2name = N'NewAppointmentsStatus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Duracion de las recervas', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'Parameters', @level2type = N'COLUMN', @level2name = N'ReservationExpiryMinutes';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Numeros de whatsapp asociados a los flujos de whatsapp especificos para el cliente (separar por coma)', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'Parameters', @level2type = N'COLUMN', @level2name = N'WhatsAppNumbers';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'urls especificas para el cliente (separar por coma)', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'Parameters', @level2type = N'COLUMN', @level2name = N'WebAppointmentDomains';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Url a una hoja de estilos que se cargará en el sitio, util para agregar fuentes o personalizacion exclusiva por cliente', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'Parameters', @level2type = N'COLUMN', @level2name = N'StylesheetUrl';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista en formato JSON con los enlaces que se mostraran en el header del portal web', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'Parameters', @level2type = N'COLUMN', @level2name = N'HeaderLinks';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Url del video tutorial que se muestra en el portal web', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'Parameters', @level2type = N'COLUMN', @level2name = N'TutorialLinkUrl';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Mensaje que se muestra en el formulario "Agendar Cita" de la pagina principal', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'Parameters', @level2type = N'COLUMN', @level2name = N'HomeMainFormMessage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Mensaje que se muestra en los formularios de autenticacion', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'Parameters', @level2type = N'COLUMN', @level2name = N'AuthFormMessage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número máximo de citas activas que un paciente puede tener para la misma combinación de actividad y especialidad. Un valor negativo o cero indica que no hay límite.', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'Parameters', @level2type = N'COLUMN', @level2name = N'MaxActiveAppointmentsPerActivitySpecialty';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Numero(s) de telefono(s) para agendamiento por call center', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'Parameters', @level2type = N'COLUMN', @level2name = N'AgentPhoneNumbers';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'JSON de la politica de provacidad sigue este formato: {type: link | modal, modalContent?: string, link?: string}', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'Parameters', @level2type = N'COLUMN', @level2name = N'PrivacyPolicy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Edad mínima en meses que debe tener un paciente para poder agendar una cita de consulta externa', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'Parameters', @level2type = N'COLUMN', @level2name = N'MaxAgeForOutpatientBooking';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Mensaje que se le mostrará si MaxAgeForOutpatientBooking está establecido y no cumple con la edad', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'Parameters', @level2type = N'COLUMN', @level2name = N'MaxAgeForOutpatientBookingMessage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de citas activias que puede tener un paciente por actividad', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'Parameters', @level2type = N'COLUMN', @level2name = N'MaxActiveAppointmentsPerActivity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de citas activias que puede tener un paciente por especialidad', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'Parameters', @level2type = N'COLUMN', @level2name = N'MaxActiveAppointmentsPerSpecialty';

