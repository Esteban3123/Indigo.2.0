CREATE TABLE [Payroll].[VacationDiscretionaryDay] (
    [Id]                 INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdVacation]         INT          NOT NULL,
    [IdEmployee]         INT          NOT NULL,
    [MarkedDate]         DATE         NOT NULL,
    [State]              TINYINT      CONSTRAINT [DF_VacationDiscretionaryDay_State] DEFAULT ((1)) NOT NULL,
    [CreationUser]       VARCHAR (20) NULL,
    [CreationDate]       DATETIME     NULL,
    [CancellationUser]   VARCHAR (20) NULL,
    [CancellationDate]   DATETIME     NULL,
    CONSTRAINT [PK_VacationDiscretionaryDay] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_VacationDiscretionaryDay_Vacation] FOREIGN KEY ([IdVacation]) REFERENCES [Payroll].[Vacation] ([Id]),
    CONSTRAINT [FK_VacationDiscretionaryDay_Employee] FOREIGN KEY ([IdEmployee]) REFERENCES [Payroll].[Employee] ([Id]),
    CONSTRAINT [UQ_VacationDiscretionaryDay_Employee_MarkedDate] UNIQUE ([IdEmployee], [MarkedDate])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registra cada día individual marcado discrecionalmente para liquidación de vacaciones tipo Disfrutar (calendario no continuo), uno por fila. State: 1=Marcado/Activo, 2=Cancelado. La restricción única (IdEmployee, MarkedDate) evita que un mismo colaborador marque el mismo día en más de una solicitud dentro del mismo periodo.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VacationDiscretionaryDay';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de la fila (INT IDENTITY, clave primaria).', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VacationDiscretionaryDay', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la solicitud de vacaciones a la que pertenece este día marcado. Referencia a Payroll.Vacation (INT FK).', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VacationDiscretionaryDay', @level2type = N'COLUMN', @level2name = N'IdVacation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del colaborador al que pertenece el día marcado. Referencia a Payroll.Employee (INT FK). Denormalizado para agilizar la validación de días ya marcados por empleado/mes sin recorrer todas las solicitudes.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VacationDiscretionaryDay', @level2type = N'COLUMN', @level2name = N'IdEmployee';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha específica del día marcado como vacación (DATE, un registro por día).', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VacationDiscretionaryDay', @level2type = N'COLUMN', @level2name = N'MarkedDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del día marcado. Tipo: TINYINT. Valores: 1=Marcado/Activo, 2=Cancelado. Un día cancelado se excluye de la liquidación de nómina pero conserva el registro histórico (no se borra físicamente).', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VacationDiscretionaryDay', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que registró la marcación del día.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VacationDiscretionaryDay', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que se registró la marcación del día.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VacationDiscretionaryDay', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que canceló el día marcado (Opción A: cancelación a nivel de día individual), si aplica.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VacationDiscretionaryDay', @level2type = N'COLUMN', @level2name = N'CancellationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que se canceló el día marcado, si aplica.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VacationDiscretionaryDay', @level2type = N'COLUMN', @level2name = N'CancellationDate';
