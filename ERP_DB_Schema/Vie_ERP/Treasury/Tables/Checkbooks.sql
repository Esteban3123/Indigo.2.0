CREATE TABLE [Treasury].[Checkbooks] (
    [Id]                   INT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdEntityBanckAccount] INT         NOT NULL,
    [InitialNumber]        BIGINT      NOT NULL,
    [EndNumber]            BIGINT      NOT NULL,
    [CurrentNumber]        BIGINT      NOT NULL,
    [Prefix]               VARCHAR (5) NOT NULL,
    [Status]               TINYINT     NOT NULL,
    [TimeStamp]            ROWVERSION  NOT NULL,
    CONSTRAINT [PK_Voucher] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Voucher_EntityBankAccount] FOREIGN KEY ([IdEntityBanckAccount]) REFERENCES [Treasury].[EntityBankAccounts] ([Id])
);


GO
ALTER TABLE [Treasury].[Checkbooks] NOCHECK CONSTRAINT [FK_Voucher_EntityBankAccount];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Checkbooks', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado (Activa=1, Bloqueada=2, Finalizada=3)', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Checkbooks', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Prefijo', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Checkbooks', @level2type = N'COLUMN', @level2name = N'Prefix';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número actual de la chequera', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Checkbooks', @level2type = N'COLUMN', @level2name = N'CurrentNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número Final de la chequera', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Checkbooks', @level2type = N'COLUMN', @level2name = N'EndNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número inicial de la chequera', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Checkbooks', @level2type = N'COLUMN', @level2name = N'InitialNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuenta bancaria', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Checkbooks', @level2type = N'COLUMN', @level2name = N'IdEntityBanckAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del registro', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Checkbooks', @level2type = N'COLUMN', @level2name = N'Id';

