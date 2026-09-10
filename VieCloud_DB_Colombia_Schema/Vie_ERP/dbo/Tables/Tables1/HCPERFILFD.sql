CREATE TABLE [dbo].[HCPERFILFD] (
    [IDPERFILFD]     INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDPERFIL]       INT            NOT NULL,
    [FECHA]          DATETIME       NOT NULL,
    [CODPRODUC]      CHAR (20)      NOT NULL,
    [UNITDOSETYPE]   VARCHAR (3)    NOT NULL,
    [ADMINISTRATION] VARCHAR (50)   NOT NULL,
    [INTERVAL]       VARCHAR (15)   NOT NULL,
    [ESTATUS]        INT            NOT NULL,
    [ALERTAS]        VARCHAR (150)  NULL,
    [OBSERVATION]    VARCHAR (4000) NULL,
    CONSTRAINT [PK__HCPERFIL__959B7444699CE337] PRIMARY KEY CLUSTERED ([IDPERFILFD] ASC),
    CONSTRAINT [fk_PerfilFarmaco] FOREIGN KEY ([IDPERFIL]) REFERENCES [dbo].[HCPFARMACOC] ([IDPERFIL])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones clínicas o notas adicionales sobre la administración farmacológica, prescripción o seguimiento del medicamento (VARCHAR 4000, texto libre).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPERFILFD', @level2type = N'COLUMN', @level2name = N'OBSERVATION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la observación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPERFILFD', @level2type = N'COLUMN', @level2name = N'OBSERVATION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPERFILFD', @level2type = N'COLUMN', @level2name = N'OBSERVATION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Alertas y advertencias asociadas al perfil farmacológico: contraindicaciones, interacciones, alergias, reacciones adversas documentadas (VARCHAR 150).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPERFILFD', @level2type = N'COLUMN', @level2name = N'ALERTAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda las alertas ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPERFILFD', @level2type = N'COLUMN', @level2name = N'ALERTAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPERFILFD', @level2type = N'COLUMN', @level2name = N'ALERTAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado o condición del registro: activo, inactivo, pendiente, suspendido, cancelado. Indica si el perfil farmacológico está vigente (INT, código de estado).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPERFILFD', @level2type = N'COLUMN', @level2name = N'ESTATUS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el estado ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPERFILFD', @level2type = N'COLUMN', @level2name = N'ESTATUS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPERFILFD', @level2type = N'COLUMN', @level2name = N'ESTATUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vía de administración del medicamento: oral, intravenosa, intramuscular, tópica, rectal, inhalada, subcutánea, etc. (VARCHAR 50).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPERFILFD', @level2type = N'COLUMN', @level2name = N'ADMINISTRATION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la aministracción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPERFILFD', @level2type = N'COLUMN', @level2name = N'ADMINISTRATION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPERFILFD', @level2type = N'COLUMN', @level2name = N'ADMINISTRATION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de unidad de dosis unitaria: comprimidos, cápsulas, ampolletas, gotas, jeringa precargada, parche, etc. (VARCHAR 3, código).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPERFILFD', @level2type = N'COLUMN', @level2name = N'UNITDOSETYPE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el tipo de dosis unitaria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPERFILFD', @level2type = N'COLUMN', @level2name = N'UNITDOSETYPE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPERFILFD', @level2type = N'COLUMN', @level2name = N'UNITDOSETYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del producto farmacéutico, medicamento o sustancia prescrita. Referencia a catálogo de productos sanitarios (CHAR 20).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPERFILFD', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el Codigo Del Producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPERFILFD', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPERFILFD', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de registro o creación del perfil farmacológico en el sistema. Marca temporal de la prescripción o actualización (DATETIME).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPERFILFD', @level2type = N'COLUMN', @level2name = N'FECHA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la Fecha de registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPERFILFD', @level2type = N'COLUMN', @level2name = N'FECHA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPERFILFD', @level2type = N'COLUMN', @level2name = N'FECHA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del perfil farmacológico del paciente. Clave foránea a HCPFARMACOC (INT, FK).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPERFILFD', @level2type = N'COLUMN', @level2name = N'IDPERFIL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el Id del Perfil', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPERFILFD', @level2type = N'COLUMN', @level2name = N'IDPERFIL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPERFILFD', @level2type = N'COLUMN', @level2name = N'IDPERFIL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consecutivo único y secuencial de la tabla HCPERFILFD. Clave primaria autoincrementable (INT IDENTITY, PK).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPERFILFD', @level2type = N'COLUMN', @level2name = N'IDPERFILFD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el consecutivo de la tabla ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPERFILFD', @level2type = N'COLUMN', @level2name = N'IDPERFILFD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPERFILFD', @level2type = N'COLUMN', @level2name = N'IDPERFILFD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Perfil farmacológico detallado del paciente: registra los medicamentos prescritos con su dosis, vía de administración, intervalo de aplicación y alertas clínicas asociadas, como parte de la historia clínica de medicamentos activos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPERFILFD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPERFILFD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Intervalo o frecuencia de administración del medicamento, por ejemplo cada 8 horas, cada 12 horas, una vez al día.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPERFILFD', @level2type = N'COLUMN', @level2name = N'INTERVAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPERFILFD', @level2type = N'COLUMN', @level2name = N'INTERVAL';
