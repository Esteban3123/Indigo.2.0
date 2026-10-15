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
    CONSTRAINT [PK_Vacation__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Vacation_VacationPeriod] FOREIGN KEY ([VacationPeriodId]) REFERENCES [Payroll].[VacationPeriod] ([Id])
);


GO
ALTER TABLE [Payroll].[Vacation] NOCHECK CONSTRAINT [FK_Vacation_VacationPeriod];


GO
CREATE NONCLUSTERED INDEX [IX_Vacation__VacationPeriodId]
    ON [Payroll].[Vacation]([VacationPeriodId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de Confirmación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario de Confirmación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de modificación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que modifico', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de creación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario de creación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la fecha de incorporacion de vacaciones del empleado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'IncorporationDateVacationReal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la fecha fin real de vacaciones, este campo solo se muestra si el tipo de vacaciones es a disfrutar y si en la tabla Payroll.PayrollSettings el campo HolidayWithoutAnticipateIBC esta en 1 de lo contrario va null    Nota: este campo el usuario no lo puede digilenciar ya que se calcula dependiendo de los dias solicitados y la fecha inicial real', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'VacationEndDateReal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la fecha inicio real de vacaciones, este campo solo se solicita si el tipo de vacaciones es a disfrutar y si en la tabla Payroll.PayrollSettings el campo HolidayWithoutAnticipateIBC esta en 1 de lo contrario va null', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'VacationStartDateReal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica los dias pendientes que se deben por aplazamiento, este campo lo utilizamos para no pagarle dos veces al empleado cuando se le realiza un ingreso forzoso por Aplazamiento, Este campo dismunye cada vez que se realiza un solicitud de vacaciones hasta que este campo llega a cero', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'DaysDeferredPending';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado Cuando se reincorpora a la fuerza   1 - Normal   2 - Esperando Ajuste Incorporacion   3 - Incorporacion Ajustada    Estos estados son cuando una persona esta en vacaciones y le hacen una incorporacion forzosa , el estado dos es que esta esperando a que la liquidacion de nomina haga el ajuste correspondiente y despues de que haga el ajuste cambia al estado 3', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'StateIncorporation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Resolución de Ingreso Forzoso', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'ForceEntryResolutionDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número de Resolución de Ingreso Forzoso', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'ForceEntryResolutionNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Resolución', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'ResolutionDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Resolución', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'ResolutionNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de incorporacion real a la empresa', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'IncorporationDateReal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dias Pedidos realmente por el empleado este campo cambia cuando se hace una incorporacion forzosa', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'TakenDaysReal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado de la vacion 1 - Esperando Pago 2- Pagadas 3 - Aplazadas', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Incremento Vacacional', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'VacationalIncreaseValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Prima de Vacaciones', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'IncentivePaymentVacationValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de Bonificación para Recreación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'VacationBonificationValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor vacaciones netos = vacaciones - aportes', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'VacationValueNet';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Aporte Fondo de Solidaridad', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'SolidarityFundValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor aporte pension', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'PensionContribution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Aporte a salud', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'HealthContribution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de la vacacion', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'VacationValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Base Liquidacion', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'BaseLiquidation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dias no trabajados del empleado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'NotWorkedDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dias trabajados del empleado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'WorkedDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de incorporacion a la compañia', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'IncorporationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dias Disfrutados por empleado contando festivos y domingos', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'EnjoyDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dias tomados por el empleado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'TakenDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de liquidacion de la nomina si elige la opcion proxima nomina', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'LiquidationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de pago 1 - Inmedito 2 - Proxima Nomina', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'TypePayment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de vacaciones   1 - Liquidar   2 - Disfrutar   3- Permiso con cargo a vacaciones   4 - Vacaciones Compensadas  5 - Interrumpidas', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'TypeVacation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de liquidacion 1 - Promedio 2 - Sueldo Basico', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'TypeLiquidation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Final de la vacacion', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'VacationEndDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha inicial de la vacacion', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'VacationStartDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID del período de vacaciones', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'VacationPeriodId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla de vaciones', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Vacation', @level2type = N'COLUMN', @level2name = N'Id';

