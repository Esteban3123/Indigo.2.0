CREATE TABLE [EHR].[DrugSheetByCycle] (
    [Id]          INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [DrugSheetId] NUMERIC (18) NOT NULL,
    [CycleNumber] INT          NULL,
    [WeekNumber]  INT          NULL,
    CONSTRAINT [PK_DrugSheetByCycle] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DrugSheetByCycle_HCHOJAMED] FOREIGN KEY ([DrugSheetId]) REFERENCES [dbo].[HCHOJAMED] ([CONSECUTI])
);


GO
ALTER TABLE [EHR].[DrugSheetByCycle] NOCHECK CONSTRAINT [FK_DrugSheetByCycle_HCHOJAMED];

