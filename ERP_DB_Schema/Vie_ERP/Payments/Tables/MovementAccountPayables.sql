CREATE TABLE [Payments].[MovementAccountPayables] (
    [Id]                    INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdAccountPayable]      INT             NOT NULL,
    [IdAccountPayableShare] INT             NOT NULL,
    [EntityId]              INT             NOT NULL,
    [EntityCode]            VARCHAR (20)    NOT NULL,
    [EntityName]            VARCHAR (250)   NOT NULL,
    [MovementDate]          DATETIME        NOT NULL,
    [Value]                 DECIMAL (18, 2) NOT NULL,
    [TimeStamp]             ROWVERSION      NOT NULL,
    CONSTRAINT [PK_MovementAccountPayables] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MovementAccountPayables_AccountPayable] FOREIGN KEY ([IdAccountPayable]) REFERENCES [Payments].[AccountPayable] ([Id]),
    CONSTRAINT [FK_MovementAccountPayables_AccountPayableShares] FOREIGN KEY ([IdAccountPayableShare]) REFERENCES [Payments].[AccountPayableShares] ([Id])
);


GO
ALTER TABLE [Payments].[MovementAccountPayables] NOCHECK CONSTRAINT [FK_MovementAccountPayables_AccountPayable];


GO
ALTER TABLE [Payments].[MovementAccountPayables] NOCHECK CONSTRAINT [FK_MovementAccountPayables_AccountPayableShares];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'MovementAccountPayables', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'MovementAccountPayables', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Movimiento', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'MovementAccountPayables', @level2type = N'COLUMN', @level2name = N'MovementDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre de la entidad quien genera el documento', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'MovementAccountPayables', @level2type = N'COLUMN', @level2name = N'EntityName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de la entidad quien genera el documento', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'MovementAccountPayables', @level2type = N'COLUMN', @level2name = N'EntityCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la entidad quien genera el documento', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'MovementAccountPayables', @level2type = N'COLUMN', @level2name = N'EntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuota', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'MovementAccountPayables', @level2type = N'COLUMN', @level2name = N'IdAccountPayableShare';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuenta por pagar', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'MovementAccountPayables', @level2type = N'COLUMN', @level2name = N'IdAccountPayable';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del registro', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'MovementAccountPayables', @level2type = N'COLUMN', @level2name = N'Id';

