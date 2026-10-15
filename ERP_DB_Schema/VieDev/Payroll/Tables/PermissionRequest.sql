CREATE TABLE [Payroll].[PermissionRequest] (
    [Id]                    INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [EmployeeId]            INT           NOT NULL,
    [PermissionReason]      TINYINT       NOT NULL,
    [PermissionReasonOther] VARCHAR (MAX) NULL,
    [PermissionDescription] VARCHAR (300) NOT NULL,
    [InitialDate]           DATETIME      NOT NULL,
    [EndDate]               DATETIME      NOT NULL,
    [PermissionType]        TINYINT       NOT NULL,
    [Status]                TINYINT       NOT NULL,
    [CreationUser]          VARCHAR (20)  NOT NULL,
    [CreationDate]          DATETIME      NOT NULL,
    [ModificationUser]      VARCHAR (20)  NULL,
    [ModificationDate]      DATETIME      NULL,
    [ApprovedUser]          VARCHAR (20)  NULL,
    [ApprovedDate]          DATETIME      NULL,
    [DeniedUser]            VARCHAR (20)  NULL,
    [DeniedDate]            DATETIME      NULL,
    [DeniedDescription]     VARCHAR (MAX) NULL,
    [TimeStamp]             ROWVERSION    NOT NULL,
    CONSTRAINT [PK_PermissionRequest] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PermissionRequest_Employee] FOREIGN KEY ([EmployeeId]) REFERENCES [Payroll].[Employee] ([Id])
);


GO
ALTER TABLE [Payroll].[PermissionRequest] NOCHECK CONSTRAINT [FK_PermissionRequest_Employee];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado de la solicitud   1 - Registrada  2 - Aprobado Parcial  3 - Aprobado Total  4 - Rechazado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PermissionRequest', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el tipo de permiso   1 - Remunerado  2 - No Remunerado  3 - Descuento por vacaciones ', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PermissionRequest', @level2type = N'COLUMN', @level2name = N'PermissionType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el motivo del permiso  1 - Cita Medica  2 - Actividad Personal  3 - Matrimonio  4 - Estudio  5 - Actividad Laboral  6 - Calamidad  7 - Otra', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PermissionRequest', @level2type = N'COLUMN', @level2name = N'PermissionReason';

