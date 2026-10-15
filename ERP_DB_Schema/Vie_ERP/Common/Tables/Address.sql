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


GO
CREATE NONCLUSTERED INDEX [IX_Address_IdPerson]
    ON [Common].[Address]([IdPerson] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Municipio al cual pertenece la dirección', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Address', @level2type = N'COLUMN', @level2name = N'CityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Departamento al cual pertenece la dirección', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Address', @level2type = N'COLUMN', @level2name = N'DepartmentId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1-Estado ni Sincronizado 2- Estado no Sincronizado', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Address', @level2type = N'COLUMN', @level2name = N'Synchronized';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado Eliminado para Sincronizacion de DB', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Address', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dirección', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Address', @level2type = N'COLUMN', @level2name = N'Addresss';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Persona', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Address', @level2type = N'COLUMN', @level2name = N'IdPerson';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Address', @level2type = N'COLUMN', @level2name = N'Id';

