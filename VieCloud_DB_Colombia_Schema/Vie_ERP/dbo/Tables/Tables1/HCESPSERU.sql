CREATE TABLE [dbo].[HCESPSERU] (
    [CODESPECI]                      CHAR (3)  NOT NULL,
    [CODSERINT]                      CHAR (20) NULL,
    [CODSERCEX]                      CHAR (20) NULL,
    [CODSERCEC]                      CHAR (20) NULL,
    [CODSERIPSINTRA]                 CHAR (20) NULL,
    [IDDESCRIPCIONRELACIONADA_INTER] INT       NULL,
    [IDDESCRIPCIONRELACIONADA_CONS]  INT       NULL,
    [IDDESCRIPCIONRELACIONADA_CONT]  INT       NULL,
    [IDDESCRIPCIONRELACIONADA_INTRA] INT       NULL,
    [CodServiceUrg]                  CHAR (20) NULL,
    [IdRelatedDescription_Urg]       INT       NULL,
    CONSTRAINT [PK_HCESPSERU_1] PRIMARY KEY CLUSTERED ([CODESPECI] ASC),
    CONSTRAINT [FK_HCESPSERU_INCUPSIPS] FOREIGN KEY ([CODSERCEX]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS]),
    CONSTRAINT [FK_HCESPSERU_INCUPSIPS1] FOREIGN KEY ([CODSERINT]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS]),
    CONSTRAINT [FK_HCESPSERU_INCUPSIPS2] FOREIGN KEY ([CodServiceUrg]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS]),
    CONSTRAINT [FK_HCESPSERU_INESPECIA] FOREIGN KEY ([CODESPECI]) REFERENCES [dbo].[INESPECIA] ([CODESPECI]),
    CONSTRAINT [FK_HCESPSERU2_INCUPSIPS] FOREIGN KEY ([CODSERIPSINTRA]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS])
);




GO



GO



GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) de descripción relacionada en VIE ERP (contract.CUPSEntityContractDescriptions) para servicios de urgencias/emergencias. Vincula especialidad con configuración CUPS urgencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESPSERU', @level2type = N'COLUMN', @level2name = N'IdRelatedDescription_Urg';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de descripcion de relacion. relacion con  VIE ERP  (contract.CUPSEntityContractDescriptions) - CUPS Urgencias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESPSERU', @level2type = N'COLUMN', @level2name = N'IdRelatedDescription_Urg';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESPSERU', @level2type = N'COLUMN', @level2name = N'IdRelatedDescription_Urg';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código CUPS (Código Único de Procedimientos y Servicios) CHAR(20) para atención de urgencias/emergencias. Referencia a INCUPSIPS; identifica procedimiento/servicio en servicio urgente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESPSERU', @level2type = N'COLUMN', @level2name = N'CodServiceUrg';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Unico de Procedimientos y Servicios para Urgencias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESPSERU', @level2type = N'COLUMN', @level2name = N'CodServiceUrg';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESPSERU', @level2type = N'COLUMN', @level2name = N'CodServiceUrg';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) de descripción relacionada en VIE ERP (contract.CUPSEntityContractDescriptions) para servicios intrahospitalarios. Vincula especialidad con configuración CUPS internación/hospitalización.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESPSERU', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA_INTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de descripcion de relacion. relacion con  VIE ERP  (contract.CUPSEntityContractDescriptions) - CUPS intraHospitalario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESPSERU', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA_INTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESPSERU', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA_INTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) de descripción relacionada en VIE ERP (contract.CUPSEntityContractDescriptions) para servicios de control/seguimiento. Vincula especialidad con configuración CUPS control posterior.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESPSERU', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA_CONT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de descripcion de relacion. relacion con  VIE ERP  (contract.CUPSEntityContractDescriptions) - CUPS Control', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESPSERU', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA_CONT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESPSERU', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA_CONT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) de descripción relacionada en VIE ERP (contract.CUPSEntityContractDescriptions) para consulta externa. Vincula especialidad con configuración CUPS consulta (primera vez o control).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESPSERU', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA_CONS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de descripcion de relacion. relacion con  VIE ERP  (contract.CUPSEntityContractDescriptions) - CUPS Consulta externa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESPSERU', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA_CONS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESPSERU', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA_CONS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) de descripción relacionada en VIE ERP (contract.CUPSEntityContractDescriptions) para interconsultas. Vincula especialidad con configuración CUPS para atención de interconsulta especializada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESPSERU', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA_INTER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de descripcion de relacion. relacion con  VIE ERP  (contract.CUPSEntityContractDescriptions) - CUPS interconsulta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESPSERU', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA_INTER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESPSERU', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA_INTER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código CUPS (Código Único de Procedimientos y Servicios) CHAR(20) para manejo intrahospitalario por especialidad. Incluye procedimientos, servicios y medicamentos en internación. Referencia a INCUPSIPS.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESPSERU', @level2type = N'COLUMN', @level2name = N'CODSERIPSINTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Parametros de HC Manejo intrahospitalario(Especialidades) (Codigo Unico de Procedimientos y Servicios - Ademas de Medicamentos)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESPSERU', @level2type = N'COLUMN', @level2name = N'CODSERIPSINTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESPSERU', @level2type = N'COLUMN', @level2name = N'CODSERIPSINTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código CUPS (Código Único de Procedimientos y Servicios) CHAR(20) para consulta externa de control/seguimiento. Referencia a INCUPSIPS; identifica servicio en atención ambulatoria posterior.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESPSERU', @level2type = N'COLUMN', @level2name = N'CODSERCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Unico de Procedimientos y Servicios para Consulta Externa(Control)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESPSERU', @level2type = N'COLUMN', @level2name = N'CODSERCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESPSERU', @level2type = N'COLUMN', @level2name = N'CODSERCEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código CUPS (Código Único de Procedimientos y Servicios) CHAR(20) para consulta externa primera vez. Referencia a INCUPSIPS; identifica servicio en atención ambulatoria inicial especializada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESPSERU', @level2type = N'COLUMN', @level2name = N'CODSERCEX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Unico de Procedimientos y Servicios para Consulta Externa(Primera Vez)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESPSERU', @level2type = N'COLUMN', @level2name = N'CODSERCEX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESPSERU', @level2type = N'COLUMN', @level2name = N'CODSERCEX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código CUPS (Código Único de Procedimientos y Servicios) CHAR(20) para interconsultas entre especialidades. Referencia a INCUPSIPS; identifica servicio de atención por otra especialidad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESPSERU', @level2type = N'COLUMN', @level2name = N'CODSERINT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Unico de Procedimientos y Servicios para Interconsultas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESPSERU', @level2type = N'COLUMN', @level2name = N'CODSERINT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESPSERU', @level2type = N'COLUMN', @level2name = N'CODSERINT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de especialidad CHAR(3). Clave primaria; referencia a INESPECIA. Identifica especialidad médica (ej: cirugía, pediatría, cardiología, etc.)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESPSERU', @level2type = N'COLUMN', @level2name = N'CODESPECI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Especialidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESPSERU', @level2type = N'COLUMN', @level2name = N'CODESPECI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESPSERU', @level2type = N'COLUMN', @level2name = N'CODESPECI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciona cada especialidad médica con los códigos de servicio (CUPS) que le corresponden según el tipo de atención: hospitalización interna, consulta externa, urgencias e intrahospitalario. Permite identificar qué servicios están habilitados para cada especialidad en los distintos ámbitos asistenciales.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESPSERU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESPSERU';
