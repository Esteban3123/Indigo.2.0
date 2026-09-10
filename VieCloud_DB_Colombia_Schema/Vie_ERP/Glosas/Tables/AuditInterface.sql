CREATE TABLE [Glosas].[AuditInterface] (
    [Id]             INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [OptionRegister] VARCHAR (50) NOT NULL,
    [NumberGlosa]    VARCHAR (50) NOT NULL,
    [InvoiceNumber]  VARCHAR (50) NOT NULL,
    [ActionCode]     INT          NOT NULL,
    [Concept]        VARCHAR (50) NOT NULL,
    [NewConsecutive] VARCHAR (50) NOT NULL,
    [TimeStamp]      ROWVERSION   NOT NULL,
    [Date]           DATETIME     CONSTRAINT [DF_AuditInterface_Date] DEFAULT ([Common].[getdate]()) NULL,
    CONSTRAINT [PK_AuditInterface] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de registro o auditoría del evento en la glosa. Tipo DATETIME, generada automáticamente con la función getdate() del sistema.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AuditInterface', @level2type = N'COLUMN', @level2name = N'Date';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AuditInterface', @level2type = N'COLUMN', @level2name = N'Date';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AuditInterface', @level2type = N'COLUMN', @level2name = N'Date';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal (TIMESTAMP) que registra el instante exacto de creación, modificación o procesamiento del evento de auditoría en la interfaz de glosas.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AuditInterface', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AuditInterface', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AuditInterface', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nuevo número consecutivo o secuencial asignado al registro o documento generado en el proceso de auditoría de glosa.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AuditInterface', @level2type = N'COLUMN', @level2name = N'NewConsecutive';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nuevo consecutivo', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AuditInterface', @level2type = N'COLUMN', @level2name = N'NewConsecutive';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AuditInterface', @level2type = N'COLUMN', @level2name = N'NewConsecutive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Concepto o clasificación del evento auditado en la glosa (VARCHAR 50). Identifica el tipo o categoría del movimiento registrado.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AuditInterface', @level2type = N'COLUMN', @level2name = N'Concept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Concepto', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AuditInterface', @level2type = N'COLUMN', @level2name = N'Concept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AuditInterface', @level2type = N'COLUMN', @level2name = N'Concept';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de acción que clasifica el tipo de comprobante contable: 1=Comprobante Contable, 2=Nota Crédito, 3=Nota Débito. Determina el impacto financiero de la glosa.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AuditInterface', @level2type = N'COLUMN', @level2name = N'ActionCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1 Comprobante Contable  2 Nota Credito  3 Nota Debito', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AuditInterface', @level2type = N'COLUMN', @level2name = N'ActionCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AuditInterface', @level2type = N'COLUMN', @level2name = N'ActionCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número o identificador de la factura asociada a la glosa auditada. Vincula el registro a la factura original de atención o servicio de salud.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AuditInterface', @level2type = N'COLUMN', @level2name = N'InvoiceNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de factura', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AuditInterface', @level2type = N'COLUMN', @level2name = N'InvoiceNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AuditInterface', @level2type = N'COLUMN', @level2name = N'InvoiceNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número único o identificador de la glosa (reclamo, objeción o reclamación). Permite rastrear el caso de glosa en procesos de auditoría y facturación.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AuditInterface', @level2type = N'COLUMN', @level2name = N'NumberGlosa';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de glosa', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AuditInterface', @level2type = N'COLUMN', @level2name = N'NumberGlosa';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AuditInterface', @level2type = N'COLUMN', @level2name = N'NumberGlosa';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de opciones o tipo de operación seleccionada en la interfaz de auditoría de glosas. Documenta la acción elegida en el proceso.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AuditInterface', @level2type = N'COLUMN', @level2name = N'OptionRegister';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Registro de opciones', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AuditInterface', @level2type = N'COLUMN', @level2name = N'OptionRegister';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AuditInterface', @level2type = N'COLUMN', @level2name = N'OptionRegister';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincremental (INT IDENTITY) que representa cada registro de auditoría en la tabla AuditInterface.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AuditInterface', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AuditInterface', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AuditInterface', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de auditoría de la interfaz de glosas: guarda cada acción ejecutada sobre una glosa (creación, modificación, respuesta), vinculando el número de glosa con la factura afectada, el tipo de acción realizada y el consecutivo generado. Permite trazabilidad y control de cambios en el proceso de glosas.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AuditInterface';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AuditInterface';
