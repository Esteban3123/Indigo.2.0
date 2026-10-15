CREATE TABLE [Treasury].[TreasurySequence] (
    [Id]         INT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdForm]     VARCHAR (5) NOT NULL,
    [IsManual]   BIT         CONSTRAINT [DF_TreasurySequence_IdManual] DEFAULT ((1)) NOT NULL,
    [Scope]      VARCHAR (2) CONSTRAINT [DF_SequenseTreasuryC_Scope] DEFAULT ('O') NOT NULL,
    [Sequential] BIT         CONSTRAINT [DF_SequenseTreasuryC_Sequential] DEFAULT ((0)) NOT NULL,
    [Rate]       TINYINT     CONSTRAINT [DF_SequenseTreasuryC_Rate] DEFAULT ((3)) NOT NULL,
    [IdSequence] INT         NULL,
    CONSTRAINT [PK_SequenseTreasuryC] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TreasurySequence_Sequense] FOREIGN KEY ([IdSequence]) REFERENCES [Common].[Sequense] ([Id])
);


GO
ALTER TABLE [Treasury].[TreasurySequence] NOCHECK CONSTRAINT [FK_TreasurySequence_Sequense];

