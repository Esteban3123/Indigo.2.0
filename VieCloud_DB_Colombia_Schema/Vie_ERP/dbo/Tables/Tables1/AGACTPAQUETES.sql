CREATE TABLE [dbo].[AGACTPAQUETES] (
    [ID]                       INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODACTMED]                CHAR (3)  NOT NULL,
    [CODSERIPS]                CHAR (20) NOT NULL,
    [IDAGPAQUETE]              INT       NOT NULL,
    [IDDESCRIPCIONRELACIONADA] INT       NULL,
    CONSTRAINT [PK_AGACTPAQUETES] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_AGACTPAQUETES_AGACTIMED] FOREIGN KEY ([CODACTMED]) REFERENCES [dbo].[AGACTIMED] ([CODACTMED]),
    CONSTRAINT [FK_AGACTPAQUETES_AGPAQUETES] FOREIGN KEY ([IDAGPAQUETE]) REFERENCES [dbo].[AGPAQUETES] ([ID]),
    CONSTRAINT [FK_AGACTPAQUETES_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS]),
    CONSTRAINT [IX_AGACTPAQUETES] UNIQUE NONCLUSTERED ([CODACTMED] ASC, [CODSERIPS] ASC, [IDAGPAQUETE] ASC)
);




GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de descripción relacionada (FK soft a VIE ERP); vinculación a CUPSEntityContractDescriptions para sincronización de detalles de servicios entre EHR e instancia ERP contable.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTPAQUETES', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de descripcion de relacion. relacion con  VIE ERP  (contract.CUPSEntityContractDescriptions) ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTPAQUETES', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTPAQUETES', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID del paquete de actividades (FK a AGPAQUETES); referencia a la tabla de paquetes que agrupa actividades médicas para contratos y facturación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTPAQUETES', @level2type = N'COLUMN', @level2name = N'IDAGPAQUETE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de tabla AGPAQUETE', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTPAQUETES', @level2type = N'COLUMN', @level2name = N'IDAGPAQUETE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTPAQUETES', @level2type = N'COLUMN', @level2name = N'IDAGPAQUETE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único SERIPS (Código de Servicios en RIPS); identificador de 20 caracteres del procedimiento, servicio o actividad en reporte RIPS.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTPAQUETES', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Unico de Procedimientos y Servicios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTPAQUETES', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTPAQUETES', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de actividad médica (CHAR 3); identificador consecutivo de la actividad sanitaria, procedimiento o servicio clínico asociado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTPAQUETES', @level2type = N'COLUMN', @level2name = N'CODACTMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo consecutivo que identifica la actividad medica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTPAQUETES', @level2type = N'COLUMN', @level2name = N'CODACTMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTPAQUETES', @level2type = N'COLUMN', @level2name = N'CODACTMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único auto-incremental (IDENTITY) de la tabla AGACTPAQUETES; clave primaria de registro.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTPAQUETES', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTPAQUETES', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTPAQUETES', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciona actividades médicas con servicios (códigos CUPS) dentro de los paquetes de agendamiento. Permite definir qué servicios o procedimientos componen cada paquete de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTPAQUETES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTPAQUETES';
