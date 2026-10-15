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
ALTER TABLE [HumanTalent].[VacationRequest] NOCHECK CONSTRAINT [FK_VacationRequest_Contract];


GO
ALTER TABLE [HumanTalent].[VacationRequest] NOCHECK CONSTRAINT [FK_VacationRequest_Employee];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'VacationRequest', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación (Se debe actualizar siempre a la Última Fecha de la Modificación del Registro)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'VacationRequest', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario de Modificación (Se debe cambiar siempre al último usuario que está modificando el registro)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'VacationRequest', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación del Registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'VacationRequest', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Creación del Registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'VacationRequest', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Último Estado (Debe actualizarse siempre el último estado que esté en la tabla VacationRequestDetail)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'VacationRequest', @level2type = N'COLUMN', @level2name = N'LastStatus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dias a Disfrutar', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'VacationRequest', @level2type = N'COLUMN', @level2name = N'EnjoyDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dias Solicitados', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'VacationRequest', @level2type = N'COLUMN', @level2name = N'DaysRequest';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Incorporacion', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'VacationRequest', @level2type = N'COLUMN', @level2name = N'IncorporationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Fin Vacaciones', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'VacationRequest', @level2type = N'COLUMN', @level2name = N'EndDateVacation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Inicio Vacaciones', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'VacationRequest', @level2type = N'COLUMN', @level2name = N'InitialDateVacation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo Vacaciones (1 - Disfrutadas, 2 - Compensadas)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'VacationRequest', @level2type = N'COLUMN', @level2name = N'TypeVacation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Contrato', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'VacationRequest', @level2type = N'COLUMN', @level2name = N'IdContract';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero Contrato Inicial', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'VacationRequest', @level2type = N'COLUMN', @level2name = N'InitialContractNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Empleado', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'VacationRequest', @level2type = N'COLUMN', @level2name = N'EmployeeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'VacationRequest', @level2type = N'COLUMN', @level2name = N'Id';

