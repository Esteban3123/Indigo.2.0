CREATE TABLE [dbo].[HCPERFILFMD] (
    [IDPERFILFMD]     INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDPERFILFD]      INT       NOT NULL,
    [CODPRODUC]       CHAR (20) NOT NULL,
    [SHIPPINGPRODUCT] BIT       NOT NULL,
    CONSTRAINT [PK__HCPERFIL__21E33C86510AB26B] PRIMARY KEY CLUSTERED ([IDPERFILFMD] ASC),
    CONSTRAINT [fk_PerfilFarmacoMedica] FOREIGN KEY ([IDPERFILFMD]) REFERENCES [dbo].[HCPERFILFD] ([IDPERFILFD])
);




GO


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de medicamentos o productos asociados a perfiles de formulación médica. Relaciona cada perfil de formulación con los productos farmacéuticos que lo componen y si aplica envío o despacho.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPERFILFMD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPERFILFMD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único interno del registro de producto en el perfil de formulación médica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPERFILFMD', @level2type = N'COLUMN', @level2name = N'IDPERFILFMD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPERFILFMD', @level2type = N'COLUMN', @level2name = N'IDPERFILFMD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del perfil de formulación al que pertenece este producto; relaciona con la cabecera del perfil.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPERFILFMD', @level2type = N'COLUMN', @level2name = N'IDPERFILFD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPERFILFMD', @level2type = N'COLUMN', @level2name = N'IDPERFILFD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del medicamento o producto farmacéutico incluido en el perfil de formulación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPERFILFMD', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPERFILFMD', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si el producto requiere envío o despacho (domicilio, farmacia externa, etc.); verdadero = sí aplica despacho.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPERFILFMD', @level2type = N'COLUMN', @level2name = N'SHIPPINGPRODUCT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPERFILFMD', @level2type = N'COLUMN', @level2name = N'SHIPPINGPRODUCT';
