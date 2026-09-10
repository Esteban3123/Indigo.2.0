CREATE TABLE [dbo].[INCONFACT] (
    [CODCONCEP]  CHAR (3)      NOT NULL,
    [DESCONCEP]  VARCHAR (100) NOT NULL,
    [RIPSCONCEP] CHAR (2)      NOT NULL,
    [INFOCONCE]  CHAR (2)      NOT NULL,
    CONSTRAINT [PK_INCONFACT] PRIMARY KEY CLUSTERED ([CODCONCEP] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación de consolidación de informes clínicos y facturación (CHAR 2). Agrupa el concepto por tipo de servicio: 1=Medicamentos; 2=Cirugías/Procedimientos; 3=Imágenes Diagnósticas; 4=Laboratorios; 5=Terapias; 6=Consultas; 7=Bancos de Sangre; 8=Elementos Adicionales; 9=Habitación/Estancia; 10=Honorarios Médicos; 11=Atención y Sala de Observación en Urgencias.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONFACT', @level2type = N'COLUMN', @level2name = N'INFOCONCE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Informe de consolidacion  1=Medicamentos;  2=Cirugias Procedimientos  3=Imagenes Diagnosticas  4=Laboratorios  5=Terapias  6=Consultas  7=Bancos de Sangre  8=Elementos adicionales  9=Habitacion  10=Honorarios Medicos  11=Atencion y sala de observacion Urgencias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONFACT', @level2type = N'COLUMN', @level2name = N'INFOCONCE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONFACT', @level2type = N'COLUMN', @level2name = N'INFOCONCE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación RIPS (Registro Individual de Prestaciones en Salud) del concepto facturado (CHAR 2). Codificación normativa: 1=Consultas; 2=Procedimientos Diagnósticos; 3=Procedimientos Terapéuticos No Quirúrgicos; 4=Procedimientos Quirúrgicos; 5=Promoción y Prevención; 6=Estancias; 7=Honorarios Médicos; 8=Derecho de Sala; 9=Materiales e Insumos; 10=Bancos de Sangre; 11=Prótesis y Ortesis; 12=Medicamentos POS; 13=Medicamentos No POS; 14=Traslado de Pacientes; 15=Otros Servicios.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONFACT', @level2type = N'COLUMN', @level2name = N'RIPSCONCEP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Rips  1=Consultas;  2=Procedimientos Diagnosticos;  3=Procedimientos Terapeuticos NO Quirurgicos;  4=Procedimientos Terapeuticos Quirurgicos;  5=Procedimientos Promocion Prevencion;  6=Estancias;  7=Honorarios;  8=Derecho de Sala;  9=Materiales e Insumos;  10=Bancos de Sangre;  11=Protesis y Ortesis;  12=Medicamentos POS;  13=Medicamentos No POS;  14=Traslado de Pacientes;  15=Otros Servicios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONFACT', @level2type = N'COLUMN', @level2name = N'RIPSCONCEP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONFACT', @level2type = N'COLUMN', @level2name = N'RIPSCONCEP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción detallada del concepto de facturación (VARCHAR 100). Nombre legible del servicio, medicamento, procedimiento o insumo facturado en la cuenta de cobro.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONFACT', @level2type = N'COLUMN', @level2name = N'DESCONCEP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion concepto de facturacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONFACT', @level2type = N'COLUMN', @level2name = N'DESCONCEP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONFACT', @level2type = N'COLUMN', @level2name = N'DESCONCEP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único de concepto de facturación (CHAR 3). Identificador del rubro o línea de servicio facturado en la factura de atención en salud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONFACT', @level2type = N'COLUMN', @level2name = N'CODCONCEP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo concepto de facturacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONFACT', @level2type = N'COLUMN', @level2name = N'CODCONCEP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONFACT', @level2type = N'COLUMN', @level2name = N'CODCONCEP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de conceptos de facturación utilizados en la generación de facturas y reportes RIPS. Cada registro define un tipo de concepto de cobro con su equivalencia en los códigos RIPS requeridos por el sistema de salud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONFACT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONFACT';
