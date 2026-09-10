CREATE TABLE [dbo].[HCCRIUNID] (
    [CODCONCEC]           NUMERIC (18) IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODTIPCRI]           CHAR (5)     NOT NULL,
    [DESTIPCRI]           CHAR (1000)  NOT NULL,
    [UFUTIPUNI]           INT          NOT NULL,
    [INDAUDFOR]           NUMERIC (18) NOT NULL,
    [CODTIPEST]           CHAR (3)     NULL,
    [TIPCRITERIO]         BIT          NULL,
    [USUARIOCREACION]     CHAR (20)    NULL,
    [FECHACREACION]       DATETIME     NULL,
    [USUARIOMODIFICACION] CHAR (20)    NULL,
    [FECHAMODIFICACION]   DATETIME     NULL,
    [Neonate]             BIT          NULL,
    CONSTRAINT [PK_HCCRIUNID_1] PRIMARY KEY CLUSTERED ([CODCONCEC] ASC),
    CONSTRAINT [FK_HCCRIUNID_SEGusuaru_1] FOREIGN KEY ([USUARIOCREACION]) REFERENCES [dbo].[SEGusuaru] ([CODUSUARI]),
    CONSTRAINT [FK_HCCRIUNID_SEGusuaru_2] FOREIGN KEY ([USUARIOMODIFICACION]) REFERENCES [dbo].[SEGusuaru] ([CODUSUARI])
);




GO



GO



