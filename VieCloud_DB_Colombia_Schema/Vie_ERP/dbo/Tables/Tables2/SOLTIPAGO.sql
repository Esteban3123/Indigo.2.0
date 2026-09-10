CREATE TABLE [dbo].[SOLTIPAGO] (
    [Autonumerico] TINYINT      IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Codigo]       VARCHAR (3)  NOT NULL,
    [Descripcion]  VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_SOLTIPAG] PRIMARY KEY CLUSTERED ([Autonumerico] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción del tipo de pago; texto que identifica la modalidad de pago (efectivo, tarjeta, cheque, transferencia, crédito, bonificación, etc.) utilizada en transacciones de facturación, cobros y liquidaciones.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLTIPAGO', @level2type = N'COLUMN', @level2name = N'Descripcion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene la descripcion del tipo de pago', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLTIPAGO', @level2type = N'COLUMN', @level2name = N'Descripcion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLTIPAGO', @level2type = N'COLUMN', @level2name = N'Descripcion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del tipo de pago; identificador alfanumérico (VARCHAR 3) que clasifica la forma de pago en facturas, recibos, liquidaciones y reportes de ingresos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLTIPAGO', @level2type = N'COLUMN', @level2name = N'Codigo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene el codigo del tipo de pago', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLTIPAGO', @level2type = N'COLUMN', @level2name = N'Codigo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLTIPAGO', @level2type = N'COLUMN', @level2name = N'Codigo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de la tabla SOLTIPAGO; clave primaria autoincrementable (TINYINT) que indexa cada registro de tipo de pago en el sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLTIPAGO', @level2type = N'COLUMN', @level2name = N'Autonumerico';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene el autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLTIPAGO', @level2type = N'COLUMN', @level2name = N'Autonumerico';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLTIPAGO', @level2type = N'COLUMN', @level2name = N'Autonumerico';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo maestro de tipos de pago disponibles en el sistema, como efectivo, tarjeta, cheque u otros medios de pago aceptados.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLTIPAGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLTIPAGO';
