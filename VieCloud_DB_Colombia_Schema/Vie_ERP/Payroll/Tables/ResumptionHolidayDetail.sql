CREATE TABLE [Payroll].[ResumptionHolidayDetail] (
    [Id]                  INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ResumptionHolidayId] INT NOT NULL,
    [VacationId]          INT NOT NULL,
    [Days]                INT NOT NULL,
    CONSTRAINT [PK_ResumptionHolidayDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ResumptionHolidayDetail_ResumptionHoliday] FOREIGN KEY ([ResumptionHolidayId]) REFERENCES [Payroll].[ResumptionHoliday] ([Id]),
    CONSTRAINT [FK_ResumptionHolidayDetail_Vacation] FOREIGN KEY ([VacationId]) REFERENCES [Payroll].[Vacation] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de días a renovar o restaurar del período de vacaciones original aplazado. Tipo: INT. Representa la porción de días de la solicitud de vacaciones que se reactiva en la reanudación.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ResumptionHolidayDetail', @level2type = N'COLUMN', @level2name = N'Days';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la Cantidad de dias que se va a renovar de el periodo de vacaciones', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ResumptionHolidayDetail', @level2type = N'COLUMN', @level2name = N'Days';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ResumptionHolidayDetail', @level2type = N'COLUMN', @level2name = N'Days';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) de la solicitud de vacaciones original que fue aplazada o postergada. Referencia a [Payroll].[Vacation]. Permite vincular el detalle con la solicitud de vacaciones madre que se reanuda.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ResumptionHolidayDetail', @level2type = N'COLUMN', @level2name = N'VacationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la solicitud de vacaciones original que se aplazo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ResumptionHolidayDetail', @level2type = N'COLUMN', @level2name = N'VacationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ResumptionHolidayDetail', @level2type = N'COLUMN', @level2name = N'VacationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) de la reanudación o reactivación de vacaciones. Referencia a [Payroll].[ResumptionHoliday]. Vincula el detalle a la solicitud madre de reanudación de vacaciones diferidas.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ResumptionHolidayDetail', @level2type = N'COLUMN', @level2name = N'ResumptionHolidayId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la reanudacion de vacaciones', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ResumptionHolidayDetail', @level2type = N'COLUMN', @level2name = N'ResumptionHolidayId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ResumptionHolidayDetail', @level2type = N'COLUMN', @level2name = N'ResumptionHolidayId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico (IDENTITY) de la tabla. Clave primaria. Tipo: INT. Generado automáticamente sin replicación (NOT FOR REPLICATION).', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ResumptionHolidayDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ResumptionHolidayDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ResumptionHolidayDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de reanudaciones de vacaciones en nómina: registra cada segmento de días de vacaciones que fueron interrumpidas y retomadas, vinculando el evento de reanudación con el período vacacional original y la cantidad de días involucrados.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ResumptionHolidayDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ResumptionHolidayDetail';
