CREATE TABLE [Security].[LicenseControlByDevice] (
    [Id]            INT           IDENTITY (1, 1) NOT NULL,
    [SuscriptionId] INT           NOT NULL,
    [DeviceMAC]     VARCHAR (200) NOT NULL,
    [DeviceName]    VARCHAR (200) NOT NULL,
    [DeviceIPV4]    VARCHAR (50)  NOT NULL,
    CONSTRAINT [PK_LicenseControlByDevice] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_LicenseControlByDevice_Suscriptions] FOREIGN KEY ([SuscriptionId]) REFERENCES [Security].[Suscriptions] ([Id])
);

