CREATE TABLE [HumanTalent].[PVCancellationRequisition] (
    [Id]                              INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PersonalRequisitionId]           INT           NOT NULL,
    [ReasonCancellationRequisitionId] INT           NOT NULL,
    [Observation]                     VARCHAR (120) NULL,
    [CreationUser]                    VARCHAR (20)  NOT NULL,
    [CreationDate]                    DATETIME      NOT NULL,
    [ModificationUser]                VARCHAR (20)  NULL,
    [ModificationDate]                DATETIME      NULL,
    CONSTRAINT [PK_PVCancellationRequisition] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PVCancellationRequisition_PersonalRequisition] FOREIGN KEY ([PersonalRequisitionId]) REFERENCES [HumanTalent].[PersonalRequisition] ([Id]),
    CONSTRAINT [FK_PVCancellationRequisition_ReasonCancellationRequisition] FOREIGN KEY ([ReasonCancellationRequisitionId]) REFERENCES [StaffPick].[ReasonCancellationRequisition] ([Id])
);


GO
ALTER TABLE [HumanTalent].[PVCancellationRequisition] NOCHECK CONSTRAINT [FK_PVCancellationRequisition_PersonalRequisition];


GO
ALTER TABLE [HumanTalent].[PVCancellationRequisition] NOCHECK CONSTRAINT [FK_PVCancellationRequisition_ReasonCancellationRequisition];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de modificación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVCancellationRequisition', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Modificación del Usuario', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVCancellationRequisition', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de creación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVCancellationRequisition', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Creación del Usuario', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVCancellationRequisition', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVCancellationRequisition', @level2type = N'COLUMN', @level2name = N'Observation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identifica la Razón de la Cancelación de la Requisición', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVCancellationRequisition', @level2type = N'COLUMN', @level2name = N'ReasonCancellationRequisitionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificacion de la solicitud del  personal', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVCancellationRequisition', @level2type = N'COLUMN', @level2name = N'PersonalRequisitionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación de la tabla', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVCancellationRequisition', @level2type = N'COLUMN', @level2name = N'Id';

