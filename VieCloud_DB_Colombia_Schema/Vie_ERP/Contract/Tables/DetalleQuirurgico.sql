CREATE TABLE [Contract].[DetalleQuirurgico] (
    [Code]           NVARCHAR (255) NULL,
    [hijo]           NVARCHAR (255) NULL,
    [ServiceAmount]  NVARCHAR (255) NULL,
    [DefaultService] NVARCHAR (255) NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Servicio quirúrgico predeterminado asociado al detalle; procedimiento por defecto aplicado en contrato de servicios de salud.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DetalleQuirurgico', @level2type = N'COLUMN', @level2name = N'DefaultService';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el servicio predeterminado.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DetalleQuirurgico', @level2type = N'COLUMN', @level2name = N'DefaultService';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DetalleQuirurgico', @level2type = N'COLUMN', @level2name = N'DefaultService';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Monto, valor o tarifa del servicio quirúrgico; importe económico del procedimiento en contrato.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DetalleQuirurgico', @level2type = N'COLUMN', @level2name = N'ServiceAmount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el monto del servicio.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DetalleQuirurgico', @level2type = N'COLUMN', @level2name = N'ServiceAmount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DetalleQuirurgico', @level2type = N'COLUMN', @level2name = N'ServiceAmount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de relación jerárquica hijo del detalle quirúrgico; componente o subprocedimiento vinculado al procedimiento principal.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DetalleQuirurgico', @level2type = N'COLUMN', @level2name = N'hijo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena hijo del detalle quirurgico.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DetalleQuirurgico', @level2type = N'COLUMN', @level2name = N'hijo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DetalleQuirurgico', @level2type = N'COLUMN', @level2name = N'hijo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del detalle quirúrgico; identificador alfanumérico del procedimiento quirúrgico en contrato de servicios.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DetalleQuirurgico', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del Detalle Quirurgico.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DetalleQuirurgico', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DetalleQuirurgico', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle quirúrgico de contrato: relaciona servicios o procedimientos quirúrgicos con sus componentes o servicios hijos, indicando cantidades y servicios por defecto asociados a cada código de procedimiento dentro de un contrato.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DetalleQuirurgico';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'DetalleQuirurgico';
