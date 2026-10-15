CREATE TABLE [Budget].[ReleaseResource] (
    [Id]               INT          NOT NULL,
    [Code]             VARCHAR (20) NOT NULL,
    [Butget]           BIT          NOT NULL,
    [Availability]     BIT          NOT NULL,
    [Commitment]       BIT          NOT NULL,
    [Obligation]       BIT          NOT NULL,
    [ObligationId]     INT          NULL,
    [CommitmentId]     INT          NULL,
    [Status]           TINYINT      NOT NULL,
    [CreationUser]     VARCHAR (20) CONSTRAINT [DF_ReleaseResource_CreationUser] DEFAULT ((999)) NOT NULL,
    [CreationDate]     DATETIME     CONSTRAINT [DF_ReleaseResource_CreationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [ModificationUser] VARCHAR (20) NULL,
    [ModificationDate] DATETIME     NULL,
    [ConfirmationUser] VARCHAR (20) NULL,
    [ConfirmationDate] DATETIME     NULL,
    [AnnulmentUser]    VARCHAR (20) NULL,
    [AnnulmentDate]    DATETIME     NULL,
    [TimeStamp]        ROWVERSION   NOT NULL,
    CONSTRAINT [PK_ReleaseResource__Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);

