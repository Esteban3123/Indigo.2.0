CREATE TABLE [Payroll].[Vacation] (
    [Id]                            INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [VacationPeriodId]              INT             NOT NULL,
    [VacationStartDate]             DATE            NOT NULL,
    [VacationEndDate]               DATE            NOT NULL,
    [TypeLiquidation]               TINYINT         NOT NULL,
    [TypeVacation]                  TINYINT         NOT NULL,
    [TypePayment]                   TINYINT         NOT NULL,
    [LiquidationDate]               DATE            NULL,
    [TakenDays]                     TINYINT         NOT NULL,
    [EnjoyDays]                     TINYINT         NOT NULL,
    [IncorporationDate]             DATE            NOT NULL,
    [WorkedDays]                    INT             NOT NULL,
    [NotWorkedDays]                 INT             NOT NULL,
    [BaseLiquidation]               NUMERIC (18, 2) NOT NULL,
    [VacationValue]                 NUMERIC (18, 2) NOT NULL,
    [HealthContribution]            NUMERIC (18, 2) NOT NULL,
    [PensionContribution]           NUMERIC (18, 2) NOT NULL,
    [SolidarityFundValue]           NUMERIC (18)    NULL,
    [VacationValueNet]              NUMERIC (18, 2) NOT NULL,
    [VacationBonificationValue]     NUMERIC (18, 2) CONSTRAINT [DF_Vacation_VacationBonificationValue] DEFAULT ((0)) NULL,
    [IncentivePaymentVacationValue] NUMERIC (18, 2) CONSTRAINT [DF_Vacation_IncentivePaymentVacationValue] DEFAULT ((0)) NULL,
    [VacationalIncreaseValue]       NUMERIC (18, 2) CONSTRAINT [DF_Vacation_VacationalIncreaseValue] DEFAULT ((0)) NULL,
    [State]                         TINYINT         NOT NULL,
    [TakenDaysReal]                 INT             NOT NULL,
    [IncorporationDateReal]         DATE            NOT NULL,
    [ResolutionNumber]              VARCHAR (50)    NULL,
    [ResolutionDate]                DATE            NULL,
    [ForceEntryResolutionNumber]    VARCHAR (50)    NULL,
    [ForceEntryResolutionDate]      DATE            NULL,
    [StateIncorporation]            TINYINT         NOT NULL,
    [DaysDeferredPending]           INT             CONSTRAINT [DF_Vacation_DaysDeferredPending] DEFAULT ((0)) NOT NULL,
    [VacationStartDateReal]         DATE            NULL,
    [VacationEndDateReal]           DATE            NULL,
    [IncorporationDateVacationReal] DATE            NULL,
    [CreationUser]                  VARCHAR (20)    NULL,
    [CreationDate]                  DATETIME        NULL,
    [ModificationUser]              VARCHAR (20)    NULL,
    [ModificationDate]              DATETIME        NULL,
    [ConfirmationUser]              VARCHAR (20)    NULL,
    [ConfirmationDate]              DATETIME        NULL,
    [PreviousMonthIBC]              NUMERIC (18)    NULL,
    CONSTRAINT [PK_Vacation__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Vacation_VacationPeriod] FOREIGN KEY ([VacationPeriodId]) REFERENCES [Payroll].[VacationPeriod] ([Id])
);


GO
ALTER TABLE [Payroll].[Vacation] NOCHECK CONSTRAINT [FK_Vacation_VacationPeriod];


