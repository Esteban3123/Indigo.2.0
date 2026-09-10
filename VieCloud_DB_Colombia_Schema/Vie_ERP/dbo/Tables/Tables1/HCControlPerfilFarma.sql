CREATE TABLE [dbo].[HCControlPerfilFarma] (
    [ID]                INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IPCODPACI]         VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [PERFILCAMBIOS]     BIT                                                                              NOT NULL,
    [FECHACREACION]     DATETIME                                                                         NOT NULL,
    [USUCREACION]       CHAR (20)                                                                        NOT NULL,
    [FECHAMODIFICACION] DATETIME                                                                         NULL,
    [USUMODIFICACION]   CHAR (20)                                                                        NULL,
    CONSTRAINT [PK_HCControlPerfilFarma] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCControlPerfilFarma_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCControlPerfilFarma].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario de modificación: identificación del usuario que realizó la última actualización del control de perfil farmacológico. Tipo: CHAR(20). Auditoría de cambios.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCControlPerfilFarma', @level2type = N'COLUMN', @level2name = N'USUMODIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de modificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCControlPerfilFarma', @level2type = N'COLUMN', @level2name = N'USUMODIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCControlPerfilFarma', @level2type = N'COLUMN', @level2name = N'USUMODIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de modificación: timestamp de la última actualización del registro de control de perfil farmacológico. Tipo: DATETIME. Nullable.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCControlPerfilFarma', @level2type = N'COLUMN', @level2name = N'FECHAMODIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de modificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCControlPerfilFarma', @level2type = N'COLUMN', @level2name = N'FECHAMODIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCControlPerfilFarma', @level2type = N'COLUMN', @level2name = N'FECHAMODIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario de creación: identificación del usuario que registró inicialmente el control de perfil farmacológico. Tipo: CHAR(20). Auditoría de origen.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCControlPerfilFarma', @level2type = N'COLUMN', @level2name = N'USUCREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de creación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCControlPerfilFarma', @level2type = N'COLUMN', @level2name = N'USUCREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCControlPerfilFarma', @level2type = N'COLUMN', @level2name = N'USUCREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación: timestamp de registro inicial del control de perfil farmacológico. Tipo: DATETIME. Auditoría de trazabilidad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCControlPerfilFarma', @level2type = N'COLUMN', @level2name = N'FECHACREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCControlPerfilFarma', @level2type = N'COLUMN', @level2name = N'FECHACREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCControlPerfilFarma', @level2type = N'COLUMN', @level2name = N'FECHACREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de cambios en perfil farmacológico: bandera booleana que señala si se han detectado modificaciones en el perfil de medicamentos del paciente. Tipo: BIT (0=sin cambios, 1=con cambios).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCControlPerfilFarma', @level2type = N'COLUMN', @level2name = N'PERFILCAMBIOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Perfil de cambios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCControlPerfilFarma', @level2type = N'COLUMN', @level2name = N'PERFILCAMBIOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCControlPerfilFarma', @level2type = N'COLUMN', @level2name = N'PERFILCAMBIOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del paciente (identificación PII): clave del paciente, equivalente a cédula, documento de identidad o número de afiliado. Tipo: VARCHAR(25) MASKED. FK a INPACIENT. Búsqueda: paciente, identificación, documento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCControlPerfilFarma', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCControlPerfilFarma', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCControlPerfilFarma', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único secuencial: clave primaria autoincrementada del control de perfil farmacológico. Tipo: INT IDENTITY. Referencia única del registro.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCControlPerfilFarma', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCControlPerfilFarma', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCControlPerfilFarma', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de control de perfiles farmacéuticos por paciente: indica si un paciente tiene habilitado el permiso para realizar cambios en su perfil farmacológico dentro de la historia clínica, junto con la trazabilidad de creación y modificación del registro.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCControlPerfilFarma';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCControlPerfilFarma';
