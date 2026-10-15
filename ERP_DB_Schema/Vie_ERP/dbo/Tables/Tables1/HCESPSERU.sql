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
ALTER TABLE [dbo].[HCESPSERU] NOCHECK CONSTRAINT [FK_HCESPSERU_INCUPSIPS];


GO
ALTER TABLE [dbo].[HCESPSERU] NOCHECK CONSTRAINT [FK_HCESPSERU_INCUPSIPS1];


GO
ALTER TABLE [dbo].[HCESPSERU] NOCHECK CONSTRAINT [FK_HCESPSERU_INCUPSIPS2];


GO
ALTER TABLE [dbo].[HCESPSERU] NOCHECK CONSTRAINT [FK_HCESPSERU_INESPECIA];


GO
ALTER TABLE [dbo].[HCESPSERU] NOCHECK CONSTRAINT [FK_HCESPSERU2_INCUPSIPS];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de descripcion de relacion. relacion con  VIE ERP  (contract.CUPSEntityContractDescriptions) - CUPS Urgencias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESPSERU', @level2type = N'COLUMN', @level2name = N'IdRelatedDescription_Urg';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Unico de Procedimientos y Servicios para Urgencias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESPSERU', @level2type = N'COLUMN', @level2name = N'CodServiceUrg';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de descripcion de relacion. relacion con  VIE ERP  (contract.CUPSEntityContractDescriptions) - CUPS intraHospitalario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESPSERU', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA_INTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de descripcion de relacion. relacion con  VIE ERP  (contract.CUPSEntityContractDescriptions) - CUPS Control', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESPSERU', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA_CONT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de descripcion de relacion. relacion con  VIE ERP  (contract.CUPSEntityContractDescriptions) - CUPS Consulta externa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESPSERU', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA_CONS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de descripcion de relacion. relacion con  VIE ERP  (contract.CUPSEntityContractDescriptions) - CUPS interconsulta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESPSERU', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA_INTER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Parametros de HC Manejo intrahospitalario(Especialidades) (Codigo Unico de Procedimientos y Servicios - Ademas de Medicamentos)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESPSERU', @level2type = N'COLUMN', @level2name = N'CODSERIPSINTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Unico de Procedimientos y Servicios para Consulta Externa(Control)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESPSERU', @level2type = N'COLUMN', @level2name = N'CODSERCEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Unico de Procedimientos y Servicios para Consulta Externa(Primera Vez)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESPSERU', @level2type = N'COLUMN', @level2name = N'CODSERCEX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Unico de Procedimientos y Servicios para Interconsultas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESPSERU', @level2type = N'COLUMN', @level2name = N'CODSERINT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Especialidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESPSERU', @level2type = N'COLUMN', @level2name = N'CODESPECI';

