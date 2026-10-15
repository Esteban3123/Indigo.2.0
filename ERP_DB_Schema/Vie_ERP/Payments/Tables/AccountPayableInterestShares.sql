CREATE TABLE [Payments].[AccountPayableInterestShares] (
    [Id]                     INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdAccountPayableShares] INT             NOT NULL,
    [DateInterestShares]     DATETIME        NOT NULL,
    [Value]                  DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [PK_AccountPayableInterestShares] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor del interes de la cuota', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableInterestShares', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha del interes de la cuota', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableInterestShares', @level2type = N'COLUMN', @level2name = N'DateInterestShares';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuota a la cual le pertenece el interes', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableInterestShares', @level2type = N'COLUMN', @level2name = N'IdAccountPayableShares';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del interes de la cuota', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableInterestShares', @level2type = N'COLUMN', @level2name = N'Id';

