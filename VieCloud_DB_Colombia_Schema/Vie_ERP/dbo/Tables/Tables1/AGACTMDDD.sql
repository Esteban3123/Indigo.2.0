CREATE TABLE [dbo].[AGACTMDDD] (
    [AUTONUMER]                INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODACTMED]                CHAR (3)        NOT NULL,
    [CODSERIPS]                CHAR (20)       NOT NULL,
    [TIPOPARAM]                NCHAR (1)       NOT NULL,
    [CONCECREC]                INT             NULL,
    [CODPRODUC]                CHAR (20)       NULL,
    [CANTRECUR]                INT             NULL,
    [REACALAUT]                BIT             NULL,
    [MEDIOCONT]                BIT             NULL,
    [APLRANEDA]                BIT             NULL,
    [MLPRODUCT]                NUMERIC (10, 2) NULL,
    [KGPRODUCT]                NUMERIC (10, 2) NULL,
    [EDADESDE]                 TINYINT         NULL,
    [EDAHASTA]                 TINYINT         NULL,
    [PRODAUTON]                INT             NULL,
    [IDDESCRIPCIONRELACIONADA] INT             NULL,
    CONSTRAINT [PK_AGACTMDDD] PRIMARY KEY CLUSTERED ([AUTONUMER] ASC),
    CONSTRAINT [FK_AGACTMDDD_AGERECURS] FOREIGN KEY ([CONCECREC]) REFERENCES [dbo].[AGERECURS] ([CODCONCEC]),
    CONSTRAINT [FK_AGACTMDDD_SOLPRODUC] FOREIGN KEY ([PRODAUTON]) REFERENCES [dbo].[SOLPRODUC] ([PRODAUTON])
);




GO



GO



