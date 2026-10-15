CREATE TABLE [HumanTalent].[VacationRequestDetail] (
    [Id]                INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdVacationRequest] INT           NOT NULL,
    [StatusRequest]     TINYINT       NOT NULL,
    [UserStatus]        VARCHAR (20)  NOT NULL,
    [DateStatus]        DATETIME      NOT NULL,
    [Comments]          VARCHAR (500) NULL,
    CONSTRAINT [PK_VacationRequestDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_VacationRequestDetail_VacationRequest] FOREIGN KEY ([IdVacationRequest]) REFERENCES [HumanTalent].[VacationRequest] ([Id])
);


GO
ALTER TABLE [HumanTalent].[VacationRequestDetail] NOCHECK CONSTRAINT [FK_VacationRequestDetail_VacationRequest];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Comentarios', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'VacationRequestDetail', @level2type = N'COLUMN', @level2name = N'Comments';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha del Registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'VacationRequestDetail', @level2type = N'COLUMN', @level2name = N'DateStatus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario que ha realizado el registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'VacationRequestDetail', @level2type = N'COLUMN', @level2name = N'UserStatus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado de la Solicitud:  1. Esperando Aprobación  2. Aprobado  3. Rechazado', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'VacationRequestDetail', @level2type = N'COLUMN', @level2name = N'StatusRequest';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Vacation Request', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'VacationRequestDetail', @level2type = N'COLUMN', @level2name = N'IdVacationRequest';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'VacationRequestDetail', @level2type = N'COLUMN', @level2name = N'Id';

