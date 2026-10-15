CREATE TABLE [Inventory].[InventorySequence] (
    [Id]                    INT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdForm]                VARCHAR (5) NOT NULL,
    [IsManual]              BIT         CONSTRAINT [DF_InventorySequence_IdManual] DEFAULT ((1)) NOT NULL,
    [Scope]                 VARCHAR (2) CONSTRAINT [DF_InventorySequence_Scope] DEFAULT ('O') NOT NULL,
    [Sequential]            BIT         CONSTRAINT [DF_InventorySequence_Sequential] DEFAULT ((0)) NOT NULL,
    [Rate]                  TINYINT     CONSTRAINT [DF_InventorySequence_Rate] DEFAULT ((3)) NOT NULL,
    [IdSequence]            INT         NULL,
    [RequireValidateLength] BIT         NULL,
    [MinimumLength]         TINYINT     NULL,
    [MaximumLength]         TINYINT     NULL,
    CONSTRAINT [PK_InventorySequence] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_InventorySequence_Sequense] FOREIGN KEY ([IdSequence]) REFERENCES [Common].[Sequense] ([Id])
);


GO
ALTER TABLE [Inventory].[InventorySequence] NOCHECK CONSTRAINT [FK_InventorySequence_Sequense];

