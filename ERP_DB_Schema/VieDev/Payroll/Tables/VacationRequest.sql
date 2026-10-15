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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado de la solicitud  1 - Registrada  2 - Aprobada  3 - Cancelada', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VacationRequest', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de vacaciones  1 - Liquidar  2 - Disfrutar', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VacationRequest', @level2type = N'COLUMN', @level2name = N'VacationType';

