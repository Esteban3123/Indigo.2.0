CREATE TABLE [HumanTalent].[HumanTalentParameter] (
    [Id]                             INT     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [VacationNumberDaysRequest]      TINYINT NOT NULL,
    [VacationNumberDaysCancellation] TINYINT NOT NULL,
    [TypeCalculationPreviousDays]    TINYINT NOT NULL,
    [MinimunVacationRequestDays]     TINYINT NOT NULL,
    CONSTRAINT [PK_HumanTalentParameter] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero Mínimo de Dias a solicitar', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'HumanTalentParameter', @level2type = N'COLUMN', @level2name = N'MinimunVacationRequestDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipología para cálculo de dias previos', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'HumanTalentParameter', @level2type = N'COLUMN', @level2name = N'TypeCalculationPreviousDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número de Días previos para Cancelación de Vacaciones', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'HumanTalentParameter', @level2type = N'COLUMN', @level2name = N'VacationNumberDaysCancellation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número de Días previos para solicitud de Vacaciones', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'HumanTalentParameter', @level2type = N'COLUMN', @level2name = N'VacationNumberDaysRequest';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'HumanTalentParameter', @level2type = N'COLUMN', @level2name = N'Id';

