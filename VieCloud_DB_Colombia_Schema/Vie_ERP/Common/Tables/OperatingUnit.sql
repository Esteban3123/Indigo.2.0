CREATE TABLE [Common].[OperatingUnit] (
    [Id]               INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdUnit]           INT           NULL,
    [UnitName]         VARCHAR (100) NOT NULL,
    [UnitCode]         VARCHAR (5)   NULL,
    [IPSCode]          VARCHAR (20)  NULL,
    [Address]          VARCHAR (100) NULL,
    [Phone]            VARCHAR (20)  NULL,
    [Email]            VARCHAR (100) NULL,
    [EmailAudit]       VARCHAR (100) NULL,
    [IdCity]           INT           NULL,
    [CreationUser]     VARCHAR (20)  CONSTRAINT [DF_OperatingUnit_CreationUser] DEFAULT ((999)) NOT NULL,
    [CreationDate]     DATETIME      CONSTRAINT [DF_OperatingUnit_CreationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [ModificationUser] VARCHAR (20)  NULL,
    [ModificationDate] DATETIME      NULL,
    [TimeStamp]        ROWVERSION    NOT NULL,
    CONSTRAINT [PK_OperatingUnit__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_OperatingUnit_City] FOREIGN KEY ([IdCity]) REFERENCES [Common].[City] ([Id]),
    CONSTRAINT [FK_OperatingUnit_OperatingUnit] FOREIGN KEY ([IdUnit]) REFERENCES [Common].[OperatingUnit] ([Id])
);




GO



GO



GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_OperatingUnit__UnitCode]
    ON [Common].[OperatingUnit]([UnitCode] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal (TIMESTAMP) que registra automáticamente el instante exacto de creación, modificación o cambio de estado del registro. Utilizado para control de concurrencia y auditoría.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'OperatingUnit', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'OperatingUnit', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'OperatingUnit', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de la última modificación del registro de la unidad operativa. DATETIME, NULL si sin cambios.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'OperatingUnit', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la fecha de modificacion', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'OperatingUnit', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'OperatingUnit', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario del sistema que realizó la última modificación del registro. VARCHAR(20), auditoría.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'OperatingUnit', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el usuario de modificacion', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'OperatingUnit', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'OperatingUnit', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro de la unidad operativa. DATETIME, default: Common.getdate().', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'OperatingUnit', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la fecha de creacion del registro', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'OperatingUnit', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'OperatingUnit', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario del sistema que creó el registro (default: 999). VARCHAR(20), requerido.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'OperatingUnit', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el usuario de creacion', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'OperatingUnit', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'OperatingUnit', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la ciudad donde se localiza la unidad operativa (FK a Common.City). Permite geografía de centros.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'OperatingUnit', @level2type = N'COLUMN', @level2name = N'IdCity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la ciudad', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'OperatingUnit', @level2type = N'COLUMN', @level2name = N'IdCity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'OperatingUnit', @level2type = N'COLUMN', @level2name = N'IdCity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Correo electrónico del auditor o responsable de auditoría para la unidad operativa. VARCHAR(100), PII sensible.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'OperatingUnit', @level2type = N'COLUMN', @level2name = N'EmailAudit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Email del auditor', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'OperatingUnit', @level2type = N'COLUMN', @level2name = N'EmailAudit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'OperatingUnit', @level2type = N'COLUMN', @level2name = N'EmailAudit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Correo electrónico institucional de contacto para la unidad operativa. VARCHAR(100).', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'OperatingUnit', @level2type = N'COLUMN', @level2name = N'Email';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Correo', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'OperatingUnit', @level2type = N'COLUMN', @level2name = N'Email';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'OperatingUnit', @level2type = N'COLUMN', @level2name = N'Email';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de teléfono de contacto de la unidad operativa o centro de atención. VARCHAR(20).', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'OperatingUnit', @level2type = N'COLUMN', @level2name = N'Phone';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Telefono', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'OperatingUnit', @level2type = N'COLUMN', @level2name = N'Phone';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'OperatingUnit', @level2type = N'COLUMN', @level2name = N'Phone';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dirección física, ubicación geográfica del centro de atención u unidad operativa. VARCHAR(100).', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'OperatingUnit', @level2type = N'COLUMN', @level2name = N'Address';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Direccion', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'OperatingUnit', @level2type = N'COLUMN', @level2name = N'Address';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'OperatingUnit', @level2type = N'COLUMN', @level2name = N'Address';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código oficial de la IPS (Institución Prestadora de Servicios) registrado ante MINSALUD. VARCHAR(20).', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'OperatingUnit', @level2type = N'COLUMN', @level2name = N'IPSCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la IPS', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'OperatingUnit', @level2type = N'COLUMN', @level2name = N'IPSCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'OperatingUnit', @level2type = N'COLUMN', @level2name = N'IPSCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código abreviado de la unidad operativa para búsquedas rápidas y reportes. VARCHAR(5).', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'OperatingUnit', @level2type = N'COLUMN', @level2name = N'UnitCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de la unidad operativa', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'OperatingUnit', @level2type = N'COLUMN', @level2name = N'UnitCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'OperatingUnit', @level2type = N'COLUMN', @level2name = N'UnitCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre descriptivo de la unidad operativa, centro de atención, IPS o sede. VARCHAR(100), requerido.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'OperatingUnit', @level2type = N'COLUMN', @level2name = N'UnitName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de la unidad operativa', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'OperatingUnit', @level2type = N'COLUMN', @level2name = N'UnitName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'OperatingUnit', @level2type = N'COLUMN', @level2name = N'UnitName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la unidad operativa padre (FK autorreferencial). Permite jerarquía de centros de atención o sedes.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'OperatingUnit', @level2type = N'COLUMN', @level2name = N'IdUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la unidad', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'OperatingUnit', @level2type = N'COLUMN', @level2name = N'IdUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'OperatingUnit', @level2type = N'COLUMN', @level2name = N'IdUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (PK) del registro de unidad operativa en el sistema. INT IDENTITY.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'OperatingUnit', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del registro', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'OperatingUnit', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'OperatingUnit', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidades operativas o sedes de la institución de salud (IPS). Registra cada centro de atención, clínica, hospital o punto de servicio con su nombre, código, dirección, teléfono y correo, permitiendo identificar dónde se prestan los servicios.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'OperatingUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'OperatingUnit';
