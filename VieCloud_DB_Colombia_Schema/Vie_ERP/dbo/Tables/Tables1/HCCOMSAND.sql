CREATE TABLE [dbo].[HCCOMSAND] (
    [ID]                       INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [COMSAMID]                 INT       NOT NULL,
    [CODSERIPS]                CHAR (20) NOT NULL,
    [TIPSERIPS]                TINYINT   NOT NULL,
    [TIPOCARGUE]               TINYINT   CONSTRAINT [DF_HCCOMSAND_TIPOCARGUE] DEFAULT ((1)) NOT NULL,
    [IDDESCRIPCIONRELACIONADA] INT       NULL,
    CONSTRAINT [PK_HCCOMSAND] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCCOMSAND_HCCOMSAN] FOREIGN KEY ([COMSAMID]) REFERENCES [dbo].[HCCOMSAN] ([ID]),
    CONSTRAINT [FK_HCCOMSAND_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de descripción relacionada (INT, FK implícita a VIE ERP contract.CUPSEntityContractDescriptions). Vincula detalles contracuales y descriptivos del servicio con el contrato.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCOMSAND', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de descripcion de relacion. relacion con  VIE ERP  (contract.CUPSEntityContractDescriptions) ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCOMSAND', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCOMSAND', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de Cargue (TINYINT, default=1). Define modo de sincronización: 1=Automático, 2=Preguntar antes de cargar. Controla integración con ERP Indigo Vie Cloud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCOMSAND', @level2type = N'COLUMN', @level2name = N'TIPOCARGUE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de cargue: 1:Automatico, 2:Preguntar antes de cargar', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCOMSAND', @level2type = N'COLUMN', @level2name = N'TIPOCARGUE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCOMSAND', @level2type = N'COLUMN', @level2name = N'TIPOCARGUE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de Servicio IPS (TINYINT). Enumera categorías: 1=Pruebas Cruzadas/Reserva de sangre, 2=Transfusión. Clasifica el tipo de procedimiento hemoterápico o servicio relacionado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCOMSAND', @level2type = N'COLUMN', @level2name = N'TIPSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Servicio  1 = Pruebas Cruzadas (Reserva)   2 = Transfusion   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCOMSAND', @level2type = N'COLUMN', @level2name = N'TIPSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCOMSAND', @level2type = N'COLUMN', @level2name = N'TIPSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del Servicio IPS (FK a INCUPSIPS, CHAR 20). Código único del servicio de salud registrado en la tabla de relación CUPS-IPS para facturación y RIPS.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCOMSAND', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Servicio IPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCOMSAND', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCOMSAND', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del componente sanguíneo (FK a HCCOMSAN). Referencia a la cabecera de pedido/solicitud de transfusión, hemoderivados o banco de sangre.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCOMSAND', @level2type = N'COLUMN', @level2name = N'COMSAMID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del componente sanguineo (Cabecera)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCOMSAND', @level2type = N'COLUMN', @level2name = N'COMSAMID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCOMSAND', @level2type = N'COLUMN', @level2name = N'COMSAMID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT) del registro de detalle en la tabla de componentes sanguíneos y servicios IPS. Clave primaria clustered, generada automáticamente por IDENTITY.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCOMSAND', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID del Detalle', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCOMSAND', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCOMSAND', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de servicios (procedimientos o ítems CUPS/IPS) asociados a una combinación o paquete de servicios de historia clínica. Registra qué servicios componen una agrupación o paquete, con su tipo y modo de cargue.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCOMSAND';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCOMSAND';
