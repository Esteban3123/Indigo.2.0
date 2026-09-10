CREATE TABLE [dbo].[HCPLANDOCCUPS] (
    [ID]          INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCPLANDOC] CHAR (6)     NOT NULL,
    [CODSERIPS]   CHAR (20)    NOT NULL,
    [CODCOMSAM]   VARCHAR (20) NULL,
    CONSTRAINT [PK_HCPLANDOCCUPS] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del componente sanguíneo (hemocomponentes tipo 9); exclusivo para procedimientos de transfusión/hemoterapia; vinculado a tabla HCCOMSAN; hematología, banco de sangre, producto sanguíneo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANDOCCUPS', @level2type = N'COLUMN', @level2name = N'CODCOMSAM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del componente sanguineo, unicamente para los procedimientos de tipo Hemocomponentes ( 9 en la tabla INCUPSIPS) relaciona con el CODCOMSAM de la tabla HCCOMSAN', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANDOCCUPS', @level2type = N'COLUMN', @level2name = N'CODCOMSAM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANDOCCUPS', @level2type = N'COLUMN', @level2name = N'CODCOMSAM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del servicio/procedimiento IPS en RIPS; relación con tabla INCUPSIPS; servicio prestado, atención, procedimiento, facturación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANDOCCUPS', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relacion con la tabla de Servicios IPS del EHR que es  INCUPSIPS  por medio del campo CODSERIPS  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANDOCCUPS', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANDOCCUPS', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de plantilla/documento de historia clínica; relación con tabla HCPLANDOC; vinculado por CODCONSEC; documento clínico, formulario, registro asistencial.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANDOCCUPS', @level2type = N'COLUMN', @level2name = N'IDHCPLANDOC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relacion con la tabla de plantillas y documentos (HCPLANDOC)    Relacion por medio del campo (CODCONSEC)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANDOCCUPS', @level2type = N'COLUMN', @level2name = N'IDHCPLANDOC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANDOCCUPS', @level2type = N'COLUMN', @level2name = N'IDHCPLANDOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementable (INT IDENTITY); clave primaria de la tabla; índice de relación entre plantilla y servicios.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANDOCCUPS', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla, campo autoincrementable ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANDOCCUPS', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANDOCCUPS', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de servicios o procedimientos CUPS asociados a los planes de documentos de historia clínica. Relaciona cada plan de documentación clínica con los códigos de servicio CUPS habilitados y su eventual código de componente o agrupador SAM.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANDOCCUPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANDOCCUPS';
