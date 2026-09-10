CREATE TABLE [HumanTalent].[VacationRequestDetail] (
    [Id]                INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdVacationRequest] INT           NOT NULL,
    [StatusRequest]     TINYINT       NOT NULL,
    [UserStatus]        VARCHAR (20)  NOT NULL,
    [DateStatus]        DATETIME      NOT NULL,
    [Comments]          VARCHAR (500) NULL,
    CONSTRAINT [PK_VacationRequestDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_VacationRequestDetail_VacationRequest] FOREIGN KEY ([IdVacationRequest]) REFERENCES [HumanTalent].[VacationRequest] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Comentarios, notas o observaciones adicionales registradas durante la gestión de la solicitud de vacaciones (VARCHAR 500, campo opcional).', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'VacationRequestDetail', @level2type = N'COLUMN', @level2name = N'Comments';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Comentarios', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'VacationRequestDetail', @level2type = N'COLUMN', @level2name = N'Comments';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'VacationRequestDetail', @level2type = N'COLUMN', @level2name = N'Comments';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora del registro del cambio de estado, timestamp de cuándo se actualizó el estatus de la solicitud de vacaciones (DATETIME, auditoria).', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'VacationRequestDetail', @level2type = N'COLUMN', @level2name = N'DateStatus';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha del Registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'VacationRequestDetail', @level2type = N'COLUMN', @level2name = N'DateStatus';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'VacationRequestDetail', @level2type = N'COLUMN', @level2name = N'DateStatus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario, login o identificación del profesional de recursos humanos o administrador que realizó el cambio de estado (VARCHAR 20, trazabilidad).', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'VacationRequestDetail', @level2type = N'COLUMN', @level2name = N'UserStatus';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario que ha realizado el registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'VacationRequestDetail', @level2type = N'COLUMN', @level2name = N'UserStatus';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'VacationRequestDetail', @level2type = N'COLUMN', @level2name = N'UserStatus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado actual de la solicitud de vacaciones: 1=Esperando Aprobación, 2=Aprobado, 3=Rechazado (TINYINT, control de flujo de aprobación).', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'VacationRequestDetail', @level2type = N'COLUMN', @level2name = N'StatusRequest';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado de la Solicitud:  1. Esperando Aprobación  2. Aprobado  3. Rechazado', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'VacationRequestDetail', @level2type = N'COLUMN', @level2name = N'StatusRequest';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'VacationRequestDetail', @level2type = N'COLUMN', @level2name = N'StatusRequest';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de la solicitud de vacaciones padre, referencia a HumanTalent.VacationRequest (INT, FK, relación 1:N).', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'VacationRequestDetail', @level2type = N'COLUMN', @level2name = N'IdVacationRequest';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Vacation Request', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'VacationRequestDetail', @level2type = N'COLUMN', @level2name = N'IdVacationRequest';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'VacationRequestDetail', @level2type = N'COLUMN', @level2name = N'IdVacationRequest';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementado de cada registro de detalle o historial de cambio de estado en la solicitud de vacaciones (INT IDENTITY, clave primaria).', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'VacationRequestDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'VacationRequestDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'VacationRequestDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Historial de estados y seguimiento de las solicitudes de vacaciones del personal: registra cada cambio de estado (aprobado, rechazado, pendiente) con el usuario responsable, la fecha y los comentarios asociados a cada transición.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'VacationRequestDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'VacationRequestDetail';
