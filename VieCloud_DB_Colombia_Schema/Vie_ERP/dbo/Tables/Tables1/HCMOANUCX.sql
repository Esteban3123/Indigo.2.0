CREATE TABLE [dbo].[HCMOANUCX] (
    [CODMOTANU] CHAR (3)      NOT NULL,
    [DESMOTANU] VARCHAR (200) NOT NULL,
    [ESTMOTANU] BIT           NOT NULL,
    CONSTRAINT [PK_HCMOANUCX] PRIMARY KEY CLUSTERED ([CODMOTANU] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado (activo/inactivo) del motivo de cancelación de cirugías; BIT flag que indica si el registro está vigente en el sistema', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOANUCX', @level2type = N'COLUMN', @level2name = N'ESTMOTANU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado Motivo Cancelacion de Cirugias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOANUCX', @level2type = N'COLUMN', @level2name = N'ESTMOTANU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOANUCX', @level2type = N'COLUMN', @level2name = N'ESTMOTANU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción detallada del motivo de cancelación de cirugía, procedimiento o intervención quirúrgica; texto que explica la razón del cancelamiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOANUCX', @level2type = N'COLUMN', @level2name = N'DESMOTANU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion Motivo Cancelacion de Cirugias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOANUCX', @level2type = N'COLUMN', @level2name = N'DESMOTANU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOANUCX', @level2type = N'COLUMN', @level2name = N'DESMOTANU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador único (PK) del motivo de cancelación de cirugías; clave primaria de 3 caracteres para clasificar razones de cancelamiento quirúrgico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOANUCX', @level2type = N'COLUMN', @level2name = N'CODMOTANU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Motivo Cancelacion de Cirugias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOANUCX', @level2type = N'COLUMN', @level2name = N'CODMOTANU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOANUCX', @level2type = N'COLUMN', @level2name = N'CODMOTANU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de motivos de anulación de cuentas o documentos clínicos. Registra las razones por las cuales se puede anular una cuenta, orden o registro en el sistema, junto con su estado de vigencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOANUCX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOANUCX';
