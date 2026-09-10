CREATE TABLE [WebAppointment].[HostessUser] (
    [IDHostessUser]        INT          IDENTITY (1, 1) NOT NULL,
    [IDSystemUser]         INT          NOT NULL,
    [DocumentType]         VARCHAR (2)  NOT NULL,
    [IdentificationNumber] VARCHAR (17) NOT NULL,
    [Hostename]            VARCHAR (50) NOT NULL,
    [Cellphone]            VARCHAR (10) NOT NULL,
    [Relationship]         VARCHAR (2)  NOT NULL,
    CONSTRAINT [PK_HostessUser] PRIMARY KEY CLUSTERED ([IDHostessUser] ASC),
    CONSTRAINT [FK__HostessUs__IDSys__0C1BC9F9] FOREIGN KEY ([IDSystemUser]) REFERENCES [WebAppointment].[SystemUser] ([IDUsername])
);




GO


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de acompañantes o responsables (hostess) asociados a usuarios del sistema de agendamiento web. Guarda los datos de contacto y parentesco de la persona que acompaña o es responsable del paciente.', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'HostessUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'HostessUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único interno del acompañante o responsable registrado.', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'HostessUser', @level2type = N'COLUMN', @level2name = N'IDHostessUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'HostessUser', @level2type = N'COLUMN', @level2name = N'IDHostessUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia al usuario del sistema web al que está vinculado este acompañante o responsable.', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'HostessUser', @level2type = N'COLUMN', @level2name = N'IDSystemUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'HostessUser', @level2type = N'COLUMN', @level2name = N'IDSystemUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de documento de identidad del acompañante (cédula, pasaporte, tarjeta de identidad, etc.).', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'HostessUser', @level2type = N'COLUMN', @level2name = N'DocumentType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'HostessUser', @level2type = N'COLUMN', @level2name = N'DocumentType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de documento, cédula o identificación del acompañante o responsable del paciente.', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'HostessUser', @level2type = N'COLUMN', @level2name = N'IdentificationNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'HostessUser', @level2type = N'COLUMN', @level2name = N'IdentificationNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre completo del acompañante, responsable o acudiente del paciente.', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'HostessUser', @level2type = N'COLUMN', @level2name = N'Hostename';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'HostessUser', @level2type = N'COLUMN', @level2name = N'Hostename';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de celular o teléfono móvil de contacto del acompañante.', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'HostessUser', @level2type = N'COLUMN', @level2name = N'Cellphone';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'HostessUser', @level2type = N'COLUMN', @level2name = N'Cellphone';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Parentesco o relación del acompañante con el paciente (familiar, tutor, cuidador, etc.).', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'HostessUser', @level2type = N'COLUMN', @level2name = N'Relationship';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'HostessUser', @level2type = N'COLUMN', @level2name = N'Relationship';
