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



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal (TIMESTAMP) del evento de creación, modificación o cambio de estado de la solicitud de permiso. Captura automáticamente el instante exacto del registro o actualización del archivo.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PermissionRequest', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PermissionRequest', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PermissionRequest', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción detallada de los motivos de negación o rechazo de la solicitud de permiso. Especifica por qué fue rechazada la solicitud de vacaciones, licencia o permiso.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PermissionRequest', @level2type = N'COLUMN', @level2name = N'DeniedDescription';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la descripcion del por que le anularon o no le aprobaron el permiso', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PermissionRequest', @level2type = N'COLUMN', @level2name = N'DeniedDescription';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PermissionRequest', @level2type = N'COLUMN', @level2name = N'DeniedDescription';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) en que se rechazó o denegó la solicitud de permiso. Registra cuándo el usuario autorizado efectuó la negación.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PermissionRequest', @level2type = N'COLUMN', @level2name = N'DeniedDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la fecha en que se denego el permiso', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PermissionRequest', @level2type = N'COLUMN', @level2name = N'DeniedDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PermissionRequest', @level2type = N'COLUMN', @level2name = N'DeniedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario (login/cuenta) que rechazó o denegó la solicitud de permiso. Referencia al profesional de recursos humanos o supervisor que ejecutó la negación.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PermissionRequest', @level2type = N'COLUMN', @level2name = N'DeniedUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el usuario que denego el permiso', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PermissionRequest', @level2type = N'COLUMN', @level2name = N'DeniedUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PermissionRequest', @level2type = N'COLUMN', @level2name = N'DeniedUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de aprobación final de la solicitud de permiso, vacaciones o licencia. Último registro de aprobación en el flujo de autorización.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PermissionRequest', @level2type = N'COLUMN', @level2name = N'ApprovedDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la ultima fecha en que un usuario aprovo la solicitus de vaciones', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PermissionRequest', @level2type = N'COLUMN', @level2name = N'ApprovedDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PermissionRequest', @level2type = N'COLUMN', @level2name = N'ApprovedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario (login/cuenta) que aprobó la solicitud de permiso, vacaciones o licencia. Último autorizado en el flujo de aprobación.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PermissionRequest', @level2type = N'COLUMN', @level2name = N'ApprovedUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el ultimo usuario que aprobo la solicvitud de vacaciones', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PermissionRequest', @level2type = N'COLUMN', @level2name = N'ApprovedUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PermissionRequest', @level2type = N'COLUMN', @level2name = N'ApprovedUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de la última modificación de la solicitud de permiso. Registra cuándo se actualizó algún dato del registro.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PermissionRequest', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Modificación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PermissionRequest', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PermissionRequest', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario (login/cuenta) que efectuó la última modificación de la solicitud de permiso.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PermissionRequest', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de Modificación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PermissionRequest', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PermissionRequest', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de creación de la solicitud de permiso. Captura cuándo el empleado registró originalmente la solicitud.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PermissionRequest', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PermissionRequest', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PermissionRequest', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario (login/cuenta) que creó la solicitud de permiso. Empleado que originó el registro de la solicitud.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PermissionRequest', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de Creación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PermissionRequest', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PermissionRequest', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado actual de la solicitud de permiso (TINYINT): 1=Registrada, 2=Aprobado Parcial, 3=Aprobado Total, 4=Rechazado. Indica etapa del flujo de aprobación.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PermissionRequest', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado de la solicitud   1 - Registrada  2 - Aprobado Parcial  3 - Aprobado Total  4 - Rechazado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PermissionRequest', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PermissionRequest', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de permiso solicitado (TINYINT): 1=Remunerado (pagado), 2=No Remunerado (sin salario), 3=Descuento por Vacaciones. Clasifica si el permiso genera pago.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PermissionRequest', @level2type = N'COLUMN', @level2name = N'PermissionType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el tipo de permiso   1 - Remunerado  2 - No Remunerado  3 - Descuento por vacaciones ', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PermissionRequest', @level2type = N'COLUMN', @level2name = N'PermissionType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PermissionRequest', @level2type = N'COLUMN', @level2name = N'PermissionType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) final del período del permiso, vacaciones o licencia. Especifica cuándo termina el permiso autorizado.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PermissionRequest', @level2type = N'COLUMN', @level2name = N'EndDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la fecha final del permiso', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PermissionRequest', @level2type = N'COLUMN', @level2name = N'EndDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PermissionRequest', @level2type = N'COLUMN', @level2name = N'EndDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) inicial del período del permiso, vacaciones o licencia. Especifica cuándo inicia el permiso solicitado.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PermissionRequest', @level2type = N'COLUMN', @level2name = N'InitialDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la fecha inicial del permiso', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PermissionRequest', @level2type = N'COLUMN', @level2name = N'InitialDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PermissionRequest', @level2type = N'COLUMN', @level2name = N'InitialDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción resumida (VARCHAR 300) del permiso solicitado. Contiene detalles o notas sobre la naturaleza del permiso, vacaciones o licencia.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PermissionRequest', @level2type = N'COLUMN', @level2name = N'PermissionDescription';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion del permiso', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PermissionRequest', @level2type = N'COLUMN', @level2name = N'PermissionDescription';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PermissionRequest', @level2type = N'COLUMN', @level2name = N'PermissionDescription';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Motivo adicional o alternativo del permiso (VARCHAR MAX). Se completa cuando la razón seleccionada es ''''Otra'''' (código 7). Especifica causa no predefinida.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PermissionRequest', @level2type = N'COLUMN', @level2name = N'PermissionReasonOther';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica cual es el otro motivo de la solicitud del permiso, Este campo solo se habilita si el motivo de permiso es otro', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PermissionRequest', @level2type = N'COLUMN', @level2name = N'PermissionReasonOther';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PermissionRequest', @level2type = N'COLUMN', @level2name = N'PermissionReasonOther';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Motivo de la solicitud de permiso (TINYINT): 1=Cita Médica, 2=Actividad Personal, 3=Matrimonio, 4=Estudio, 5=Actividad Laboral, 6=Calamidad, 7=Otra.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PermissionRequest', @level2type = N'COLUMN', @level2name = N'PermissionReason';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el motivo del permiso  1 - Cita Medica  2 - Actividad Personal  3 - Matrimonio  4 - Estudio  5 - Actividad Laboral  6 - Calamidad  7 - Otra', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PermissionRequest', @level2type = N'COLUMN', @level2name = N'PermissionReason';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PermissionRequest', @level2type = N'COLUMN', @level2name = N'PermissionReason';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK → Payroll.Employee.Id) del empleado que genera la solicitud de permiso. Referencia al registro del trabajador en nómina.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PermissionRequest', @level2type = N'COLUMN', @level2name = N'EmployeeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del empleado que esta generando la solicitud', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PermissionRequest', @level2type = N'COLUMN', @level2name = N'EmployeeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PermissionRequest', @level2type = N'COLUMN', @level2name = N'EmployeeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT, PK) de la solicitud de permiso. Clave primaria que identifica cada registro de permiso en el sistema.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PermissionRequest', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del permiso', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PermissionRequest', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PermissionRequest', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Solicitudes de permisos laborales de los empleados: registra cada pedido de permiso (ausencia, licencia, salida), su motivo, período de tiempo, tipo, estado actual (pendiente, aprobado, rechazado) y el historial de quién lo creó, aprobó o denegó.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PermissionRequest';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PermissionRequest';
