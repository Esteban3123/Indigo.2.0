CREATE TABLE [MedicalFees].[GlosaMedicalFees] (
    [Id]               INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]             VARCHAR (25)    NOT NULL,
    [DocumentDate]     DATETIME        NOT NULL,
    [SupplierId]       INT             NOT NULL,
    [Observation]      VARCHAR (300)   NULL,
    [PendingValue]     DECIMAL (18, 2) NOT NULL,
    [GlossedValue]     DECIMAL (18, 2) NOT NULL,
    [Status]           TINYINT         NOT NULL,
    [CreationUser]     VARCHAR (20)    NOT NULL,
    [CreationDate]     DATETIME        NOT NULL,
    [ModificationUser] VARCHAR (20)    NULL,
    [ModificationDate] DATETIME        NULL,
    [ConfirmationUser] VARCHAR (20)    NULL,
    [ConfirmationDate] DATETIME        NULL,
    [AnnulmentUser]    VARCHAR (20)    NULL,
    [AnnulmentDate]    DATETIME        NULL,
    [TimeStamp]        ROWVERSION      NOT NULL,
    CONSTRAINT [PK_GlosaMedicalFees__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_GlosaMedicalFees_Supplier] FOREIGN KEY ([SupplierId]) REFERENCES [Common].[Supplier] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal automática (TIMESTAMP SQL Server) que registra el instante exacto de creación, modificación, confirmación o anulación del registro de glosa. Tipo BINARY(8), no editable, auditoria de cambios.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFees', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFees', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFees', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de anulación de la glosa (DATETIME). Cuando el registro pasa a estado Anulado (3). Null si no ha sido anulado.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFees', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Anulación', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFees', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFees', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario o profesional de salud que anuló la glosa (VARCHAR 20). Credencial de quien ejecutó la anulación del registro.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFees', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Anulación', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFees', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFees', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de confirmación de la glosa (DATETIME). Cuando el registro pasa a estado Confirmado (2). Null si aún está en proceso.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFees', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Confirmación', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFees', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFees', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario o profesional de salud que confirmó la glosa (VARCHAR 20). Credencial de quien validó y confirmó el registro.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFees', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Confirmación', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFees', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFees', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de última modificación del registro de glosa (DATETIME). Null si no ha sido modificado desde su creación.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFees', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFees', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFees', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario o profesional de salud que modificó por última vez la glosa (VARCHAR 20). Null si no ha sufrido cambios.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFees', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFees', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFees', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación inicial del registro de glosa (DATETIME). Marca auditoria de ingreso al sistema.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFees', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFees', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFees', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario o profesional de salud que creó el registro de glosa (VARCHAR 20). Credencial obligatoria de quien registró.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFees', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFees', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFees', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado actual de la glosa (TINYINT): 1=Registrado, 2=Confirmado, 3=Anulado. Indica el ciclo de vida y validación de la glosa.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFees', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del registro (Registrado = 1, Confirmado = 2, Anulado = 3)', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFees', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFees', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Monto monetario glosado (DECIMAL 18,2). Valor en COP que el proveedor o aseguradora observa, rechaza o cuestiona en la factura.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFees', @level2type = N'COLUMN', @level2name = N'GlossedValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Glosado', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFees', @level2type = N'COLUMN', @level2name = N'GlossedValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFees', @level2type = N'COLUMN', @level2name = N'GlossedValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Monto monetario pendiente de resolver (DECIMAL 18,2). Diferencia o saldo en disputa entre proveedor e IPS/aseguradora por cobrar/aclarar.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFees', @level2type = N'COLUMN', @level2name = N'PendingValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor pendiente', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFees', @level2type = N'COLUMN', @level2name = N'PendingValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFees', @level2type = N'COLUMN', @level2name = N'PendingValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Notas, comentarios o justificación de la glosa (VARCHAR 300, nullable). Detalle del motivo de rechazo, reclamo o ajuste en la factura.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFees', @level2type = N'COLUMN', @level2name = N'Observation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observación', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFees', @level2type = N'COLUMN', @level2name = N'Observation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFees', @level2type = N'COLUMN', @level2name = N'Observation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del proveedor/IPS (INT, FK→Common.Supplier.Id). Vincula la glosa al centro de atención, laboratorio, clínica o prestador de servicios.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFees', @level2type = N'COLUMN', @level2name = N'SupplierId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Proveedor', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFees', @level2type = N'COLUMN', @level2name = N'SupplierId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFees', @level2type = N'COLUMN', @level2name = N'SupplierId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de emisión del documento de glosa (DATETIME). Referencia temporal de cuándo se originó la observación, reclamo o ajuste a facturación.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFees', @level2type = N'COLUMN', @level2name = N'DocumentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha del documento ', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFees', @level2type = N'COLUMN', @level2name = N'DocumentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFees', @level2type = N'COLUMN', @level2name = N'DocumentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único o número de referencia de la glosa (VARCHAR 25). Identificador alfanumérico para búsqueda rápida y trazabilidad en RIPS/facturación.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFees', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFees', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFees', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro de glosa (INT, Identity, PK). Clave primaria auto-incremental para referencia interna en la base de datos.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFees', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del registro', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFees', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFees', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Encabezados de glosas médicas generadas a proveedores o prestadores de servicios de salud. Registra el valor glosado, el valor pendiente de resolución, el estado de la glosa y la trazabilidad de creación, modificación, confirmación y anulación de cada documento de glosa.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFees';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFees';
