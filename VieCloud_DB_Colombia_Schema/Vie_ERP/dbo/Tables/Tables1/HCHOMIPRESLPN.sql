CREATE TABLE [dbo].[HCHOMIPRESLPN] (
    [ID]              INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODMIPRES]       VARCHAR (7)    NOT NULL,
    [NOMBRECOMERCIAL] VARCHAR (60)   NOT NULL,
    [GRUPONIVEL]      VARCHAR (20)   NOT NULL,
    [FORMA]           VARCHAR (20)   NOT NULL,
    [PRESENTACION]    NUMERIC (7, 2) NOT NULL,
    [UNIDAD]          VARCHAR (3)    NOT NULL,
    [ESTADO]          BIT            NOT NULL,
    CONSTRAINT [PK__HCHOMIPRESLPN] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del registro (BIT) en MIPRES: 1=Activo, 0=Inactivo, indica vigencia del producto de nutrición', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOMIPRESLPN', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado de registro en mipres', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOMIPRESLPN', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOMIPRESLPN', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de medida (VARCHAR 3) de presentación del producto de nutrición: mg, ml, g, etc.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOMIPRESLPN', @level2type = N'COLUMN', @level2name = N'UNIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad de lista de producto nutrición', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOMIPRESLPN', @level2type = N'COLUMN', @level2name = N'UNIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOMIPRESLPN', @level2type = N'COLUMN', @level2name = N'UNIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Presentación comercial (NUMERIC 7,2) cantidad/dosis del producto de nutrición por unidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOMIPRESLPN', @level2type = N'COLUMN', @level2name = N'PRESENTACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Presentación comercial de lista de producto nutrición', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOMIPRESLPN', @level2type = N'COLUMN', @level2name = N'PRESENTACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOMIPRESLPN', @level2type = N'COLUMN', @level2name = N'PRESENTACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Forma farmacéutica/presentación tipo (VARCHAR 20) del producto de nutrición: polvo, líquido, cápsula, etc.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOMIPRESLPN', @level2type = N'COLUMN', @level2name = N'FORMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Forma de lista de producto nutrición', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOMIPRESLPN', @level2type = N'COLUMN', @level2name = N'FORMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOMIPRESLPN', @level2type = N'COLUMN', @level2name = N'FORMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Grupo nivel 1 (VARCHAR 20) de clasificación del producto de nutrición en catálogo MIPRES', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOMIPRESLPN', @level2type = N'COLUMN', @level2name = N'GRUPONIVEL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Grupo nivel1 de lista de producto nutrición', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOMIPRESLPN', @level2type = N'COLUMN', @level2name = N'GRUPONIVEL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOMIPRESLPN', @level2type = N'COLUMN', @level2name = N'GRUPONIVEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre comercial (VARCHAR 60) del producto de nutrición registrado en MIPRES', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOMIPRESLPN', @level2type = N'COLUMN', @level2name = N'NOMBRECOMERCIAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre comercial de lista de producto nutrición', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOMIPRESLPN', @level2type = N'COLUMN', @level2name = N'NOMBRECOMERCIAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOMIPRESLPN', @level2type = N'COLUMN', @level2name = N'NOMBRECOMERCIAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código MIPRES (VARCHAR 7) de producto de nutrición, identificador en lista de medicamentos/productos MIPRES', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOMIPRESLPN', @level2type = N'COLUMN', @level2name = N'CODMIPRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de lista de producto nutrición mipres', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOMIPRESLPN', @level2type = N'COLUMN', @level2name = N'CODMIPRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOMIPRESLPN', @level2type = N'COLUMN', @level2name = N'CODMIPRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT, PK) de registro en tabla HCHOMIPRESLPN, clave primaria autonumerada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOMIPRESLPN', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador  de la tabla ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOMIPRESLPN', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOMIPRESLPN', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de medicamentos MIPRES con Listado de Precios de Notificación (LPN). Registra los medicamentos autorizados por MIPRES con su nombre comercial, presentación, forma farmacéutica y estado de vigencia, usado para validar prescripciones y dispensación de medicamentos no incluidos en el plan de beneficios.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOMIPRESLPN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOMIPRESLPN';
