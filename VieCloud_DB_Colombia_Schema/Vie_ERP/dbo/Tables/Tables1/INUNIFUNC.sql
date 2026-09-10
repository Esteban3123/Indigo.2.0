CREATE TABLE [dbo].[INUNIFUNC] (
    [UFUCODIGO]            CHAR (10)    NOT NULL,
    [UFUDESCRI]            CHAR (60)    NOT NULL,
    [UFUTIPUNI]            INT          NOT NULL,
    [TIPOCAMAS]            CHAR (1)     NULL,
    [ARSCODIGO]            CHAR (10)    NOT NULL,
    [INDAUDFOR]            NUMERIC (18) NOT NULL,
    [FunctionalUnitGroup]  INT          NULL,
    [ApplyOtherProcedures] BIT          CONSTRAINT [DF_INUNIFUNC_ApplyOtherProcedures] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_INUNIFUNC] PRIMARY KEY CLUSTERED ([UFUCODIGO] ASC)
);




GO
CREATE NONCLUSTERED INDEX [IX_INUNIFUNC]
    ON [dbo].[INUNIFUNC]([UFUCODIGO] ASC, [UFUDESCRI] ASC);

GO
CREATE INDEX IX_INUNIFUNC_TIPUNI
  ON INUNIFUNC (UFUTIPUNI) INCLUDE (UFUCODIGO); 

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT) que determina si la unidad funcional aplica a otros procedimientos adicionales: Sí=1, No=0. Controla disponibilidad de procedimientos complementarios en la unidad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INUNIFUNC', @level2type = N'COLUMN', @level2name = N'ApplyOtherProcedures';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Aplica a otros procedimientos    Si     No    ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INUNIFUNC', @level2type = N'COLUMN', @level2name = N'ApplyOtherProcedures';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INUNIFUNC', @level2type = N'COLUMN', @level2name = N'ApplyOtherProcedures';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación agrupada de la unidad funcional (INT): 1=Consulta externa, 2=Apoyo diagnóstico y complementación terapéutica, 3=Internación, 4=Quirúrgico, 5=Atención inmediata/Urgencias. Agrupa tipos de servicio para reportes y facturación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INUNIFUNC', @level2type = N'COLUMN', @level2name = N'FunctionalUnitGroup';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Grupo de la unidad funcional:   1. Consulta externa.  2. Apoyo diagnóstico y complementación terapéutica.  3. Internación.  4. Quirúrgico.  5. Atención inmediata.  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INUNIFUNC', @level2type = N'COLUMN', @level2name = N'FunctionalUnitGroup';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INUNIFUNC', @level2type = N'COLUMN', @level2name = N'FunctionalUnitGroup';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código numérico de auditoría (NUMERIC 18) asociado a la unidad funcional. Identificador para trazabilidad, cumplimiento normativo y auditoría interna de procesos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INUNIFUNC', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INUNIFUNC', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INUNIFUNC', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del área de servicio (CHAR 10, FK→INAREASER). Identifica la línea de negocio, departamento o centro de atención a la que pertenece la unidad funcional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INUNIFUNC', @level2type = N'COLUMN', @level2name = N'ARSCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del area de servicio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INUNIFUNC', @level2type = N'COLUMN', @level2name = N'ARSCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INUNIFUNC', @level2type = N'COLUMN', @level2name = N'ARSCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de cama de hospitalización (CHAR 1): 1=Ninguna, 2=Observación-Urgencia, 3=Hospitalaria. Especifica capacidad y modalidad de alojamiento en internación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INUNIFUNC', @level2type = N'COLUMN', @level2name = N'TIPOCAMAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tpo de Cama Hospitalizacion 1: Ninguna 2: Observacion-Urgencia 3: Hospitalaria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INUNIFUNC', @level2type = N'COLUMN', @level2name = N'TIPOCAMAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INUNIFUNC', @level2type = N'COLUMN', @level2name = N'TIPOCAMAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de unidad funcional (INT, 1-35): incluye urgencias, hospitalización, apoyo diagnóstico (laboratorio, radiología, cardiología), cuidado intensivo/intermedio (adulto, pediátrico, neonatal), oncología, radioterapia, cirugía, consulta externa, atención domiciliaria, hemodinamia. Define especialidad y nivel de complejidad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INUNIFUNC', @level2type = N'COLUMN', @level2name = N'UFUTIPUNI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Unidad Funcional:  1: Urgencias  2: Hospitalizacion  3: Apoyo Dx  4: Apoyo Terapeutico  5: Unidades de Cuidado Intensivo Adulto  6: Unidades de Cuidado Intermedio Adulto  7: Unidades de Cuidado Intensivo Pediatrica  8: Unidades de Cuidado Intermedio Pediatrica  9: Unidades de Cuidado Intensivo Neonatal  10: Unidades de Cuidado Intermedio Neonatal  11: Unidades de Cuidado Basico Neonatal  12: Unidad Renal  13 Unidad Oncologica  14: Unidad Medicina Nuclear  15: Consulta Externa  16: Unidad Mental  17: Unidad de Quemados  18: Unidad de Cuidado Paliativo  19: Cirugia  20: Laboratorio  21: Cardiologia No Invasiva  22: Cardiologia Invasiva  23: Gineco-Obstetricia  24: Consulta Externa - Gineco-Obstetricia  30: Otras  31: Consulta Prioritaria  32: Atención domiciliaria  33: Unidad Radioterapia  34: Unidad Braquiterapia  35: Hemodinamia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INUNIFUNC', @level2type = N'COLUMN', @level2name = N'UFUTIPUNI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INUNIFUNC', @level2type = N'COLUMN', @level2name = N'UFUTIPUNI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción textual de la unidad funcional (CHAR 60). Nombre o denominación legible del servicio, área clínica o unidad de atención para identificación operacional y administrativa.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INUNIFUNC', @level2type = N'COLUMN', @level2name = N'UFUDESCRI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INUNIFUNC', @level2type = N'COLUMN', @level2name = N'UFUDESCRI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INUNIFUNC', @level2type = N'COLUMN', @level2name = N'UFUDESCRI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único de la unidad funcional (CHAR 10, PK). Identificador primario que relaciona procedimientos, ingresos, facturación y auditoría a la unidad específica del centro de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INUNIFUNC', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INUNIFUNC', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INUNIFUNC', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
CREATE NONCLUSTERED INDEX [IX_INUNIFUNC_UFUCODIGO_UFUTIPUNI]
    ON [dbo].[INUNIFUNC]([UFUCODIGO] ASC, [UFUTIPUNI] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo maestro de unidades funcionales (servicios, salas o áreas de atención) del centro de salud. Permite identificar cada unidad, su tipo, si maneja camas y a qué área o sede pertenece.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INUNIFUNC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INUNIFUNC';
