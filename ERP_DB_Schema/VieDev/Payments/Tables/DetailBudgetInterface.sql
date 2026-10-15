CREATE TABLE [Payments].[DetailBudgetInterface] (
    [Id]               INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdAccountPayable] INT             NOT NULL,
    [CommitmentDetail] INT             NOT NULL,
    [Value]            DECIMAL (18, 2) NOT NULL,
    [ExpirationDate]   DATETIME        NOT NULL,
    [Contract]         VARCHAR (30)    NULL,
    [TimeStamp]        ROWVERSION      NOT NULL,
    CONSTRAINT [PK_DetailBudgetInterface] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DetailBudgetInterface_AccountPayable] FOREIGN KEY ([IdAccountPayable]) REFERENCES [Payments].[AccountPayable] ([Id])
);


GO
ALTER TABLE [Payments].[DetailBudgetInterface] NOCHECK CONSTRAINT [FK_DetailBudgetInterface_AccountPayable];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DetailBudgetInterface', @level2type = N'COLUMN', @level2name = N'TimeStamp';

