CREATE TABLE [HumanTalent].[HumanTalentParameter] (
    [Id]                             INT     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [VacationNumberDaysRequest]      TINYINT NOT NULL,
    [VacationNumberDaysCancellation] TINYINT NOT NULL,
    [TypeCalculationPreviousDays]    TINYINT NOT NULL,
    [MinimunVacationRequestDays]     TINYINT NOT NULL,
    CONSTRAINT [PK_HumanTalentParameter] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número mínimo de días requeridos para solicitar vacaciones. Parámetro de configuración de política de ausencias, licencias o permisos del personal.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'HumanTalentParameter', @level2type = N'COLUMN', @level2name = N'MinimunVacationRequestDays';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero Mínimo de Dias a solicitar', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'HumanTalentParameter', @level2type = N'COLUMN', @level2name = N'MinimunVacationRequestDays';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'HumanTalentParameter', @level2type = N'COLUMN', @level2name = N'MinimunVacationRequestDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipología o método de cálculo para días previos (anticipación requerida). Define regla de negocio: calendario, días hábiles u otro criterio de antigüedad o planificación.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'HumanTalentParameter', @level2type = N'COLUMN', @level2name = N'TypeCalculationPreviousDays';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipología para cálculo de dias previos', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'HumanTalentParameter', @level2type = N'COLUMN', @level2name = N'TypeCalculationPreviousDays';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'HumanTalentParameter', @level2type = N'COLUMN', @level2name = N'TypeCalculationPreviousDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de días previos mínimos requeridos para cancelar o revocar una solicitud de vacaciones. Parámetro de política de ausencias y gestión de talento humano.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'HumanTalentParameter', @level2type = N'COLUMN', @level2name = N'VacationNumberDaysCancellation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de Días previos para Cancelación de Vacaciones', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'HumanTalentParameter', @level2type = N'COLUMN', @level2name = N'VacationNumberDaysCancellation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'HumanTalentParameter', @level2type = N'COLUMN', @level2name = N'VacationNumberDaysCancellation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de días previos mínimos requeridos para solicitar vacaciones. Parámetro de anticipación o preaviso obligatorio en política de ausencias y licencias.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'HumanTalentParameter', @level2type = N'COLUMN', @level2name = N'VacationNumberDaysRequest';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de Días previos para solicitud de Vacaciones', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'HumanTalentParameter', @level2type = N'COLUMN', @level2name = N'VacationNumberDaysRequest';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'HumanTalentParameter', @level2type = N'COLUMN', @level2name = N'VacationNumberDaysRequest';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autonumérico (INT IDENTITY) y clave primaria de configuración de parámetros de gestión de talento humano.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'HumanTalentParameter', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'HumanTalentParameter', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'HumanTalentParameter', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Parámetros de configuración del módulo de Talento Humano para la gestión de vacaciones: define la cantidad de días de anticipación requeridos para solicitar o cancelar vacaciones, el método de cálculo de días previos y el mínimo de días permitidos por solicitud.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'HumanTalentParameter';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'HumanTalentParameter';
