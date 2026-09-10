CREATE TABLE [HumanTalent].[VacationRequest] (
    [Id]                    INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [EmployeeId]            INT          NOT NULL,
    [InitialContractNumber] INT          NOT NULL,
    [IdContract]            INT          NOT NULL,
    [TypeVacation]          INT          NOT NULL,
    [InitialDateVacation]   DATE         NULL,
    [EndDateVacation]       DATE         NULL,
    [IncorporationDate]     DATE         NULL,
    [DaysRequest]           TINYINT      NOT NULL,
    [EnjoyDays]             TINYINT      NOT NULL,
    [LastStatus]            TINYINT      NOT NULL,
    [CreationDate]          DATETIME     NOT NULL,
    [CreationUser]          VARCHAR (20) NOT NULL,
    [ModificationUser]      VARCHAR (20) NULL,
    [ModificationDate]      DATETIME     NULL,
    [TimeStamp]             ROWVERSION   NOT NULL,
    CONSTRAINT [PK_VacationRequest_1] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_VacationRequest_Contract] FOREIGN KEY ([IdContract]) REFERENCES [Payroll].[Contract] ([Id]),
    CONSTRAINT [FK_VacationRequest_Employee] FOREIGN KEY ([EmployeeId]) REFERENCES [Payroll].[Employee] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca de tiempo/versión de fila (TIMESTAMP, generada automáticamente por SQL Server para control de concurrencia optimista).', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'VacationRequest', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'VacationRequest', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'VacationRequest', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Última fecha y hora en que se modificó el registro (DATETIME, nullable, actualización de auditoría).', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'VacationRequest', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación (Se debe actualizar siempre a la Última Fecha de la Modificación del Registro)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'VacationRequest', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'VacationRequest', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Último usuario/login que modificó el registro de la solicitud (VARCHAR 20, nullable, auditoría de cambios).', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'VacationRequest', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de Modificación (Se debe cambiar siempre al último usuario que está modificando el registro)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'VacationRequest', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'VacationRequest', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario/login que realizó el registro inicial de la solicitud (VARCHAR 20, auditoría de creación).', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'VacationRequest', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación del Registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'VacationRequest', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'VacationRequest', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación/registro inicial de la solicitud de vacaciones (DATETIME, marca de auditoría).', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'VacationRequest', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Creación del Registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'VacationRequest', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'VacationRequest', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Último estado de la solicitud, refleja el estado más reciente registrado en VacationRequestDetail (TINYINT, ej: pendiente, aprobado, rechazado).', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'VacationRequest', @level2type = N'COLUMN', @level2name = N'LastStatus';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Último Estado (Debe actualizarse siempre el último estado que esté en la tabla VacationRequestDetail)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'VacationRequest', @level2type = N'COLUMN', @level2name = N'LastStatus';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'VacationRequest', @level2type = N'COLUMN', @level2name = N'LastStatus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de días de vacaciones a disfrutar o gozar efectivamente (TINYINT).', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'VacationRequest', @level2type = N'COLUMN', @level2name = N'EnjoyDays';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dias a Disfrutar', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'VacationRequest', @level2type = N'COLUMN', @level2name = N'EnjoyDays';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'VacationRequest', @level2type = N'COLUMN', @level2name = N'EnjoyDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de días de vacaciones solicitados por el empleado (TINYINT, máx 255 días).', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'VacationRequest', @level2type = N'COLUMN', @level2name = N'DaysRequest';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dias Solicitados', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'VacationRequest', @level2type = N'COLUMN', @level2name = N'DaysRequest';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'VacationRequest', @level2type = N'COLUMN', @level2name = N'DaysRequest';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha prevista de reincorporación del empleado al trabajo tras finalizar vacaciones.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'VacationRequest', @level2type = N'COLUMN', @level2name = N'IncorporationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Incorporacion', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'VacationRequest', @level2type = N'COLUMN', @level2name = N'IncorporationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'VacationRequest', @level2type = N'COLUMN', @level2name = N'IncorporationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha final del período de vacaciones solicitado, cierre de disfrute (DATE, formato aaaa-mm-dd).', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'VacationRequest', @level2type = N'COLUMN', @level2name = N'EndDateVacation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Fin Vacaciones', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'VacationRequest', @level2type = N'COLUMN', @level2name = N'EndDateVacation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'VacationRequest', @level2type = N'COLUMN', @level2name = N'EndDateVacation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de inicio del período de vacaciones solicitado (DATE, formato aaaa-mm-dd).', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'VacationRequest', @level2type = N'COLUMN', @level2name = N'InitialDateVacation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Inicio Vacaciones', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'VacationRequest', @level2type = N'COLUMN', @level2name = N'InitialDateVacation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'VacationRequest', @level2type = N'COLUMN', @level2name = N'InitialDateVacation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de vacaciones: 1=Disfrutadas (goce de vacaciones), 2=Compensadas (pago en efectivo, liquidación).', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'VacationRequest', @level2type = N'COLUMN', @level2name = N'TypeVacation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo Vacaciones (1 - Disfrutadas, 2 - Compensadas)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'VacationRequest', @level2type = N'COLUMN', @level2name = N'TypeVacation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'VacationRequest', @level2type = N'COLUMN', @level2name = N'TypeVacation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del contrato vigente vinculado a la solicitud, referencia a Payroll.Contract (FK, clave foránea).', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'VacationRequest', @level2type = N'COLUMN', @level2name = N'IdContract';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Contrato', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'VacationRequest', @level2type = N'COLUMN', @level2name = N'IdContract';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'VacationRequest', @level2type = N'COLUMN', @level2name = N'IdContract';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número inicial del contrato del empleado al momento de la solicitud (documento contrato).', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'VacationRequest', @level2type = N'COLUMN', @level2name = N'InitialContractNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero Contrato Inicial', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'VacationRequest', @level2type = N'COLUMN', @level2name = N'InitialContractNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'VacationRequest', @level2type = N'COLUMN', @level2name = N'InitialContractNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del empleado solicitante, referencia a tabla Payroll.Employee (FK, clave foránea).', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'VacationRequest', @level2type = N'COLUMN', @level2name = N'EmployeeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Empleado', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'VacationRequest', @level2type = N'COLUMN', @level2name = N'EmployeeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'VacationRequest', @level2type = N'COLUMN', @level2name = N'EmployeeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autonumérico único de la solicitud de vacaciones (INT IDENTITY, PK).', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'VacationRequest', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'VacationRequest', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'VacationRequest', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Solicitudes de vacaciones del personal: registra cada petición de disfrute de vacaciones de un empleado, indicando el tipo de vacaciones, las fechas solicitadas, los días pedidos y disfrutados, y el estado actual de la solicitud.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'VacationRequest';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'VacationRequest';
