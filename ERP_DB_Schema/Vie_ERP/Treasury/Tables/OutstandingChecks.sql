CREATE TABLE [Treasury].[OutstandingChecks] (
    [Id]          INT    IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdCheckBook] INT    NOT NULL,
    [CheckNumber] BIGINT NOT NULL,
    CONSTRAINT [PK_OutstandingChecks] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_OutstandingChecks_Checkbooks] FOREIGN KEY ([IdCheckBook]) REFERENCES [Treasury].[Checkbooks] ([Id])
);


GO
ALTER TABLE [Treasury].[OutstandingChecks] NOCHECK CONSTRAINT [FK_OutstandingChecks_Checkbooks];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número del cheque que queda pendiente', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'OutstandingChecks', @level2type = N'COLUMN', @level2name = N'CheckNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la chequera asociada al cheque', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'OutstandingChecks', @level2type = N'COLUMN', @level2name = N'IdCheckBook';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'id del registro', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'OutstandingChecks', @level2type = N'COLUMN', @level2name = N'Id';

