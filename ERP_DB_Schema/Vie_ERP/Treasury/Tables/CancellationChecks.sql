CREATE TABLE [Treasury].[CancellationChecks] (
    [Id]               INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdEntityAccount]  INT           NOT NULL,
    [IdCheckBook]      INT           NOT NULL,
    [CancellationDate] DATETIME      NOT NULL,
    [CheckNumber]      VARCHAR (100) NOT NULL,
    [Description]      VARCHAR (MAX) NOT NULL,
    [CreationUser]     VARCHAR (20)  NOT NULL,
    [CreationDate]     DATETIME      NOT NULL,
    [ModificationUser] VARCHAR (20)  NULL,
    [ModificationDate] DATETIME      NULL,
    [TimeStamp]        ROWVERSION    NOT NULL,
    CONSTRAINT [PK_CancellationCheck] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CancellationCheck_EntityBankAccount] FOREIGN KEY ([IdEntityAccount]) REFERENCES [Treasury].[EntityBankAccounts] ([Id]),
    CONSTRAINT [FK_CancellationChecks_Checkbooks] FOREIGN KEY ([IdCheckBook]) REFERENCES [Treasury].[Checkbooks] ([Id])
);


GO
ALTER TABLE [Treasury].[CancellationChecks] NOCHECK CONSTRAINT [FK_CancellationCheck_EntityBankAccount];


GO
ALTER TABLE [Treasury].[CancellationChecks] NOCHECK CONSTRAINT [FK_CancellationChecks_Checkbooks];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CancellationChecks', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CancellationChecks', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CancellationChecks', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CancellationChecks', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CancellationChecks', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'descripcion de la cancelacion', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CancellationChecks', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero del cheque', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CancellationChecks', @level2type = N'COLUMN', @level2name = N'CheckNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de cancelacion del cheque', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CancellationChecks', @level2type = N'COLUMN', @level2name = N'CancellationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'id de la chequera asociada al cheque bloqueado', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CancellationChecks', @level2type = N'COLUMN', @level2name = N'IdCheckBook';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la entidad bancaria', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CancellationChecks', @level2type = N'COLUMN', @level2name = N'IdEntityAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del registro', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CancellationChecks', @level2type = N'COLUMN', @level2name = N'Id';

