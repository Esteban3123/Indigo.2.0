CREATE TABLE [Taxes].[TaxesMagneticMediaDetail] (
    [Id]                   INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [MagneticMediaId]      INT            NOT NULL,
    [NameOrBusinessName]   NVARCHAR (200) NOT NULL,
    [IdentificationNumber] NVARCHAR (20)  NOT NULL,
    [ValueOperation]       NUMERIC (18)   NOT NULL,
    [TaxWithheld]          NUMERIC (18)   NOT NULL,
    CONSTRAINT [PK_MagneticMediaDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MagneticMediaDetail_MagnetiMedia] FOREIGN KEY ([MagneticMediaId]) REFERENCES [Taxes].[TaxesMagneticMedia] ([Id])
);


GO
ALTER TABLE [Taxes].[TaxesMagneticMediaDetail] NOCHECK CONSTRAINT [FK_MagneticMediaDetail_MagnetiMedia];

