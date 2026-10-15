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
ALTER TABLE [Payroll].[ResumptionHolidayDetail] NOCHECK CONSTRAINT [FK_ResumptionHolidayDetail_ResumptionHoliday];


GO
ALTER TABLE [Payroll].[ResumptionHolidayDetail] NOCHECK CONSTRAINT [FK_ResumptionHolidayDetail_Vacation];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la Cantidad de dias que se va a renovar de el periodo de vacaciones', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ResumptionHolidayDetail', @level2type = N'COLUMN', @level2name = N'Days';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la solicitud de vacaciones original que se aplazo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ResumptionHolidayDetail', @level2type = N'COLUMN', @level2name = N'VacationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la reanudacion de vacaciones', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ResumptionHolidayDetail', @level2type = N'COLUMN', @level2name = N'ResumptionHolidayId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ResumptionHolidayDetail', @level2type = N'COLUMN', @level2name = N'Id';

