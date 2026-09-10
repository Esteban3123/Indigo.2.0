CREATE TABLE [dbo].[INPLANTID] (
    [CODPLANTI] CHAR (5)   NOT NULL,
    [PKCONTROL] CHAR (30)  NOT NULL,
    [PKTIPCONT] INT        NOT NULL,
    [PKVALORCO] CHAR (300) NOT NULL,
    CONSTRAINT [PK_INPLANTID] PRIMARY KEY CLUSTERED ([CODPLANTI] ASC, [PKCONTROL] ASC),
    CONSTRAINT [FK_INPLANTID_INPLANTIC] FOREIGN KEY ([CODPLANTI]) REFERENCES [dbo].[INPLANTIC] ([CODPLANTI])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor o contenido reservado por arquitecto; almacena datos de configuración, parámetros o valores de control específicos del sistema (CHAR 300, PII potencial según contexto).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANTID', @level2type = N'COLUMN', @level2name = N'PKVALORCO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Reservado Arquitecto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANTID', @level2type = N'COLUMN', @level2name = N'PKVALORCO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANTID', @level2type = N'COLUMN', @level2name = N'PKVALORCO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de control reservado por arquitecto; clasificación numérica del control (1=TE, 2=BE, 3=DE, 4=PC, 5=RG, 6=LE, 7=ME, 8=CE); INT para identificar categoría o modalidad de validación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANTID', @level2type = N'COLUMN', @level2name = N'PKTIPCONT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Reservado Arquitecto:  1: TE  2: BE  3: DE  4: PC  5: RG  6: LE  7: ME  8: CE', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANTID', @level2type = N'COLUMN', @level2name = N'PKTIPCONT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANTID', @level2type = N'COLUMN', @level2name = N'PKTIPCONT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código o identificador reservado del control por arquitecto; clave primaria compuesta que referencia el tipo específico de control o regla aplicada (CHAR 30).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANTID', @level2type = N'COLUMN', @level2name = N'PKCONTROL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Reservado Arquitecto  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANTID', @level2type = N'COLUMN', @level2name = N'PKCONTROL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANTID', @level2type = N'COLUMN', @level2name = N'PKCONTROL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de plantilla para Indigo Crystal.Net; identificador único de la plantilla de configuración del sistema (CHAR 5, FK a INPLANTIC); referencia el template o estructura base.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANTID', @level2type = N'COLUMN', @level2name = N'CODPLANTI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Plantilla para Indigo Crystal.Net', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANTID', @level2type = N'COLUMN', @level2name = N'CODPLANTI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANTID', @level2type = N'COLUMN', @level2name = N'CODPLANTI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de identificadores de control por plantilla del sistema. Guarda las claves primarias y valores de control asociados a cada plantilla, permitiendo rastrear los registros maestros vinculados a formularios o plantillas configuradas en el ERP.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANTID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANTID';
