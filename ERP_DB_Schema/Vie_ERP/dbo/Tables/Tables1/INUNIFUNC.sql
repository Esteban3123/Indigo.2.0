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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Aplica a otros procedimientos    Si     No    ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INUNIFUNC', @level2type = N'COLUMN', @level2name = N'ApplyOtherProcedures';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Grupo de la unidad funcional:   1. Consulta externa.  2. Apoyo diagnóstico y complementación terapéutica.  3. Internación.  4. Quirúrgico.  5. Atención inmediata.  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INUNIFUNC', @level2type = N'COLUMN', @level2name = N'FunctionalUnitGroup';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INUNIFUNC', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del area de servicio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INUNIFUNC', @level2type = N'COLUMN', @level2name = N'ARSCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tpo de Cama Hospitalizacion 1: Ninguna 2: Observacion-Urgencia 3: Hospitalaria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INUNIFUNC', @level2type = N'COLUMN', @level2name = N'TIPOCAMAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Unidad Funcional:  1: Urgencias  2: Hospitalizacion  3: Apoyo Dx  4: Apoyo Terapeutico  5: Unidades de Cuidado Intensivo Adulto  6: Unidades de Cuidado Intermedio Adulto  7: Unidades de Cuidado Intensivo Pediatrica  8: Unidades de Cuidado Intermedio Pediatrica  9: Unidades de Cuidado Intensivo Neonatal  10: Unidades de Cuidado Intermedio Neonatal  11: Unidades de Cuidado Basico Neonatal  12: Unidad Renal  13 Unidad Oncologica  14: Unidad Medicina Nuclear  15: Consulta Externa  16: Unidad Mental  17: Unidad de Quemados  18: Unidad de Cuidado Paliativo  19: Cirugia  20: Laboratorio  21: Cardiologia No Invasiva  22: Cardiologia Invasiva  23: Gineco-Obstetricia  24: Consulta Externa - Gineco-Obstetricia  30: Otras  31: Consulta Prioritaria  32: Atención domiciliaria  33: Unidad Radioterapia  34: Unidad Braquiterapia  35: Hemodinamia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INUNIFUNC', @level2type = N'COLUMN', @level2name = N'UFUTIPUNI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INUNIFUNC', @level2type = N'COLUMN', @level2name = N'UFUDESCRI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INUNIFUNC', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';

