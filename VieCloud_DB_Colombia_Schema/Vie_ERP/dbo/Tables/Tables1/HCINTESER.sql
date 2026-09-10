CREATE TABLE [dbo].[HCINTESER] (
    [CODCENATE]                CHAR (10) NOT NULL,
    [CODSERIPS]                CHAR (20) NOT NULL,
    [TIPSERIPS]                INT       NOT NULL,
    [TIPMODALI]                CHAR (2)  NULL,
    [IDDESCRIPCIONRELACIONADA] INT       NULL,
    [ID]                       INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [USUARIOCREACION]          CHAR (20) NULL,
    [FECHACREACION]            DATETIME  NULL,
    CONSTRAINT [PK_HCINTESER] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCINTESER_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCINTESER_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS]),
    CONSTRAINT [FK_HCINTESER_SEGusuaru_1] FOREIGN KEY ([USUARIOCREACION]) REFERENCES [dbo].[SEGusuaru] ([CODUSUARI])
);




GO
CREATE NONCLUSTERED INDEX IX_HCINTESER_Servicio_Centro
ON dbo.HCINTESER (CODSERIPS, CODCENATE)
INCLUDE (TIPMODALI, IDDESCRIPCIONRELACIONADA);

GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro en la tabla de integración de servicios RIPS. Tipo: DATETIME. Auditoría de cuándo se registró la relación entre centro de atención y servicio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTESER', @level2type = N'COLUMN', @level2name = N'FECHACREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Creación del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTESER', @level2type = N'COLUMN', @level2name = N'FECHACREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTESER', @level2type = N'COLUMN', @level2name = N'FECHACREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario o profesional que creó el registro. Tipo: CHAR(20). Referencia a SEGusuaru.CODUSUARI. Trazabilidad de quién registró la integración de servicio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTESER', @level2type = N'COLUMN', @level2name = N'USUARIOCREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTESER', @level2type = N'COLUMN', @level2name = N'USUARIOCREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTESER', @level2type = N'COLUMN', @level2name = N'USUARIOCREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único secuencial (Identity) del registro en HCINTESER. Tipo: INT. Clave primaria de la tabla de integración de servicios de centro de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTESER', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo de la Tabla ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTESER', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTESER', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de descripción relacionada con contrato en VIE ERP (CUPSEntityContractDescriptions). Tipo: INT nullable. Vinculación con descripción comercial o contratual del servicio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTESER', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de descripcion de relacion. relacion con  VIE ERP  (contract.CUPSEntityContractDescriptions) ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTESER', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTESER', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Modalidad del estudio diagnóstico, aplicable solo para servicios de imagen. Tipo: CHAR(2) nullable. Ej: ecografía, radiografía, resonancia, tomografía.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTESER', @level2type = N'COLUMN', @level2name = N'TIPMODALI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Modalidad del Estudio:  Solo para Imagenes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTESER', @level2type = N'COLUMN', @level2name = N'TIPMODALI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTESER', @level2type = N'COLUMN', @level2name = N'TIPMODALI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de servicio RIPS: 1=Laboratorios clínicos, 2=Patologías/Histopatología, 3=Imágenes diagnósticas. Tipo: INT. Clasificación del procedimiento médico según estándar RIPS.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTESER', @level2type = N'COLUMN', @level2name = N'TIPSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Servicio  1: Laboratorios  2: Patologias  3: Imagenes Diagnosticas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTESER', @level2type = N'COLUMN', @level2name = N'TIPSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTESER', @level2type = N'COLUMN', @level2name = N'TIPSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único CUPS/RIPS de procedimiento o servicio de salud. Tipo: CHAR(20). Referencia a INCUPSIPS.CODSERIPS. Identificador estándar de facturación y auditoría.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTESER', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Unico de Procedimientos y Servicios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTESER', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTESER', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención, clínica, hospital o unidad funcional. Tipo: CHAR(10). Referencia a ADCENATEN.CODCENATE. Identifica dónde se presta el servicio de salud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTESER', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTESER', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTESER', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación entre servicios (CUPS/IPS) y centros de atención, indicando el tipo de servicio y modalidad habilitada. Permite configurar qué servicios están disponibles en cada sede o centro de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTESER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTESER';
