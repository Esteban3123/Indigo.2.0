CREATE TABLE [dbo].[SOL_AUTORI] (
    [AUTO]       INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [COMAUTO]    INT           NOT NULL,
    [FECHAUTORI] SMALLDATETIME NOT NULL,
    [CODUSUARI]  CHAR (20)     NOT NULL,
    [NOVEDADES]  BIT           NULL,
    CONSTRAINT [PK_SOL_AUTORI] PRIMARY KEY CLUSTERED ([AUTO] ASC),
    CONSTRAINT [FK_SOL_AUTORI_SOLCOMPRA] FOREIGN KEY ([COMAUTO]) REFERENCES [dbo].[SOLCOMPRA] ([COMAUTON])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT) que marca si existen cambios, observaciones o novedades en la autorización respecto a la solicitud original.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOL_AUTORI', @level2type = N'COLUMN', @level2name = N'NOVEDADES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'contiene las novedades de la utorizacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOL_AUTORI', @level2type = N'COLUMN', @level2name = N'NOVEDADES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOL_AUTORI', @level2type = N'COLUMN', @level2name = N'NOVEDADES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código o usuario (CHAR 20) del profesional/personal que autoriza la compra; trazabilidad de quien aprueba.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOL_AUTORI', @level2type = N'COLUMN', @level2name = N'CODUSUARI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'contiene el codigo del usuario que autoriza', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOL_AUTORI', @level2type = N'COLUMN', @level2name = N'CODUSUARI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOL_AUTORI', @level2type = N'COLUMN', @level2name = N'CODUSUARI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (SMALLDATETIME) en que se registra la autorización de la solicitud de compra; timestamp del evento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOL_AUTORI', @level2type = N'COLUMN', @level2name = N'FECHAUTORI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'contiene la fecha de autorizacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOL_AUTORI', @level2type = N'COLUMN', @level2name = N'FECHAUTORI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOL_AUTORI', @level2type = N'COLUMN', @level2name = N'FECHAUTORI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia a solicitud de compra (FK → SOLCOMPRA.COMAUTON); número secuencial que vincula a la orden de compra original.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOL_AUTORI', @level2type = N'COLUMN', @level2name = N'COMAUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'contiene el autonumerico de la solicitud de compra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOL_AUTORI', @level2type = N'COLUMN', @level2name = N'COMAUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOL_AUTORI', @level2type = N'COLUMN', @level2name = N'COMAUTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (IDENTITY, INT) de la autorización de compra; clave primaria de la tabla SOL_AUTORI.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOL_AUTORI', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene el autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOL_AUTORI', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOL_AUTORI', @level2type = N'COLUMN', @level2name = N'AUTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de autorizaciones realizadas en el sistema, indicando quién las aprobó, cuándo y si tienen novedades pendientes. Se usa para controlar el flujo de aprobaciones de solicitudes médicas o administrativas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOL_AUTORI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOL_AUTORI';
