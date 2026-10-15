CREATE TABLE [Treasury].[CheckBlock] (
    [Id]          INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdCheckbook] INT          NOT NULL,
    [CheckNumber] BIGINT       NOT NULL,
    [CodUser]     VARCHAR (20) NOT NULL,
    CONSTRAINT [PK_CheckBlock] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CheckBlock_Checkbooks] FOREIGN KEY ([IdCheckbook]) REFERENCES [Treasury].[Checkbooks] ([Id])
);


GO
ALTER TABLE [Treasury].[CheckBlock] NOCHECK CONSTRAINT [FK_CheckBlock_Checkbooks];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'id del usuario que bloquea el cheque', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CheckBlock', @level2type = N'COLUMN', @level2name = N'CodUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'número del cheque bloqueado', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CheckBlock', @level2type = N'COLUMN', @level2name = N'CheckNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'id de la chequera asociada al cheque bloqueado', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CheckBlock', @level2type = N'COLUMN', @level2name = N'IdCheckbook';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'id del cheque bloqueado', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CheckBlock', @level2type = N'COLUMN', @level2name = N'Id';

