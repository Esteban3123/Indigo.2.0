CREATE TABLE [HumanTalent].[AdditionalInformation] (
    [Id]                     INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CandidateId]            INT          NULL,
    [EmployeeId]             INT          NULL,
    [AvailabilityTravelId]   INT          NULL,
    [AvailabilityTransferId] INT          NULL,
    [DriverLicense]          VARCHAR (50) NULL,
    [HowVacany]              VARCHAR (50) NULL,
    [DataAuthorizationId]    INT          NOT NULL,
    [CreationUser]           VARCHAR (20) NOT NULL,
    [CreationDate]           DATETIME     NOT NULL,
    [ModificationUser]       VARCHAR (20) NULL,
    [ModificationDate]       DATETIME     NULL,
    CONSTRAINT [PK__Addition__3214EC0772362190] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [fk_CandidateIdAI] FOREIGN KEY ([CandidateId]) REFERENCES [HumanTalent].[Candidate] ([Id])
);




GO


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Información adicional de candidatos y empleados en el módulo de Talento Humano, incluyendo disponibilidad para viajes o traslados, licencia de conducción, cómo se enteró de la vacante y autorización de tratamiento de datos personales.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AdditionalInformation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AdditionalInformation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único interno del registro de información adicional.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AdditionalInformation', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AdditionalInformation', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del candidato al que pertenece esta información, vinculado al proceso de selección.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AdditionalInformation', @level2type = N'COLUMN', @level2name = N'CandidateId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AdditionalInformation', @level2type = N'COLUMN', @level2name = N'CandidateId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica la disponibilidad del candidato o empleado para viajar (referencia a tabla maestra de disponibilidad de viaje).', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AdditionalInformation', @level2type = N'COLUMN', @level2name = N'AvailabilityTravelId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AdditionalInformation', @level2type = N'COLUMN', @level2name = N'AvailabilityTravelId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica la disponibilidad del candidato o empleado para ser trasladado a otra ciudad o sede.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AdditionalInformation', @level2type = N'COLUMN', @level2name = N'AvailabilityTransferId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AdditionalInformation', @level2type = N'COLUMN', @level2name = N'AvailabilityTransferId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Categoría o número de licencia de conducción del candidato o empleado.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AdditionalInformation', @level2type = N'COLUMN', @level2name = N'DriverLicense';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AdditionalInformation', @level2type = N'COLUMN', @level2name = N'DriverLicense';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica cómo se enteró el candidato de la vacante (ej: bolsa de empleo, referido, redes sociales).', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AdditionalInformation', @level2type = N'COLUMN', @level2name = N'HowVacany';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AdditionalInformation', @level2type = N'COLUMN', @level2name = N'HowVacany';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia a la autorización de tratamiento de datos personales otorgada por el candidato o empleado (habeas data).', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AdditionalInformation', @level2type = N'COLUMN', @level2name = N'DataAuthorizationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AdditionalInformation', @level2type = N'COLUMN', @level2name = N'DataAuthorizationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario del sistema que creó el registro.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AdditionalInformation', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AdditionalInformation', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que se creó el registro.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AdditionalInformation', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AdditionalInformation', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario del sistema que realizó la última modificación del registro.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AdditionalInformation', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AdditionalInformation', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de la última modificación del registro.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AdditionalInformation', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AdditionalInformation', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del empleado al que pertenece esta información, cuando el registro corresponde a un empleado activo.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AdditionalInformation', @level2type = N'COLUMN', @level2name = N'EmployeeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AdditionalInformation', @level2type = N'COLUMN', @level2name = N'EmployeeId';
