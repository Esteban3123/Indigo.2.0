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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PermissionRequest', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la descripcion del por que le anularon o no le aprobaron el permiso', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PermissionRequest', @level2type = N'COLUMN', @level2name = N'DeniedDescription';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la fecha en que se denego el permiso', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PermissionRequest', @level2type = N'COLUMN', @level2name = N'DeniedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el usuario que denego el permiso', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PermissionRequest', @level2type = N'COLUMN', @level2name = N'DeniedUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la ultima fecha en que un usuario aprovo la solicitus de vaciones', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PermissionRequest', @level2type = N'COLUMN', @level2name = N'ApprovedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el ultimo usuario que aprobo la solicvitud de vacaciones', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PermissionRequest', @level2type = N'COLUMN', @level2name = N'ApprovedUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Modificación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PermissionRequest', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario de Modificación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PermissionRequest', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PermissionRequest', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario de Creación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PermissionRequest', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado de la solicitud   1 - Registrada  2 - Aprobado Parcial  3 - Aprobado Total  4 - Rechazado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PermissionRequest', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el tipo de permiso   1 - Remunerado  2 - No Remunerado  3 - Descuento por vacaciones ', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PermissionRequest', @level2type = N'COLUMN', @level2name = N'PermissionType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la fecha final del permiso', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PermissionRequest', @level2type = N'COLUMN', @level2name = N'EndDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la fecha inicial del permiso', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PermissionRequest', @level2type = N'COLUMN', @level2name = N'InitialDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion del permiso', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PermissionRequest', @level2type = N'COLUMN', @level2name = N'PermissionDescription';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica cual es el otro motivo de la solicitud del permiso, Este campo solo se habilita si el motivo de permiso es otro', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PermissionRequest', @level2type = N'COLUMN', @level2name = N'PermissionReasonOther';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el motivo del permiso  1 - Cita Medica  2 - Actividad Personal  3 - Matrimonio  4 - Estudio  5 - Actividad Laboral  6 - Calamidad  7 - Otra', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PermissionRequest', @level2type = N'COLUMN', @level2name = N'PermissionReason';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del empleado que esta generando la solicitud', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PermissionRequest', @level2type = N'COLUMN', @level2name = N'EmployeeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del permiso', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PermissionRequest', @level2type = N'COLUMN', @level2name = N'Id';

