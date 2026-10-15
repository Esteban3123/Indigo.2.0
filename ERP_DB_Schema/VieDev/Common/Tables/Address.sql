CREATE TABLE [Common].[Address] (
    [Id]           INT                                                IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdPerson]     INT                                                NOT NULL,
    [Addresss]     VARCHAR (200) MASKED WITH (FUNCTION = 'default()') NOT NULL,
    [State]        BIT                                                NOT NULL,
    [Synchronized] CHAR (1)                                           NOT NULL,
    [DepartmentId] INT                                                NULL,
    [CityId]       INT                                                NULL,
    CONSTRAINT [PK_Addresses] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Address_City] FOREIGN KEY ([CityId]) REFERENCES [Common].[City] ([Id]),
    CONSTRAINT [FK_Address_Department] FOREIGN KEY ([DepartmentId]) REFERENCES [Common].[Department] ([Id]),
    CONSTRAINT [FK_Addresses_Person] FOREIGN KEY ([IdPerson]) REFERENCES [Common].[Person] ([Id])
);


GO
ALTER TABLE [Common].[Address] NOCHECK CONSTRAINT [FK_Address_City];


GO
ALTER TABLE [Common].[Address] NOCHECK CONSTRAINT [FK_Address_Department];


GO
ALTER TABLE [Common].[Address] NOCHECK CONSTRAINT [FK_Addresses_Person];

