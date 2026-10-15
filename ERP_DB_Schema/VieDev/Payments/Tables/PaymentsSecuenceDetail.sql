CREATE TABLE [Payments].[PaymentsSecuenceDetail] (
    [Id]                  INT    IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdSequensePaymentsC] INT    NOT NULL,
    [IdSequense]          INT    NOT NULL,
    [IdOperatingUnit]     INT    NULL,
    [Next]                BIGINT CONSTRAINT [DF_SequensePaymentsD_Next] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_SequensePaymentsD] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SequensePaymentsD_OperatingUnit] FOREIGN KEY ([IdOperatingUnit]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [FK_SequensePaymentsD_Sequense] FOREIGN KEY ([IdSequense]) REFERENCES [Common].[Sequense] ([Id]),
    CONSTRAINT [FK_SequensePaymentsD_SequensePaymentsC] FOREIGN KEY ([IdSequensePaymentsC]) REFERENCES [Payments].[PaymentsSecuence] ([Id])
);


GO
ALTER TABLE [Payments].[PaymentsSecuenceDetail] NOCHECK CONSTRAINT [FK_SequensePaymentsD_OperatingUnit];


GO
ALTER TABLE [Payments].[PaymentsSecuenceDetail] NOCHECK CONSTRAINT [FK_SequensePaymentsD_Sequense];


GO
ALTER TABLE [Payments].[PaymentsSecuenceDetail] NOCHECK CONSTRAINT [FK_SequensePaymentsD_SequensePaymentsC];

