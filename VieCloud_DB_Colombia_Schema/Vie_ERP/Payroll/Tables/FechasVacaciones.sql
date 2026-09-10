CREATE TABLE [Payroll].[FechasVacaciones] (
    [NumeroIdentificacion] VARCHAR (20) NULL,
    [FechaPeriodo]         DATE         NULL,
    [DiasPendientes]       VARCHAR (20) NULL
);
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Registra los días de vacaciones pendientes por empleado, identificado por su número de identificación, asociados a un período específico. Forma parte del módulo de nómina (schema Payroll) y permite llevar el control del saldo de días vacacionales adeudados a cada trabajador en un corte de tiempo determinado.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'TABLE', @level1name=N'FechasVacaciones';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'TABLE', @level1name=N'FechasVacaciones';
GO
