CREATE TABLE [AccountManagement].[RejectionReasonUser] (
    [Id]                INT          IDENTITY (1, 1) NOT NULL,
    [RejectionReasonId] INT          NOT NULL,
    [UserId]            INT          NOT NULL,
    [Usercode]          VARCHAR (20) NOT NULL,
    CONSTRAINT [PK_RejectionReasonUser_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RejectionReasonUser_RejectionReason] FOREIGN KEY ([RejectionReasonId]) REFERENCES [AccountManagement].[RejectionReason] ([Id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Codigo del usuario', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'RejectionReasonUser', @level2type = N'COLUMN', @level2name = N'Usercode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del usuario', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'RejectionReasonUser', @level2type = N'COLUMN', @level2name = N'UserId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del motivo del rechazo', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'RejectionReasonUser', @level2type = N'COLUMN', @level2name = N'RejectionReasonId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificacion de la tabla', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'RejectionReasonUser', @level2type = N'COLUMN', @level2name = N'Id';

