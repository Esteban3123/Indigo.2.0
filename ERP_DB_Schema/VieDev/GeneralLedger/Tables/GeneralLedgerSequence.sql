CREATE TABLE [GeneralLedger].[GeneralLedgerSequence] (
    [Id]         INT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdForm]     VARCHAR (5) NOT NULL,
    [IsManual]   BIT         CONSTRAINT [DF_GeneralLedgerSequence_IdManual] DEFAULT ((1)) NOT NULL,
    [Scope]      VARCHAR (2) CONSTRAINT [DF_SequenseAccountingC_Scope] DEFAULT ('O') NOT NULL,
    [Sequential] BIT         CONSTRAINT [DF_SequenseAccountingC_Sequential] DEFAULT ((0)) NOT NULL,
    [Rate]       TINYINT     CONSTRAINT [DF_SequenseAccountingC_Rate] DEFAULT ((3)) NOT NULL,
    CONSTRAINT [PK_GeneralLedgerSequence__Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_GeneralLedgerSequence__IdForm]
    ON [GeneralLedger].[GeneralLedgerSequence]([IdForm] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especifica si es manual | 1 = Si | 0 = No', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSequence', @level2type = N'COLUMN', @level2name = N'IsManual';

