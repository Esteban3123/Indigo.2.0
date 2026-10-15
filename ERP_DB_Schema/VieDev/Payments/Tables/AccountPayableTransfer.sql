CREATE TABLE [Payments].[AccountPayableTransfer] (
    [Id]                 INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]               VARCHAR (20) NOT NULL,
    [TranferType]        TINYINT      CONSTRAINT [DF_AccountPayableTransfer_TranferType] DEFAULT ((1)) NOT NULL,
    [FilingUnitSourceId] INT          NOT NULL,
    [FilingUnitTargetId] INT          NOT NULL,
    [Status]             TINYINT      NOT NULL,
    [CreationUser]       VARCHAR (20) NOT NULL,
    [CreationDate]       DATETIME     NOT NULL,
    [ModificationUser]   VARCHAR (20) NULL,
    [ModificationDate]   DATETIME     NULL,
    [ConfirmationUser]   VARCHAR (20) NULL,
    [ConfirmationDate]   DATETIME     NULL,
    [AnnulmentUser]      VARCHAR (20) NULL,
    [AnnulmentDate]      DATETIME     NULL,
    [TimeStamp]          ROWVERSION   NOT NULL,
    CONSTRAINT [PK_AccountPayableTransfer] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AccountPayableTransfer_FilingUnit] FOREIGN KEY ([FilingUnitSourceId]) REFERENCES [Payments].[FilingUnit] ([Id]),
    CONSTRAINT [FK_AccountPayableTransfer_FilingUnit1] FOREIGN KEY ([FilingUnitTargetId]) REFERENCES [Payments].[FilingUnit] ([Id])
);


GO
ALTER TABLE [Payments].[AccountPayableTransfer] NOCHECK CONSTRAINT [FK_AccountPayableTransfer_FilingUnit];


GO
ALTER TABLE [Payments].[AccountPayableTransfer] NOCHECK CONSTRAINT [FK_AccountPayableTransfer_FilingUnit1];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el estado del traslado  1 - Registrado  2 - Confirmado  3 - Anulado  4 - Evaluado- se actualiza en el frontal de aceptacion o rechazo de traslado.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableTransfer', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Tipo de Traslado   1 - Cuenta Por Pagar  2 - Reembolsos', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableTransfer', @level2type = N'COLUMN', @level2name = N'TranferType';

