CREATE TABLE [dbo].[INREPUNFU] (
    [CODCENATE]  NCHAR (10) NOT NULL,
    [UFUCODIGO]  NCHAR (10) NOT NULL,
    [CODREPORT]  NCHAR (10) NOT NULL,
    [PERMITEIMP] BIT        NOT NULL,
    [NUMCOPIAS]  NCHAR (10) NULL,
    [PERMODCOP]  BIT        NULL,
    CONSTRAINT [PK_INREPUNFU] PRIMARY KEY CLUSTERED ([CODCENATE] ASC, [UFUCODIGO] ASC, [CODREPORT] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera (BIT) que indica si se permite modificar y copiar el reporte; controla permisos de edición y duplicación de reportes en la unidad funcional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INREPUNFU', @level2type = N'COLUMN', @level2name = N'PERMODCOP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Permitir modificar y copiar', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INREPUNFU', @level2type = N'COLUMN', @level2name = N'PERMODCOP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INREPUNFU', @level2type = N'COLUMN', @level2name = N'PERMODCOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad numérica de copias a imprimir del reporte; define el número de ejemplares generados en cada impresión (NCHAR).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INREPUNFU', @level2type = N'COLUMN', @level2name = N'NUMCOPIAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de copias a imprimir', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INREPUNFU', @level2type = N'COLUMN', @level2name = N'NUMCOPIAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INREPUNFU', @level2type = N'COLUMN', @level2name = N'NUMCOPIAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera (BIT) que habilita o deshabilita la impresión del reporte: 1=habilitado/sí, 0=deshabilitado/no; controla disponibilidad de impresión.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INREPUNFU', @level2type = N'COLUMN', @level2name = N'PERMITEIMP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Permite identificar si el reporte se encuetra habilitado 1:si  2:no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INREPUNFU', @level2type = N'COLUMN', @level2name = N'PERMITEIMP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INREPUNFU', @level2type = N'COLUMN', @level2name = N'PERMITEIMP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del reporte (NCHAR 10); identificador del template o plantilla de reporte configurado en el sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INREPUNFU', @level2type = N'COLUMN', @level2name = N'CODREPORT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo reporte', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INREPUNFU', @level2type = N'COLUMN', @level2name = N'CODREPORT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INREPUNFU', @level2type = N'COLUMN', @level2name = N'CODREPORT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la unidad funcional (NCHAR 10); identifica el área clínica o administrativa que usa el reporte (FK a tabla de unidades).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INREPUNFU', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la unidad funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INREPUNFU', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INREPUNFU', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención (NCHAR 10); identifica la institución o sede donde se configura el reporte (FK a tabla de centros).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INREPUNFU', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del centro de atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INREPUNFU', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INREPUNFU', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Configuración de reportes o documentos habilitados por unidad funcional y centro de atención: indica qué reportes se pueden imprimir en cada unidad, cuántas copias se generan y si el usuario puede modificar esa cantidad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INREPUNFU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INREPUNFU';
