CREATE TABLE [Billing].[MipresCode] (
    [Id]                   INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ServiceOrderDetailId] INT          NOT NULL,
    [Code]                 VARCHAR (20) NOT NULL,
    [CreationUser]         VARCHAR (20) CONSTRAINT [DF_MipresCode_CreationUser] DEFAULT ((999)) NOT NULL,
    [CreationDate]         DATETIME     CONSTRAINT [DF_MipresCode_CreationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [IdMipres]             VARCHAR (20) NULL,
    CONSTRAINT [PK_MipresCode] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MipresCode_ServiceOrderDetail] FOREIGN KEY ([ServiceOrderDetailId]) REFERENCES [Billing].[ServiceOrderDetail] ([Id]),
    CONSTRAINT [IX_MipresCode] UNIQUE NONCLUSTERED ([ServiceOrderDetailId] ASC, [Code] ASC)
);




GO




GO
-- Índice optimizado para consultas que requieren ServiceOrderDetailId, Code e IdMipres



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único asignado por el sistema MIPRES (Ministerio de Protección Social). Referencia de la prescripción de medicamentos, dispositivos médicos y procedimientos en el régimen de salud colombiano. VARCHAR(20), opcional, permite trazabilidad normativa.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'MipresCode', @level2type = N'COLUMN', @level2name = N'IdMipres';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene el Id del mipres', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'MipresCode', @level2type = N'COLUMN', @level2name = N'IdMipres';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'MipresCode', @level2type = N'COLUMN', @level2name = N'IdMipres';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro del código MIPRES en el sistema. DATETIME, valor por defecto fecha-hora actual. Auditoría de ingreso de prescripciones.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'MipresCode', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'MipresCode', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'MipresCode', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario o identificación de quien registró el código MIPRES en el sistema. VARCHAR(20), valor por defecto 999 (sistema). Trazabilidad de responsable de carga.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'MipresCode', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'MipresCode', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'MipresCode', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código MIPRES de medicamento, dispositivo médico o procedimiento prescrito. VARCHAR(20), obligatorio, clave única combinada con ServiceOrderDetailId. Identificación normativa de prescripción según MIPRES.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'MipresCode', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código Mipres', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'MipresCode', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'MipresCode', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea que referencia el detalle específico de la orden de servicio/atención médica. INT, obligatorio, vincula la prescripción MIPRES a la orden de servicio. FK a [Billing].[ServiceOrderDetail].', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'MipresCode', @level2type = N'COLUMN', @level2name = N'ServiceOrderDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle de la órden de servicio', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'MipresCode', @level2type = N'COLUMN', @level2name = N'ServiceOrderDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'MipresCode', @level2type = N'COLUMN', @level2name = N'ServiceOrderDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (clave primaria) del registro de código MIPRES. INT IDENTITY(1,1), agrupación de prescripciones MIPRES en el módulo de facturación y control de medicamentos.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'MipresCode', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador único del registro del código Mipres.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'MipresCode', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'MipresCode', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Códigos MIPRES asociados a detalles de órdenes de servicio. Registra el número de prescripción MIPRES (sistema de prescripción de tecnologías no financiadas por la UPC) vinculado a cada ítem de orden, permitiendo trazabilidad entre la facturación y las prescripciones autorizadas por el Ministerio de Salud.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'MipresCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'MipresCode';
