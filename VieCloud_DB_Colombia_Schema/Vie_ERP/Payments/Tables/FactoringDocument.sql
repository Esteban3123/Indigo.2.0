CREATE TABLE [Payments].[FactoringDocument] (
    [Id]               INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]             VARCHAR (20)  NOT NULL,
    [DocumentDate]     DATETIME      NOT NULL,
    [Description]      VARCHAR (300) NOT NULL,
    [Status]           TINYINT       CONSTRAINT [DF__Factoring__Statu__72225B8C] DEFAULT ((1)) NOT NULL,
    [CreationUser]     VARCHAR (20)  NOT NULL,
    [CreationDate]     DATETIME      NOT NULL,
    [ModificationUser] VARCHAR (20)  NULL,
    [ModificationDate] DATETIME      NULL,
    [TimeStamp]        ROWVERSION    NOT NULL,
    [CodeHash]         VARCHAR (36)  NULL,
    CONSTRAINT [PK_FactoringDocument] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hash criptográfico (VARCHAR 36) generado para verificación de integridad del documento factoring, prevención de alteraciones, auditoria y validación de documento único.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'FactoringDocument', @level2type = N'COLUMN', @level2name = N'CodeHash';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo hash que viene de la verificacion', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'FactoringDocument', @level2type = N'COLUMN', @level2name = N'CodeHash';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'FactoringDocument', @level2type = N'COLUMN', @level2name = N'CodeHash';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal de auditoría (TIMESTAMP) que registra automáticamente el instante exacto de creación, modificación o cambio de estado del documento factoring.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'FactoringDocument', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'FactoringDocument', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'FactoringDocument', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de la última modificación del documento factoring; NULLABLE si no ha sido modificado desde su creación.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'FactoringDocument', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de modificación', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'FactoringDocument', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'FactoringDocument', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de usuario (VARCHAR 20) que realizó la última modificación del documento factoring; NULLABLE si no ha sido editado.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'FactoringDocument', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario que modifico', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'FactoringDocument', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'FactoringDocument', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de creación inicial del documento factoring en el sistema.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'FactoringDocument', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creación', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'FactoringDocument', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'FactoringDocument', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de usuario (VARCHAR 20) que creó originalmente el documento factoring en el sistema.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'FactoringDocument', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de creación', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'FactoringDocument', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'FactoringDocument', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del documento factoring (TINYINT, valores: 1=Guardado/Borrador, 2=Confirmado/Activo, 3=Anulado/Cancelado); por defecto 1.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'FactoringDocument', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del documento - 1:Guardado, 2:Confirmado, 3:anulado', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'FactoringDocument', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'FactoringDocument', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción narrativa (VARCHAR 300) del documento factoring, detalles comerciales o referencia de financiamiento.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'FactoringDocument', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion del documento factoring', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'FactoringDocument', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'FactoringDocument', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del documento (DATETIME) cuando se expide o se firma el acuerdo de factoring; clave para valoración y vencimiento.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'FactoringDocument', @level2type = N'COLUMN', @level2name = N'DocumentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del registro', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'FactoringDocument', @level2type = N'COLUMN', @level2name = N'DocumentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'FactoringDocument', @level2type = N'COLUMN', @level2name = N'DocumentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador único (VARCHAR 20) del documento factoring, referencia de búsqueda y link con procesos de cobro y contabilidad.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'FactoringDocument', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del documento factoring', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'FactoringDocument', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'FactoringDocument', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador numérico único incremental (INT IDENTITY) del documento factoring; clave primaria del registro.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'FactoringDocument', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id incremental del documento factoring', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'FactoringDocument', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'FactoringDocument', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Documentos de factoring (cesión o venta de cartera/facturas a un tercero financiero). Registra cada documento de factoring con su código, fecha, estado y trazabilidad de creación y modificación.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'FactoringDocument';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'FactoringDocument';
