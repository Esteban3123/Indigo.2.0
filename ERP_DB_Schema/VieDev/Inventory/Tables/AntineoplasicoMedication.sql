CREATE TABLE [Inventory].[AntineoplasicoMedication] (
    [Id]               INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [AtcId]            INT          NOT NULL,
    [ClassificationId] TINYINT      NOT NULL,
    [StateAPM]         TINYINT      NOT NULL,
    [CreationUser]     VARCHAR (20) NOT NULL,
    [CreationDate]     DATETIME     NOT NULL,
    [InactivateUser]   VARCHAR (20) NULL,
    [InactivateDate]   DATETIME     NULL,
    [TimeStamp]        ROWVERSION   NOT NULL,
    CONSTRAINT [PK_AntineoplasicoMedication_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AntineoplasicoMedication_ATC] FOREIGN KEY ([AtcId]) REFERENCES [Inventory].[ATC] ([Id])
);


GO
ALTER TABLE [Inventory].[AntineoplasicoMedication] NOCHECK CONSTRAINT [FK_AntineoplasicoMedication_ATC];

