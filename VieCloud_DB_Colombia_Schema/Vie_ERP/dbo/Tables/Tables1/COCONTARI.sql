CREATE TABLE [dbo].[COCONTARI] (
    [CODCONTRA] CHAR (6)        NOT NULL,
    [CODPLANBE] CHAR (2)        NOT NULL,
    [CODCENATE] CHAR (10)       NOT NULL,
    [UFUCODIGO] CHAR (10)       NOT NULL,
    [CODPLASER] CHAR (3)        NOT NULL,
    [CODPLAPRO] CHAR (3)        NOT NULL,
    [CODPLAHOS] CHAR (3)        NOT NULL,
    [EXIGIRAUT] BIT             NOT NULL,
    [INDAUDFOR] NUMERIC (18, 9) NOT NULL,
    CONSTRAINT [PK_COCONTARI] PRIMARY KEY CLUSTERED ([CODCONTRA] ASC, [CODPLANBE] ASC, [CODCENATE] ASC, [UFUCODIGO] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Índice de auditoría INDIGO (NUMERIC 18,9); porcentaje o factor de auditoría para validación de atenciones, procedimientos y facturas en el contrato.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTARI', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Auditoria INDIGO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTARI', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTARI', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) que establece si se exige autorización previa en el formulario de ingresos, urgencias o atenciones; control de gating de acceso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTARI', @level2type = N'COLUMN', @level2name = N'EXIGIRAUT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Establece si se exige autorizacion en el formulario de Ingresos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTARI', @level2type = N'COLUMN', @level2name = N'EXIGIRAUT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTARI', @level2type = N'COLUMN', @level2name = N'EXIGIRAUT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código (CHAR 3) de plantilla de hospitalización; define tarifas, copagos y cobertura para servicios de internación, UCI, hospitalización general.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTARI', @level2type = N'COLUMN', @level2name = N'CODPLAHOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Plantilla de Hospitalizacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTARI', @level2type = N'COLUMN', @level2name = N'CODPLAHOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTARI', @level2type = N'COLUMN', @level2name = N'CODPLAHOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código (CHAR 3) de plantilla de medicamentos; contiene tarifa y cobertura de fármacos, medicinas y principios activos para el plan de beneficios.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTARI', @level2type = N'COLUMN', @level2name = N'CODPLAPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Plantilla de Medicamentos - Trae el valor de los medicamentos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTARI', @level2type = N'COLUMN', @level2name = N'CODPLAPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTARI', @level2type = N'COLUMN', @level2name = N'CODPLAPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código (CHAR 3) de plantilla para servicios IPS; define valor y cobertura de servicios de diagnóstico, procedimientos, consultas y prestaciones sanitarias.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTARI', @level2type = N'COLUMN', @level2name = N'CODPLASER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Plantilla para Servicios IPS - Trae el valor de los servicios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTARI', @level2type = N'COLUMN', @level2name = N'CODPLASER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTARI', @level2type = N'COLUMN', @level2name = N'CODPLASER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código (CHAR 10) de unidad funcional; identifica área, departamento, especialidad médica o sección operativa del centro de atención contratado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTARI', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTARI', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTARI', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código (CHAR 10) del centro de atención, IPS, clínica u hospital; referencia a sede, sucursal o instalación donde se prestan los servicios.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTARI', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTARI', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTARI', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código (CHAR 2) del plan de beneficios; define cobertura, cuotas y prestaciones sanitarias asociadas al contrato (plan A, B, C, etc.).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTARI', @level2type = N'COLUMN', @level2name = N'CODPLANBE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Plan de Beneficios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTARI', @level2type = N'COLUMN', @level2name = N'CODPLANBE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTARI', @level2type = N'COLUMN', @level2name = N'CODPLANBE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código (CHAR 6) del contrato; identificador único del acuerdo de prestación de servicios de salud con aseguradora o tercero pagador.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTARI', @level2type = N'COLUMN', @level2name = N'CODCONTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Contrato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTARI', @level2type = N'COLUMN', @level2name = N'CODCONTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTARI', @level2type = N'COLUMN', @level2name = N'CODCONTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Configuración de tarifas y reglas de autorización por contrato, plan de beneficios y centro de atención. Define qué planes de servicios, procedimientos y hospitalización aplican para cada combinación de contrato y unidad funcional, indicando si se requiere autorización y el indicador de auditoría.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTARI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTARI';