GO
CREATE NONCLUSTERED INDEX [IX_AGACTMDDD]
    ON [dbo].[AGACTMDDD]([CODSERIPS] ASC, [CODACTMED] ASC, [IDDESCRIPCIONRELACIONADA] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de descripción de relación con VIE ERP (contract.CUPSEntityContractDescriptions). Referencia a descripción adicional del contrato o acuerdo de servicios. INT, clave de integración.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTMDDD', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de descripcion de relacion. relacion con  VIE ERP  (contract.CUPSEntityContractDescriptions) ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTMDDD', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTMDDD', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Autonumérico del producto. FK a SOLPRODUC.PRODAUTON. Identifica unívocamente el producto, insumo o material asociado. INT IDENTITY.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTMDDD', @level2type = N'COLUMN', @level2name = N'PRODAUTON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene el autonumerico del producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTMDDD', @level2type = N'COLUMN', @level2name = N'PRODAUTON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTMDDD', @level2type = N'COLUMN', @level2name = N'PRODAUTON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Edad máxima en años. Límite superior del rango etario aplicable a la actividad médica. TINYINT (0-255 años).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTMDDD', @level2type = N'COLUMN', @level2name = N'EDAHASTA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Edad Hasta (Años)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTMDDD', @level2type = N'COLUMN', @level2name = N'EDAHASTA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTMDDD', @level2type = N'COLUMN', @level2name = N'EDAHASTA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Edad mínima en años. Límite inferior del rango etario aplicable a la actividad médica. TINYINT (0-255 años).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTMDDD', @level2type = N'COLUMN', @level2name = N'EDADESDE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Edad Desde (Años)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTMDDD', @level2type = N'COLUMN', @level2name = N'EDADESDE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTMDDD', @level2type = N'COLUMN', @level2name = N'EDADESDE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad en kilogramos del producto. Unidad de peso para materiales, medicamentos o insumos. NUMERIC(10,2).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTMDDD', @level2type = N'COLUMN', @level2name = N'KGPRODUCT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'KiloGramos(kg) del producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTMDDD', @level2type = N'COLUMN', @level2name = N'KGPRODUCT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTMDDD', @level2type = N'COLUMN', @level2name = N'KGPRODUCT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad en mililitros del producto. Unidad de volumen para líquidos, medicamentos o soluciones. NUMERIC(10,2).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTMDDD', @level2type = N'COLUMN', @level2name = N'MLPRODUCT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'MiliLitros(ml) del producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTMDDD', @level2type = N'COLUMN', @level2name = N'MLPRODUCT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTMDDD', @level2type = N'COLUMN', @level2name = N'MLPRODUCT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador: aplica restricción por rango de edad. BIT (0=no aplica, 1=sí aplica) para filtrar por EDADESDE/EDAHASTA.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTMDDD', @level2type = N'COLUMN', @level2name = N'APLRANEDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Aplica rango de edad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTMDDD', @level2type = N'COLUMN', @level2name = N'APLRANEDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTMDDD', @level2type = N'COLUMN', @level2name = N'APLRANEDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador: contiene medios de contraste. BIT (0=no, 1=sí). Marcador para procedimientos radiológicos que requieren contraste.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTMDDD', @level2type = N'COLUMN', @level2name = N'MEDIOCONT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Medios de contraste', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTMDDD', @level2type = N'COLUMN', @level2name = N'MEDIOCONT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTMDDD', @level2type = N'COLUMN', @level2name = N'MEDIOCONT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador: realizar cálculo automático de cantidad o dosificación. BIT (0=manual, 1=automático).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTMDDD', @level2type = N'COLUMN', @level2name = N'REACALAUT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Realizar Calculo Automatico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTMDDD', @level2type = N'COLUMN', @level2name = N'REACALAUT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTMDDD', @level2type = N'COLUMN', @level2name = N'REACALAUT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de recursos o productos utilizados. Número de unidades requeridas en la actividad médica. INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTMDDD', @level2type = N'COLUMN', @level2name = N'CANTRECUR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'cantidad utilizada de los recursos-productos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTMDDD', @level2type = N'COLUMN', @level2name = N'CANTRECUR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTMDDD', @level2type = N'COLUMN', @level2name = N'CANTRECUR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código interno del producto, insumo o material. CHAR(20). Identificador del sistema para búsquedas y trazabilidad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTMDDD', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Interno del Producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTMDDD', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTMDDD', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número consecutivo identificador del recurso. FK a AGERECURS.CODCONCEC. Vincula costos y recursos del servicio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTMDDD', @level2type = N'COLUMN', @level2name = N'CONCECREC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'numero consecutivo que identifica el recurso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTMDDD', @level2type = N'COLUMN', @level2name = N'CONCECREC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTMDDD', @level2type = N'COLUMN', @level2name = N'CONCECREC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de parámetro agregado a la actividad médica. NCHAR(1). Clasifica si es componente, recurso, insumo o variable.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTMDDD', @level2type = N'COLUMN', @level2name = N'TIPOPARAM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'define tipo de parametro agregado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTMDDD', @level2type = N'COLUMN', @level2name = N'TIPOPARAM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTMDDD', @level2type = N'COLUMN', @level2name = N'TIPOPARAM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de servicio IPS según RIPS. CHAR(20). Identifica el servicio ambulatorio, hospitalario o procedimiento con cobertura.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTMDDD', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'codigo del servicio ips asociado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTMDDD', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTMDDD', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de actividad médica asociada. CHAR(3). Clasifica procedimiento, consulta, examen, diagnóstico o intervención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTMDDD', @level2type = N'COLUMN', @level2name = N'CODACTMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'codigo de la actividad medica asociada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTMDDD', @level2type = N'COLUMN', @level2name = N'CODACTMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTMDDD', @level2type = N'COLUMN', @level2name = N'CODACTMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Autonumérico único de la tabla. INT IDENTITY(1,1). Clave primaria para identificar cada registro de actividad-recurso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTMDDD', @level2type = N'COLUMN', @level2name = N'AUTONUMER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'autonumerico ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTMDDD', @level2type = N'COLUMN', @level2name = N'AUTONUMER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTMDDD', @level2type = N'COLUMN', @level2name = N'AUTONUMER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Parámetros y recursos asociados a cada actividad médica (procedimiento o servicio CUPS): define qué insumos, productos o condiciones se requieren para ejecutar una actividad médica, incluyendo cantidades, rangos de edad y reglas de aplicación automática.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTMDDD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTMDDD';
