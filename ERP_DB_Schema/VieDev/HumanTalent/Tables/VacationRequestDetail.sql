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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado de la Solicitud:  1. Esperando Aprobación  2. Aprobado  3. Rechazado', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'VacationRequestDetail', @level2type = N'COLUMN', @level2name = N'StatusRequest';

