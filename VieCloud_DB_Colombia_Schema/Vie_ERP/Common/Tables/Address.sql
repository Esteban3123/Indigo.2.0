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
ADD SENSITIVITY CLASSIFICATION TO
    [Common].[Address].[Addresss]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'Address');




GO
CREATE NONCLUSTERED INDEX [IX_Address_IdPerson]
    ON [Common].[Address]([IdPerson] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del municipio, ciudad o localidad específica donde se ubica el domicilio. Referencia FK a Common.City. Complementa ubicación geográfica para facturación, RIPS, centro de atención y cobertura.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Address', @level2type = N'COLUMN', @level2name = N'CityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Municipio al cual pertenece la dirección', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Address', @level2type = N'COLUMN', @level2name = N'CityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Address', @level2type = N'COLUMN', @level2name = N'CityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del departamento (región, estado, provincia) administrativo donde se localiza la dirección. Referencia FK a Common.Department. Contexto geográfico para reportes y auditoría de atención.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Address', @level2type = N'COLUMN', @level2name = N'DepartmentId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Departamento al cual pertenece la dirección', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Address', @level2type = N'COLUMN', @level2name = N'DepartmentId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Address', @level2type = N'COLUMN', @level2name = N'DepartmentId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de estado de sincronización (CHAR 1): ''''1''''=Sincronizado, ''''0''''=No sincronizado. Refleja si el cambio de dirección se propagó a sistemas externos, RIPS u otros módulos del ERP/EHR.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Address', @level2type = N'COLUMN', @level2name = N'Synchronized';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1-Estado ni Sincronizado 2- Estado no Sincronizado', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Address', @level2type = N'COLUMN', @level2name = N'Synchronized';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Address', @level2type = N'COLUMN', @level2name = N'Synchronized';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador lógico (BIT: 0=activo, 1=eliminado lógicamente) que marca si el registro de dirección está vigente o marcado para sincronización/eliminación de base de datos. Bandera de auditoría.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Address', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado Eliminado para Sincronizacion de DB', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Address', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Address', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dirección postal completa (VARCHAR 200, enmascarada PII). Incluye calle, número, barrio, complemento. Campo ofuscado por protección de datos. Localidad de domicilio del paciente, centro de atención u organización.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Address', @level2type = N'COLUMN', @level2name = N'Addresss';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dirección', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Address', @level2type = N'COLUMN', @level2name = N'Addresss';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Address', @level2type = N'COLUMN', @level2name = N'Addresss';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la persona (cédula, pasaporte, documento de identidad) propietaria del domicilio. Referencia FK a Common.Person. Vincula paciente, profesional de salud o tercero con su dirección registrada.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Address', @level2type = N'COLUMN', @level2name = N'IdPerson';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Persona', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Address', @level2type = N'COLUMN', @level2name = N'IdPerson';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Address', @level2type = N'COLUMN', @level2name = N'IdPerson';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincremental (INT IDENTITY) de la dirección en la tabla Address. Clave primaria que permite referenciar unívocamente cada registro de domicilio.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Address', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Address', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Address', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Direcciones de residencia o contacto registradas para cada persona en el sistema. Permite asociar una o más ubicaciones (departamento, ciudad, dirección) a pacientes, profesionales u otros actores del negocio.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Address';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Address';
