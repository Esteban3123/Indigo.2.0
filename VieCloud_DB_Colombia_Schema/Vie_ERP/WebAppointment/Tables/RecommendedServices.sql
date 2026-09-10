CREATE TABLE [WebAppointment].[RecommendedServices] (
    [ActivityCode]          CHAR (3)  NOT NULL,
    [IPSServiceCode]        CHAR (20) NOT NULL,
    [ContractDescriptionId] INT       NULL,
    [Position]              INT       NOT NULL,
    [SpecialtyCode]         CHAR (3)  NULL,
    [ProcedureType]         TINYINT   NULL,
    [ServiceType]           INT       NOT NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación del tipo de servicio de salud: 1=Consultas (atención médica, consultoría), 2=Procedimientos (intervenciones, servicios quirúrgicos). INT, clave para categorizar la oferta de servicios recomendados.', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'RecommendedServices', @level2type = N'COLUMN', @level2name = N'ServiceType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1-> Consultas 2-> Procedimientos', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'RecommendedServices', @level2type = N'COLUMN', @level2name = N'ServiceType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'RecommendedServices', @level2type = N'COLUMN', @level2name = N'ServiceType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Categoría de procedimiento complementario: 1=Laboratorio (análisis, pruebas de laboratorio), 2=Imágenes Diagnósticas (radiología, ecografía, resonancia, tomografía), 3=Otros Procedimientos (terapias, intervenciones especializadas). TINYINT, nullable.', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'RecommendedServices', @level2type = N'COLUMN', @level2name = N'ProcedureType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1-> Laboratorio 2-> Imagenes Diagnosticas 3-> Otros Procedimientos', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'RecommendedServices', @level2type = N'COLUMN', @level2name = N'ProcedureType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'RecommendedServices', @level2type = N'COLUMN', @level2name = N'ProcedureType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (FK) de la tabla Contract.ContractDescriptions; vincula el servicio recomendado al contrato/afiliación específica. INT, nullable.', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'RecommendedServices', @level2type = N'COLUMN', @level2name = N'ContractDescriptionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla Contract.ContractDescriptions', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'RecommendedServices', @level2type = N'COLUMN', @level2name = N'ContractDescriptionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'RecommendedServices', @level2type = N'COLUMN', @level2name = N'ContractDescriptionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de servicio IPS (CODSERIPS) de la tabla dbo.INCUPSIPS; identifica el servicio de salud según nomenclatura de procedimientos y servicios. CHAR(20), clave para RIPS, facturación y búsqueda de servicios disponibles.', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'RecommendedServices', @level2type = N'COLUMN', @level2name = N'IPSServiceCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'CODSERIPS de la tabla dbo.INCUPSIPS', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'RecommendedServices', @level2type = N'COLUMN', @level2name = N'IPSServiceCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'RecommendedServices', @level2type = N'COLUMN', @level2name = N'IPSServiceCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de actividad médica (CODACTMED) de la tabla dbo.AGACTIMED; referencia la actividad/procedimiento realizado en centros de atención. CHAR(3), vincula con catálogo de actividades clínicas autorizadas.', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'RecommendedServices', @level2type = N'COLUMN', @level2name = N'ActivityCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'CODACTMED de la tabla dbo.AGACTIMED', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'RecommendedServices', @level2type = N'COLUMN', @level2name = N'ActivityCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'RecommendedServices', @level2type = N'COLUMN', @level2name = N'ActivityCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Servicios recomendados para el agendamiento web, que asocia actividades, procedimientos y especialidades con contratos y tipos de servicio disponibles para agendar citas en línea.', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'RecommendedServices';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'RecommendedServices';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Orden o posición en que se muestra el servicio recomendado dentro de la lista de opciones al paciente.', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'RecommendedServices', @level2type = N'COLUMN', @level2name = N'Position';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'RecommendedServices', @level2type = N'COLUMN', @level2name = N'Position';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la especialidad médica asociada al servicio recomendado, por ejemplo medicina general, ortopedia, cardiología.', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'RecommendedServices', @level2type = N'COLUMN', @level2name = N'SpecialtyCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'RecommendedServices', @level2type = N'COLUMN', @level2name = N'SpecialtyCode';
