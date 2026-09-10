CREATE TABLE [dbo].[HCFAVORTI] (
    [OPCIONFAV]                CHAR (10) NOT NULL,
    [CODCATEGO]                CHAR (1)  NOT NULL,
    [CODIGITEM]                CHAR (20) NOT NULL,
    [PRIORIDAD]                INT       NULL,
    [TIPFAVORTI]               CHAR (1)  NOT NULL,
    [ROL]                      INT       CONSTRAINT [DF__HCFAVORTI__ROL__1DE1AD90] DEFAULT ((0)) NOT NULL,
    [ID]                       INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDDESCRIPCIONRELACIONADA] INT       NULL,
    [USUARIOCREACION]          CHAR (20) NULL,
    [FECHACREACION]            DATETIME  NULL,
    CONSTRAINT [PK_HCFAVORTI_1] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCFAVORTI_SEGusuaru_1] FOREIGN KEY ([USUARIOCREACION]) REFERENCES [dbo].[SEGusuaru] ([CODUSUARI])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro de favorito; tipo DATETIME; auditoria de cuando fue agregado el item a favoritos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFAVORTI', @level2type = N'COLUMN', @level2name = N'FECHACREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Creación del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFAVORTI', @level2type = N'COLUMN', @level2name = N'FECHACREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFAVORTI', @level2type = N'COLUMN', @level2name = N'FECHACREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que creó el registro; FK a SEGusuaru.CODUSUARI; PII - identificación del profesional de salud que marcó como favorito', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFAVORTI', @level2type = N'COLUMN', @level2name = N'USUARIOCREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFAVORTI', @level2type = N'COLUMN', @level2name = N'USUARIOCREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFAVORTI', @level2type = N'COLUMN', @level2name = N'USUARIOCREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de descripción relacionada con contrato ERP Indigo Vie (VIE_ERP.contract.CUPSEntityContractDescriptions); vinculación externa de CUPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFAVORTI', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de descripcion de relacion. relacion con  VIE ERP  (contract.CUPSEntityContractDescriptions) ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFAVORTI', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFAVORTI', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (identity), clave primaria, consecutivo secuencial de la tabla de favoritos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFAVORTI', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFAVORTI', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFAVORTI', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Rol del profesional para el favorito (0=defecto/nulo, 1=Médico, 2=Enfermería, 3=Terapeuta); INT; default 0; filtra favoritos por perfil', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFAVORTI', @level2type = N'COLUMN', @level2name = N'ROL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ROL:   Otras categorias diferentes de Insumos  0=por defecto (nulo)  Categoria Insumos:  0= lista vacia   1=Medico     2=Enfermeria     3=Terapeuta  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFAVORTI', @level2type = N'COLUMN', @level2name = N'ROL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFAVORTI', @level2type = N'COLUMN', @level2name = N'ROL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de favorito: 1=Unidad Funcional, 2=Especialidad; CHAR(1); clasifica la naturaleza del item guardado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFAVORTI', @level2type = N'COLUMN', @level2name = N'TIPFAVORTI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Favorito 1-Unidad Funciona 2- Especialidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFAVORTI', @level2type = N'COLUMN', @level2name = N'TIPFAVORTI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFAVORTI', @level2type = N'COLUMN', @level2name = N'TIPFAVORTI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Orden de prioridad del favorito; INT NULL; menor valor = mayor prioridad en listados', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFAVORTI', @level2type = N'COLUMN', @level2name = N'PRIORIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Prioridad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFAVORTI', @level2type = N'COLUMN', @level2name = N'PRIORIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFAVORTI', @level2type = N'COLUMN', @level2name = N'PRIORIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del ítem favorito (medicamento, laboratorio, procedimiento, patología, imagen, etc.); CHAR(20)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFAVORTI', @level2type = N'COLUMN', @level2name = N'CODIGITEM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del ITEM', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFAVORTI', @level2type = N'COLUMN', @level2name = N'CODIGITEM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFAVORTI', @level2type = N'COLUMN', @level2name = N'CODIGITEM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Categoría del ítem: 1=Medicamentos, 2=Laboratorios, 3=Patologías, 4=Imágenes DX, 5=Procedimientos QX, 6=Procedimientos No QX, 7=Interconsultas; CHAR(1)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFAVORTI', @level2type = N'COLUMN', @level2name = N'CODCATEGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Categoria:  1. Medicamentos  2. Labaratorios  3. Patologias  4. Imagenes DX  5. Procedimientos QX  6. Procedimientos No Qx  7. Interconsultas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFAVORTI', @level2type = N'COLUMN', @level2name = N'CODCATEGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFAVORTI', @level2type = N'COLUMN', @level2name = N'CODCATEGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de unidad funcional asociada al favorito; CHAR(10); NOT NULL; identifica el centro de atención/departamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFAVORTI', @level2type = N'COLUMN', @level2name = N'OPCIONFAV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFAVORTI', @level2type = N'COLUMN', @level2name = N'OPCIONFAV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFAVORTI', @level2type = N'COLUMN', @level2name = N'OPCIONFAV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de ítems favoritos u opciones predefinidas para la historia clínica, organizados por categoría, rol y prioridad. Permite a los profesionales de la salud acceder rápidamente a diagnósticos, procedimientos, medicamentos u otras opciones de uso frecuente dentro del módulo de historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFAVORTI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFAVORTI';
