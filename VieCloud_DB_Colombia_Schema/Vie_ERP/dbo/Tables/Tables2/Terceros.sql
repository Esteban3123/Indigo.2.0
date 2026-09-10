CREATE TABLE [dbo].[Terceros] (
    [NumeroIdentificacion] VARCHAR (50)  NULL,
    [DigitoVerificacion]   TINYINT       NULL,
    [Nombre]               VARCHAR (200) NULL,
    [TipoPersona]          TINYINT       NULL,
    [TipoRetencion]        TINYINT       NULL,
    [TipoContribuyente]    TINYINT       NULL,
    [ManejaICA]            TINYINT       NULL,
    [PorcICA]              TINYINT       NULL,
    [TopeICA]              TINYINT       NULL,
    [ValorTopeICA]         TINYINT       NULL
);
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla que almacena información tributaria y fiscal de terceros (personas naturales o jurídicas), incluyendo su número de identificación con dígito de verificación, tipo de persona, régimen de retención y condición de contribuyente. Registra además la configuración del Impuesto de Industria y Comercio (ICA), con porcentaje, tope y valor tope aplicables, lo que sugiere uso en procesos de facturación o contabilidad fiscal.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Terceros';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Terceros';
GO