GO
CREATE NONCLUSTERED INDEX [IX_Vacation__VacationPeriodId]
    ON [Payroll].[Vacation]([VacationPeriodId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de confirmación del registro de vacaciones por usuario autorizado (DATETIME, auditoría)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Confirmación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que confirmó el registro de vacaciones, validación de autorización (VARCHAR 20, auditoría PII)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de Confirmación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de última modificación del registro de vacaciones (DATETIME, auditoría)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de modificación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que realizó la última modificación del registro de vacaciones (VARCHAR 20, auditoría PII)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario que modifico', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación inicial del registro de vacaciones en el sistema (DATETIME, auditoría)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que creó el registro de vacaciones en el sistema (VARCHAR 20, auditoría PII)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de creación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha real de reincorporación del empleado después de disfrutar vacaciones, se calcula según días efectivos (DATE)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'IncorporationDateVacationReal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la fecha de incorporacion de vacaciones del empleado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'IncorporationDateVacationReal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'IncorporationDateVacationReal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha fin real de vacaciones del empleado, solo aplica si tipo es ''''Disfrutar'''' y HolidayWithoutAnticipateIBC=1, calculada automáticamente, no editable (DATE, calculado)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'VacationEndDateReal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la fecha fin real de vacaciones, este campo solo se muestra si el tipo de vacaciones es a disfrutar y si en la tabla Payroll.PayrollSettings el campo HolidayWithoutAnticipateIBC esta en 1 de lo contrario va null    Nota: este campo el usuario no lo puede digilenciar ya que se calcula dependiendo de los dias solicitados y la fecha inicial real', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'VacationEndDateReal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'VacationEndDateReal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha inicio real de vacaciones del empleado, solo se requiere si tipo es ''''Disfrutar'''' y HolidayWithoutAnticipateIBC=1, registrada por usuario (DATE)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'VacationStartDateReal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la fecha inicio real de vacaciones, este campo solo se solicita si el tipo de vacaciones es a disfrutar y si en la tabla Payroll.PayrollSettings el campo HolidayWithoutAnticipateIBC esta en 1 de lo contrario va null', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'VacationStartDateReal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'VacationStartDateReal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Días pendientes por pagar por aplazamiento de vacaciones, decrece en cada solicitud nueva hasta cero, evita doble pago en incorporación forzosa (INT, cálculo)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'DaysDeferredPending';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica los dias pendientes que se deben por aplazamiento, este campo lo utilizamos para no pagarle dos veces al empleado cuando se le realiza un ingreso forzoso por Aplazamiento, Este campo dismunye cada vez que se realiza un solicitud de vacaciones hasta que este campo llega a cero', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'DaysDeferredPending';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'DaysDeferredPending';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de reincorporación forzosa: 1=Normal, 2=Esperando ajuste liquidación, 3=Incorporación ajustada; cambios automáticos en nómina (TINYINT, enum)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'StateIncorporation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado Cuando se reincorpora a la fuerza   1 - Normal   2 - Esperando Ajuste Incorporacion   3 - Incorporacion Ajustada    Estos estados son cuando una persona esta en vacaciones y le hacen una incorporacion forzosa , el estado dos es que esta esperando a que la liquidacion de nomina haga el ajuste correspondiente y despues de que haga el ajuste cambia al estado 3', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'StateIncorporation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'StateIncorporation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de resolución administrativa para incorporación forzosa del empleado durante vacaciones (DATE, PII administrativa)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'ForceEntryResolutionDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Resolución de Ingreso Forzoso', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'ForceEntryResolutionDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'ForceEntryResolutionDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de resolución que autoriza incorporación forzosa del empleado en período de vacaciones (VARCHAR 50, PII administrativa)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'ForceEntryResolutionNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de Resolución de Ingreso Forzoso', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'ForceEntryResolutionNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'ForceEntryResolutionNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de resolución de aprobación o trámite del registro de vacaciones (DATE, PII administrativa)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'ResolutionDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Resolución', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'ResolutionDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'ResolutionDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de resolución que autoriza o documenta el registro de vacaciones del empleado (VARCHAR 50, PII administrativa)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'ResolutionNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Resolución', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'ResolutionNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'ResolutionNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha real en que el empleado se reincorpora a labores en la empresa después de vacaciones (DATE)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'IncorporationDateReal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de incorporacion real a la empresa', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'IncorporationDateReal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'IncorporationDateReal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Días de vacaciones realmente solicitados por empleado, se actualiza si hay incorporación forzosa (INT)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'TakenDaysReal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dias Pedidos realmente por el empleado este campo cambia cuando se hace una incorporacion forzosa', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'TakenDaysReal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'TakenDaysReal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del registro de vacaciones: 1=Esperando pago, 2=Pagadas, 3=Aplazadas (TINYINT, enum)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado de la vacion 1 - Esperando Pago 2- Pagadas 3 - Aplazadas', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor en pesos del incremento vacacional adicional al salario base (NUMERIC 18,2, monto)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'VacationalIncreaseValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Incremento Vacacional', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'VacationalIncreaseValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'VacationalIncreaseValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Prima de vacaciones, incentivo salarial adicional por período vacacional (NUMERIC 18,2, monto)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'IncentivePaymentVacationValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Prima de Vacaciones', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'IncentivePaymentVacationValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'IncentivePaymentVacationValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor en pesos de bonificación para recreación, beneficio asociado a vacaciones (NUMERIC 18,2, monto)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'VacationBonificationValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de Bonificación para Recreación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'VacationBonificationValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'VacationBonificationValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor neto de vacaciones en pesos = VacationValue menos aportes a salud y pensión (NUMERIC 18,2, monto neto)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'VacationValueNet';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor vacaciones netos = vacaciones - aportes', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'VacationValueNet';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'VacationValueNet';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Aporte en pesos al fondo de solidaridad pensional, descuento sobre valor de vacaciones (NUMERIC 18, monto)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'SolidarityFundValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Aporte Fondo de Solidaridad', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'SolidarityFundValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'SolidarityFundValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Aporte en pesos a fondos de pensión descontado del valor de vacaciones (NUMERIC 18,2, monto)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'PensionContribution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor aporte pension', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'PensionContribution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'PensionContribution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Aporte en pesos a EPS/salud descontado del valor de vacaciones (NUMERIC 18,2, monto)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'HealthContribution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Aporte a salud', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'HealthContribution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'HealthContribution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor total en pesos de las vacaciones antes de aportes y descuentos (NUMERIC 18,2, monto bruto)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'VacationValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de la vacacion', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'VacationValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'VacationValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor base en pesos para liquidación de vacaciones, según tipo promedio o sueldo básico (NUMERIC 18,2, monto)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'BaseLiquidation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Base Liquidacion', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'BaseLiquidation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'BaseLiquidation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de días no trabajados en el período por ausencias, incapacidades o licencias (INT, contador)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'NotWorkedDays';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dias no trabajados del empleado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'NotWorkedDays';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'NotWorkedDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de días efectivamente trabajados en el período de cálculo de vacaciones (INT, contador)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'WorkedDays';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dias trabajados del empleado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'WorkedDays';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'WorkedDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha en que el empleado se incorpora a la empresa, fecha base para acumulación de vacaciones (DATE)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'IncorporationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de incorporacion a la compañia', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'IncorporationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'IncorporationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Días disfrutados realmente por empleado incluidos festivos y domingos en período de vacaciones (TINYINT, contador)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'EnjoyDays';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dias Disfrutados por empleado contando festivos y domingos', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'EnjoyDays';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'EnjoyDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Días de vacaciones solicitados por el empleado en esta solicitud (TINYINT, contador)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'TakenDays';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dias tomados por el empleado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'TakenDays';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'TakenDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de liquidación en nómina si empleado elige opción ''''Próxima Nómina'''', null si es inmediato (DATE)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'LiquidationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de liquidacion de la nomina si elige la opcion proxima nomina', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'LiquidationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'LiquidationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de pago de vacaciones: 1=Inmediato, 2=Próxima nómina (TINYINT, enum)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'TypePayment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de pago 1 - Inmedito 2 - Proxima Nomina', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'TypePayment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'TypePayment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de vacaciones solicitadas: 1=Liquidar, 2=Disfrutar, 3=Permiso con cargo, 4=Compensadas, 5=Interrumpidas (TINYINT, enum)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'TypeVacation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de vacaciones   1 - Liquidar   2 - Disfrutar   3- Permiso con cargo a vacaciones   4 - Vacaciones Compensadas  5 - Interrumpidas', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'TypeVacation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'TypeVacation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de cálculo para liquidación: 1=Promedio de últimos 3 meses, 2=Sueldo básico (TINYINT, enum)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'TypeLiquidation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de liquidacion 1 - Promedio 2 - Sueldo Basico', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'TypeLiquidation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'TypeLiquidation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha final programada de las vacaciones del empleado (DATE)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'VacationEndDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Final de la vacacion', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'VacationEndDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'VacationEndDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha inicial programada de las vacaciones del empleado (DATE)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'VacationStartDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha inicial de la vacacion', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'VacationStartDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'VacationStartDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del período de vacaciones, FK a [Payroll].[VacationPeriod] para ciclo anual o específico (INT, FK)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'VacationPeriodId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID del período de vacaciones', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'VacationPeriodId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'VacationPeriodId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro de vacaciones en la tabla, clave primaria (INT IDENTITY, PK)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla de vaciones', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Liquidaciones de vacaciones del personal: registra cada período vacacional de un empleado con sus fechas de disfrute, tipo de liquidación y pago, días tomados y trabajados, valores calculados (base, neto, bonificación, incremento, incentivo), aportes a salud y pensión, fechas reales de incorporación, resoluciones de aprobación y estado del proceso.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'IBC del mes anterior usado para cálculo de salud/pensión en vacaciones (C42/C43/C44 del archivo PILA). Se toma de PeriodJCB de la última liquidación confirmada del mes previo. Para salario integral ya incluye el factor 0.7. NUMERIC(18,0) NULL.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'PreviousMonthIBC';
