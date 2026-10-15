CREATE TABLE [GeneralLedger].[ClosedMonth] (
    [Id]                    INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Year]                  INT          NOT NULL,
    [Month]                 INT          NOT NULL,
    [Status]                BIT          CONSTRAINT [DF_ClosedMonth_Status] DEFAULT (NULL) NULL,
    [RecalculatingBalances] BIT          NOT NULL,
    [CreationUser]          VARCHAR (20) NOT NULL,
    [CreationDate]          DATETIME     NOT NULL,
    [ModificationUser]      VARCHAR (20) NULL,
    [ModificationDate]      DATETIME     NULL,
    [TimeStamp]             ROWVERSION   NOT NULL,
    CONSTRAINT [PK_ClosedMonth__Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Recalcula valores | 1 = Si | 0 = No', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'ClosedMonth', @level2type = N'COLUMN', @level2name = N'RecalculatingBalances';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del cierre  1 - Abierto  0 - Cerrado', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'ClosedMonth', @level2type = N'COLUMN', @level2name = N'Status';

