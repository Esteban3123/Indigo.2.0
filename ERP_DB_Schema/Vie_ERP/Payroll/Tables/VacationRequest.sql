CREATE TABLE [Payroll].[VacationRequest] (
    [Id]                 INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [EmployeeId]         INT           NOT NULL,
    [VacationType]       TINYINT       NOT NULL,
    [Days]               INT           NOT NULL,
    [InitialDate]        DATE          NOT NULL,
    [EndDate]            DATE          NOT NULL,
    [IncorporationDate]  DATE          NOT NULL,
    [EnjoyDay]           INT           NOT NULL,
    [Status]             TINYINT       NOT NULL,
    [CancellationReason] VARCHAR (300) NOT NULL,
    [CreationUser]       VARCHAR (20)  NOT NULL,
    [CreationDate]       DATETIME      NOT NULL,
    [ModificationUser]   VARCHAR (20)  NULL,
    [ModificationDate]   DATETIME      NULL,
    [ApprovalUser]       VARCHAR (20)  NULL,
    [ApprovalDate]       DATETIME      NULL,
    [CancellationUser]   VARCHAR (20)  NULL,
    [CancellationDate]   DATETIME      NULL,
    CONSTRAINT [PK_VacationRequest] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_VacationRequest_Employee] FOREIGN KEY ([EmployeeId]) REFERENCES [Payroll].[Employee] ([Id])
);


GO
ALTER TABLE [Payroll].[VacationRequest] NOCHECK CONSTRAINT [FK_VacationRequest_Employee];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Cancelacion', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VacationRequest', @level2type = N'COLUMN', @level2name = N'CancellationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Cancelacion', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VacationRequest', @level2type = N'COLUMN', @level2name = N'CancellationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Aprovacion', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VacationRequest', @level2type = N'COLUMN', @level2name = N'ApprovalDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Aprobacion', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VacationRequest', @level2type = N'COLUMN', @level2name = N'ApprovalUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificacion', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VacationRequest', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificacion', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VacationRequest', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Creacion', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VacationRequest', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creacion', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VacationRequest', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Razon de cancelacion', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VacationRequest', @level2type = N'COLUMN', @level2name = N'CancellationReason';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado de la solicitud  1 - Registrada  2 - Aprobada  3 - Cancelada', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VacationRequest', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dias a disfrutar del empleado, Calculado por el sistema, es la cantidad de dias que va estar por fuera de sus vacaciones incluyendo festivos', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VacationRequest', @level2type = N'COLUMN', @level2name = N'EnjoyDay';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de incorporacion del empleado, Calculado por el sistema', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VacationRequest', @level2type = N'COLUMN', @level2name = N'IncorporationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Final de vacaciones, calculador por el sistema', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VacationRequest', @level2type = N'COLUMN', @level2name = N'EndDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Inicial de vacaciones', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VacationRequest', @level2type = N'COLUMN', @level2name = N'InitialDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dias de solicitud de vacaciones', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VacationRequest', @level2type = N'COLUMN', @level2name = N'Days';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de vacaciones  1 - Liquidar  2 - Disfrutar', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VacationRequest', @level2type = N'COLUMN', @level2name = N'VacationType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del empleado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VacationRequest', @level2type = N'COLUMN', @level2name = N'EmployeeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la solicitud de vacaciones', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VacationRequest', @level2type = N'COLUMN', @level2name = N'Id';

