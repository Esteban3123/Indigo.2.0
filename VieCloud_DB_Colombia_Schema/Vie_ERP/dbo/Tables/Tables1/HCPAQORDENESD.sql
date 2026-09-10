CREATE TABLE [dbo].[HCPAQORDENESD] (
    [ID]                       INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCPAQORDENESC]          INT           NOT NULL,
    [CODIGOSERVICIO]           CHAR (20)     NOT NULL,
    [TIPOSERVICIO]             INT           NULL,
    [IDDESCRIPCIONRELACIONADA] INT           NULL,
    [OrderQuantity]            INT           NULL,
    [ClinicalDataRelevant]     VARCHAR (500) NULL,
    CONSTRAINT [PK_HCPAQORDENESD] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCPAQORDENESD_HCPAQORDENESC] FOREIGN KEY ([IDHCPAQORDENESC]) REFERENCES [dbo].[HCPAQORDENESC] ([ID])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo de texto (VARCHAR 500) para almacenar datos clínicos relevantes, hallazgos, observaciones o notas médicas asociadas a la orden de servicio (laboratorio, imagen, procedimiento, medicamento, interconsulta, insumo).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAQORDENESD', @level2type = N'COLUMN', @level2name = N'ClinicalDataRelevant';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo para almacenar los datos clínicos relevantes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAQORDENESD', @level2type = N'COLUMN', @level2name = N'ClinicalDataRelevant';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAQORDENESD', @level2type = N'COLUMN', @level2name = N'ClinicalDataRelevant';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad numérica (INT) registrada para la orden en el formulario de Paquetes de órdenes; representa unidades, dosis o repeticiones del servicio solicitado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAQORDENESD', @level2type = N'COLUMN', @level2name = N'OrderQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene la cantidad registrada para la orden en page de ordenes del formulario de Paquetes de ordenes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAQORDENESD', @level2type = N'COLUMN', @level2name = N'OrderQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAQORDENESD', @level2type = N'COLUMN', @level2name = N'OrderQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) de la descripción relacionada con la parametrización de servicios en VIE ERP (FK a contract.CUPSEntityContractDescriptions); vincula la orden al catálogo de servicios y CUPS.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAQORDENESD', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de descripcion de relacion. relacion con  VIE ERP  (contract.CUPSEntityContractDescriptions) ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAQORDENESD', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAQORDENESD', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de servicio solicitado (INT, OBSOLETO desde 11-10-2024): 1=Laboratorios, 2=Imágenes, 3=Procedimiento Quirúrgico, 4=Procedimiento No Quirúrgico, 5=Patologías, 6=Medicamentos, 7=Interconsultas, 8=Insumos. Debe usarse parametrización de contrato.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAQORDENESD', @level2type = N'COLUMN', @level2name = N'TIPOSERVICIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'11-10-2024: columna que se pone obsoleta, puesto que este dato debe tomarlo siempre como esta la parametrización.



1 - Laboratorios  2 - Imagenes  3 - Procedimiento Qx  4 - Procedimiento NoQx  5 - Patologias  6 - Medicamentos  7 - Interconsultas  8 - Insumos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAQORDENESD', @level2type = N'COLUMN', @level2name = N'TIPOSERVICIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAQORDENESD', @level2type = N'COLUMN', @level2name = N'TIPOSERVICIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único (CHAR 20) del servicio, procedimiento, examen, medicamento o insumo solicitado en la orden; identificador funcional del catálogo de servicios.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAQORDENESD', @level2type = N'COLUMN', @level2name = N'CODIGOSERVICIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Servicio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAQORDENESD', @level2type = N'COLUMN', @level2name = N'CODIGOSERVICIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAQORDENESD', @level2type = N'COLUMN', @level2name = N'CODIGOSERVICIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) que relaciona el detalle con la cabecera de la orden (HCPAQORDENESC.ID); agrupa los servicios de un mismo acto de solicitud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAQORDENESD', @level2type = N'COLUMN', @level2name = N'IDHCPAQORDENESC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relacion con la tabla cabecera ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAQORDENESD', @level2type = N'COLUMN', @level2name = N'IDHCPAQORDENESC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAQORDENESD', @level2type = N'COLUMN', @level2name = N'IDHCPAQORDENESC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT, IDENTITY) de cada línea de detalle en la orden de servicios; consecutivo autonumérico de la tabla de detalles.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAQORDENESD', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAQORDENESD', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAQORDENESD', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de los servicios solicitados en órdenes médicas del paciente (historia clínica). Cada registro representa un ítem de la orden: el servicio o procedimiento ordenado, su tipo, cantidad y datos clínicos relevantes asociados.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAQORDENESD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAQORDENESD';
