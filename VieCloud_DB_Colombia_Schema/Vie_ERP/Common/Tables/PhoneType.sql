CREATE TABLE [Common].[PhoneType] (
    [Id]               SMALLINT     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]             VARCHAR (20) NOT NULL,
    [Name]             VARCHAR (30) NOT NULL,
    [State]            BIT          NOT NULL,
    [CreationUser]     VARCHAR (20) CONSTRAINT [DF_PhoneType_CreationUser] DEFAULT ((999)) NOT NULL,
    [CreationDate]     DATETIME     CONSTRAINT [DF_PhoneType_CreationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [ModificationUser] VARCHAR (20) NULL,
    [ModificationDate] DATETIME     NULL,
    [TimeStamp]        ROWVERSION   NOT NULL,
    CONSTRAINT [PK_PhoneType__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [UQ_PhoneType__Code] UNIQUE NONCLUSTERED ([Code] ASC)
);


GO
CREATE NONCLUSTERED INDEX [PX_PhoneType_CodeState]
    ON [Common].[PhoneType]([Code] ASC, [State] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal (TIMESTAMP) que registra automáticamente el instante de creación, modificación o cambio de estado del tipo de teléfono en la base de datos. Utilizado para auditoría y control de versiones.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PhoneType', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PhoneType', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PhoneType', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de la última modificación del registro del tipo de teléfono. Nulo si nunca fue modificado después de su creación.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PhoneType', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PhoneType', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PhoneType', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (VARCHAR) del usuario que realizó la última modificación del tipo de teléfono. Nulo si el registro no ha sido editado.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PhoneType', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PhoneType', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PhoneType', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) en que se creó el registro del tipo de teléfono en el sistema. Valor por defecto: fecha/hora actual del servidor.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PhoneType', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PhoneType', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PhoneType', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (VARCHAR) del usuario que creó el registro del tipo de teléfono. Valor por defecto: 999 (sistema).', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PhoneType', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PhoneType', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PhoneType', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del tipo de teléfono (BIT): 1=Activo, 0=Inactivo. Controla si el tipo de teléfono está disponible para uso en registros de contacto.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PhoneType', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del tipo de telefono 1- Activo 0- Inactivo', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PhoneType', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PhoneType', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre descriptivo (VARCHAR 30) del tipo de teléfono. Ejemplos: Celular, Fijo, Comercial, Personal, Domicilio, Emergencia.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PhoneType', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del tipo de telefono', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PhoneType', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PhoneType', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único (VARCHAR 20) que identifica el tipo de teléfono. Clave alternativa (UNIQUE). Ejemplos: CEL, FIJ, COM, PER.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PhoneType', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del tipo de telefono', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PhoneType', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PhoneType', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (SMALLINT IDENTITY) auto-incremental de cada tipo de teléfono. Clave primaria de la tabla PhoneType.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PhoneType', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla de tipos de telefono', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PhoneType', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PhoneType', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de tipos de teléfono (fijo, móvil, fax, etc.) disponibles para registrar los contactos telefónicos de pacientes, profesionales y otras entidades del sistema.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PhoneType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PhoneType';