GO
CREATE NONCLUSTERED INDEX [IX_HCCRIUNID__CODTIPCRI]
    ON [dbo].[HCCRIUNID]([CODTIPCRI] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT que determina si el criterio de ingreso aplica a pacientes neonatos (recién nacidos). Activado (1) o desactivado (0) para incluir/excluir población neonatal en unidades de cuidado intensivo, intermedio o básico neonatal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCRIUNID', @level2type = N'COLUMN', @level2name = N'Neonate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el si la opcion de neonato del criterio de ingreso esta activa o no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCRIUNID', @level2type = N'COLUMN', @level2name = N'Neonate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCRIUNID', @level2type = N'COLUMN', @level2name = N'Neonate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca de tiempo DATETIME del última modificación del registro de criterio de ingreso. Auditoría de cambios en la configuración de criterios clínicos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCRIUNID', @level2type = N'COLUMN', @level2name = N'FECHAMODIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCRIUNID', @level2type = N'COLUMN', @level2name = N'FECHAMODIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCRIUNID', @level2type = N'COLUMN', @level2name = N'FECHAMODIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del usuario (FK a SEGusuaru.CODUSUARI) que realizó la última modificación del criterio. Trazabilidad de cambios en la definición de criterios de ingreso/egreso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCRIUNID', @level2type = N'COLUMN', @level2name = N'USUARIOMODIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCRIUNID', @level2type = N'COLUMN', @level2name = N'USUARIOMODIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCRIUNID', @level2type = N'COLUMN', @level2name = N'USUARIOMODIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca de tiempo DATETIME de creación del registro de criterio de ingreso a unidad funcional. Auditoría de origen de la configuración.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCRIUNID', @level2type = N'COLUMN', @level2name = N'FECHACREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Creación del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCRIUNID', @level2type = N'COLUMN', @level2name = N'FECHACREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCRIUNID', @level2type = N'COLUMN', @level2name = N'FECHACREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del usuario (FK a SEGusuaru.CODUSUARI) que creó el criterio de ingreso. Trazabilidad del autor de la definición clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCRIUNID', @level2type = N'COLUMN', @level2name = N'USUARIOCREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCRIUNID', @level2type = N'COLUMN', @level2name = N'USUARIOCREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCRIUNID', @level2type = N'COLUMN', @level2name = N'USUARIOCREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de criterio BIT: 1=Criterio de Ingreso/Estancia, 2=Criterio de Egreso. Clasifica si el criterio regula admisión a la unidad funcional o alta/egreso del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCRIUNID', @level2type = N'COLUMN', @level2name = N'TIPCRITERIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo para conocer tipo de criterio 1-> (Ingreso,Estancia)  2-> (Egreso)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCRIUNID', @level2type = N'COLUMN', @level2name = N'TIPCRITERIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCRIUNID', @level2type = N'COLUMN', @level2name = N'TIPCRITERIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código CHAR(3) del tipo de estancia asociado al criterio (ej: hospitalización ordinaria, cuidado intensivo). Referencia a clasificación de estancias clínicas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCRIUNID', @level2type = N'COLUMN', @level2name = N'CODTIPEST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Tipo de Estancia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCRIUNID', @level2type = N'COLUMN', @level2name = N'CODTIPEST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCRIUNID', @level2type = N'COLUMN', @level2name = N'CODTIPEST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código NUMERIC(18) de auditoría/folio para trazabilidad de auditoria clínica y administrativa. Vinculación con procesos de aseguramiento de calidad y RIPS.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCRIUNID', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCRIUNID', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCRIUNID', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código INT de tipo de unidad funcional (1-24): Urgencias, Hospitalización, Diagnóstico, Terapia, UCI/Pediátrica/Neonatal, Renal, Oncología, Psiquiatría, Cirugía, Laboratorio, Cardiología, Gineco-Obstetricia, Cuidado Paliativo, otras.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCRIUNID', @level2type = N'COLUMN', @level2name = N'UFUTIPUNI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Unidad Funcional:  1: Urgencias  2: Hospitalizacion  3: Apoyo Dx  4: Apoyo Terapeutico  5: Unidades de Cuidado Intensivo Adulto  6: Unidades de Cuidado Intermedio Adulto  7: Unidades de Cuidado Intensivo Pediatrica  8: Unidades de Cuidado Intermedio Pediatrica  9: Unidades de Cuidado Intensivo Neonatal  10: Unidades de Cuidado Intermedio Neonatal  11: Unidades de Cuidado Basico Neonatal  12: Unidad Renal  13 Unidad Oncologica  14: Unidad Medicina Nuclear  15: Consulta Externa  16: Unidad Mental  17: Unidad de Quemados  18: Unidad de Cuidado Paliativo  19: Cirugia  20: Laboratorio  21: Cardiologia No Invasiva  22: Cardiologia Invasiva  23: Gineco-Obstetricia  24: Otras', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCRIUNID', @level2type = N'COLUMN', @level2name = N'UFUTIPUNI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCRIUNID', @level2type = N'COLUMN', @level2name = N'UFUTIPUNI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción CHAR(1000) detallada del criterio de ingreso a unidad funcional. Explica requisitos clínicos, severidad, diagnósticos o condiciones que justifican el ingreso/egreso del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCRIUNID', @level2type = N'COLUMN', @level2name = N'DESTIPCRI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion Tipos de Criterios Ingreso a Unidades', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCRIUNID', @level2type = N'COLUMN', @level2name = N'DESTIPCRI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCRIUNID', @level2type = N'COLUMN', @level2name = N'DESTIPCRI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código CHAR(5) único del tipo de criterio de ingreso a unidad funcional. Identificador semántico para búsqueda de criterios de admisión clínica (ej: ''''CRIT001'''').', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCRIUNID', @level2type = N'COLUMN', @level2name = N'CODTIPCRI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Tipo de Critrios Ingreso a Unidades', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCRIUNID', @level2type = N'COLUMN', @level2name = N'CODTIPCRI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCRIUNID', @level2type = N'COLUMN', @level2name = N'CODTIPCRI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consecutivo NUMERIC(18) autoincrementable (IDENTITY) y clave primaria (PK) de la tabla. Identificador único del registro de criterio de ingreso/egreso a unidades.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCRIUNID', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo de la Tabla ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCRIUNID', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCRIUNID', @level2type = N'COLUMN', @level2name = N'CODCONCEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de criterios de ingreso a unidades especiales (UCI, unidad neonatal, etc.), donde se definen los tipos de criterios clínicos que determinan si un paciente puede o debe ser admitido en una unidad de cuidado intensivo u otras unidades especializadas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCRIUNID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCRIUNID';
