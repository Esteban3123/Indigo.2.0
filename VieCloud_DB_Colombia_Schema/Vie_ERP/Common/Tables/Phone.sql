CREATE TABLE [Common].[Phone] (
    [Id]           INT                                                                     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdPerson]     INT                                                                     NOT NULL,
    [Phone]        VARCHAR (15) MASKED WITH (FUNCTION = 'partial(0, "Phone_Ofuscado", 0)') NOT NULL,
    [State]        BIT                                                                     NOT NULL,
    [Synchronized] CHAR (1)                                                                NOT NULL,
    [IdPhoneType]  SMALLINT                                                                NOT NULL,
    CONSTRAINT [PK_Phones] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Phones_Person] FOREIGN KEY ([IdPerson]) REFERENCES [Common].[Person] ([Id]),
    CONSTRAINT [FK_Phones_PhoneType] FOREIGN KEY ([IdPhoneType]) REFERENCES [Common].[PhoneType] ([Id])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [Common].[Phone].[Phone]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'Contact Info');



GO
CREATE NONCLUSTERED INDEX [IX_Phone_IdPerson_IdPhoneType]
    ON [Common].[Phone]([IdPerson] ASC, [IdPhoneType] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de teléfono: 1-Móvil, 2-Fijo, 3-Fax, 4-Oficina, 5-PIN. Clasificación del número telefónico según su categoría de uso (celular, línea fija, facsímile, extensión de oficina o PIN de comunicación). Referencia FK a tabla PhoneType.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Phone', @level2type = N'COLUMN', @level2name = N'IdPhoneType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de telefono  1-Movil  2-Fijo  3-Fax  4-Oficina  5-PIN', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Phone', @level2type = N'COLUMN', @level2name = N'IdPhoneType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Phone', @level2type = N'COLUMN', @level2name = N'IdPhoneType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de sincronización: 1-Sincronizado, 2-No sincronizado. Indicador que controla si el registro de teléfono fue replicado exitosamente hacia otros nodos o bases de datos del sistema ERP/EHR.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Phone', @level2type = N'COLUMN', @level2name = N'Synchronized';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1-Estado ni Sincronizado 2- Estado no Sincronizado', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Phone', @level2type = N'COLUMN', @level2name = N'Synchronized';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Phone', @level2type = N'COLUMN', @level2name = N'Synchronized';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de eliminación lógica para sincronización de BD. Bandera que marca si el registro está activo (1) o eliminado lógicamente (0), usado en procesos de replicación y sincronización de datos entre instancias.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Phone', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado Eliminado para Sincronizacion de DB', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Phone', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Phone', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de teléfono (VARCHAR 15). Dato sensible PII enmascarado con ofuscación parcial. Almacena el dígito telefónico del paciente, profesional o contacto (móvil, fijo, fax, extensión u otro medio de contacto telefónico).', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Phone', @level2type = N'COLUMN', @level2name = N'Phone';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Telefono', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Phone', @level2type = N'COLUMN', @level2name = N'Phone';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Phone', @level2type = N'COLUMN', @level2name = N'Phone';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador numérico de la Persona (FK). Clave foránea que vincula el teléfono a su propietario en la tabla Common.Person (paciente, profesional de salud, contacto, etc.). Permite asociar múltiples números a una misma persona.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Phone', @level2type = N'COLUMN', @level2name = N'IdPerson';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id persona', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Phone', @level2type = N'COLUMN', @level2name = N'IdPerson';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Phone', @level2type = N'COLUMN', @level2name = N'IdPerson';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador primario (INT IDENTITY). Clave principal autoincremental de la tabla Phone. Valor único que identifica cada registro de número telefónico en el sistema, sin replicación.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Phone', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Phone', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Phone', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Teléfonos de contacto registrados para cada persona (paciente, profesional u otro actor del sistema). Permite almacenar uno o más números telefónicos por persona, con su tipo (celular, fijo, etc.) y estado de vigencia.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Phone';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Phone';

GO
CREATE NONCLUSTERED INDEX [IX_Phone_IdPerson]
    ON [Common].[Phone]([IdPerson] ASC)
    INCLUDE([Phone]);
